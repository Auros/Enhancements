using System;
using System.IO;
using System.Linq;
using UnityEngine;
using IPA.Utilities;
using SiraUtil.Tools;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;

namespace Enhancements.Breaktime
{
    public class BreaktimeLoader : IDisposable
    {
        private static readonly string BREAKTIME_FOLDER = Path.Combine(UnityGame.UserDataPath, "Enhancements", "Breaktime");
        internal static readonly string IMAGE_FOLDER = Path.Combine(BREAKTIME_FOLDER, "Images");
        internal static readonly string AUDIO_FOLDER = Path.Combine(BREAKTIME_FOLDER, "Audio");
        private static readonly System.Random _random = new System.Random();

        private readonly BreaktimeSettings _settings;
        private readonly CachedMediaAsyncLoader _mediaLoader;
        private readonly CachedMediaAsyncLoader _spriteLoader;
        private readonly CancellationTokenSource _cancellationTokenSource;

        public BreaktimeLoader(BreaktimeSettings settings, CachedMediaAsyncLoader mediaLoader, CachedMediaAsyncLoader spriteLoader)
        {
            _settings = settings;
            _mediaLoader = mediaLoader;
            _spriteLoader = spriteLoader;
            _cancellationTokenSource = new CancellationTokenSource();
            if (!Directory.Exists(IMAGE_FOLDER))
            {
                Directory.CreateDirectory(IMAGE_FOLDER);
            }
            if (!Directory.Exists(AUDIO_FOLDER))
            {
                Directory.CreateDirectory(AUDIO_FOLDER);
            }
            LoadDefaults();
        }

        public Profile SelectedProfile()
        {
            return _settings.Profiles.FirstOrDefault(x => x.Name == _settings.SelectedProfile);
        }

        public Profile ActiveProfile()
        {
            Profile profile;
            switch (_settings.RandomizeMode)
            {
                case RandomizeMode.Effects:
                    profile = MatchRandomEffectsForProfile();
                    break;

                case RandomizeMode.Profiles:
                    profile = MatchRandomProfile();
                    break;

                default:
                    profile = SelectedProfile();
                    break;
            }
            return profile;
        }

        public async Task<Tuple<Sprite, AudioClip>> GetProfileAssets(Profile profile)
        {
            Sprite spr = null;
            AudioClip clip = null;

            if (!(string.IsNullOrEmpty(profile.ImagePath) || !File.Exists(Path.Combine(IMAGE_FOLDER, profile.ImagePath)) || profile.ImagePath.EndsWith("gif") || profile.ImagePath.EndsWith("apng")))
            {
                try
                {
                    spr = await _spriteLoader.LoadSpriteAsync(Path.Combine(IMAGE_FOLDER, profile.ImagePath), _cancellationTokenSource.Token);
                }
                catch { }
            }
            if (!string.IsNullOrEmpty(profile.AudioPath) && File.Exists(Path.Combine(AUDIO_FOLDER, profile.AudioPath)))
            {
                try
                {
                    clip = await _mediaLoader.LoadAudioClipAsync(Path.Combine(AUDIO_FOLDER, profile.AudioPath), _cancellationTokenSource.Token);
                }
                catch { }
            }

            return new Tuple<Sprite, AudioClip>(spr, clip);
        }

        private Profile MatchRandomProfile()
        {
            return _settings.Profiles[_random.Next(0, _settings.Profiles.Count)];
        }

        private Profile MatchRandomEffectsForProfile()
        {
            var images = GetImageFileInfo();
            var audios = GetAudioFileInfo();

            var image = images[_random.Next(0, images.Length)];
            var audio = audios[_random.Next(0, audios.Length)];

            return new Profile
            {
                ImagePath = image.Name,
                AudioPath = audio.Name,
                Name = "Randomly Generated Effect Profile",
                Animation = Animation.FadeIn,
                ImageColor = Color.white,
                ImageOpacity = 1f,
                ShowText = true,
                TextColor = Color.white
            };
        }

        public FileInfo[] GetImageFileInfo()
        {
            DirectoryInfo di = new DirectoryInfo(IMAGE_FOLDER);
            var files = di.GetFiles();
            var images = files.Where(x => x.Extension.EndsWith("png") || x.Extension.EndsWith("jpeg") || x.Extension.EndsWith("jpg") || x.Extension.EndsWith("gif") || x.Extension.EndsWith("apng"));
            return images.ToArray();
        }

        public FileInfo[] GetAudioFileInfo()
        {
            DirectoryInfo di = new DirectoryInfo(AUDIO_FOLDER);
            var files = di.GetFiles();
            var audios = files.Where(x => x.Extension.EndsWith("ogg") || x.Extension.EndsWith("wav"));
            return audios.ToArray();
        }

        private void LoadDefaults()
        {
            if (_settings.FirstLaunch)
            {
                File.WriteAllBytes(Path.Combine(AUDIO_FOLDER, "osu!.wav"), BeatSaberMarkupLanguage.Utilities.GetResource(Assembly.GetExecutingAssembly(), "Enhancements.Resources.osu!.wav"));
                File.WriteAllBytes(Path.Combine(AUDIO_FOLDER, "Bobbie.wav"), BeatSaberMarkupLanguage.Utilities.GetResource(Assembly.GetExecutingAssembly(), "Enhancements.Resources.Bobbie.wav"));
                File.WriteAllBytes(Path.Combine(AUDIO_FOLDER, "Default.wav"), BeatSaberMarkupLanguage.Utilities.GetResource(Assembly.GetExecutingAssembly(), "Enhancements.Resources.Default.wav"));

                File.WriteAllBytes(Path.Combine(IMAGE_FOLDER, "osu!.png"), BeatSaberMarkupLanguage.Utilities.GetResource(Assembly.GetExecutingAssembly(), "Enhancements.Resources.osu!.png"));
                File.WriteAllBytes(Path.Combine(IMAGE_FOLDER, "Bobbie.png"), BeatSaberMarkupLanguage.Utilities.GetResource(Assembly.GetExecutingAssembly(), "Enhancements.Resources.Bobbie.png"));
                File.WriteAllBytes(Path.Combine(IMAGE_FOLDER, "Default.png"), BeatSaberMarkupLanguage.Utilities.GetResource(Assembly.GetExecutingAssembly(), "Enhancements.Resources.Default.png"));

                _settings.Profiles.Add(new Profile
                {
                    Name = "Default",
                    Animation = Animation.FadeIn,
                    AudioPath = "Default.wav",
                    ImagePath = "Default.png"
                });

                _settings.Profiles.Add(new Profile
                {
                    Name = "osu!",
                    Animation = Animation.FadeIn,
                    AudioPath = "osu!.wav",
                    ImagePath = "osu!.png"
                });

                _settings.Profiles.Add(new Profile
                {
                    Name = "Bobbie",
                    Animation = Animation.None,
                    AudioPath = "Bobbie.wav",
                    ImagePath = "Bobbie.png"
                });
                _settings.FirstLaunch = false;
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}