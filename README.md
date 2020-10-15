Ce projet est un modèle de projet pour réaliser une API avec les options suivantes :
- CQRS : Mediator / AggregateRoot / Session / Repository / Unit Of Work / {Command|Query|Event}[Handler]
- Audit : toutes les requêtes HTTP faite à votre API, toutes les commandes, les queries et les évènements envoyés par le médiateur, tous les changements effectuées sur vos bases de données.


Pour réutiliser ce projet il vous faut :
- Choisir les options que votre aggregate root doit supporter, à savoir avec ou sans audit, suppression logique, event sourcing.
- Modifier l'appsettings.json de sorte à définir le type d'audit que vous souhaitez supporter.
- Supprimer les répertoires Learning.AggregateRoot.Domain.ExampleToDelete et Learning.AggregateRoot.Infrastructure.ExampleToDelete.
- Modifier le nom, le contenu et la chaîne de connection de votre DbContext.