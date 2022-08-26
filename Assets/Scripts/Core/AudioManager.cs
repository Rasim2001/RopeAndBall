using Graf.Properties;
using UnityEngine;

namespace Graf.Audio
{
    public class AudioManager : MonoBehaviour, IAudioManager
    {
        public static AudioManager Instance;

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);

                PlayBackgroundMusic("BackMusic");
            }
        }

        protected GameObject music;
        protected AudioSource musicAudio;

        public AudioClip this[string name]
        {
            get
            {
                return Resources.Load<AudioClip>($"Clips/{name}");
            }
        }

        public BoolHashed MusicIsMute
        {
            get;
        } = new BoolHashed("MusicIsMute");

        public BoolHashed SoundIsMute
        {
            get;
        } = new BoolHashed("SoundIsMute");

        public void SetVolume(bool isOn)
        {
            SoundIsMute.Value = !isOn;

            if (musicAudio != null)
                musicAudio.volume = isOn ? 100f : 0f;
        }

        public void PlayOneShot(AudioClip clip)
        {
            PlayOneShot(clip, 1f, SoundIsMute.Value ? 0 : 1f);
        }

        public void PlayOneShot(AudioClip clip, float pitch)
        {
            PlayOneShot(clip, pitch, SoundIsMute.Value ? 0 : 1f);
        }

        public void PlayOneShot(AudioClip clip, float pitch, float volume)
        {
            if (!SoundIsMute.Value)
            {
                GameObject go = new GameObject("Audio");

                AudioSource source = go.AddComponent<AudioSource>();
                source.clip = clip;
                source.volume = volume;
                source.pitch = pitch;
                source.Play();

                Destroy(go, source.clip.length);
            }
        }

        public void PlayOneShot(string name)
        {
            PlayOneShot(name, 1f, SoundIsMute.Value ? 0 : 1f);
        }

        public void PlayOneShot(string name, float pitch)
        {
            PlayOneShot(name, pitch, SoundIsMute.Value ? 0 : 1f);
        }

        public void PlayOneShot(string name, float pitch, float volume)
        {
            if (!SoundIsMute.Value)
            {
                GameObject go = new GameObject("Audio: " + name);

                AudioSource source = go.AddComponent<AudioSource>();
                source.clip = this[name];
                source.volume = volume;
                source.pitch = pitch;
                source.Play();

                Destroy(go, source.clip.length);
            }
        }

        public void PlayBackgroundMusic(string name)
        {
            music = new GameObject("Music: " + name);
            DontDestroyOnLoad(music);

            AudioSource source = music.AddComponent<AudioSource>();
            musicAudio = source;
            source.clip = this[name];
            source.volume = SoundIsMute.Value ? 0f : 100f;
            source.pitch = 1f;
            source.loop = true;
            source.Play();
        }

        public void StopBackgroundMusic()
        {
            Destroy(music);
        }
    }
}