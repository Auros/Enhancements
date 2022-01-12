using System;
using Zenject;

namespace Enhancements.Volume
{
    public class GameVolumeModifier : IInitializable
    {
        private readonly VolumeSettings _volumeSettings;
        private AudioTimeSyncController _audioTimeSyncController;

        public GameVolumeModifier(VolumeSettings volumeSettings, AudioTimeSyncController audioTimeSyncController)
        {
            _volumeSettings = volumeSettings;
            _audioTimeSyncController = audioTimeSyncController;
        }

        public void Initialize()
        {
            Plugin.AudioSource(ref _audioTimeSyncController).volume = _volumeSettings.Music;
        }
    }
}