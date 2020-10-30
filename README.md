# API Starter
**Année de développement :** 2020.</br>


Cette solution propose un modèle de projet architecturé en CQRS avec de l'audit et du monitoring qui permet de mieux comprendre vos utilisateurs et votre business afin d'utiliser ces informations pour le faire évoluer plus efficacement mais aussi de permettre à vos développeurs de trouver et de corriger toutes les erreurs et les latences de vos applications.

Ce projet propose les visualisations suivantes :
- Où se localisent vos utilisateurs ?
- Les fonctionnalités les plus utilisées par vos utilisateurs depuis le début / par mois / par semaine / aujourd’hui.
- Les navigateurs / systèmes d'exploitations / appareils les plus utilisés par vos utilisateurs.
- Évolution du nombre de visiteurs unique / du nombre de requêtes faite sur votre application depuis le début / par mois / par semaine / aujourd'hui.
- Évolution du temps moyen d'une requête de cette année / de ce mois / de la semaine / de la journée.
- Toutes les erreurs côté serveur / côté client triées par date.
- Évolution du nombre de requête en erreur côté serveur / côté client depuis le début / par mois / par semaine / aujourd’hui.
- Les requêtes les plus longues de cette année / de ce mois / de la semaine / de la journée.

Par ailleurs cette solution offre un modèle de lecture entièrement reproductible et sans interruption de services.

Pour réutiliser ce projet il vous faut :
- Choisir les options que votre aggregate root doit supporter, à savoir avec ou sans audit, suppression logique, event sourcing.
- Modifier l'appsettings.json de sorte à définir le type d'audit que vous souhaitez supporter, à savoir toutes les requêtes HTTP réalisées sur votre API, tous les changements appliqués sur votre base de données, l'ensemble des commandes, des requêtes et des évènements lancés par le médiateur.
- Supprimer les répertoires WriteModel.Domain.ExampleToDelete et WriteModel.Infrastructure.ExampleToDelete.
- Modifier le nom, le contenu et la chaîne de connection de votre DbContext. 


![Image of Yaktocat](https://imgur.com/de1nouL.png)
![Image of Yaktocat](https://imgur.com/wbASat4.png)
![Image of Yaktocat](https://imgur.com/cPgua94.png)
![Image of Yaktocat](https://imgur.com/NtU7Zif.png)
![Image of Yaktocat](https://imgur.com/Oba6OOO.png)

![Image of Yaktocat](https://imgur.com/rwzs9Cb.png)
![Image of Yaktocat](https://imgur.com/NiUCwwf.png)

![Image of Yaktocat](https://imgur.com/1PCxn6x.png)
![Image of Yaktocat](https://imgur.com/vHtTAOv.png)
![Image of Yaktocat](https://imgur.com/dP5wgBz.png)
![Image of Yaktocat](https://imgur.com/36EqePL.png)
![Image of Yaktocat](https://imgur.com/DZ9HkoB.png)
![Image of Yaktocat](https://imgur.com/DZ9HkoB.png)
