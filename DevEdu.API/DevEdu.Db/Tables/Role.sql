﻿CREATE TABLE [dbo].[Role] (
	Id int NOT NULL,
	Name nvarchar(50) NOT NULL UNIQUE,
  CONSTRAINT [PK_ROLE] PRIMARY KEY CLUSTERED
  (
  [Id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)
)