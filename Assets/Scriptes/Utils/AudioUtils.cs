using UnityEngine;

namespace Scriptes.Utils
{
    public class AudioUtils
    {
        private const string SfxSourceTag = "SfxAudioSource";
        
        public static AudioSource FindSfxSource()
        {
            return GameObject.FindWithTag(SfxSourceTag).GetComponent<AudioSource>();
        }
    }
}