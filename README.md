# Crate Escape

Crate Escape is a small arcade score-attack game made in Unity.
The project is designed as a portfolio piece with a strong focus on clean architecture,
decoupled systems, and real-world platform integration.

## Game Concept (Mini GDD)

**Genre:** Arcade / Score Attack

**Core Mechanic:**  
The player controls a car that cannot stop — only turn.
While driving, the car constantly drops boxes behind it.
After a short delay, each box transforms into a persistent object
that alters the level layout.

**Gameplay Loop:**
- Drive and maneuver around the arena
- Collect coins to increase score
- Avoid hazards and obstacles
- Survive as long as possible and beat your high score

**Lose Conditions:**
- Running out of lives
- Falling off the arena

**Progression:**
- High score tracking
- Unlockable cars with small gameplay tweaks

**Revive System:**
- One-time revive using in-game currency
- One-time revive via rewarded advertisement

---

## Technical Overview

This project follows a layered architecture and emphasizes separation of concerns.

### Technologies Used
- **Unity**
- **Zenject** — Dependency Injection and lifecycle management
- **UniRx** — Reactive event and state handling
- **DOTween** — Procedural animations
- **WebGL** — Target platform (Yandex Games)

---

## Architecture

The codebase is divided into four main layers:

### Domain
Pure data models and game rules.
No Unity dependencies.

### Application
Core game logic and services:
- Game state management
- Scoring and lives
- Object spawning and transformation
- Revive logic

### Infrastructure
External systems and platform-specific code:
- Ads SDK abstraction
- Save/load system

All SDKs are accessed via interfaces and can be swapped
without modifying game logic.

### Presentation
Unity MonoBehaviours:
- Player control
- Visual representation
- UI
- Animations

---

## Ads Integration

Advertisements are accessed through an abstraction layer of IAdsService

This allows:
- Mock implementation in the Editor
- Platform-specific implementations (e.g. Yandex Games, Unity Ads)

## Project Goals

- Demonstrate clean Unity project architecture
- Show practical use of Zenject, UniRx, and DOTween
- Provide an example of SDK abstraction and WebGL deployment
- Deliver a complete, finished game rather than a prototype

