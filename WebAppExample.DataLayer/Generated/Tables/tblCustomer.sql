CREATE TABLE tblCustomer ( 
    Id BIGINT,
    Comment NVARCHAR(MAX),
    PersonId BIGINT,
    PersonComment NVARCHAR(MAX),
    PersonFirstName NVARCHAR(16),
    PersonSurname NVARCHAR(16),
    PersonPatronymicName NVARCHAR(16),
    PersonGender BIGINT FOREIGN KEY REFERENCES tblDirectoryValue(Id),
    PersonMaritalStatus BIGINT FOREIGN KEY REFERENCES tblDirectoryValue(Id),
    PersonBirthDate DATETIMEOFFSET,
    PersonAmountOfChildren BIGINT,
    PersonAddressOfCurrLivingPlace NVARCHAR(32),
    PersonContactsId BIGINT,
    PersonContactsActualAddress NVARCHAR(32),
    PersonContactsFormalAddress NVARCHAR(32),
    PersonContactsPersonalMobilePhone NVARCHAR(11),
    PersonContactsWorkMobilePhone NVARCHAR(11),
    PersonContactsHomePhone NVARCHAR(11),
    PersonContactsWorkPhone NVARCHAR(24),
    PersonContactsPersonalEMail NVARCHAR(24),
    PersonContactsWorkEMail NVARCHAR(24),
    PersonContactsMessenger1 NVARCHAR(16),
    PersonContactsMessenger2 NVARCHAR(16),
    PersonContactsMessenger3 NVARCHAR(16),
    PersonPassportInfoId BIGINT,
    PersonPassportInfoComment NVARCHAR(MAX),
    PersonPassportInfoSeries NVARCHAR(8),
    PersonPassportInfoNumber NVARCHAR(16),
    PersonPassportInfoIssueDate DATETIMEOFFSET,
    PersonPassportInfoWhoIsIssuer NVARCHAR(512),
    PersonPassportInfoAddressOfResidence NVARCHAR(32),
    PersonPassportInfoAddressOfRegistration NVARCHAR(32),
    PersonPassportInfoLastChangeDate DATETIMEOFFSET,
    PaymentAccountId BIGINT,
    PaymentAccountComment NVARCHAR(MAX),
    PaymentAccountBalance BIGINT,
    PaymentAccountAccountState BIGINT FOREIGN KEY REFERENCES tblDirectoryValue(Id),
 ); 
GO
