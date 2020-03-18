USE bobDB;   -- Get out of the master database
SET NOCOUNT ON;		-- Report only errors


																				---------------
																				-- Reminders --
																				---------------


--DROP PROCEDURE CreateReminder;  
--GO  

--go
--CREATE PROCEDURE CreateReminder
--	@Title NVARCHAR(255),
--	@Time TIME,
--	@Date DATETIME
--AS BEGIN
--INSERT INTO TReminders(reminderTitle, reminderTime, isDeleted)
--Values(@Title, @Time, 0);
--INSERT INTO TReminderDates(reminderID, reminderDate)
--Select MAX(reminderID),@Date FROM TReminders
--end
--go

--Exec CreateReminder @Title = 'Title1', @Time = '11:00', @Date = '12/12/20';

--Select * From TReminders
--Select * From TReminderDates

--DROP PROCEDURE UpdateReminder;  
--GO  

--go
--CREATE PROCEDURE UpdateReminder
--	@Title NVARCHAR(255),
--	@Time TIME,
--	@Date DATETIME,
--	@ID int
--AS BEGIN
--	Update TReminders Set reminderTime = @Time, reminderTitle = @Title Where reminderID = @ID
--	Update TReminderDates Set reminderDate = @Date Where reminderID = @ID
--END
--GO

--Exec UpdateReminder @Title = 'UpdatedTitle', @Time = '08:00', @Date = '11/11/20', @ID = 3;

--Select * From TReminders
--Select * From TReminderDates

--DROP PROCEDURE DeleteReminder;  
--GO  

--go
--CREATE PROCEDURE DeleteReminder

--	@ID int
--AS BEGIN
--	Update TReminders Set isDeleted = 1 Where reminderID = @ID
--END
--GO

--Exec DeleteReminder @ID = 3;

--Select * From TReminders
--Select * From TReminderDates


--DROP PROCEDURE QueryReminder;  
--GO  

--go
--CREATE PROCEDURE QueryReminder
--	@ID int = null
--AS BEGIN
--Select TReminders.reminderID, TReminders.reminderTitle, TReminders.reminderTime, TReminderDates.reminderDate, TReminders.isDeleted
--From TReminders, TReminderDates
--Where TReminders.reminderID = TReminderDates.reminderID
--and TReminders.reminderID = COALESCE(@ID,TReminders.reminderID)
--END
--GO

--Exec QueryReminder;

																				------------
																				-- ALARMS --
																				------------

--DROP PROCEDURE CreateAlarm;  
--GO  

--go
--CREATE PROCEDURE CreateAlarm
--	@Title NVARCHAR(255),
--	@Time TIME,
--	@Date DATETIME
--AS BEGIN
--INSERT INTO TAlarms(AlarmTitle, AlarmTime, isDeleted)
--Values(@Title, @Time, 0);
--INSERT INTO TAlarmDates(AlarmID, AlarmDate)
--Select MAX(AlarmID),@Date FROM TAlarms
--end
--go

--Exec CreateAlarm @Title = 'Title1', @Time = '11:00', @Date = '12/12/20';

--Select * From TAlarms
--Select * From TAlarmDates

--DROP PROCEDURE UpdateAlarm;  
--GO  

--go
--CREATE PROCEDURE UpdateAlarm
--	@Title NVARCHAR(255),
--	@Time TIME,
--	@Date DATETIME,
--	@ID int
--AS BEGIN
--	Update TAlarms Set AlarmTime = @Time, AlarmTitle = @Title Where AlarmID = @ID
--	Update TAlarmDates Set AlarmDate = @Date Where AlarmID = @ID
--END
--GO

--Exec UpdateAlarm @Title = 'UpdatedTitle', @Time = '08:00', @Date = '11/11/20', @ID = 3;

--Select * From TAlarms
--Select * From TAlarmDates

--DROP PROCEDURE DeleteAlarm;  
--GO  

--go
--CREATE PROCEDURE DeleteAlarm

--	@ID int
--AS BEGIN
--	Update TAlarms Set isDeleted = 1 Where AlarmID = @ID
--END
--GO

--Exec DeleteAlarm @ID = 3;

--Select * From TAlarms
--Select * From TAlarmDates


--DROP PROCEDURE QueryAlarm;  
--GO  

--go
--CREATE PROCEDURE QueryAlarm
--	@ID int = null
--AS BEGIN
--Select TAlarms.AlarmID, TAlarms.AlarmTitle, TAlarms.AlarmTime, TAlarmDates.AlarmDate, TAlarms.isDeleted
--From TAlarms, TAlarmDates
--Where TAlarms.AlarmID = TAlarmDates.AlarmID
--and TAlarms.AlarmID = COALESCE(@ID,TAlarms.AlarmID)
--END
--GO

--Exec QueryAlarm;

																				-----------------
																				-- Voice Notes --
																				-----------------

--DROP PROCEDURE CreateVoiceNote;  
--GO  

--go
--CREATE PROCEDURE CreateVoiceNote
--	@FileName			VARCHAR(255),
--	@DisplayName		NVARCHAR(255),
--	@recordingDuration	INTEGER,
--	@filePath			NVARCHAR(255),
--	@recordDate			DATE,
--	@recordTime			TIME
--AS BEGIN
--INSERT INTO TVoiceMemos([fileName],displayName,recordingDuration,filePath,recordDate,recordTime)
--Values(@FileName,@DisplayName,@recordingDuration,@filePath,@recordDate,@recordTime)

--end
--go

--Exec CreateVoiceNote @Filename = 'FileName', @DisplayName = 'MemoDisplayName',
-- @recordingDuration = 10, @filePath = 'Documents/Memo', @recordDate = '11/12/20', @recordTime = '11:00'


--DROP PROCEDURE DeleteVoiceNote;  
--GO  

--go
--CREATE PROCEDURE DeleteVoiceNote

--	@ID int
--AS BEGIN
--	DELETE FROM TVoiceMemos Where TVoiceMemos.voiceMemoID = @ID;
--END
--GO

--Exec DeleteVoiceNote @ID = 3;

--DROP PROCEDURE UpdateVoiceNote;  
--GO  

--go
--CREATE PROCEDURE UpdateVoiceNote

--	@Title NVARCHAR(255),
--	@ID	int
--AS BEGIN
--	Update TVoiceMemos Set displayName = @Title Where voiceMemoID = @ID
--END
--GO

--Exec UpdateVoiceNote @Title = 'Updated Title', @ID = 2


--DROP PROCEDURE QueryWeatherProvider;  
--GO  

--go
--CREATE PROCEDURE QueryWeatherProvider
--	@ID int = null
--AS BEGIN
--Select TWeatherProviders.weatherProviderID, TWeatherProviders.weatherProviderName,TWeatherProviderURLS.weatherProviderURL,TWeatherProviderURLParts.weatherProviderURLPartURLString,TWeatherProviderAccessTypes.weatherProviderAccessType
--From TWeatherProviders, TWeatherProviderURLS, TWeatherProviderURLParts, TWeatherProviderAccessTypes
--Where TWeatherProviders.weatherProviderID = COALESCE(@ID,TWeatherProviders.weatherProviderID)
--and TWeatherProviders.weatherProviderID = TWeatherProviderURLS.weatherProviderID
--and TWeatherProviders.weatherProviderID = TWeatherProviderURLParts.weatherProviderID
--and TWeatherProviders.weatherProviderID = TWeatherProviderAccessTypes.weatherProviderID
--END
--GO

--Exec QueryWeatherProvider @ID = 2

--DROP PROCEDURE QueryMapProvider;  
--GO  

--go
--CREATE PROCEDURE QueryMapProvider
--	@ID int = null
--AS BEGIN
--Select TmapProviders.mapProviderID, TmapProviders.mapProviderName,TMapProvidersURLS.mapProviderURL,TMapProvidersURLParts.mapProviderURLPartType,TMapProvidersURLParts.mapProviderURLPartURL,TmapProviderAccessTypes.mapProviderAccessType
--From TmapProviders, TMapProvidersURLS, TmapProvidersURLParts, TmapProviderAccessTypes
--Where TmapProviders.mapProviderID = COALESCE(@ID,TmapProviders.mapProviderID)
--and TmapProviders.mapProviderID = TmapProvidersURLS.mapProviderID
--and TmapProviders.mapProviderID = TMapProvidersURLParts.mapProviderID
--and TmapProviders.mapProviderID = TmapProviderAccessTypes.mapProviderID
--END
--GO

--Exec QuerymapProvider @ID = 1



--DROP PROCEDURE QuerySearchableWebsites;  
--GO  

--go
--CREATE PROCEDURE QuerySearchableWebsites
--	@ID int = null
--AS BEGIN
--Select TSearchableWebsites.searchableWebsitesID, TSearchableWebsites.searchableWebsiteName, TSearchableWebsites.searchableWebsiteBaseURL, TSearchableWebsites.searchableWebsiteQueryString
--From TSearchableWebsites
--Where TSearchableWebsites.searchableWebsitesID = COALESCE(@ID,TSearchableWebsites.searchableWebsitesID)

--END
--GO

--Exec QuerySearchableWebsites


--DROP PROCEDURE QuerySearchEngine;  
--GO  

--go
--CREATE PROCEDURE QuerySearchEngine
--	@ID int = null
--AS BEGIN
--Select TSearchEngines.searchEngineID, TSearchEngines.searchEngineName, TSearchEngines.searchEngineBaseURL, TSearchEngines.searchEngineQueryString
--From TSearchEngines
--Where TSearchEngines.searchEngineID = COALESCE(@ID,TSearchEngines.searchEngineID)
--END
--GO

--Exec QuerySearchEngine @id = 1


--DROP PROCEDURE QueryWeatherProviderNames;  
--GO  

--go
--CREATE PROCEDURE QueryWeatherProviderNames
	
--AS BEGIN
--Select TWeatherProviders.weatherProviderID,TWeatherProviders.weatherProviderName
--From TWeatherProviders
--END
--GO


--DROP PROCEDURE QueryMapProviderNames;  
--GO  

--go
--CREATE PROCEDURE QueryMapProviderNames
	
--AS BEGIN
--Select TMapProviders.mapProviderID,TMapProviders.mapProviderName
--From TMapProviders
--END
--GO

--Exec QueryMapProviderNames


--DROP PROCEDURE QuerySearchableWebsiteNames;  
--GO  

--go
--CREATE PROCEDURE QuerySearchableWebsiteNames
	
--AS BEGIN
--Select TSearchableWebsites.searchableWebsitesID,TSearchableWebsites.searchableWebsitesID
--From TSearchableWebsites
--END
--GO

--Exec QuerySearchableWebsiteNames

--DROP PROCEDURE QueryAllSettings;  
--GO  

--go
--CREATE PROCEDURE QueryAllSettings
--@ID int = null
--AS BEGIN
--Select TSettings.settingID, TSettings.settingDisplayName, TSettingOptions.optionDisplayName,TSettingOptions.isSelected
--From TSettings, TSettingOptions
--Where TSettings.settingID = COALESCE(@ID,TSettings.settingID)
--and TSettings.settingID = TSettingOptions.settingID
--END
--GO

--Exec QueryAllSettings


--DROP PROCEDURE UpdateSetting;  
--GO  

--go
--CREATE PROCEDURE UpdateSetting
--@ID int,
--@SELECTED bit
--AS BEGIN
--Update TSettingOptions 
--Set isSelected = @SELECTED 
--Where settingOptionID = @ID
--END
--GO

--Exec UpdateSetting @ID = 3, @SELECTED = 0
