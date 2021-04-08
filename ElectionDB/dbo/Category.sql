CREATE TABLE [dbo].[Category]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY DEFAULT newid(), 
    [ElectionId] UNIQUEIDENTIFIER NOT NULL, 
    [CategoryTypeId] INT NOT NULL, 
    [SubcategoryTypeId] INT NOT NULL , 
    [Heading] NVARCHAR(256) NOT NULL, 
    [Title] NVARCHAR(100) NULL, 
    [District] INT NOT NULL DEFAULT 0, 
    [JudgePosition] INT NOT NULL DEFAULT 0, 
    [Information] NVARCHAR(MAX) NULL, 
    [SubTitle] NVARCHAR(200) NULL, 
    [CreateDate] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [LastUpdated] DATETIME NOT NULL DEFAULT sysutcdatetime(), 
    [Sequence] INT NOT NULL DEFAULT 0, 
    [Selection] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [FK_Election1] FOREIGN KEY ([ElectionId]) REFERENCES [Election](Id) ON DELETE CASCADE, 
    CONSTRAINT [FK_CategoryType1] FOREIGN KEY (CategoryTypeId) REFERENCES [CategoryType](Id) 

)
