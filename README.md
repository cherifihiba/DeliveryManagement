DeliveryManagement 🚚
DeliveryManagement est une application web de gestion des livraisons réalisée dans le cadre d’un projet académique.

🎯 Fonctionnalités
Gestion des commandes (CRUD)

Gestion des livreurs (CRUD)

Attribution des livreurs aux commandes

Génération de reçus PDF à l’aide de QuestPDF

Interface responsive et moderne avec Bootstrap

Base de données MySQL avec Entity Framework Core

🛠️ Technologies utilisées
ASP.NET Core MVC (C#)

Entity Framework Core

QuestPDF

Bootstrap

MySQL

Visual Studio 2022

⚙️ Structure du projet
Controllers/ — contient la logique des pages (CommandsController, DeliverersController)

Models/ — contient les classes de données (Command, Deliverer, ReceiptViewModel)

Views/ — les interfaces utilisateur Razor (Create, Edit, Delete, Details)

Services/ — génération des reçus PDF

Data/ — configuration EF Core + Migrations


▶️ Lancer le projet
Cloner le dépôt :
git clone https://github.com/TON-UTILISATEUR/DeliveryManagement.git

Ouvrir le projet dans Visual Studio

Appliquer les migrations :
dotnet ef database update

Exécuter le projet :
dotnet run

Accéder à :
http://localhost:PORT/Commands

🧾 Licence
Projet académique réalisé par Cherifi Hiba.
Utilisation de QuestPDF sous licence Community.
