using UnityEngine;

namespace SnowRush.Audio
{
    public sealed class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioSource music;
        [SerializeField] private AudioSource sfx;
        [SerializeField] private AudioClip coin;
        [SerializeField] private AudioClip jump;
        [SerializeField] private AudioClip crash;
        public void SetMusicEnabled(bool enabled) => music.mute = !enabled;
        public void SetSfxEnabled(bool enabled) => sfx.mute = !enabled;
        public void PlayCoin() => sfx.PlayOneShot(coin);
        public void PlayJump() => sfx.PlayOneShot(jump);
        public void PlayCrash() => sfx.PlayOneShot(crash);
        public void HapticLight()
        {
#if UNITY_ANDROID || UNITY_IOS
            Handheld.Vibrate();
#endif
        }
    }
}
