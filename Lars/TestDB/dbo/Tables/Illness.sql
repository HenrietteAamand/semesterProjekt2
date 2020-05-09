CREATE TABLE [dbo].[Illness] (
    [ID]                 INT            NOT NULL,
    [About]              NVARCHAR (MAX) NULL,
    [stMax]              FLOAT (53)     NULL,
    [srMax]              FLOAT (53)     NULL,
    [STSegmentElevated]  BIT            NULL,
    [STSegmentDepressed] BIT            NULL,
    [Name]               NVARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

