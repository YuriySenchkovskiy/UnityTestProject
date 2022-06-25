using UnityEngine;

namespace Utils
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