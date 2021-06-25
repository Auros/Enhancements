using HMUI;
using System;
using Zenject;
using UnityEngine;
using System.Linq;
using VRUIControls;
using IPA.Utilities;
using UnityEngine.UI;
using System.Collections.Generic;
using BeatSaberMarkupLanguage.Parser;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.FloatingScreen;
using BeatSaberMarkupLanguage.ViewControllers;
using BeatSaberMarkupLanguage.Components.Settings;

namespace Enhancements.Timers
{
    [ViewDefinition("Enhancements.Views.Timers.new-reminder-view.bsml")]
    [HotReload(RelativePathToLayout = @"..\Views\Timers\new-reminder-view.bsml")]
    public class NewReminderView : BSMLAutomaticViewController, IInitializable
    {
        private FloatingScreen _floatingScreen;
        private ITimerController _timerController;
        private PhysicsRaycasterWithCache _physicsRaycasterWithCache;

        [UIParams]
        protected BSMLParserParams parserParams;

        [UIComponent("string-setting")]
        protected StringSetting stringSetting;

        [UIComponent("dropdown")]
        protected DropDownListSetting dropdownSetting;

        [UIComponent("cancel-button")]
        protected Button cancelButton;

        [UIComponent("create-button")]
        protected Button createButton;

        [UIValue("text")]
        protected string Text { get; set; } = "New Notification";

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

        [UIValue("unit-options")]
        public List<object> unitOptions;

        [UIValue("unit")]
        protected TimeType Unit { get; set; } = TimeType.Minutes;

        public bool Visible
        {
            get => _floatingScreen.gameObject.activeInHierarchy;
            set
            {
                _floatingScreen.SetRootViewController(value ? this : null, value ? AnimationType.In : AnimationType.Out);
            }
        }

        [Inject]
        public void Construct(ITimerController timerController, PhysicsRaycasterWithCache physicsRaycasterWithCache)
        {
            unitOptions = new List<object>();
            unitOptions.AddRange(new TimeType[]
            {
                TimeType.Seconds,
                TimeType.Minutes,
                TimeType.Hours,
            }.Select(x => x as object));
            _timerController = timerController;
            _physicsRaycasterWithCache = physicsRaycasterWithCache;
            gameObject.SetActive(true);
            CreateScreen();
        }

        public void SetPositionAndRotation(Vector3 pos, Quaternion rot)
        {
            _floatingScreen.ScreenPosition = pos;
            _floatingScreen.ScreenRotation = rot;
        }

        private void CreateScreen()
        {
            _floatingScreen = FloatingScreen.CreateFloatingScreen(new Vector2(130f, 70f), false, new Vector3(0f, 3.5f, 2.5f), Quaternion.Euler(new Vector3(325f, 0f, 0f)));
            _floatingScreen.GetComponent<VRGraphicRaycaster>().SetField("_physicsRaycaster", _physicsRaycasterWithCache);
            _floatingScreen.gameObject.SetActive(true);
            Visible = false;
        }

        [UIAction("#post-parse")]
        protected void Parsed()
        {
            stringSetting.modalKeyboard.transform.localPosition = new Vector3(0f, 0f, -10f);
            var modalGO = dropdownSetting.dropdown.GetField<ModalView, DropdownWithTableView>("_modalView").gameObject;
            modalGO.transform.localPosition = new Vector3(modalGO.transform.localPosition.x, modalGO.transform.localPosition.y, -5f);

            cancelButton.gameObject.SetActive(true);
            createButton.gameObject.SetActive(true);
        }

        [UIAction("format-units")]
        protected string FormatUnits(TimeType timeType)
        {
            return Length == 1 ? timeType.ToString().TrimEnd('s') : timeType.ToString();
        }

        [UIAction("cancel")]
        protected void Cancel()
        {
            Text = "New Notification";
            Length = 5;
            Unit = default;
            parserParams.EmitEvent("get");
            parserParams.EmitEvent("get-units");
            Visible = false;
        }

        [UIAction("create")]
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
            _timerController.RegisterNotification(new GenericNotification(Text, time));
            Visible = false;
        }

        public void Initialize()
        {

        }
    }
}