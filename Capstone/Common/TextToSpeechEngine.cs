using System;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;

namespace Capstone.Common
{
    public static class TextToSpeechEngine
    {
        /// <summary>
        /// Allows for speaking uninflected text in the windows default voice.
        /// </summary>
        /// <param name="media">A MediaElement, likely one hidden on the page somewhere</param>
        /// <param name="textToSpeak">The text you want spoken</param>
        public static async void SpeakText(MediaElement media, string textToSpeak)
        {
            // create the synthesizer
            var synthesizer = new SpeechSynthesizer();
            // synthesize the text and create a stream from it
            SpeechSynthesisStream stream = await synthesizer.SynthesizeTextToStreamAsync(textToSpeak);
            // prepare our media to output the stream and then output it
            media.SetSource(stream, stream.ContentType);
            media.Play();
        }

        public static async void SpeakInflectedText(MediaElement media, string ssmlTextToSpeak)
        {
            var synthesizer = new SpeechSynthesizer();
            SpeechSynthesisStream stream = await synthesizer.SynthesizeSsmlToStreamAsync(ssmlTextToSpeak);
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

        private SSMLBuilder CloseSSMLText()
        {
            this.ssmlText += "</speak>";
            return this;
        }

        public SSMLBuilder Sentence(string sentence)
        {
            this.ssmlText += $"<sentence>{sentence}</sentence>";
            return this;
        }

        public SSMLBuilder Paragraph(string text)
        {
            this.ssmlText += $"<p>{text}</p>";
            return this;
        }

        public SSMLBuilder Break(int amount = 0)
        {
            this.ssmlText += $"<break {(amount > 0 ? $"time='{amount}'" : "")}/>";
            return this;
        }

        public SSMLBuilder Prosody(string text, string pitch = "", string contour = "", string range = "", string rate = "", string duration = "")
        {
            this.ssmlText += $"<prosody {(pitch.Length > 0 ? $"pitch='{pitch}'" : "")} {(contour.Length > 0 ? $"contour='{contour}'" : "")} {(range.Length > 0 ? $"range='{range}'" : "")} {(duration.Length > 0 ? $"duration='{duration}'" : "")}>{text}</prosody>";
            return this;
        }

        public SSMLBuilder SayAs(string text, SayAsTypes type)
        {
            // convert the type to a string value
            string sayAsType = null;
            switch (type)
            {
                case SayAsTypes.ADDRESS:
                    sayAsType = "address";
                    break;
                case SayAsTypes.CARDINAL:
                    sayAsType = "cardinal";
                    break;
                case SayAsTypes.CHARACTERS:
                    sayAsType = "characters";
                    break;
                case SayAsTypes.DATE:
                    sayAsType = "date";
                    break;
                case SayAsTypes.DIGITS:
                    sayAsType = "digits";
                    break;
                case SayAsTypes.FRACTION:
                    sayAsType = "fraction";
                    break;
                case SayAsTypes.ORDINAL:
                    sayAsType = "ordinal";
                    break;
                case SayAsTypes.TELEPHONE:
                    sayAsType = "telephone";
                    break;
                case SayAsTypes.TIME:
                    sayAsType = "time";
                    break;
            }
            this.ssmlText += $"<say-as interpret-as='{sayAsType}'>{text}</say-as>";
            return this;
        }

        public SSMLBuilder Sub(string text, string substitute)
        {
            this.ssmlText += $"<sub alias='{substitute}'>{text}</sub>";
            return this;
        }

        public string Build()
        {
            this.CloseSSMLText();
            return this.ssmlText;
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
