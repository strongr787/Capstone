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

    public class SSMLBuilder
    {
        public string ssmlText { get; private set; } = "";

        public SSMLBuilder()
        {
            this.ssmlText = "<speak version='1.0' xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>";
        }

        public string Build()
        {
            this.ssmlText += "</speak>";
            return this.ssmlText;
        }

        public SSMLBuilder Break(int duration = 0)
        {
            this.ssmlText += $"<break {(duration > 0 ? $"time='{duration}' " : "")}/>";
            return this;
        }

        public SSMLBuilder Sentence(string text)
        {
            this.ssmlText += $"<sentence>{text}</sentence>";
            return this;
        }

        public SSMLBuilder Paragraph(string text)
        {
            this.ssmlText += $"<p>{text}</p>";
            return this;
        }

        public SSMLBuilder Prosody(string text, string pitch = "", string contour = "", string range = "", string rate = "", string duration = "")
        {
            this.ssmlText += $"<prosody{(pitch.Length > 0 ? $" pitch='{pitch}'" : "")}{(contour.Length > 0 ? $" contour='{contour}'" : "")}{(range.Length > 0 ? $" range='{range}'" : "")}{(rate.Length > 0 ? $" rate='{rate}'" : "")}{(duration.Length > 0 ? $" duration='{duration}'" : "")}>{text}</prosody>";
            return this;
        }

        public SSMLBuilder SayAs(string text, SayAsTypes type)
        {
            string sayType = Enum.GetName(typeof(SayAsTypes), type).ToLower();
            this.ssmlText += $"<say-as interpret-as='{sayType}'>{text}</say-as>";
            return this;
        }

        public SSMLBuilder Sub(string text, string substitute)
        {
            this.ssmlText += $"<sub alias='{substitute}'>{text}</sub>";
            return this;
        }

        public enum SayAsTypes
        {
            ADDRESS = 0,
            CARDINAL = 1,
            CHARACTERS = 2,
            DATE = 3,
            DIGITS = 4,
            FRACTION = 5,
            ORDINAL = 6,
            TELEPHONE = 7,
            TIME = 8
        }
    }
}
