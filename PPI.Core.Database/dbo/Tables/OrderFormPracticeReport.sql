CREATE TABLE [dbo].[OrderFormPracticeReport]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [OrderFormId] INT NOT NULL, 
    [PracticeReportId] INT NOT NULL, 
    CONSTRAINT [FK_OrderFormPracticeReport_OrderForm] FOREIGN KEY ([OrderFormId]) REFERENCES [OrderForm]([Id]), 
    CONSTRAINT [FK_OrderFormPracticeReport_PracticeReport] FOREIGN KEY ([PracticeReportId]) REFERENCES [PracticeReport]([Id])
)
