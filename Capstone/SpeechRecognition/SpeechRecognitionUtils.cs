using Capstone.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Media.Playback;
using Windows.Media.SpeechRecognition;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace Capstone.SpeechRecognition
{
    public static class SpeechRecognitionUtils
    {
        private static SpeechRecognizer recognizer;
        public static bool IsStarted { get; private set; } = false;
        // if the user has disabled the "get to know you" setting, this is the error message
        private static readonly uint HResultPrivacyStatementDeclined = 0x80045509;
        private static readonly string activatorString = "hey bob";
        // the text box to populate the spoken words with
        public static TextBox commandBox;
        private static Thread thread;

        public static async void Start(Action<string> speechInputFunction, TextBox box)
        {
            if (!IsStarted)
            {
                IsStarted = true;
                commandBox = box;
                recognizer = new SpeechRecognizer();
                // compile the grammar and speech contstraings. TODO we may want to create our own grammar file. I don't know how much effort that will take up though
                await recognizer.CompileConstraintsAsync();
                recognizer.HypothesisGenerated += Recognizer_HypothesisGenerated;
                SpeechRecognitionResult result = null;

                thread = new Thread(new ThreadStart(async () =>
                {
                    while (IsStarted)
                    {
                        try
                        {
                            result = await recognizer.RecognizeAsync();
                            if (result != null && StringUtils.Contains(result.Text, activatorString))
                            {
                                // clear the command box and run the command
                                Utils.RunOnMainThread(() =>
                                {
                                    AudioPlayer.PlaySound("bob_activate");
                                    // give the sound enough time to play
                                    Thread.Sleep(750);
                                    speechInputFunction.Invoke(commandBox.Text);
                                });
                            }
                        }
                        catch (ObjectDisposedException)
                        {
                            // the page was changed, or we were stopped. Either way, be sure to mark us as not started just in case
                            IsStarted = false;
                        }
                        catch (Exception exception)
                        {

                            if ((uint)exception.HResult == HResultPrivacyStatementDeclined)
                            {
                                var message = new MessageDialog("The privacy statement was declined." +
                                                                "Go to Settings -> Privacy -> Speech, inking and typing, and ensure you" +
                                                                "have viewed the privacy policy, and 'Get To Know You' is enabled.");
                                await message.ShowAsync();

                            }
                        }
                    }
                }));
                thread.IsBackground = true;
                thread.Start();
            }
        }

        public static void Stop()
        {
            if (IsStarted)
            {
                recognizer.HypothesisGenerated -= Recognizer_HypothesisGenerated;
                recognizer.Dispose();
                IsStarted = false;
            }
        }

        private static void Recognizer_HypothesisGenerated(SpeechRecognizer recognizer, SpeechRecognitionHypothesisGeneratedEventArgs args)
        {
            if (StringUtils.Contains(args.Hypothesis.Text, activatorString))
            {
                Utils.RunOnMainThread(() =>
                {
                    if (commandBox != null)
                    {
                        commandBox.Text = args.Hypothesis.Text;
                    }
                });

            }
        }

    }
}
