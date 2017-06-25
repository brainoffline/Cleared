using System;
using System.IO;
using AVFoundation;
using Foundation;

namespace Cleared.iOS.Engine
{
    public class MusicEngine
    {
		AVAudioPlayer backgroundMusic;

        static MusicEngine instance;
        public static MusicEngine Instance 
        {
            get { return (instance = instance ?? new MusicEngine()); }
        }

        public bool MusicOn { get; set; } = true;
        public string Song { get; set; }

        public MusicEngine()
        {
            instance = this;
            ActivateAudioSession();
        }

        public void ActivateAudioSession()
        {
			var session = AVAudioSession.SharedInstance();
            session.SetCategory(AVAudioSession.CategoryPlayback, out NSError err);
			session.SetActive(true);
		}

        public void DeactivateAudioSession() 
        {
            AVAudioSession.SharedInstance().SetActive(false);
		}

        public void PlayMusic(string filename)
		{
			StopMusic();

			string sFilePath = NSBundle.MainBundle.PathForResource(Path.GetFileNameWithoutExtension(filename), Path.GetExtension(filename));
			var url = NSUrl.FromString(sFilePath);
            backgroundMusic = AVAudioPlayer.FromUrl(url);

			backgroundMusic.Volume = 0.4f;
            backgroundMusic.NumberOfLoops = -1;
			backgroundMusic.PrepareToPlay();
			backgroundMusic.FinishedPlaying += (object sender, AVStatusEventArgs e) =>
			{
				backgroundMusic = null;
			};
			backgroundMusic.Play();
		}

		public void StopMusic()
		{
            if (backgroundMusic != null)
            {
                backgroundMusic.Stop();
                backgroundMusic.Dispose();
                backgroundMusic = null;
            }
            Song = null;
		}

        public void SuspendBackgroundMusic() 
        {
            StopMusic();
        }

        public void RestartBackgroundMusic()
        {
            if (!MusicOn) return;
            if (string.IsNullOrWhiteSpace(Song)) return;

            PlayMusic(Song);
        }
	}
}
