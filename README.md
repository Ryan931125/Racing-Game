# Racing Game 🏎️

Simple 3D kart racing game made in Unity (think mini Mario Kart). We also built a real-world Arduino steering controller (I'll add pics + video later).

## What This Game Lets You Do

* Drive a kart around different tracks.
* Collect shiny coins (they make sounds).
* Pass through glowing checkpoints so the lap counts only if you hit them all.
* Hear engine sounds that change with speed (idle, rev, driving).
* Pick different track styles (oval, mountain, winding, etc.).
* Built with Unity 2022.3.20f1 (LTS) so it’s stable.
* (Soon) Use a homemade Arduino controller instead of a keyboard.

## Why We Made It

We wanted to learn how a racing game works: movement physics, laps, checkpoints, sounds, 3D assets, and hooking up a custom hardware controller. This project shows we can put code, art, audio, and hardware together into one game that actually runs.

## Basic Controls (Keyboard for now)

* W / Up Arrow = Accelerate
* S / Down Arrow = Brake / Reverse
* A / Left Arrow = Turn Left
* D / Right Arrow = Turn Right
* Esc (in Unity editor) = Stop play mode

## How Laps Work

1. You cross the start line.
2. You must drive through every checkpoint ring in order.
3. When you cross the start line again after all checkpoints: Lap +1.
4. Miss a checkpoint? Lap doesn’t count. Go back and hit it.

## Main Pieces We Coded

* `PlayerController.cs` – Moves the kart and handles input.
* `CheckPoint.cs` + `LevelManager.cs` – Keeps track of which checkpoint you hit and counts laps.
* Audio scripts – Swap engine / coin / music sounds.
* Prefabs – Ready-made kart, coins, tracks, hands (for future VR / interaction ideas).

## Project Folder Stuff (Important Parts Only)

* `Assets/` – All the game content (scripts, scenes, prefabs, sounds, models, materials).
* `Packages/` – Which Unity packages we use (version locked so it’s reproducible).
* `ProjectSettings/` – Unity project settings (graphics, input, etc.).
* We ignore the giant auto-generated folders like `Library/` so the repo stays small.

## What We Already Finished ✅

* Core driving + steering.
* Checkpoint + lap tracking logic.
* Coin pickup with sound.
* Engine idle / rev / run sound blending.
* Multiple track prefabs.
* Basic UI-ready structure (can extend later).

## What’s Coming Next 🔧

* Add Arduino steering + throttle mapping notes.
* Photos of the controller build.
* Short gameplay video / GIF.
* Maybe add lap timer + UI scoreboard.
* Maybe power-ups (speed boost?).

## How To Open It Yourself

1. Install Unity Hub.
2. Add the project with Unity version: 2022.3.20f1.
3. Open the scene: `Assets/Scenes/KartScene.unity` or `Assets/CSL-LAB2.unity`.
4. Press Play.

## Quick Troubleshooting

| Problem | Fix |
|---------|-----|
| Pink materials | Install/upgrade Universal Render Pipeline (URP) and let Unity re-import. |
| Missing scripts | Make sure the `Assets/Scripts` folder is there (pull latest). |
| Errors about Input | Unity might recompile; wait a few seconds. |

## Arduino Controller (Placeholder)

Will go here: wiring diagram, parts list, how we map serial data to steering + speed.

## License

Personal / learning project. Ask before reusing big chunks.

---
More media coming soon. (Future me: drop images in a `Media/` folder and update this.)

