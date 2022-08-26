using Graf.Properties;

namespace Graf.Audio
{
    public interface IAudioManager
    {

        BoolHashed MusicIsMute
        {
            get;
        }

        BoolHashed SoundIsMute
        {
            get;
        }

        void PlayOneShot(string name);
        void PlayOneShot(string name, float pitch);
        void PlayOneShot(string name, float pitch, float volume);

        void PlayBackgroundMusic(string name);
        void StopBackgroundMusic();
    }
}