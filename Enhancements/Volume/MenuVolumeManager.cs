using IPA.Utilities;
using UnityEngine;
using Zenject;

namespace Enhancements.Volume
{
    public class MenuVolumeManager : IInitializable
    {
        private SongPreviewPlayer _songPreviewPlayer;
        private readonly VolumeSettings _volumeSettings;

        private static readonly FieldAccessor<SongPreviewPlayer, float>.Accessor PreviewVolume = FieldAccessor<SongPreviewPlayer, float>.GetAccessor("_volumeScale");
        private static readonly FieldAccessor<SongPreviewPlayer, AudioClip>.Accessor DefaultAudioClip = FieldAccessor<SongPreviewPlayer, AudioClip>.GetAccessor("_defaultAudioClip");

        public MenuVolumeManager(VolumeSettings volumeSettings, SongPreviewPlayer songPreviewPlayer)
        {
            _volumeSettings = volumeSettings;
            _songPreviewPlayer = songPreviewPlayer;
        }

        public void Initialize()
        {
            SetMenuPreviewVolume(_volumeSettings.SongPreview);
        }

        public void SetMenuPreviewVolume(float volume)
        {
            PreviewVolume(ref _songPreviewPlayer) = volume;
        }

        public void SetMenuAmbienceVolume(float volume)
        {
            var audioClip = DefaultAudioClip(ref _songPreviewPlayer);
            _songPreviewPlayer.CrossfadeTo(audioClip, AudioHelpers.NormalizedVolumeToDB(volume), Mathf.Max(Random.Range(0f, audioClip.length - 0.1f), 0f), -1f, true);
        }
    }
}