using Capstone.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace Capstone.SpeechRecognition
{
    public static class SpeechRecognitionUtils
    {
        private static SpeechRecognizer recognizer;
        private static bool started = false;
        // if the user has disabled the "get to know you" setting, this is the error message
        private static readonly uint HResultPrivacyStatementDeclined = 0x80045509;

        public static async void Start(Action<string> speechInputFunction)
        {
            if (!started)
            {
                recognizer = new SpeechRecognizer();
                // compile the grammar and speech contstraings. TODO we may want to create our own grammar file. I don't know how much effort that will take up though
                await recognizer.CompileConstraintsAsync();
                SpeechRecognitionResult result = null;

                Thread thread = new Thread(new ThreadStart(async () =>
                {
                    while (true)
                    {
                        try
                        {
                            result = await recognizer.RecognizeAsync();
                            if (result != null && StringUtils.StartsWith(result.Text, "hey bob"))
                            {
                                await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                                {
                                    speechInputFunction.Invoke(result.Text);
                                });
                            }
                        }
                        catch (Exception exception)
                        {

                            if ((uint)exception.HResult == HResultPrivacyStatementDeclined)
                            {
                                var message = new MessageDialog("The privacy statement was declined." +
                                                                "Go to Settings -> Privacy -> Speech, inking and typing, and ensure you" +
                                                                "have viewed the privacy policy, and 'Get To Know You' is enabled.");
                                await message.ShowAsync();

                                return;
                            }
                        }
                    }
                }));
                thread.IsBackground = true;
                thread.Start();

                started = true;
            }
        }

        public static void Stop()
        {
            if (started)
            {
                recognizer.Dispose();
                started = false;
            }
        }

    }
}
