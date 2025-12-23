create database Inkspire;
USE Inkspire;

CREATE TABLE Users (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Username VARCHAR(50) NOT NULL UNIQUE,
    Email VARCHAR(100) NULL,
    PasswordHash VARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP
);
CREATE TABLE Challenges (
    Id INT PRIMARY KEY AUTO_INCREMENT,
	ShapeId INT,
	TopicId INT,
    CreatedBy INT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CreatedBy) REFERENCES Users(Id),
    FOREIGN KEY (ShapeId) REFERENCES Shapes(id),
	FOREIGN KEY (TopicId) REFERENCES Topics(id)
);
CREATE TABLE Drawings (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    UserId INT NOT NULL,
    ChallengeId INT NOT NULL,
    DrawingData TEXT NOT NULL,
    CreatedAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES Users(Id),
    FOREIGN KEY (ChallengeId) REFERENCES Challenges(Id)
);
CREATE TABLE Topics (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name TEXT NOT NULL,
    description text
);
CREATE TABLE Shapes (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    svgPath TEXT NOT NULL,
    complexityLevel INT CHECK (complexityLevel >= 1 AND complexityLevel <= 10)
);
CREATE TABLE DrawingFeedback (
    id INT AUTO_INCREMENT PRIMARY KEY,
    drawingId INT NOT NULL,
    userId INT, 
    feedbackText TEXT NOT NULL,
    rating INT CHECK (rating BETWEEN 1 AND 5),
    createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (drawingId) REFERENCES Drawings(id),
    FOREIGN KEY (userId) REFERENCES Users(id)
);