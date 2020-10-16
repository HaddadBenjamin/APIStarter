Cette solution propose un modèle de projet pour réaliser des API architecturées en CQRS qui auditent toutes les requêtes HTTP réalisées sur votre API, tous les changements appliqués sur votre base de données, l'ensemble des commandes, des requêtes et des évènements lancés par le médiateur.


Pour réutiliser ce projet il vous faut :
- Choisir les options que votre aggregate root doit supporter, à savoir avec ou sans audit, suppression logique, event sourcing.
- Modifier l'appsettings.json de sorte à définir le type d'audit que vous souhaitez supporter, à savoir toutes les requêtes HTTP réalisées sur votre API, tous les changements appliqués sur votre base de données, l'ensemble des commandes, des requêtes et des évènements lancés par le médiateur.
- Supprimer les répertoires Learning.AggregateRoot.Domain.ExampleToDelete et Learning.AggregateRoot.Infrastructure.ExampleToDelete.
- Modifier le nom, le contenu et la chaîne de connection de votre DbContext.

![Image of Yaktocat](https://imgur.com/1PCxn6x.png)
![Image of Yaktocat](https://imgur.com/vHtTAOv.png)
![Image of Yaktocat](https://imgur.com/dP5wgBz.png)
![Image of Yaktocat](https://imgur.com/36EqePL.png)
![Image of Yaktocat](https://imgur.com/DZ9HkoB.png)