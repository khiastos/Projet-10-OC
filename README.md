# Projet 10 : Développez une solution en microservices pour votre client

Ce dernier projet de ma formation avait pour objectif de mettre en pratique l’ensemble des notions abordées au cours des derniers mois, tout en intégrant de nouvelles technologies.
L’application développée vise à aider des médecins à détecter le diabète de type 2, en leur permettant de gérer les informations patients ainsi que des notes médicales associées.
---
## Outils et technologies utilisés

- **Visual Studio 2022**
- **Visual Studio Code**
- **C# / ASP.NET Core**
- **Angular**
- **Entity Framework Core**
- **SQL Server**
- **MongoDB**
- **Postman**
- **Docker**
- **Ocelot Gateway**
- **Identity**
- **JWT**

---
## Création d'une API REST en microservices

Le projet est structuré autour de 5 microservices, chacun exposant une API REST avec des responsabilités bien définies.
Chaque microservice est organisé selon une architecture claire, comprenant :
- des services (via interfaces),
- des contrôleurs pour la gestion du CRUD,
- des modèles (POCO et DTO),
- des clients (HTTP Clients) et utilitaires (JWT, IdentitySeeder) lorsque nécessaire.

Cette séparation des responsabilités s’inscrit dans une démarche visant à garantir une application propre, maintenable et scalable.

---
## Base de données SQL (SQL Server)

Le microservice PatientsService utilise une base de données SQL Server afin de stocker et gérer les informations personnelles des patients (création, consultation, modification et suppression).

L’authentification et la gestion des comptes utilisateurs reposent sur ASP.NET Identity, avec un stockage des données également assuré via SQL Server.

L’ensemble des routes a été testé à l’aide de Postman avant l’intégration du front-end.

---
## Base de données NoSQL (MongoDb)

Le microservice NotesService repose sur une base de données NoSQL MongoDB, adaptée au stockage de données non structurées.

Il permet la gestion complète (CRUD) de notes médicales associées à des patients existants, tout en restant découplé du microservice de gestion des patients.

---
## Sécurité et gestion des accès

Vu que ce projet est consistué de plusieurs microservices, les API doivent être exposées de manière contrôlée.

L’authentification des utilisateurs repose sur ASP.NET Identity, permettant la gestion des comptes (création, connexion, stockage sécurisé des mots de passe via hachage).
Une fois authentifié, l’utilisateur reçoit un JSON Web Token (JWT), utilisé pour sécuriser les échanges entre le client et les différents microservices.

Les principaux mécanismes mis en place sont :
- Authentification par JWT, garantissant l’accès uniquement aux utilisateurs connectés
- Autorisation basée sur les rôles, afin de restreindre l’accès aux routes uniquement aux administrateurs
- Protection des endpoints sensibles via des attributs d’autorisation

---
## Utilisation de docker

// à remplir

---
## Utilisation d'Ocelot Gateway

Dans un contexte microservices, l’utilisation d’un API Gateway s’est révélée nécessaire afin de centraliser les accès via une URL unique et de simplifier la communication avec le front-end.

Le choix s’est porté sur Ocelot Gateway, pour sa simplicité de configuration et son intégration efficace avec ASP.NET Core.

---
## Création du front avec Angular

Ma formation étant principalement orientée back-end, l’utilisation de MVC était initialement recommandée.
Cependant, ayant déjà réalisé un projet front avec MVC, j’ai choisi de découvrir Angular, une technologie largement utilisée sur le marché du travail et souvent associée à des projets en C#.

Le front-end, développé avec Angular, reste volontairement simple mais fonctionnel, conformément aux attentes du projet.

Ce projet constituait mon premier contact avec Angular, ce qui m’a amenée à consacrer du temps à la compréhension de son fonctionnement, de ses conventions et de son architecture.
J’ai structuré l’application selon un modèle feature-based, avec une séparation claire des fonctionnalités, des services front correspondant aux contrôleurs back-end, et une organisation cohérente des routes et des pages.

---
## Installation  

1. `git clone https://github.com/khiastos/Projet-10-OC.git`  
2. Ouvrir la solution dans Visual Studio  
