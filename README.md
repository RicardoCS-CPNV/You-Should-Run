# YOU SHOULD RUN
 2D Platform Game

Définir le jeu :

- Thème du jeu :
	- Grotte
	- City
	- (Sky Buildings)
	- Desert

- Déplacement du joueur :
	Le joueur peut se déplacer vers la gauche ainsi que vers la droite. Il peut sauter mais aussi monter et descendre des échelles.

- Différents pièges :
	- Inversion des mouvements
	- Sol qui s'effondre

_______________

Objectif du joueur :

	- Le joueur devra atteindre la fin du niveau avec le meilleur temps.
	- Lorsque le joueur est touché par un ennemi, il meurt et recommence le niveau.
	- Lorsque le joueur tombe dans l'eau, le joueur ne meurt pas mais réapparaît vers le CheckPoint le plus proche.
	- Il y a différents items qui permettent au joueur d'obtenir certaines compétences/avantages pendant un laps de temps.
	- Le joueur pourra ramasser des pièces afin de s'acheter des items (peut-être changer de skin).

_______________

Environnement du jeu :

- L'environnement est en trois parties :
	- Le premier plan : Plan au niveau du joueur, différents obstacles..., se déplace à la vitesse du joueur.
	- Le deuxième plan : Détail de l'arrière-plan qui sera plus proche du joueur, se déplace lentement.
	- Le troisième plan : Arrière-plan du jeu, le fond se déplace très lentement.

_______________

Personnage :

- Le personnage aura différentes animations en fonction de ses déplacements.

_______________

Mouvements :

- Lier les animations avec les déplacements.

_______________

Obstacles :

- Le joueur ne peut ni traverser les murs ni les sols, sauf les plateformes, dans ce cas il peut descendre (appuyer sur la touche pour aller vers le bas).
- L'eau fait réapparaître le joueur au début ou au CheckPoint le plus proche déjà touché.

_______________

Ennemis :

- Les ennemis se déplacent horizontalement (peut-être verticalement si ajout d'araignée) et ne lancent pas de projectile.
- Si le joueur entre en collision avec un ennemi, alors il meurt. Sauf si le joueur touche l'ennemi sur sa tête.

_______________

Niveaux et Progression :

- Chaque niveau a son propre thème, cela permet une bonne diversité du jeu.
- Les parties seront sauvegardées ainsi que les pièces et les items.

_______________

Audio :

- Ajout de bruit lorsque le joueur (ou bien l'ennemi) se déplace(ent).
- Lorsque le sol s'effondre.
- Lorsque le joueur meurt ou bien qu'il tue un ennemi.
- Musique d'ambiance afin de rendre le jeu plus immersif.

_______________

Optimisation :

- Gérer les problèmes de collisions
- (Alléger le code afin de fluidifier le Gameplay).


### FIN ###