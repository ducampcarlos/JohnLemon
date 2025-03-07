John Lemon's Haunted Jaunt - Custom Enhancements

Overview

This project is an enhanced version of the Unity tutorial John Lemon’s Haunted Jaunt. The tutorial provides a basic stealth game, and additional mechanics have been implemented to extend gameplay and improve AI behavior using Unity's NavMesh system.

Features

🔹 Invisibility Mechanic

The player can turn invisible by holding the Shift key.

Invisibility drains energy, and the player must wait for it to recharge.

A cooldown prevents spamming the invisibility ability.

A particle effect plays when invisibility is activated.

A visual marker (glowing sphere) indicates the player's position while invisible.

👻 AI Improvements (Ghosts)

Ghosts follow waypoints using NavMesh.

When they see the player, they chase instead of following waypoints.

If the player turns invisible, they return to patrolling waypoints.

If a ghost catches the player, the game ends.

Technical Details

Implemented using Unity 6 with URP.

Uses NavMesh for AI pathfinding.

Energy and cooldown mechanics are handled through coroutines.

UI elements (energy bar) are updated dynamically.

How to Play

Use WASD to move John Lemon.

Hold Left Shift to become invisible.

Avoid ghosts by hiding or using invisibility.

If caught, the game ends!

Dependencies

Unity 6 (URP)

Starter Assets (Character Controllers for movement)

NavMesh System (for AI pathfinding)

Future Improvements

Add more AI behaviors.

Implement sound-based detection.

Introduce additional enemy types with different behaviors.

💡 Feedback and suggestions are welcome! If you have ideas for improvements, feel free to contribute! 🚀

