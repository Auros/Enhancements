using System;
using Zenject;
using UnityEngine;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.FloatingScreen;
using BeatSaberMarkupLanguage.ViewControllers;

namespace Enhancements.Timers
{
    [ViewDefinition("Enhancements.Views.Timers.notification-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\Views\Timers\notification-view.bsml")]
    public class NotificationView : BSMLAutomaticViewController
    {
        private Notifier _notifier;
        private FloatingScreen _floatingScreen;
        private ITimerController _timerController;
        private ITimeNotification _currentNotification;

        [UIParams]
        protected BSMLParserParams parserParams;

        private int _length = 5;
        [UIValue("length")]
        protected int Length
        {
            get => _length;
            set
            {
                _length = value;
                parserParams.EmitEvent("get-units");
            }
        }

        private string _title = "Notification";
        [UIValue("title")]
        protected string Title
        {
            get => _title;
            set
            {
                _title = value;
                NotifyPropertyChanged();
            }
        }

        [UIValue("unit-options")]
        public List<object> unitOptions;

        [UIValue("unit")]
        protected TimeType Unit { get; set; } = TimeType.Minutes;

        public bool Visible
        {
            get => _floatingScreen.gameObject.activeInHierarchy;
            set
            {
                _notifier.IsViewing = value;
                _floatingScreen.SetRootViewController(value ? this : null, false);
                // TODO: DISABLE OR ENABLE THE SCREEN AFTER ANIMATION COMPLETED
            }
        }

        [Inject]
        public void Construct(ITimerController controller, Notifier notifier)
        {
            _notifier = notifier;
            _timerController = controller;
            unitOptions = new List<object>();
            unitOptions.AddRange(new TimeType[]
            {
                TimeType.Seconds,
                TimeType.Minutes,
                TimeType.Hours,
            }.Select(x => x as object));
            CreateScreen();
            _notifier.NotificationPing += ShowNotification;
        }

        protected void OnEnable()
        {
            if (!(_notifier is null))
            {
                _notifier.NotificationPing += ShowNotification;
                Catch();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _notifier.NotificationPing -= ShowNotification;
        }

        public void ShowNotification(ITimeNotification notification)
        {
            if (!(notification is null))
            {
                _currentNotification = notification;
                Title = _currentNotification.Text;
                Visible = true;
            }
        }

        private void CreateScreen()
        {
            _floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(100f, 25f), false, new Vector3(0f, 3.5f, 2.1f), Quaternion.Euler(new Vector3(325f, 0f, 0f)));
            Visible = false;
        }

        [UIAction("format-units")]
        protected string FormatUnits(TimeType timeType)
        {
            return Length == 1 ? timeType.ToString().TrimEnd('s') : timeType.ToString();
        }

        protected void Reset()
        {
            Length = 5;
            Unit = default;
            parserParams.EmitEvent("get");
            parserParams.EmitEvent("get-units");
        }

        [UIAction("cancel")]
        protected void Cancel()
        {
            Reset();
            parserParams.EmitEvent("hide-modal");
        }

        [UIAction("confirm")]
        protected void Confirm()
        {
            Create();
            parserParams.EmitEvent("hide-modal");
            Reset();
        }

        protected void Create()
        {
            DateTime time = DateTime.Now;
            switch (Unit)
            {
                case TimeType.Minutes:
                    time = time.AddMinutes(Length);
                    break;
                case TimeType.Seconds:
                    time = time.AddSeconds(Length);
                    break;
                case TimeType.Hours:
                    time = time.AddHours(Length);
                    break;
            }
            _timerController.RegisterNotification(new GenericNotification(_currentNotification.Text, time));
            Visible = false;
        }

        [UIAction("dismiss")]
        protected void Dismiss()
        {

            Visible = false;
            Catch();
        }

        protected async void Catch()
        {
            await Task.Run(() => Thread.Sleep(500));
            var nextNotif = _notifier.NextNotification();
            if (!(nextNotif is null))
            {
                ShowNotification(nextNotif);
            }
        }
    }
}