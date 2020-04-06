using Capstone.Common;
using Capstone.Models;
using Capstone.Providers;
using System;
using System.Collections.Generic;

namespace Capstone.Actions
{
    public class WeatherAction : Action
    {

        public WeatherAction(string CommandString)
        {
            this.CommandString = CommandString;
        }

        public async override void PerformAction()
        {
            List<WeatherInfo> weatherInfos = await WeatherService.GetWeather();
            // if there's a date in the command string, find it and compare it with the dates provided
            DateTime commandDate = DateTimeParser.ParseDateTimeFromText(this.CommandString);
            // get the first applicable weather info
            WeatherInfo firstApplicableWeatherInfo = weatherInfos.Find(info => info.DateApplicable >= commandDate);
            if (firstApplicableWeatherInfo != null && this.MediaElement != null)
            {
                // TODO get better at determining where there should be inflection. Right now this works but sounds a bit too robotic
                string inflectionData = new SSMLBuilder().Prosody(firstApplicableWeatherInfo.Description, contour: "(30%,+10%) (60%,-10%) (90%,+5%)").Build();
                TextToSpeechEngine.SpeakInflectedText(this.MediaElement, inflectionData);
            }
            else if (firstApplicableWeatherInfo == null)
            {
                TextToSpeechEngine.SpeakText(this.MediaElement, "I could not find any weather info for the date specified. Try making sure that you have location enabled, and that this app can access your location through settings, privacy, location");
            }
        }
    }
}
