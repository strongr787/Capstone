using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
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

            String strTime = Time.ToString("yyyy-MM-dd HH:mm");
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
            String strTime = Time.ToString("yyyy-MM-dd HH:mm");
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
        public static string QueryReminder(int ID = -1)
        {
            String strData = "";
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
                    strData += reader.GetString(1);
                }
            }
            conn.Close();
            return strData;
        }
        public static void CreateAlarm(string Title, DateTime Time, DateTime Date)
        {

            String strTime = Time.ToString("yyyy-MM-dd HH:mm");
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
            String strTime = Time.ToString("yyyy-MM-dd HH:mm");
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
        public static string QueryAlarm(int ID = -1)
        {
            String strData = "";
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
                    strData += reader.GetString(1);
                }
            }
            conn.Close();
            return strData;
        }
        public static void CreateVoiceNote(string FileName, string DsiplayName, int RecordingDuration, string FilePath, DateTime RecordDate, DateTime RecordTime)
        {

            String strRecordTime = RecordTime.ToString("yyyy-MM-dd HH:mm");
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
    }
}
