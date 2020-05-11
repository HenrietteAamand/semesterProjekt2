CREATE TABLE [dbo].[AnalyzedECG] (
    [AECGID]       INT             NOT NULL,
    [ECGID]        INT             NOT NULL,
    [CPR]          NCHAR (11)      NOT NULL,
    [BLOBValues]   VARBINARY (MAX) NOT NULL,
    [Illness]      INT             NULL,
    [Date]         DATETIME        NOT NULL,
    [BLOBstValues] VARBINARY (MAX) NULL,
    [Samplerate]   DECIMAL (18, 2) NOT NULL,
    [MonitorID]    NCHAR (10)      NOT NULL,
    [IsRead]       BIT             NULL,
    [STStartIndex] INT             NULL,
    [Baseline]     FLOAT (53)      NULL,
    PRIMARY KEY CLUSTERED ([AECGID] ASC),
    FOREIGN KEY ([CPR]) REFERENCES [dbo].[Patient] ([CPR]),
    FOREIGN KEY ([ECGID]) REFERENCES [dbo].[ECG] ([ECGID]),
    FOREIGN KEY ([Illness]) REFERENCES [dbo].[Illness] ([ID]),
    FOREIGN KEY ([MonitorID]) REFERENCES [dbo].[ECGMonitor] ([ECGMonitorID])
);

