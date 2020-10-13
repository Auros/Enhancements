using Zenject;
using UnityEngine;
using IPA.Utilities;

namespace Enhancements.Volume
{
    public class MenuVolumeManager : IInitializable
    {
        private SongPreviewPlayer _songPreviewPlayer;
        private readonly VolumeSettings _volumeSettings;

        private static readonly FieldAccessor<SongPreviewPlayer, float>.Accessor PreviewVolume = FieldAccessor<SongPreviewPlayer, float>.GetAccessor("_volume");
        private static readonly FieldAccessor<SongPreviewPlayer, float>.Accessor AmbienceVolume = FieldAccessor<SongPreviewPlayer, float>.GetAccessor("_ambientVolumeScale");
        private static readonly FieldAccessor<SongPreviewPlayer, AudioClip>.Accessor DefaultAudioClip = FieldAccessor<SongPreviewPlayer, AudioClip>.GetAccessor("_defaultAudioClip");

        public MenuVolumeManager(VolumeSettings volumeSettings, SongPreviewPlayer songPreviewPlayer)
        {
            _volumeSettings = volumeSettings;
            _songPreviewPlayer = songPreviewPlayer;
        }

        public void Initialize()
        {
            SetMenuPreviewVolume(_volumeSettings.SongPreview);
            SetMenuAmbienceVolume(_volumeSettings.MenuBackground);
        }

        public void SetMenuPreviewVolume(float volume)
        {
            PreviewVolume(ref _songPreviewPlayer) = volume;
            SetMenuAmbienceVolume(_volumeSettings.MenuBackground);
        }

        public void SetMenuAmbienceVolume(float volume)
        {
            AmbienceVolume(ref _songPreviewPlayer) = volume / PreviewVolume(ref _songPreviewPlayer);
            var audioClip = DefaultAudioClip(ref _songPreviewPlayer);
            _songPreviewPlayer.CrossfadeTo(audioClip, Mathf.Max(Random.Range(0f, audioClip.length - 0.1f), 0f), -1f, AmbienceVolume(ref _songPreviewPlayer));
        }
    }
}