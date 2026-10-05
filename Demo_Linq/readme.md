# Démo LinQ

## Exercice pour se chauffer
Créer une application qui permettra de stocker un bibliotheque de livre.

### Les informations suivantes doivent être stockage :
- Des livres (Titre, desc, isbn, categorie, auteurs, Editeur, dimension [enum])
- Des categories (Nom, desc)
- Des Auteurs (Nom, Prenom, Date de naissance et mort)
- Les étageres de la biloteque (Code, Nombre d'emplacement)
- Editeur (Nom, desc, Pays, Actif)

### Contrainte pour cette exercice: 
- Il faut symboliser le lien par des clef (Id ←→ Ref)
- Il faut mettre en place : 
  - Les modeles
    - Redéfinir la méthode toString et 
    - Définir un constructeur
    - Redéfinir l'opérateur "==" de la classe "Category"
  - Une classe "DataContext" scellé qui contient une liste pour chaque élément

### Regle metiere (Merci Mathieu)
Les code des étageres doit être de 0-9, mais pas 4 qui est une ancienne catégorie plus utilisée