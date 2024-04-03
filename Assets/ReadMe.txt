Scripting 1 Todo

Multiple Bullet types
Interacatble script for bullet collisions/triggers
====Use Tags and types to naviage individual interactions



GameInput::     Need to make menu and additional keybindings
		---GUI/Menu Overlay
		=Freeze Time scale or Don't destroy on Load?

Guns::  @IMPORTANT! The Bullets NEEEEED to inherit the Ship's velocity so that the ship doesn't catch them and so that they don't bounce off the side of the ship.
		Cap number of bullets that can exist in a scene.
		Make an animation for the guns (Squash and stretch)
		Make Particle System for them as well
		Pickle Bullet should have more mass to it so it can plow through small things like broken Asteroids
		Fix Audio Spawn problem


Asteroids:: They need some form of natural movement to make them hazardous. What's the way? Reasearch this.
				==Spawner?
			Swap asteroid particles for instantiated mesh with rb and disperse using explosion force that only applies to the _RB's it impacts? Better col?

POWERUPS: Forcefield(Onion Rings?)
			For Speed Powerup. Switch Particles from in Obj to instantiated



ADD:
ENEMIES :: Maybe?
Cam :: Make a Camera switch for death?
Inventory Script: Need to add this
					Tie Speed Collectibles to Inventory and then use a key binding to initate them
WinVolume/Objective: Need to add this



DEBUG:
	Fix the problem with AudioPlayers not despawning in the AudioHelper() script
	EDITOR GUI for Collision Event Handler. I DO NOT want to have the GUI full of like 80 Different input boxes and the script to be 90% IFs
			-----HazardVolume() is current biggest offender
	REWORK HAZARD VOLUME, with that being said	