
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/06/2012 20:23:36
-- Generated from EDMX file: C:\Code\GetItDone\GetItDone\DAL\TaskModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GetItDone];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ContextTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_ContextTask];
GO
IF OBJECT_ID(N'[dbo].[FK_ProjectTask]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_ProjectTask];
GO
IF OBJECT_ID(N'[dbo].[FK_TaskAssignment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Tasks] DROP CONSTRAINT [FK_TaskAssignment];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Contexts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contexts];
GO
IF OBJECT_ID(N'[dbo].[People]', 'U') IS NOT NULL
    DROP TABLE [dbo].[People];
GO
IF OBJECT_ID(N'[dbo].[Projects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Projects];
GO
IF OBJECT_ID(N'[dbo].[Tasks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Tasks];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'People'
CREATE TABLE [dbo].[People] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [UserId] uniqueidentifier  NOT NULL,
    [OwnerId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Tasks'
CREATE TABLE [dbo].[Tasks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Notes] nvarchar(max)  NOT NULL,
    [OwnerId] uniqueidentifier  NOT NULL,
    [AssignedToId] int  NULL,
    [ContextId] int  NULL,
    [ProjectId] int  NULL,
    [Finished] bit  NOT NULL,
    [DueDate] datetime  NULL
);
GO

-- Creating table 'Contexts'
CREATE TABLE [dbo].[Contexts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [OwnerId] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Projects'
CREATE TABLE [dbo].[Projects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [OwnerId] uniqueidentifier  NOT NULL,
    [Future] bit  NOT NULL,
    [Purpose] nvarchar(max)  NULL,
    [Principles] nvarchar(max)  NULL,
    [Vision] nvarchar(max)  NULL,
    [Brainstorm] nvarchar(max)  NULL,
    [Organization] nvarchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'People'
ALTER TABLE [dbo].[People]
ADD CONSTRAINT [PK_People]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [PK_Tasks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Contexts'
ALTER TABLE [dbo].[Contexts]
ADD CONSTRAINT [PK_Contexts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Projects'
ALTER TABLE [dbo].[Projects]
ADD CONSTRAINT [PK_Projects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [AssignedToId] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_TaskAssignment]
    FOREIGN KEY ([AssignedToId])
    REFERENCES [dbo].[People]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_TaskAssignment'
CREATE INDEX [IX_FK_TaskAssignment]
ON [dbo].[Tasks]
    ([AssignedToId]);
GO

-- Creating foreign key on [ContextId] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_ContextTask]
    FOREIGN KEY ([ContextId])
    REFERENCES [dbo].[Contexts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ContextTask'
CREATE INDEX [IX_FK_ContextTask]
ON [dbo].[Tasks]
    ([ContextId]);
GO

-- Creating foreign key on [ProjectId] in table 'Tasks'
ALTER TABLE [dbo].[Tasks]
ADD CONSTRAINT [FK_ProjectTask]
    FOREIGN KEY ([ProjectId])
    REFERENCES [dbo].[Projects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjectTask'
CREATE INDEX [IX_FK_ProjectTask]
ON [dbo].[Tasks]
    ([ProjectId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------