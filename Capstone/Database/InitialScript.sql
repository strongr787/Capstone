CREATE TABLE IF NOT EXISTS "TSettings"
(
	 "settingID"			INTEGER PRIMARY KEY NOT NULL
	,"settingDisplayName"	VARCHAR(255)	NOT NULL
);

CREATE TABLE IF NOT EXISTS "TSettingOptions"
(
	 "settingOptionID"	    INTEGER 		PRIMARY KEY NOT NULL
	,"optionDisplayName"    VARCHAR(255)	NOT NULL
	,"isSelected"           BIT				NOT NULL
	,"settingID"            INTEGER			NOT NULL
    ,FOREIGN KEY ( settingID ) REFERENCES TSettings ( settingID )
);

CREATE TABLE IF NOT EXISTS "TSearchEngines"
(
	 "searchEngineID"			INTEGER			PRIMARY KEY	NOT NULL
	,"searchEngineName"			NVARCHAR(255)	NOT NULL
	,"searchEngineBaseURL"		NVARCHAR(255)	NOT NULL
	,"searchEngineQueryString"  NVARCHAR(255)	NOT NULL
);

CREATE TABLE IF NOT EXISTS "TSearchableWebsites"
(						
	 "searchableWebsitesID"				INTEGER			PRIMARY KEY NOT NULL
	,"searchableWebsiteName"			NVARCHAR(255)	NOT NULL
	,"searchableWebsiteBaseURL"			NVARCHAR(255)	NOT NULL
	,"searchableWebsiteQueryString"	    NVARCHAR(255)	NOT NULL
);

CREATE TABLE IF NOT EXISTS "TAlarms"
(						
	 "alarmID"	    INTEGER			PRIMARY KEY NOT NULL
	,"alarmTime"    TIME			NOT NULL
	,"alarmTitle"   NVARCHAR(255)	NOT NULL
	,"isDeleted"    BIT				NOT NULL
);

CREATE TABLE IF NOT EXISTS "TAlarmDates"
(						
	 "alarmDateID"  INTEGER			PRIMARY KEY NOT NULL
	,"alarmID"	    INTEGER			NOT NULL
	,"alarmDate"	DATETIME		NOT NULL
    ,FOREIGN KEY ( alarmID ) REFERENCES TAlarms ( alarmID )
);

CREATE TABLE IF NOT EXISTS "TReminders"
(
	 "reminderID"		INTEGER			PRIMARY KEY NOT NULL
	,"reminderTitle"    NVARCHAR(255)	NOT NULL
	,"reminderTime"	    TIME			NOT NULL
	,"isDeleted"		BIT				NOT NULL
);

CREATE TABLE IF NOT EXISTS "TReminderDates"
(
	 "reminderDateID"   INTEGER			PRIMARY KEY NOT NULL
	,"reminderID"	    INTEGER			NOT NULL
	,"reminderDate"     DATETIME		NOT NULL
    ,FOREIGN KEY ( reminderID ) REFERENCES TReminders ( reminderID )
);


CREATE TABLE IF NOT EXISTS "TMapProviders"
(
	 "mapProviderID"    INTEGER			PRIMARY KEY NOT NULL
	,"mapProviderName"  NVARCHAR(255)	NOT NULL
);


CREATE TABLE IF NOT EXISTS "TMapProvidersURLS"
(
	 "mapProviderURLID" INTEGER			PRIMARY KEY NOT NULL
	,"mapProviderID"    INTEGER			NOT NULL
	,"mapProviderURL"   NVARCHAR(255)	NOT NULL
    ,FOREIGN KEY ( mapProviderID ) REFERENCES TMapProviders ( mapProviderID )
);

CREATE TABLE IF NOT EXISTS "TMapProviderAccessTypes"
(
	 "mapProviderAccessTypeID"  INTEGER			PRIMARY KEY NOT NULL
	,"mapProviderID"            INTEGER			NOT NULL
	,"mapProviderAccessType"    NVARCHAR(255)	NOT NULL
    ,FOREIGN KEY ( mapProviderID ) REFERENCES TMapProviders ( mapProviderID )			
);

CREATE TABLE IF NOT EXISTS "TMapProvidersURLParts"
(
	 "mapProviderURLPartID"     INTEGER			PRIMARY KEY NOT NULL
	,"mapProviderID"            INTEGER			NOT NULL
	,"mapProviderURLPartType"   VARCHAR(255)	NOT NULL
	,"mapProviderURLPartURL"    NVARCHAR(255)	NOT NULL
    ,FOREIGN KEY ( mapProviderID ) REFERENCES TMapProviders ( mapProviderID )
);

CREATE TABLE IF NOT EXISTS "TVoiceMemos"
(
	 "voiceMemoID"          INTEGER			PRIMARY KEY NOT NULL
	,"fileName"         	VARCHAR(255)	NOT NULL
	,"displayName"          NVARCHAR(255)	NOT NULL
	,"recordingDuration"    INTEGER			NOT NULL
	,"filePath"         	NVARCHAR(255)	NOT NULL
    ,"recordDate"           DATE			NOT NULL
    ,"recordTime"           TIME			NOT NULL
);

CREATE TABLE IF NOT EXISTS "TUserInfos"
(
	 "userInfoID"           INTEGER			PRIMARY KEY NOT NULL
	,"userInfoTypeName"     VARCHAR(255)	NOT NULL
	,"userInfoTypeValue"    NVARCHAR(255)	NOT NULL
);


CREATE TABLE IF NOT EXISTS "TCaches"
(
	 "cacheKey"		        INTEGER			PRIMARY KEY NOT NULL
	,"cacheContents"	    NVARCHAR(255)	NOT NULL
	,"cacheExpirationDate"  DATETIME		NOT NULL
);

CREATE TABLE IF NOT EXISTS "TBobResponses"
(
	 "bobResponseID"        INTEGER			PRIMARY KEY NOT NULL
	,"bobResponseString"    NVARCHAR(255)	NOT NULL
);

CREATE TABLE IF NOT EXISTS "TBobResponseInputs"
(
	 "bobResponseInputID"   INTEGER			PRIMARY KEY NOT NULL
	,"bobResponseID"	    INTEGER			NOT NULL
    ,FOREIGN KEY ( bobResponseID ) REFERENCES TBobResponses( bobResponseID )
);

CREATE TABLE IF NOT EXISTS "TWeatherProviders"
(
	 "weatherProviderID"    INTEGER			PRIMARY KEY NOT NULL
	,"weatherProviderName"  NVARCHAR(255)	NOT NULL
);

CREATE TABLE IF NOT EXISTS "TWeatherProviderURLS"
(
	 "weatherProviderURLID" INTEGER			PRIMARY KEY NOT NULL
	,"weatherProviderID"	INTEGER			NOT NULL
	,"weatherProviderURL"	NVARCHAR(255)	NOT NULL
    ,FOREIGN KEY ( weatherProviderID ) REFERENCES TWeatherProviders ( weatherProviderID )
);


CREATE TABLE IF NOT EXISTS "TWeatherProviderURLParts"
(
	 "weatherProviderURLPartID"			INTEGER			PRIMARY KEY NOT NULL
	,"weatherProviderID"				INTEGER			NOT NULL
	,"weatherProviderURLPartURLString"  NVARCHAR(255)	NOT NULL
    ,FOREIGN KEY ( weatherProviderID ) REFERENCES TWeatherProviders ( weatherProviderID )
);


CREATE TABLE IF NOT EXISTS "TWeatherProviderAccessTypes"
(
	 "weatherProviderAccessTypeID"  INTEGER			PRIMARY KEY NOT NULL
	,"weatherProviderID"            INTEGER			NOT NULL
	,"weatherProviderAccessType"    NVARCHAR(255)	NOT NULL
    ,FOREIGN KEY ( weatherProviderID ) REFERENCES TWeatherProviders ( weatherProviderID )
);