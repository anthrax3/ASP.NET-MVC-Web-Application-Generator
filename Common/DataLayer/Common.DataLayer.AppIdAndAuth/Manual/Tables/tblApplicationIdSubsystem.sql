﻿CREATE TABLE [dbo].[tblApplicationIdSubsystem] -- Subsystem = Entity = ComplexEntity
(
    [Id] BIGINT IDENTITY (1,1) NOT NULL PRIMARY KEY, 
    [EnumName] NVARCHAR(64) NOT NULL,
	[ApplicationId] BIGINT NOT NULL FOREIGN KEY REFERENCES tblApplicationIdApplication(Id)
)
