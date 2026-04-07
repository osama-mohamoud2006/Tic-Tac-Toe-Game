# Tic-Tac-Toe Game — Initial Version vs Refactored Version

<img width="1028" height="477" alt="image" src="https://github.com/user-attachments/assets/b8456dc4-aab3-4aa5-8cf2-2dbe1a4753db" />

<img width="1396" height="860" alt="image" src="https://github.com/user-attachments/assets/c8a1d795-3be9-4120-923f-f1062b89a512" />

---

## 1) Project Overview

This repository contains a full **Windows Forms Tic-Tac-Toe game in C# (.NET Framework 4.8.1)** implemented in two complete stages:

1. **Initial Version** (`TheGameForm/` && `initial game folder`)
2. **Refactored Version** (root `Form1.cs` project)

Both versions are **playable** and include **core** game features such as:
- Two-player turn-based gameplay
- Win detection
- Draw detection
- Restart flow
- Custom board rendering

The refactored version focuses on **cleaner** structure and maintainability while preserving full gameplay behavior.

---

## 2) Tech Stack

- **Language:** C#
- **Framework:** .NET Framework 4.8.1
- **UI:** Windows Forms
- **Type:** Desktop GUI game

---

## 3) Repository Layout

### Refactored Version (main/root)
- `Form1.cs`
- `Form1.Designer.cs`
- `Form1.resx`
- `Program.cs`
- `Tic-Tac-Toe Refactored Ver.csproj`

### Full Initial Version
- `TheGameForm/Form1.cs`
- `TheGameForm/Form1.Designer.cs`
- `TheGameForm/Form1.resx`
- `TheGameForm/Program.cs`
- `TheGameForm/TheGameProject.csproj`

### Earlier initial folder copy / assets
- `Tic-Tac-Toe Game initial ver/` (contains historical project files, resources, and assets)

---

## 4) Game Rules (Both Versions)

- 2 local players play on a 3×3 board.
- Players alternate each turn.
- A player wins by placing 3 symbols in:
  - a row,
  - a column,
  - or a diagonal.
- If all 9 positions are filled with no winner, result is **Draw**.
- Restart resets board and state for a new round.

---

## 5) Initial Version (Full Implementation) — Technical Notes

The full initial implementation is represented by `TheGameForm/Form1.cs`.

### Core Characteristics
- Uses **PictureBox-based board** (`pictureBox1..pictureBox9`).
- Turn flow is controlled with:
  - `enCurrentPlayer` enum
  - `currentPlayer` / `prevPlayer`
- Win state is tracked through:
  - `enWinningCondition`
  - `WhoWon`
- Move count uses `Totalcounter` to detect draw at 9 plays.

### Gameplay Pipeline
1. User clicks a picture box (`pictureBox_MouseDown`).
2. Turn is selected in `CurrentPlayerChange`.
3. Move applied in `PictureBoxClick` (image + tag + disabling clicked cell).
4. Winner checked using `CheckWinner` + `GetTheWinner`.
5. If winner exists → `EndGame`.
6. If no winner and 9 moves reached → `MadeDraw`.

### UI/UX Behavior
- Custom board lines drawn in `groupBox1_Paint`.
- Status labels are updated for current player/result.
- Restart is controlled through `btnRestart_Click` and `SetAllPictureBoxesToDefault`.

### Important Implementation Detail
The initial implementation includes image loading with absolute paths (`Image.FromFile(...)`) in gameplay methods.  


---

## 6) Refactored Version (Full Implementation) — Technical Notes

The refactored implementation is in root `Form1.cs` (`Tic_Tac_Toe_Refactored_Ver` namespace).

### Core State Modeling
- `enPlayer` → active player
- `enWinner` → result state
- `stGameStatus` struct:
  - `Winner`
  - `GameOver`
  - `PlayCount`

### Gameplay Pipeline
1. Shared button handler `button_Click`.
2. Main move logic in `ChangeImage(Button btn)`:
   - validates free cell by tag (`"?"`)
   - places X/O image
   - updates turn
   - increments play count
3. Winner logic in `DetermineTheWinnerFromSelectedBtns()`.
4. End flow in `EndGame(enWinner Winner)`.
5. Reset flow in `RestToDefault()` + restart button.

### UI/UX Behavior
- Board drawn in `Form1_Paint` using `Graphics.DrawLine`.
- Buttons show image symbols and color feedback.
- Duplicate cell selection is blocked with an error prompt.

### Portability Improvement
Refactored version uses embedded resources (`Properties.Resources.X`, `Properties.Resources.O`, etc.), making it more portable across systems.

---

## 7) Initial vs Refactored — Real Differences

Both versions are complete and playable.  
The differences are mostly about architecture, control choice, and maintainability.

### A) UI Control Strategy
- **Initial:** PictureBox-driven board.
- **Refactored:** Button-driven board.

### B) State Organization
- **Initial:** Uses multiple player/winner variables with explicit procedural flow.
- **Refactored:** Introduces cleaner enum+struct grouping (`stGameStatus`) for readability and maintenance.

### C) Winner Evaluation Style
- **Initial:** `GetTheWinner` with grouped/branch-based checks.
- **Refactored:** Centralized combination checks via helper method `CheckWinnerOrNot`.

### D) Resource Handling
- **Initial:** Includes absolute path image loading in logic.
- **Refactored:** Uses project resources directly (better portability).

### E) Reset and End-Game Structure
- **Initial:** `DisableControls`, `MadeDraw`, `SetAllPictureBoxesToDefault`.
- **Refactored:** `DisableAllBtns`, `EndGame`, `RestToDefault`.

---

## 8) Why the Refactor Matters (Without Reducing Initial Version)

The refactor does **not** turn an incomplete app into a complete one — both are full.  
It improves:
- code clarity,
- consistency,
- easier future extension,
- safer resource usage across machines.

---

## 9) How to Run

1. Open the solution/project in Visual Studio.
2. Build using **.NET Framework 4.8.1**.
3. Run either:
   - Initial version project (`TheGameForm`),
   - or refactored root project.
4. Play and use Restart for new rounds.

---
