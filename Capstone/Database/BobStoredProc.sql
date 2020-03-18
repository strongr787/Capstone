USE bobDB;   -- Get out of the master database
SET NOCOUNT ON;		-- Report only errors


																				---------------
																				-- Reminders --
																				---------------


DROP PROCEDURE CreateReminder;  
GO  


CREATE PROCEDURE CreateReminder
	@Title NVARCHAR(255),
	@Time TIME,
	@Date DATETIME
AS BEGIN
INSERT INTO TReminders(reminderTitle, reminderTime, isDeleted)
Values(@Title, @Time, 0);
INSERT INTO TReminderDates(reminderID, reminderDate)
Select MAX(reminderID),@Date FROM TReminders
end
go




DROP PROCEDURE UpdateReminder;  
GO  


CREATE PROCEDURE UpdateReminder
	@Title NVARCHAR(255),
	@Time TIME,
	@Date DATETIME,
	@ID int
AS BEGIN
	Update TReminders Set reminderTime = @Time, reminderTitle = @Title Where reminderID = @ID
	Update TReminderDates Set reminderDate = @Date Where reminderID = @ID
END
GO




DROP PROCEDURE DeleteReminder;  
GO  


CREATE PROCEDURE DeleteReminder

	@ID int
AS BEGIN
	Update TReminders Set isDeleted = 1 Where reminderID = @ID
END
GO



DROP PROCEDURE QueryReminder;  
GO  


CREATE PROCEDURE QueryReminder
	@ID int = null
AS BEGIN
Select TReminders.reminderID, TReminders.reminderTitle, TReminders.reminderTime, TReminderDates.reminderDate, TReminders.isDeleted
From TReminders, TReminderDates
Where TReminders.reminderID = TReminderDates.reminderID
and TReminders.reminderID = COALESCE(@ID,TReminders.reminderID)
END
GO



																				------------
																				-- ALARMS --
																				------------

DROP PROCEDURE CreateAlarm;  
GO  


CREATE PROCEDURE CreateAlarm
	@Title NVARCHAR(255),
	@Time TIME,
	@Date DATETIME
AS BEGIN
INSERT INTO TAlarms(AlarmTitle, AlarmTime, isDeleted)
Values(@Title, @Time, 0);
INSERT INTO TAlarmDates(AlarmID, AlarmDate)
Select MAX(AlarmID),@Date FROM TAlarms
end
go


DROP PROCEDURE UpdateAlarm;  
GO  


CREATE PROCEDURE UpdateAlarm
	@Title NVARCHAR(255),
	@Time TIME,
	@Date DATETIME,
	@ID int
AS BEGIN
	Update TAlarms Set AlarmTime = @Time, AlarmTitle = @Title Where AlarmID = @ID
	Update TAlarmDates Set AlarmDate = @Date Where AlarmID = @ID
END
GO


DROP PROCEDURE DeleteAlarm;  
GO  


CREATE PROCEDURE DeleteAlarm

	@ID int
AS BEGIN
	Update TAlarms Set isDeleted = 1 Where AlarmID = @ID
END
GO






DROP PROCEDURE QueryAlarm;  
GO  


CREATE PROCEDURE QueryAlarm
	@ID int = null
AS BEGIN
Select TAlarms.AlarmID, TAlarms.AlarmTitle, TAlarms.AlarmTime, TAlarmDates.AlarmDate, TAlarms.isDeleted
From TAlarms, TAlarmDates
Where TAlarms.AlarmID = TAlarmDates.AlarmID
and TAlarms.AlarmID = COALESCE(@ID,TAlarms.AlarmID)
END
GO



																				-----------------
																				-- Voice Notes --
																				-----------------

DROP PROCEDURE CreateVoiceNote;  
GO  


CREATE PROCEDURE CreateVoiceNote
	@FileName			VARCHAR(255),
	@DisplayName		NVARCHAR(255),
	@recordingDuration	INTEGER,
	@filePath			NVARCHAR(255),
	@recordDate			DATE,
	@recordTime			TIME
AS BEGIN
INSERT INTO TVoiceMemos([fileName],displayName,recordingDuration,filePath,recordDate,recordTime)
Values(@FileName,@DisplayName,@recordingDuration,@filePath,@recordDate,@recordTime)

end
go




DROP PROCEDURE DeleteVoiceNote;  
GO  


CREATE PROCEDURE DeleteVoiceNote

	@ID int
AS BEGIN
	DELETE FROM TVoiceMemos Where TVoiceMemos.voiceMemoID = @ID;
END
GO



DROP PROCEDURE UpdateVoiceNote;  
GO  


CREATE PROCEDURE UpdateVoiceNote

	@Title NVARCHAR(255),
	@ID	int
AS BEGIN
	Update TVoiceMemos Set displayName = @Title Where voiceMemoID = @ID
END
GO




DROP PROCEDURE QueryWeatherProvider;  
GO  

CREATE PROCEDURE QueryWeatherProvider
	@ID int = null
AS BEGIN
Select TWeatherProviders.weatherProviderID, TWeatherProviders.weatherProviderName,TWeatherProviderURLS.weatherProviderURL,TWeatherProviderURLParts.weatherProviderURLPartURLString,TWeatherProviderAccessTypes.weatherProviderAccessType
From TWeatherProviders, TWeatherProviderURLS, TWeatherProviderURLParts, TWeatherProviderAccessTypes
Where TWeatherProviders.weatherProviderID = COALESCE(@ID,TWeatherProviders.weatherProviderID)
and TWeatherProviders.weatherProviderID = TWeatherProviderURLS.weatherProviderID
and TWeatherProviders.weatherProviderID = TWeatherProviderURLParts.weatherProviderID
and TWeatherProviders.weatherProviderID = TWeatherProviderAccessTypes.weatherProviderID
END
GO



DROP PROCEDURE QueryMapProvider;  
GO  


CREATE PROCEDURE QueryMapProvider
	@ID int = null
AS BEGIN
Select TmapProviders.mapProviderID, TmapProviders.mapProviderName,TMapProvidersURLS.mapProviderURL,TMapProvidersURLParts.mapProviderURLPartType,TMapProvidersURLParts.mapProviderURLPartURL,TmapProviderAccessTypes.mapProviderAccessType
From TmapProviders, TMapProvidersURLS, TmapProvidersURLParts, TmapProviderAccessTypes
Where TmapProviders.mapProviderID = COALESCE(@ID,TmapProviders.mapProviderID)
and TmapProviders.mapProviderID = TmapProvidersURLS.mapProviderID
and TmapProviders.mapProviderID = TMapProvidersURLParts.mapProviderID
and TmapProviders.mapProviderID = TmapProviderAccessTypes.mapProviderID
END
GO





DROP PROCEDURE QuerySearchableWebsites;  
GO  


CREATE PROCEDURE QuerySearchableWebsites
	@ID int = null
AS BEGIN
Select TSearchableWebsites.searchableWebsitesID, TSearchableWebsites.searchableWebsiteName, TSearchableWebsites.searchableWebsiteBaseURL, TSearchableWebsites.searchableWebsiteQueryString
From TSearchableWebsites
Where TSearchableWebsites.searchableWebsitesID = COALESCE(@ID,TSearchableWebsites.searchableWebsitesID)

END
GO




DROP PROCEDURE QuerySearchEngine;  
GO  


CREATE PROCEDURE QuerySearchEngine
	@ID int = null
AS BEGIN
Select TSearchEngines.searchEngineID, TSearchEngines.searchEngineName, TSearchEngines.searchEngineBaseURL, TSearchEngines.searchEngineQueryString
From TSearchEngines
Where TSearchEngines.searchEngineID = COALESCE(@ID,TSearchEngines.searchEngineID)
END
GO




DROP PROCEDURE QueryWeatherProviderNames;  
GO  


CREATE PROCEDURE QueryWeatherProviderNames
	
AS BEGIN
Select TWeatherProviders.weatherProviderID,TWeatherProviders.weatherProviderName
From TWeatherProviders
END
GO


DROP PROCEDURE QueryMapProviderNames;  
GO  

CREATE PROCEDURE QueryMapProviderNames
	
AS BEGIN
Select TMapProviders.mapProviderID,TMapProviders.mapProviderName
From TMapProviders
END
GO




DROP PROCEDURE QuerySearchableWebsiteNames;  
GO  


CREATE PROCEDURE QuerySearchableWebsiteNames
	
AS BEGIN
Select TSearchableWebsites.searchableWebsitesID,TSearchableWebsites.searchableWebsitesID
From TSearchableWebsites
END
GO



DROP PROCEDURE QueryAllSettings;  
GO  


CREATE PROCEDURE QueryAllSettings
@ID int = null
AS BEGIN
Select TSettings.settingID, TSettings.settingDisplayName, TSettingOptions.optionDisplayName,TSettingOptions.isSelected
From TSettings, TSettingOptions
Where TSettings.settingID = COALESCE(@ID,TSettings.settingID)
and TSettings.settingID = TSettingOptions.settingID
END
GO




DROP PROCEDURE UpdateSetting;  
GO  


CREATE PROCEDURE UpdateSetting
@ID int,
@SELECTED bit
AS BEGIN
Update TSettingOptions 
Set isSelected = @SELECTED 
Where settingOptionID = @ID
END
GO

