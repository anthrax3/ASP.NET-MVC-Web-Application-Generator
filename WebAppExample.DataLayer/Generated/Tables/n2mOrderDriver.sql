CREATE TABLE n2mOrderDriver(
    [Order] BIGINT FOREIGN KEY REFERENCES tblOrder(Id),
    [Driver] BIGINT FOREIGN KEY REFERENCES tblDriver(Id),
);
GO