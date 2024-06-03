CREATE DATABASE hopital_ajc;
GO

-- Utilisation de la base de données
USE hopital_ajc;
GO

-- Création de la table patients
CREATE TABLE patients (
    id INT PRIMARY KEY IDENTITY(1,1),
    nom NVARCHAR(50) NOT NULL,
    prenom NVARCHAR(50) NOT NULL,
    age INT NOT NULL,
    adresse NVARCHAR(100) NOT NULL,
    telephone NVARCHAR(15) NOT NULL
);
GO

-- Création de la table visites
CREATE TABLE visites (
    id INT PRIMARY KEY IDENTITY(1,1),
    idpatient INT NOT NULL,
    date DATETIME NOT NULL,
    medecin NVARCHAR(50) NOT NULL,
    num_salle INT NOT NULL,
    tarif INT NOT NULL  DEFAULT 23,
    FOREIGN KEY (idpatient) REFERENCES patients(id)
);
GO

-- Création de la table authentification
CREATE TABLE authentification (
    login NVARCHAR(50) PRIMARY KEY NOT NULL,
    password NVARCHAR(50) NOT NULL,
    nom NVARCHAR(50) NOT NULL,
    metier INT NOT NULL
);
GO

-- Insertion de données dans la table authentification
INSERT INTO authentification (login, password, nom, metier)
VALUES
    ('secretaire', 'mdpsecretaire', 'Jeanne Dupont', 0),
    ('medecin1', 'mdpmedecin1', 'Pierre Martin', 1),
    ('medecin2', 'mdpmedecin2', 'Marie Durand', 2),
    ('admin', 'mdpadmin', 'Administrateur', -1);