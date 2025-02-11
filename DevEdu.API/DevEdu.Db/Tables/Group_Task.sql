﻿CREATE TABLE [Group_Task] (
	Id int NOT NULL IDENTITY(1,1),
	GroupId int NOT NULL,
	TaskId int NOT NULL,
	StartDate datetime NOT NULL,
	EndDate datetime NOT NULL,
  CONSTRAINT [PK_GROUP_TASK] PRIMARY KEY CLUSTERED
  (
  [Id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)
)
go

ALTER TABLE [Group_Task] WITH CHECK ADD CONSTRAINT [Group_Task_fk0] FOREIGN KEY ([GroupId]) REFERENCES [Group]([Id])
ON UPDATE NO ACTION
GO
ALTER TABLE [Group_Task] CHECK CONSTRAINT [Group_Task_fk0]
GO
ALTER TABLE [Group_Task] WITH CHECK ADD CONSTRAINT [Group_Task_fk1] FOREIGN KEY ([TaskId]) REFERENCES [Task]([Id])
ON UPDATE NO ACTION
GO
ALTER TABLE [Group_Task] CHECK CONSTRAINT [Group_Task_fk1]
GO
ALTER TABLE [dbo].[Group_Task] ADD CONSTRAINT UC_GroupId_TaskId UNIQUE([GroupId], [TaskId])
GO