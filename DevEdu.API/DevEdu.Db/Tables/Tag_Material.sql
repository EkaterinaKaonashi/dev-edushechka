﻿CREATE TABLE [Tag_Material] (
	Id int NOT NULL IDENTITY(1,1),
	TagId int NOT NULL,
	MaterialId int NOT NULL,
  CONSTRAINT [PK_TAG_MATERIAL] PRIMARY KEY CLUSTERED
  (
  [Id] ASC
  ) WITH (IGNORE_DUP_KEY = OFF)

)
go

ALTER TABLE [Tag_Material] WITH CHECK ADD CONSTRAINT [Tag_Material_fk0] FOREIGN KEY ([TagId]) REFERENCES [Tag]([Id])
ON UPDATE NO ACTION
GO
ALTER TABLE [Tag_Material] CHECK CONSTRAINT [Tag_Material_fk0]
GO
ALTER TABLE [Tag_Material] WITH CHECK ADD CONSTRAINT [Tag_Material_fk1] FOREIGN KEY ([MaterialId]) REFERENCES [Material]([Id])
ON UPDATE NO ACTION
GO
ALTER TABLE [Tag_Material] CHECK CONSTRAINT [Tag_Material_fk1]
GO
ALTER TABLE [dbo].[Tag_Material]
ADD CONSTRAINT UC_TagId_MaterialId UNIQUE(TagId, MaterialId)
GO