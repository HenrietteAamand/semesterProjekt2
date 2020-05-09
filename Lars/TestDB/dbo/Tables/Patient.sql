CREATE TABLE [dbo].[Patient] (
    [CPR]       NCHAR (11)    NOT NULL,
    [FirstName] NVARCHAR (20) NOT NULL,
    [LastName]  NVARCHAR (30) NOT NULL,
    [LinkedECG] NCHAR (10)    NULL,
    PRIMARY KEY CLUSTERED ([CPR] ASC),
    FOREIGN KEY ([LinkedECG]) REFERENCES [dbo].[ECGMonitor] ([ECGMonitorID])
);

