using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Actions
{
    public static class ActionRouter
    {
        private static readonly Dictionary<string, Dictionary<string, Func<string, Action>>> actionTree = new Dictionary<string, Dictionary<string, Func<string, Action>>>();

        public static void SetUp()
        {
            // add all the main keys to the dictionary
            SetUpAlarmBranches();
        }

        private static void SetUpWeatherBranches()
        {

        }

        private static void SetUpAlarmBranches()
        {
            // a dictionary that takes a string for the key and returns a lambda that takes a string for the command and returns an AlarmAction
            var alarmDict = new Dictionary<string, Func<string, Action>>();
            // the different types of actions that can be taken for an alarm
            Func<string, Action> createAlarm = (commandText) => new AlarmAction(AlarmAction.AlarmActionTypes.CREATE, commandText);
            Func<string, Action> deleteAlarm = (commandText) => new AlarmAction(AlarmAction.AlarmActionTypes.DELETE, commandText);
            Func<string, Action> editAlarm = (commandText) => new AlarmAction(AlarmAction.AlarmActionTypes.EDIT, commandText);
            /* add the different key phrases for the alarm creation, update, and deletion*/
            // create alarm ====================
            alarmDict.Add("create", createAlarm);
            alarmDict.Add("set", createAlarm);
            alarmDict.Add("new", createAlarm);
            alarmDict.Add("add", createAlarm);
            // edit alarm ======================
            alarmDict.Add("edit", editAlarm);
            alarmDict.Add("update", editAlarm);
            alarmDict.Add("change", editAlarm);
            alarmDict.Add("alter", editAlarm);
            // delete alarm ====================
            alarmDict.Add("delete", deleteAlarm);
            alarmDict.Add("remove", deleteAlarm);
            alarmDict.Add("clear", deleteAlarm);
            alarmDict.Add("void", deleteAlarm);
            // add the alarm dict to the main one
            actionTree.Add("alarm", alarmDict);
        }

        /// <summary>
        /// Searches through the passed <paramref name="DictToCheck"/> for a keyword that most matches a word in the passed <paramref name="inputString"/>
        /// </summary>
        /// <param name="inputString">A string that contains any amount of text. Each space-separated word is used in an attemptto match against a key in the dictionary</param>
        /// <param name="DictToCheck">The dictionary whose keys will be checked for a keyword</param>
        /// <returns>The found keyword if one was found, otherwise null is returned</returns>
        public static string FindKeyword(string inputString, Dictionary<string, dynamic> DictToCheck)
        {
            string foundKeyword = null;
            // split the inputString into tokens
            string[] tokens = inputString.ToLower().Split(" ");
            List<string> keys = DictToCheck.Keys.ToList();
            // iterate through the keys to find the keyword
            foreach (string key in keys)
            {
                if (tokens.Contains(key.ToLower()))
                {
                    foundKeyword = key.ToLower();
                    break;
                }
            }
            return foundKeyword;
        }
    }
}
