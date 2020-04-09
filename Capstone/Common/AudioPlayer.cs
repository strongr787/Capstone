using Windows.UI.Xaml.Controls;
using System.IO;
using Windows.Media.Playback;
using Windows.Media.Core;
using System;
using System.Threading;

namespace Capstone.Common
{
    public class AudioPlayer
    {
        private static MediaPlayer mediaPlayer;
        private static bool IsStarted = false;

        public static void Start()
        {
            // must use a gate so that we don't cause memory leaks by not disposing the media player
            if (!IsStarted)
            {
                IsStarted = true;
                mediaPlayer = new MediaPlayer();
            }
        }

        public static void Stop()
        {
            if (IsStarted && mediaPlayer != null)
            {
                mediaPlayer.Dispose();
                mediaPlayer = null;
                IsStarted = false;
            }
        }

        public static void PlaySound(string SoundName, bool DelayThread = true)
        {
            if (IsStarted)
            {
                mediaPlayer.Source = MediaSource.CreateFromUri(new Uri($"ms-appx:///Assets/Sounds/{SoundName}.wav"));
                mediaPlayer.Play();
            }
        }
    }
}
