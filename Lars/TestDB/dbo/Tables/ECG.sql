CREATE TABLE [dbo].[ECG] (
    [ECGID]      INT             IDENTITY (1, 1) NOT NULL,
    [CPR]        NCHAR (11)      NOT NULL,
    [BLOBValues] VARBINARY (MAX) NOT NULL,
    [Date]       DATETIME        NOT NULL,
    [Samplerate] REAL            NOT NULL,
    [MonitorID]  NCHAR (10)      NOT NULL,
    [IsAnalyzed] BIT             DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ECGID] ASC),
    FOREIGN KEY ([CPR]) REFERENCES [dbo].[Patient] ([CPR]),
    FOREIGN KEY ([MonitorID]) REFERENCES [dbo].[ECGMonitor] ([ECGMonitorID])
);

