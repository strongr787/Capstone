using Capstone.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Common
{
    class StoredProcedures
    {
        public static async Task CreateDatabase()
        {
            string targetDbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Database\\BobDB.db");
            if (!File.Exists(targetDbPath))
            {
                var installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
                using (var input = await installedLocation.OpenStreamForReadAsync("Assets\\BobDB.db"))
                {
                    using (var output = await Windows.Storage.ApplicationData.Current.LocalFolder.OpenStreamForWriteAsync("Database\\BobDB.db", Windows.Storage.CreationCollisionOption.FailIfExists))
                    {
                        await input.CopyToAsync(output);
                    }
                }
            }
        }
        public static SqliteConnection OpenDatabase()
        {
            string targetDbPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "Database\\BobDB.db");
            SqliteConnection conn = new SqliteConnection(@"Data Source = " + targetDbPath);
            return conn;
        }

        public static void CreateReminder(string Title, DateTime Time, DateTime Date)
        {

            String strTime = Time.ToString("HH:mm");
            String strDate = Date.ToString("yyyy-MM-dd HH:mm");
            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"INSERT INTO TReminders(reminderTitle, reminderTime, isDeleted) Values('{Title}','{strTime}', 0);";
            command.ExecuteNonQuery();
            command.CommandText = $"INSERT INTO TReminderDates(reminderID, reminderDate) Select MAX(reminderID), '{strDate}' FROM TReminders;";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void UpdateReminder(int ID, string Title, DateTime Time, DateTime Date)
        {
            int intID = ID;
            String strTime = Time.ToString("HH:mm");
            String strDate = Date.ToString("yyyy-MM-dd HH:mm");
            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Update TReminders Set reminderTime = '{Time}', reminderTitle = '{Title}' Where reminderID = {intID};";
            command.ExecuteNonQuery();
            command.CommandText = $"Update TReminderDates Set reminderDate = '{Date}' Where reminderID = {intID};";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void DeleteReminder(int ID)
        {
            int intID = ID;
            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Update TReminders Set isDeleted = 1 Where reminderID = {intID};";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static Reminder QueryReminder(int ID = -1)
        {
            Reminder reminder = new Reminder();
            string intID;
            if (ID == -1)
            {
                intID = "null";
            }
            else
            {
                intID = ID.ToString();
            }
            
            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Select TReminders.reminderID, TReminders.reminderTitle, TReminders.reminderTime, TReminderDates.reminderDate, TReminders.isDeleted From TReminders, TReminderDates Where TReminders.reminderID = TReminderDates.reminderID and TReminders.reminderID = COALESCE({intID}, TReminders.reminderID); ";
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int intReminderID = (int)(long)reader["reminderID"];
                    reminder.ReminderID = intReminderID;
                    reminder.Title = reader["reminderTitle"].ToString();
                    var date = DateTime.Parse($"{reader["reminderDate"]} {reader["reminderTime"]}");
                    reminder.ActivateDateAndTime = date;
                    reminder.IsDeleted = Convert.ToBoolean((long)reader["isDeleted"]);
                }
            }
            conn.Close();
            return reminder;
        }
        public static void CreateAlarm(string Title, DateTime Time, DateTime Date)
        {

            String strTime = Time.ToString("HH:mm");
            String strDate = Date.ToString("yyyy-MM-dd HH:mm");
            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"INSERT INTO TAlarms(AlarmTitle, AlarmTime, isDeleted) Values('{Title}','{strTime}', 0);";
            command.ExecuteNonQuery();
            command.CommandText = $"INSERT INTO TAlarmDates(AlarmID, AlarmDate) Select MAX(AlarmID), '{strDate}' FROM TAlarms;";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void UpdateAlarm(int ID, string Title, DateTime Time, DateTime Date)
        {
            int intID = ID;
            String strTime = Time.ToString("HH:mm");
            String strDate = Date.ToString("yyyy-MM-dd HH:mm");
            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Update TAlarms Set AlarmTime = '{Time}', AlarmTitle = '{Title}' Where AlarmID = {intID};";
            command.ExecuteNonQuery();
            command.CommandText = $"Update TAlarmDates Set AlarmDate = '{Date}' Where AlarmID = {intID};";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void DeleteAlarm(int ID)
        {
            int intID = ID;
            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Update TAlarms Set isDeleted = 1 Where AlarmID = {intID};";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static Alarm QueryAlarm(int ID = -1)
        {
            Alarm alarm = new Alarm();
            string intID;
            if (ID == -1)
            {
                intID = "null";
            }
            else
            {
                intID = ID.ToString();
            }

            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Select TAlarms.AlarmID, TAlarms.AlarmTitle, TAlarms.AlarmTime, TAlarmDates.AlarmDate, TAlarms.isDeleted From TAlarms, TAlarmDates Where TAlarms.AlarmID = TAlarmDates.AlarmID and TAlarms.AlarmID = COALESCE({intID}, TAlarms.AlarmID); ";
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int intAlarmID = (int)(long)reader["AlarmID"];
                    alarm.AlarmID = intAlarmID;
                    alarm.Title = reader["AlarmTitle"].ToString();
                    var date = DateTime.Parse($"{reader["AlarmDate"]} {reader["AlarmTime"]}");
                    alarm.ActivateDateAndTime = date;
                    alarm.IsDeleted = Convert.ToBoolean((long)reader["isDeleted"]);
                }
            }
            conn.Close();
            return alarm;
        }
            
        public static void CreateVoiceNote(string FileName, string DsiplayName, int RecordingDuration, string FilePath, DateTime RecordDate, DateTime RecordTime)
        {

            String strRecordTime = RecordTime.ToString("HH:mm");
            String strRecordDate = RecordDate.ToString("yyyy-MM-dd HH:mm");
            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"INSERT INTO TVoiceMemos(fileName,displayName,recordingDuration,filePath,recordDate,recordTime) Values('{FileName}', '{DsiplayName}', '{RecordingDuration}', '{FilePath}', '{strRecordDate}', '{strRecordTime}'); ";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void DeleteVoiceNote(int ID)
        {
            int intID = ID;
            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Delete From TVoiceMemos Where TVoiceMemos.voiceMemoID = {intID};";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static void UpdateVoiceNote(int ID, string Title)
        {
            int intID = ID;
            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Update TVoiceMemos Set displayName = '{Title}' Where voiceMemoID = {intID};";
            command.ExecuteNonQuery();
            conn.Close();
        }

        public static VoiceMemo QueryVoiceMemo(int ID = -1)
        {
            VoiceMemo voiceMemo = new VoiceMemo();
            string intID;
            if (ID == -1)
            {
                intID = "null";
            }
            else
            {
                intID = ID.ToString();
            }

            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Select voiceMemoID, fileName, displayName, recordingDuration, filePath, recordDate, recordTime From TVoiceMemos Where voiceMemoID = COALESCE({intID}, voiceMemoID);";
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int intVoiceMemoID = (int)(long)reader["voiceMemoID"];
                    voiceMemo.VoiceMemoID = intVoiceMemoID;
                    voiceMemo.FileName = reader["fileName"].ToString();
                    voiceMemo.DisplayName = reader["displayName"].ToString();
                    voiceMemo.RecordingDuration = (int)reader["recordingDuration"];
                    voiceMemo.FullFilePath = reader["filePath"].ToString();
                    voiceMemo.DateRecorded = (DateTime)reader["recordDate"];
                }
            }
            conn.Close();
            return voiceMemo;
        }

        public static void UpdateSettings(int ID, bool IsSelected)
        {
            int intID = ID;
            bool boolIsSelected = IsSelected;
            int intIsSelected = boolIsSelected ? 1 : 0;
            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Update TSettingOptions Set isSelected = {intIsSelected} Where settingOptionID = {intID};";
            command.ExecuteNonQuery();
            conn.Close();
        }
        public static Setting QuerySetting(int ID = -1)
        {
            Setting setting = new Setting();
            List<SettingOption> SettingOptionList = new List<SettingOption>();
            string intID;
            if (ID == -1)
            {
                intID = "null";
            }
            else
            {
                intID = ID.ToString();
            }

            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Select TSettings.settingID, TSettings.settingDisplayName,TSettingOptions.settingOptionID, TSettingOptions.optionDisplayName,TSettingOptions.isSelected From TSettings, TSettingOptions Where TSettings.settingID = COALESCE({intID}, TSettings.settingID) and TSettings.settingID = TSettingOptions.settingID;";
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    SettingOption settingOption = new SettingOption();
                    int intSettingOptionID = (int)(long)reader["settingOptionID"];
                    settingOption.OptionID = intSettingOptionID;
                    settingOption.DisplayName = reader["optionDisplayName"].ToString();
                    settingOption.IsSelected = Convert.ToBoolean((long)reader["isSelected"]);
                    SettingOptionList.Add(settingOption);
                }
            }
            command.CommandText = $"Select TSettings.settingID, TSettings.settingDisplayName, TSettingOptions.optionDisplayName,TSettingOptions.isSelected From TSettings, TSettingOptions Where TSettings.settingID = COALESCE({intID}, TSettings.settingID) and TSettings.settingID = TSettingOptions.settingID;";
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int intSettingID = (int)(long)reader["settingID"];
                    setting.SettingID = intSettingID;
                    setting.DisplayName = reader["settingDisplayName"].ToString();
                    setting.Options = SettingOptionList;
                }
            }
            conn.Close();
            return setting;
        }
        public static WeatherProvider QueryWeatherProvider(int ID = -1)
        {
            WeatherProvider weatherProvider = new WeatherProvider();
            string intID;
            if (ID == -1)
            {
                intID = "null";
            }
            else
            {
                intID = ID.ToString();
            }

            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Select TWeatherProviders.weatherProviderID, TWeatherProviders.weatherProviderName,TWeatherProviderURLS.weatherProviderURL,TWeatherProviderURLParts.weatherProviderURLPartURLString,TWeatherProviderAccessTypes.weatherProviderAccessType From TWeatherProviders, TWeatherProviderURLS, TWeatherProviderURLParts, TWeatherProviderAccessTypes Where TWeatherProviders.weatherProviderID = COALESCE({intID}, TWeatherProviders.weatherProviderID) and TWeatherProviders.weatherProviderID = TWeatherProviderURLS.weatherProviderID and TWeatherProviders.weatherProviderID = TWeatherProviderURLParts.weatherProviderID and TWeatherProviders.weatherProviderID = TWeatherProviderAccessTypes.weatherProviderID; ";
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int intWeatherProviderID = (int)(long)reader["weatherProviderID"];
                    weatherProvider.WeatherProviderID = intWeatherProviderID;
                    weatherProvider.Name = reader["weatherProviderName"].ToString();
                    weatherProvider.BaseURL = reader["weatherProviderURL"].ToString();
                    weatherProvider.AccessType = reader["weatherProviderAccessType"];
                    weatherProvider.APIKey = reader[""];
                }
            }
            conn.Close();
            return weatherProvider;
        }
        public static MapProvider QueryMapProvider(int ID = -1)
        {
            MapProvider mapProvider = new MapProvider();
            string intID;
            if (ID == -1)
            {
                intID = "null";
            }
            else
            {
                intID = ID.ToString();
            }

            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Select TmapProviders.mapProviderID, TmapProviders.mapProviderName,TMapProvidersURLS.mapProviderURL,TMapProvidersURLParts.mapProviderURLPartType,TMapProvidersURLParts.mapProviderURLPartURL,TmapProviderAccessTypes.mapProviderAccessType From TmapProviders, TMapProvidersURLS, TmapProvidersURLParts, TmapProviderAccessTypes Where TmapProviders.mapProviderID = COALESCE({intID}, TmapProviders.mapProviderID) and TmapProviders.mapProviderID = TmapProvidersURLS.mapProviderID and TmapProviders.mapProviderID = TMapProvidersURLParts.mapProviderID and TmapProviders.mapProviderID = TmapProviderAccessTypes.mapProviderID;";
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    string AccessType = reader["mapProviderAccessType"].ToString();
                    int intMapProviderID = (int)(long)reader["mapProviderID"];
                    mapProvider.MapProviderID = intMapProviderID;
                    mapProvider.Name = reader["mapProviderName"].ToString();
                    mapProvider.BaseURL = reader["mapProviderURL"].ToString();
                    mapProvider.AccessType = AccessType;
                    mapProvider.APIKey = reader[""];
                }
            }
            conn.Close();
            return mapProvider;
        }
        public static SearchableWebsite QuerySearchableWebsite(int ID = -1)
        {
            SearchableWebsite searchableWebsite = new SearchableWebsite();
            string intID;
            if (ID == -1)
            {
                intID = "null";
            }
            else
            {
                intID = ID.ToString();
            }

            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Select TSearchableWebsites.searchableWebsitesID, TSearchableWebsites.searchableWebsiteName, TSearchableWebsites.searchableWebsiteBaseURL, TSearchableWebsites.searchableWebsiteQueryString From TSearchableWebsites Where TSearchableWebsites.searchableWebsitesID = COALESCE({intID}, TSearchableWebsites.searchableWebsitesID);";
            using (SqliteDataReader reader = command.ExecuteReader())
            {
            while (reader.Read())
            {
                int intSearchableWebsitesID = (int)(long)reader["searchableWebsitesID"];
                searchableWebsite.SearchableWebsiteID = intSearchableWebsitesID;
                searchableWebsite.Name = reader["searchableWebsiteName"].ToString();
                searchableWebsite.BaseURL = reader["searchableWebsiteBaseURL"].ToString();
                searchableWebsite.QueryString = reader["searchableWebsiteQueryString"].ToString();
                }
            }
            conn.Close();
            return searchableWebsite;
        }
        public static SearchEngine QuerySearchEngine(int ID = -1)
        {
            SearchEngine searchEngine = new SearchEngine();
            string intID;
            if (ID == -1)
            {
                intID = "null";
            }
            else
            {
                intID = ID.ToString();
            }

            SqliteConnection conn = OpenDatabase();
            conn.Open();
            SqliteCommand command = conn.CreateCommand();
            command.CommandText = $"Select TSearchEngines.searchEngineID, TSearchEngines.searchEngineName, TSearchEngines.searchEngineBaseURL, TSearchEngines.searchEngineQueryString From TSearchEngines Where TSearchEngines.searchEngineID = COALESCE({intID}, TSearchEngines.searchEngineID);";
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int intSearchEngineID = (int)(long)reader["searchEngineID"];
                    searchEngine.SearchEngineID = intSearchEngineID;
                    searchEngine.Name = reader["searchEngineName"].ToString();
                    searchEngine.BaseURL = reader["searchEngineBaseURL"].ToString();
                    searchEngine.QueryString = reader["searchEngineQueryString"].ToString();
                }
            }
            conn.Close();
            return searchEngine;
        }

    }
}
