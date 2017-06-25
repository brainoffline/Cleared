using Android.Content;
using Android.Media;

namespace Cleared.Droid.Engine
{
    public class MusicManager
    {
        public static MusicManager Instance { get; private set; }
        private static Context Context { get; set; }

        MediaPlayer backgroundMusic;

        public static void Init(Context context)
        {
            Context = context;
            Instance = new MusicManager();
        }

        public void PlayMusic()
        {
            if (backgroundMusic != null)        // Already playing
                return;

            backgroundMusic = MediaPlayer.Create(Context, Resource.Raw.carefree);
            backgroundMusic.SetVolume(0.4f, 0.4f);
            backgroundMusic.Looping = true;
            backgroundMusic.Start();
        }

        public void StopMusic()
        {
            if (backgroundMusic != null)
            {
                backgroundMusic.Stop();
                backgroundMusic.Dispose();
                backgroundMusic = null;
            }
        }

        public void SuspendBackgroundMusic()
        {
            StopMusic();
        }

        public void RestartBackgroundMusic()
        {
            StopMusic();

            PlayMusic();
        }
    }
}