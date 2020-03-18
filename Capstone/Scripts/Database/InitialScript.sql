-- --------------------------------------------------------------------------------
-- Project Bob
-- Capstone 2020
-- --------------------------------------------------------------------------------

-- --------------------------------------------------------------------------------
-- Options
-- --------------------------------------------------------------------------------

IF DB_ID('bobDB') IS NULL
BEGIN
CREATE DATABASE bobDB
END


USE bobDB;   -- Get out of the master database
SET NOCOUNT ON;		-- Report only errors

-- uspCleanDatabase

-- --------------------------------------------------------------------------------
-- Drop Tables
-- --------------------------------------------------------------------------------
IF OBJECT_ID( 'TSettingOptions' )							IS NOT NULL DROP TABLE TSettingOptions
IF OBJECT_ID( 'TSettings' )									IS NOT NULL DROP TABLE TSettings

IF OBJECT_ID( 'TSearchEngines' )							IS NOT NULL DROP TABLE TSearchEngines
IF OBJECT_ID( 'TSearchableWebsites' )						IS NOT NULL DROP TABLE TSearchableWebsites

IF OBJECT_ID( 'TAlarmDates' )								IS NOT NULL DROP TABLE TAlarmDates
IF OBJECT_ID( 'TAlarms' )									IS NOT NULL DROP TABLE TAlarms

IF OBJECT_ID( 'TReminderDates' )							IS NOT NULL DROP TABLE TReminderDates
IF OBJECT_ID( 'TReminders')									IS NOT NULL DROP TABLE TReminders

IF OBJECT_ID( 'TMapProvidersURLS' )							IS NOT NULL DROP TABLE TMapProvidersURLS
IF OBJECT_ID( 'TMapProviderAccessTypes' )					IS NOT NULL DROP TABLE TMapProviderAccessTypes
IF OBJECT_ID( 'TMapProvidersURLParts' )						IS NOT NULL DROP TABLE TMapProvidersURLParts
IF OBJECT_ID( 'TMapProviders' )								IS NOT NULL DROP TABLE TMapProviders

IF OBJECT_ID( 'TVoiceMemos' )								IS NOT NULL DROP TABLE TVoiceMemos

IF OBJECT_ID( 'TUserInfos' )								IS NOT NULL DROP TABLE TUserInfos

IF OBJECT_ID( 'TCaches' )									IS NOT NULL DROP TABLE TCaches

IF OBJECT_ID( 'TBobResponseInputs' )						IS NOT NULL DROP TABLE TBobResponseInputs
IF OBJECT_ID( 'TBobResponses' )								IS NOT NULL DROP TABLE TBobResponses

IF OBJECT_ID( 'TWeatherProviderURLS' )						IS NOT NULL DROP TABLE TWeatherProviderURLS
IF OBJECT_ID( 'TWeatherProviderURLParts' )					IS NOT NULL DROP TABLE TWeatherProviderURLParts
IF OBJECT_ID( 'TWeatherProviderAccessTypes' )				IS NOT NULL DROP TABLE TWeatherProviderAccessTypes
IF OBJECT_ID( 'TWeatherProviders' )							IS NOT NULL DROP TABLE TWeatherProviders




-- --------------------------------------------------------------------------------
-- Step #1.1: Create Tables
-- --------------------------------------------------------------------------------
CREATE TABLE TSettings
(
	 settingID							INTEGER			NOT NULL
	,settingDisplayName					VARCHAR(255)	NOT NULL
	,CONSTRAINT TSettings_PK PRIMARY KEY ( settingID )
)

CREATE TABLE TSettingOptions
(
	 settingOptionID					INTEGER			NOT NULL
	,optionDisplayName					VARCHAR(255)	NOT NULL
	,isSelected							BIT				NOT NULL
	,settingID							INTEGER			NOT NULL
	,CONSTRAINT TSettingOptions_PK PRIMARY KEY ( settingOptionID )
)

CREATE TABLE TSearchEngines
(
	 searchEngineID						INTEGER			NOT NULL
	,searchEngineName					NVARCHAR(255)	NOT NULL
	,searchEngineBaseURL				NVARCHAR(255)	NOT NULL
	,searchEngineQueryString			NVARCHAR(255)	NOT NULL
	,CONSTRAINT TSearchEngines_PK PRIMARY KEY ( searchEngineID )
)

CREATE TABLE TSearchableWebsites
(						
	 searchableWebsitesID				INTEGER			NOT NULL
	,searchableWebsiteName				NVARCHAR(255)	NOT NULL
	,searchableWebsiteBaseURL			NVARCHAR(255)	NOT NULL
	,searchableWebsiteQueryString		NVARCHAR(255)	NOT NULL
	,CONSTRAINT TSearchableWebsites_PK PRIMARY KEY ( searchableWebsitesID )
)

CREATE TABLE TAlarms
(						
	 alarmID							INTEGER			IDENTITY(1,1) NOT NULL
	,alarmTime							TIME			NOT NULL
	,alarmTitle							NVARCHAR(255)	NOT NULL
	,isDeleted							BIT				NOT NULL
	,CONSTRAINT TAlarms_PK PRIMARY KEY ( alarmID )
)

CREATE TABLE TAlarmDates
(						
	 alarmDateID						INTEGER			IDENTITY(1,1) NOT NULL
	,alarmID							INTEGER			NOT NULL
	,alarmDate							DATETIME		NOT NULL
	,CONSTRAINT TAlarmDates_PK PRIMARY KEY ( alarmDateID )
)

CREATE TABLE TReminders
(
	 reminderID							INTEGER			IDENTITY(1,1) NOT NULL
	,reminderTitle						NVARCHAR(255)	NOT NULL
	,reminderTime						TIME			NOT NULL
	,isDeleted							BIT				NOT NULL
	,CONSTRAINT TReminders_PK PRIMARY KEY ( reminderID )
)

CREATE TABLE TReminderDates
(
	 reminderDateID						INTEGER			IDENTITY(1,1) NOT NULL
	,reminderID							INTEGER			NOT NULL
	,reminderDate						DATETIME		NOT NULL
	,CONSTRAINT TReminderDates_PK PRIMARY KEY ( reminderDateID )
)


CREATE TABLE TMapProviders
(
	 mapProviderID						INTEGER		    NOT NULL
	,mapProviderName					NVARCHAR(255)	NOT NULL
	,CONSTRAINT TMapProviders_PK PRIMARY KEY ( mapProviderID )
)


CREATE TABLE TMapProvidersURLS
(
	 mapProviderURLID					INTEGER			NOT NULL
	,mapProviderID						INTEGER			NOT NULL
	,mapProviderURL						NVARCHAR(255)	NOT NULL
	,CONSTRAINT TMapProvidersURLS_PK PRIMARY KEY ( mapProviderURLID )
)

CREATE TABLE TMapProviderAccessTypes
(
	 mapProviderAccessTypeID			INTEGER			NOT NULL
	,mapProviderID						INTEGER			NOT NULL
	,mapProviderAccessType				NVARCHAR(255)	NOT NULL			
	,CONSTRAINT TMapProviderAccessTypes_PK PRIMARY KEY ( mapProviderAccessTypeID )
)

CREATE TABLE TMapProvidersURLParts
(
	 mapProviderURLPartID				INTEGER			NOT NULL
	,mapProviderID						INTEGER			NOT NULL
	,mapProviderURLPartType				VARCHAR(255)	NOT NULL
	,mapProviderURLPartURL				NVARCHAR(255)	NOT NULL
	,CONSTRAINT TMapProvidersURLParts_PK PRIMARY KEY (  mapProviderURLPartID	 )
)

CREATE TABLE TVoiceMemos
(
	 voiceMemoID						INTEGER			IDENTITY(1,1) NOT NULL
	,[fileName]							VARCHAR(255)	NOT NULL -- fileName on its own is a keyword so we need to wrap it in square brackets
	,displayName						NVARCHAR(255)	NOT NULL
	,recordingDuration					INTEGER			NOT NULL
	,filePath							NVARCHAR(255)	NOT NULL
	,recordDate							DATE			NOT NULL
	,recordTime							TIME			NOT NULL
	,CONSTRAINT TVoiceMemos_PK PRIMARY KEY (  voiceMemoID	 )
)

CREATE TABLE TUserInfos
(
	 userInfoID							INTEGER			NOT NULL
	,userInfoTypeName					VARCHAR(255)	NOT NULL
	,userInfoTypeValue					NVARCHAR(255)	NOT NULL
	,CONSTRAINT TUserInfos_PK PRIMARY KEY ( userInfoID )
)


CREATE TABLE TCaches
(
	 cacheKey							INTEGER			NOT NULL
	,cacheContents						NVARCHAR(255)	NOT NULL
	,cacheExpirationDate				DATETIME		NOT NULL
	,CONSTRAINT TCaches_PK PRIMARY KEY ( cacheKey )
)

CREATE TABLE TBobResponses
(
	 bobResponseID						INTEGER			NOT NULL
	,bobResponseString					NVARCHAR(255)	NOT NULL
	,CONSTRAINT  TBobResponses_PK PRIMARY KEY ( bobResponseID )
)

CREATE TABLE TBobResponseInputs
(
	 bobResponseInputID					INTEGER			NOT NULL
	,bobResponseID						INTEGER			NOT NULL
	,CONSTRAINT  TBobResponseInputs_PK PRIMARY KEY ( bobResponseInputID )
)

CREATE TABLE TWeatherProviders
(
	 weatherProviderID					INTEGER			NOT NULL
	,weatherProviderName				NVARCHAR(255)	NOT NULL
	,CONSTRAINT  TWeatherProviders_PK PRIMARY KEY ( weatherProviderID )
)

CREATE TABLE TWeatherProviderURLS
(
	 weatherProviderURLID				INTEGER			NOT NULL
	,weatherProviderID					INTEGER			NOT NULL
	,weatherProviderURL					NVARCHAR(255)	NOT NULL
	,CONSTRAINT TWeatherProviderURLS_PK PRIMARY KEY ( weatherProviderURLID )
)


CREATE TABLE TWeatherProviderURLParts
(
	 weatherProviderURLPartID			INTEGER			NOT NULL
	,weatherProviderID					INTEGER			NOT NULL
	,weatherProviderURLPartURLString	NVARCHAR(255)	NOT NULL
	,CONSTRAINT  TWeatherProviderURLParts_PK PRIMARY KEY (  weatherProviderURLPartID	 )
)


CREATE TABLE TWeatherProviderAccessTypes
(
	 weatherProviderAccessTypeID		INTEGER			NOT NULL
	,weatherProviderID					INTEGER			NOT NULL
	,weatherProviderAccessType			NVARCHAR(255)	NOT NULL
	,CONSTRAINT  TWeatherProviderAccessTypes_PK PRIMARY KEY (  weatherProviderAccessTypeID	 )
)


-- --------------------------------------------------------------------------------
-- Step #1.2: Identify and Create Foreign Keys
-- --------------------------------------------------------------------------------
--
-- #	Child										Parent							Column(s)
-- -	-----										------							---------
-- 1	TAlarmDates									TAlarms							alarmID   --

-- 2	TReminderDates								TReminders						reminderID--

-- 3	TMapProvidersURLS							TMapProviders					mapProviderID --
-- 4	TMapProviderAccessTypes						TMapProviders					mapProviderID --
-- 5	TMapProvidersURLParts						TMapProviders					mapProviderID --

-- 6	TBobResponseInputs							TBobResponses					bobResponseID --

-- 7	TWeatherProviderURLS						TWeatherProviders				weatherProviderID --
-- 8	TWeatherProviderURLParts					TWeatherProviders				weatherProviderID --
-- 9	TWeatherProviderAccessTypes					TWeatherProviders				weatherProviderID --

-- 10	TSettingOptions								TSettings						settingID --
-- 
			


-- 1
ALTER TABLE TAlarmDates ADD CONSTRAINT TAlarmDates_TAlarms_FK
FOREIGN KEY ( alarmID ) REFERENCES TAlarms ( alarmID )

-- 2
ALTER TABLE TReminderDates ADD CONSTRAINT TReminderDates_TReminders_FK
FOREIGN KEY ( reminderID ) REFERENCES TReminders ( reminderID )

-- 3
ALTER TABLE TMapProvidersURLS ADD CONSTRAINT TJobs_TMapProviders_FK
FOREIGN KEY ( mapProviderID ) REFERENCES TMapProviders ( mapProviderID )

-- 4
ALTER TABLE TMapProviderAccessTypes ADD CONSTRAINT TMapProviderAccessTypes_TMapProviders_FK
FOREIGN KEY ( mapProviderID ) REFERENCES TMapProviders ( mapProviderID )

-- 5
ALTER TABLE TMapProvidersURLParts ADD CONSTRAINT TMapProvidersURLParts_TMapProviders_FK
FOREIGN KEY ( mapProviderID ) REFERENCES TMapProviders ( mapProviderID )

-- 6
ALTER TABLE TBobResponseInputs ADD CONSTRAINT TBobResponseInputs_TBobResponses_FK
FOREIGN KEY ( bobResponseID ) REFERENCES TBobResponses( bobResponseID )

-- 7
ALTER TABLE TWeatherProviderURLS ADD CONSTRAINT TWeatherProviderURLS_TWeatherProviders_FK
FOREIGN KEY ( weatherProviderID ) REFERENCES TWeatherProviders ( weatherProviderID )

-- 8
ALTER TABLE TWeatherProviderURLParts ADD CONSTRAINT TWeatherProviderURLParts_TWeatherProviders_FK
FOREIGN KEY ( weatherProviderID ) REFERENCES TWeatherProviders ( weatherProviderID )

-- 9
ALTER TABLE TWeatherProviderAccessTypes ADD CONSTRAINT TWeatherProviderAccessTypes_TWeatherProviders_FK
FOREIGN KEY ( weatherProviderID ) REFERENCES TWeatherProviders ( weatherProviderID )

-- 10
ALTER TABLE TSettingOptions ADD CONSTRAINT TSettingOptions_TSettings_FK
FOREIGN KEY ( settingID ) REFERENCES TSettings ( settingID )
