CREATE TABLE [dbo].[ECGMonitor] (
    [ECGMonitorID] NCHAR (10) NOT NULL,
    [InUse]        BIT        DEFAULT ((0)) NOT NULL,
    PRIMARY KEY CLUSTERED ([ECGMonitorID] ASC)
);

