# Crate Escape

Crate Escape is a small arcade score-attack game made in Unity.
The project is designed as a portfolio piece with a strong focus on clean architecture,
decoupled systems, and real-world platform integration.

<img width="1445" height="806" alt="Снимок экрана 2026-01-04 195234" src="https://github.com/user-attachments/assets/1ef1dd62-da36-4ed3-b650-41dadc14dd1e" />

# Crate Escape 📦🚗

**A Unity WebGL Endless Runner demonstrating Clean Architecture, Zenject, and UniRx.**

![Unity](https://img.shields.io/badge/Unity-6.3-black?style=flat&logo=unity)
![Zenject](https://img.shields.io/badge/DI-Zenject-blue)
![UniRx](https://img.shields.io/badge/Reactive-UniRx-purple)
![Platform](https://img.shields.io/badge/Platform-WebGL%20%7C%20Mobile-green)

## 📖 Overview

**Crate Escape** is an arcade driving game where the player controls a vehicle that constantly drops crates behind it. These crates solidify into obstacles, turning the gameplay into a chaotic test of memory and reflexes.

This project serves as a technical showcase for **Dependency Injection**, **Reactive Programming**, and **Layered Architecture** within Unity, specifically targeting the Yandex Games platform.

---

## 🎮 Game Design Document (Mini-GDD)

### Core Gameplay
* **Genre:** Endless Runner / Arcade.
* **Goal:** Drive as long as possible, collect coins, and avoid obstacles.
* **Unique Mechanic:** The player's car periodically drops crates. After a short delay, these crates become solid obstacles. Over time, the gaming arena is becoming increasingly complex.
* **Controls:**
    * **Desktop:** Arrow Keys / A-D / Left-Right Gamepad Shoulders (Steering).
    * **Mobile:** Touch input (Left/Right side of screen).

### Progression
* **Economy:** Collect coins to unlock new vehicles.
* **Vehicles:** Different cars have different stats (Speed, Handling, Max Lives).
* **Game Over:** Occurs when lives reach zero. Revive is possible once per run (via coins or Ads).

---

## 🏗 Architecture

The project follows a strict **Layered Architecture** to ensure separation of concerns, testability, and scalability. Dependencies flow inwards or downwards; the Domain never depends on Unity.

### 1. Domain Layer (Core)
* **Responsibility:** Pure C# data models, configurations, and business rules.
* **Dependencies:** None (No Unity, No Plugins).
* **Examples:** `CarConfig`, `GameState` (Enum), `AudioDatabase`.

### 2. Application Layer (Logic)
* **Responsibility:** Game rules, state management, and orchestration.
* **Dependencies:** Domain Layer, UniRx.
* **Examples:**
    * `GameFlowService`: Orchestrates the game loop (Start -> Play -> GameOver).
    * `PlayerSpawnService`: Handles the creation of the player avatar using config data.
    * `ScoreService`: Manages current session score and high scores.

### 3. Infrastructure Layer (External)
* **Responsibility:** Implementation of external systems (Save/Load, Ads, Analytics).
* **Dependencies:** Application Layer Interfaces.
* **Examples:**
    * `YandexAdsService`: Implements `IAdsService`.
    * `PlayerPrefsSaveService`: Implements `ISaveService`.

### 4. Presentation Layer (Unity)
* **Responsibility:** Rendering, Input, Physics, and UI. This layer "listens" to the Application layer via Reactive Properties.
* **Dependencies:** Application Layer, Zenject, DOTween.
* **Examples:**
    * `CarController`: Handles Rigidbody physics based on input.
    * `MainMenuView`: Reacts to game state changes to show/hide UI.
    * `LevelDataProvider`: A bridge component that exposes Scene-specific data (Spawn Points) to the Application layer.

---

## 🛠 Tech Stack & Patterns

### Zenject (Dependency Injection)
* Used for loose coupling between layers.
* **ProjectContext**: Holds global services (Audio, Input, GameState).
* **SceneContext**: Holds level-specific dependencies.
* **Pattern Highlight:** Uses a **Registry/Bridge Pattern** (`ILevelProvider`) to allow global services to access scene-specific data (like Spawn Points) without breaking context hierarchy rules.

### UniRx (Reactive Extensions)
* Used to drive the game state without tight coupling.
* **Example:** `GameStateService` exposes a `ReactiveProperty<GameState>`. The UI observes this property to switch screens. No direct calls like `ui.ShowMenu()` are made from the logic layer.

### DOTween
* Used for procedural animations (UI transitions, Crate drop effects).

---

## 🚀 Installation

1.  Clone the repository.
2.  Open in **Unity 6.3 LTS** (or newer).
3.  Open the scene `Assets/Scenes/GameScene`.
4.  Ensure `ProjectContext` resource is generated (Zenject specific).
5.  Press **Play**.

---

## 📝 Roadmap

* [x] DI Container setup.
* [x] Object Pooling for Crates (Optimization).
* [x] Car Physics & Control.
* [ ] Core Loop (Menu -> Gameplay -> GameOver).
* [ ] Car Shop
* [ ] VFX & Audio.
* [ ] Localization (RU/EN).
