using System;
using Zenject;

namespace Enhancements.Volume
{
    public class GameVolumeModifier : IInitializable
    {
        private readonly VolumeSettings _volumeSettings;
        private readonly AudioTimeSyncController _audioTimeSyncController;

        public GameVolumeModifier(VolumeSettings volumeSettings, AudioTimeSyncController audioTimeSyncController)
        {
            _volumeSettings = volumeSettings;
            _audioTimeSyncController = audioTimeSyncController;
        }

        public void Initialize()
        {
            _audioTimeSyncController.audioSource.volume = _volumeSettings.Music;
        }
    }
}
