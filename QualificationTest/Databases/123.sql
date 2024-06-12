-- Script Date: 05.06.2024 13:50  - ErikEJ.SqlCeScripting version 3.5.2.95
-- Database information:
-- Database: C:\Users\pmc\source\Repos\sadasd933\PP_2024_Zhidkov\QualificationTest\Databases\Database.db
-- ServerVersion: 3.40.0
-- DatabaseSize: 100 KB
-- Created: 27.04.2024 0:14

-- User Table information:
-- Number of tables: 6
-- Answers: -1 row(s)
-- Questions: -1 row(s)
-- Results: -1 row(s)
-- Tests: -1 row(s)
-- UserAnswers: -1 row(s)
-- Users: -1 row(s)

SELECT 1;
PRAGMA foreign_keys=OFF;
BEGIN TRANSACTION;
CREATE TABLE [Users] (
  [UsersID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [UsersName] text NULL
, [UsersLogin] text NULL
, [UsersPassword] text NULL
, [UsersRole] text NULL
);
CREATE TABLE [Tests] (
  [TestID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [NumberOfQuestions] bigint NULL
, [Difficulty] text NULL
);
CREATE TABLE [Results] (
  [ResultsID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [TesterName] text NULL
, [PercentageOfCorrectAnswers] bigint NULL
, [NumberOfQuestions] bigint NULL
, [NumberOfCorrectAnswers] bigint NULL
);
CREATE TABLE [Questions] (
  [QuestionID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [QuestionText] text NULL
, [NumOfCorrectAnswers] bigint NULL
, [TestID] bigint NULL
, CONSTRAINT [FK_Questions_0_0] FOREIGN KEY ([TestID]) REFERENCES [Tests] ([TestID]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
CREATE TABLE [UserAnswers] (
  [UserAnswerID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [CorrectAnswer] text NULL
, [UsersAnswer] text NULL
, [UserID] bigint NULL
, [QuestionID] bigint NULL
, [ResultID] bigint NULL
, CONSTRAINT [FK_UserAnswers_0_0] FOREIGN KEY ([ResultID]) REFERENCES [Results] ([ResultsID]) ON DELETE NO ACTION ON UPDATE NO ACTION
, CONSTRAINT [FK_UserAnswers_1_0] FOREIGN KEY ([UserID]) REFERENCES [Users] ([UsersID]) ON DELETE NO ACTION ON UPDATE NO ACTION
, CONSTRAINT [FK_UserAnswers_2_0] FOREIGN KEY ([QuestionID]) REFERENCES [Questions] ([QuestionID]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
CREATE TABLE [Answers] (
  [AnswerID] INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL
, [AnswerText] text NULL
, [IsCorrect] text NULL
, [QuestionID] bigint NULL
, CONSTRAINT [FK_Answers_0_0] FOREIGN KEY ([QuestionID]) REFERENCES [Questions] ([QuestionID]) ON DELETE NO ACTION ON UPDATE NO ACTION
);
CREATE UNIQUE INDEX [Users_sqlite_autoindex_Users_1] ON [Users] ([UsersID] ASC);
CREATE UNIQUE INDEX [Tests_sqlite_autoindex_Tests_1] ON [Tests] ([TestID] ASC);
CREATE UNIQUE INDEX [Questions_sqlite_autoindex_Questions_1] ON [Questions] ([QuestionID] ASC);
CREATE UNIQUE INDEX [UserAnswers_sqlite_autoindex_UserAnswers_1] ON [UserAnswers] ([UserAnswerID] ASC);
CREATE UNIQUE INDEX [Answers_sqlite_autoindex_Answers_1] ON [Answers] ([AnswerID] ASC);
CREATE TRIGGER [fki_Questions_TestID_Tests_TestID] BEFORE Insert ON [Questions] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Questions violates foreign key constraint FK_Questions_0_0') WHERE NEW.TestID IS NOT NULL AND(SELECT TestID FROM Tests WHERE  TestID = NEW.TestID) IS NULL; END;
CREATE TRIGGER [fku_Questions_TestID_Tests_TestID] BEFORE Update ON [Questions] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Questions violates foreign key constraint FK_Questions_0_0') WHERE NEW.TestID IS NOT NULL AND(SELECT TestID FROM Tests WHERE  TestID = NEW.TestID) IS NULL; END;
CREATE TRIGGER [fki_UserAnswers_ResultID_Results_ResultsID] BEFORE Insert ON [UserAnswers] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table UserAnswers violates foreign key constraint FK_UserAnswers_0_0') WHERE NEW.ResultID IS NOT NULL AND(SELECT ResultsID FROM Results WHERE  ResultsID = NEW.ResultID) IS NULL; END;
CREATE TRIGGER [fku_UserAnswers_ResultID_Results_ResultsID] BEFORE Update ON [UserAnswers] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table UserAnswers violates foreign key constraint FK_UserAnswers_0_0') WHERE NEW.ResultID IS NOT NULL AND(SELECT ResultsID FROM Results WHERE  ResultsID = NEW.ResultID) IS NULL; END;
CREATE TRIGGER [fki_UserAnswers_UserID_Users_UsersID] BEFORE Insert ON [UserAnswers] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table UserAnswers violates foreign key constraint FK_UserAnswers_1_0') WHERE NEW.UserID IS NOT NULL AND(SELECT UsersID FROM Users WHERE  UsersID = NEW.UserID) IS NULL; END;
CREATE TRIGGER [fku_UserAnswers_UserID_Users_UsersID] BEFORE Update ON [UserAnswers] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table UserAnswers violates foreign key constraint FK_UserAnswers_1_0') WHERE NEW.UserID IS NOT NULL AND(SELECT UsersID FROM Users WHERE  UsersID = NEW.UserID) IS NULL; END;
CREATE TRIGGER [fki_UserAnswers_QuestionID_Questions_QuestionID] BEFORE Insert ON [UserAnswers] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table UserAnswers violates foreign key constraint FK_UserAnswers_2_0') WHERE NEW.QuestionID IS NOT NULL AND(SELECT QuestionID FROM Questions WHERE  QuestionID = NEW.QuestionID) IS NULL; END;
CREATE TRIGGER [fku_UserAnswers_QuestionID_Questions_QuestionID] BEFORE Update ON [UserAnswers] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table UserAnswers violates foreign key constraint FK_UserAnswers_2_0') WHERE NEW.QuestionID IS NOT NULL AND(SELECT QuestionID FROM Questions WHERE  QuestionID = NEW.QuestionID) IS NULL; END;
CREATE TRIGGER [fki_Answers_QuestionID_Questions_QuestionID] BEFORE Insert ON [Answers] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Insert on table Answers violates foreign key constraint FK_Answers_0_0') WHERE NEW.QuestionID IS NOT NULL AND(SELECT QuestionID FROM Questions WHERE  QuestionID = NEW.QuestionID) IS NULL; END;
CREATE TRIGGER [fku_Answers_QuestionID_Questions_QuestionID] BEFORE Update ON [Answers] FOR EACH ROW BEGIN SELECT RAISE(ROLLBACK, 'Update on table Answers violates foreign key constraint FK_Answers_0_0') WHERE NEW.QuestionID IS NOT NULL AND(SELECT QuestionID FROM Questions WHERE  QuestionID = NEW.QuestionID) IS NULL; END;
COMMIT;

