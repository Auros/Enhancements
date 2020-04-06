using System.Collections.Generic;
using UnityEngine;

namespace Enhancements.Utilities
{
    public class AudioUtil : MonoBehaviour
    {
        private static AudioUtil _instance;
        public static AudioUtil Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject @object = new GameObject("Enhancements AudioUtil");
                    _instance = @object.AddComponent<AudioUtil>();
                    DontDestroyOnLoad(@object);
                }
                return _instance;
            }
        }

        private readonly List<AudioSource> oneShotPool = new List<AudioSource>();
        private AudioSource AvailableOneShot
        {
            get
            {
                for (int i = 0; i < oneShotPool.Count; i++)
                {
                    if (oneShotPool[i].isPlaying) continue;
                    return oneShotPool[i];
                }
                AudioSource newOneShot = gameObject.AddComponent<AudioSource>();
                MakeSourceNonDimensional(newOneShot, false);
                return newOneShot;
            }
        }

        public void PlayAudioClip(AudioClip clip)
        {
            var oneshot = AvailableOneShot;
            oneshot.clip = clip;
            oneshot.PlayOneShot(clip, 1f);

        }

        public static void MakeSourceNonDimensional(AudioSource source, bool loop)
        {
            source.loop = loop;
            source.bypassEffects = true;
            source.bypassListenerEffects = true;
            source.bypassReverbZones = true;
            source.spatialBlend = 0;
            source.spatialize = false;
            source.velocityUpdateMode = AudioVelocityUpdateMode.Fixed;
        }

        public static void MakeSourceNonDimensional(AudioSource source)
        {
            MakeSourceNonDimensional(source, source.loop);
        }
    }
}
