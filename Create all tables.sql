CREATE TABLE Order_Services (
    serviceID  INT            NOT NULL,
    orderID    INT            NOT NULL,
    Photo      NVARCHAR (MAX) NOT NULL,
    NumbCopies INT            NOT NULL,
    CONSTRAINT fk_serviceId_ServicesId FOREIGN KEY (serviceID) REFERENCES Seveces (serviceId) ON DELETE CASCADE,
    CONSTRAINT fk_ordereID_ServicesId FOREIGN KEY (orderID) REFERENCES Orders (OrderId) ON DELETE CASCADE
);

CREATE TABLE Orders (
    OrderId    INT IDENTITY (1, 1) NOT NULL,
    UserId     INT NOT NULL,
    TotalPrice DECIMAL (18) NOT NULL,
    StatusId   INT DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED (OrderId ASC),
    CONSTRAINT fk_Status_Order FOREIGN KEY (StatusId) REFERENCES Statuses (Id) ON DELETE CASCADE,
    CONSTRAINT fk_User_Order FOREIGN KEY (UserId) REFERENCES Users (UserId) ON DELETE CASCADE
);
CREATE TABLE Statuses (
    Id  INT           IDENTITY (1, 1) NOT NULL,
    Name NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED (Id ASC)
);
CREATE TABLE Users (
    UserId     INT            IDENTITY (1, 1) NOT NULL,
    UserName    NVARCHAR (100) NOT NULL,
    UserSurname NVARCHAR (100) NOT NULL,
    RoleID      INT            DEFAULT ((2)) NOT NULL,
    Phone       NVARCHAR (10)  DEFAULT (NULL) NULL,
    Email       NVARCHAR (50)  NOT NULL,
    Password    NVARCHAR (50)  NOT NULL,
    PRIMARY KEY CLUSTERED (UserId ASC),
    CONSTRAINT fk_User_Role FOREIGN KEY (RoleID) REFERENCES Role (RoleId)
);
CREATE TABLE Seveces (
    serviceId   INT            IDENTITY (1, 1) NOT NULL,
    SName       NVARCHAR (255) NOT NULL,
    PhotoFormat NVARCHAR (100) NOT NULL,
    Description NVARCHAR (MAX) NOT NULL,
    ColorType   NVARCHAR (255) NOT NULL,
    Price       DECIMAL (18)   NOT NULL,
    Image       VARCHAR (MAX)  NOT NULL,
    PRIMARY KEY CLUSTERED (serviceId ASC)
);
CREATE TABLE Role (
    RoleId   INT           IDENTITY (1, 1) NOT NULL,
    RoleName NVARCHAR (50) DEFAULT ('user') NOT NULL,
    PRIMARY KEY CLUSTERED (RoleId ASC)
); 



