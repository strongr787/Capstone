using System;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;

namespace Capstone.Common
{
    public static class TextToSpeechEngine
    {
        /// <summary>
        /// Speaks the passed text in an uninflected voice through the passed MediaElement
        /// </summary>
        /// <param name="media"> the media element on the page to play the sound through</param>
        /// <param name="text">the text to speak</param>
        public static async void SpeakText(MediaElement media, string text)
        {
            var synth = new SpeechSynthesizer();
            // create the audio stream from our text
            SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(text);
            // prepare the media object to speak and play the audio
            media.SetSource(stream, stream.ContentType);
            media.Play();
        }

        public static async void SpeakSSMLText(MediaElement media, string ssmlText)
        {
            var synth = new SpeechSynthesizer();
            // create the audio stream from our text
            SpeechSynthesisStream stream = await synth.SynthesizeSsmlToStreamAsync(ssmlText);
            // prepare the media object to speak and play the audio
            media.SetSource(stream, stream.ContentType);
            media.Play();
        }
    }
}
