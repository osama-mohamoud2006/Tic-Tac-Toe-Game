# Tic-Tac-Toe Application: 
<img width="1028" height="477" alt="image" src="https://github.com/user-attachments/assets/b8456dc4-aab3-4aa5-8cf2-2dbe1a4753db" />


## 1. Overview
This project is a classic Two-Player Tic-Tac-Toe game built using C# and Windows Forms (.NET Framework 4.8.1). The application relies on a standard 3x3 grid created using nine `PictureBox` controls. The game logic manages turn-taking, win/draw detection, and dynamic UI updates without relying on external game engines.

---

## 2. State Management

The core state of the game is governed by a set of variables and enumerations that track whose turn it is and the current state of the board.

### Enumerations
*   **`enCurrentPlayer`**: Defines the players. Values: `none` (0), `Player1` (1, represents "O"), `Player2` (2, represents "X").
*   **`enWinningCondition`**: Maps the win state. Values: `none` (0), `Player1win` (1), `Player2win` (2).

### Variables
*   **`currentPlayer` & `prevPlayer`**: Used to toggle turns. 
*   **`Totalcounter`**: A `byte` counter that tracks the total number of valid moves played. If this reaches 9 and no winner is found, the game results in a draw.
*   **`WhoWon`**: Stores the winning player to accurately format the end-game UI.

---

## 3. Deep Dive: Core Game Logic

### A. Turn Processing and User Input
The primary entry point for gameplay is the `pictureBox_MouseDown` event, which is wired to all nine grid cells.

1.  **Turn Toggling (`CurrentPlayerChange`)**: 
    When a player clicks a picture box, the system checks `prevPlayer` to determine the next turn. It updates the `currentPlayer` variable, changes the active player label color (Green for Player 1, Red for Player 2), and assigns `prevPlayer = currentPlayer` for the next cycle.
2.  **Move Execution (`PictureBoxClick`)**: 
    - The `Totalcounter` is incremented.
    - Depending on the active player, an "O" or "X" image is loaded into the clicked `PictureBox`.
    - **Crucial Step**: The `PictureBox.Enabled` property is set to `false` to prevent the cell from being clicked again.
    - **Crucial Step**: The `PictureBox.Tag` property is set to the name of the current player enum (e.g., `"Player1"`). This acts as the underlying data model for win detection.

### B. Win Detection Logic
After every move, the system evaluates the board via `CheckWinner` and its underlying method `GetTheWinner`.

*   **`GetTheWinner(enCurrentPlayer CurrentPlayer)`**: 
    The logic utilizes a 1D array referencing the 9 picture boxes: `pictureBox1` to `pictureBox9`. It checks the `.Tag` property of these boxes for string matches against the current player's name.
    
    The checks are grouped into logical clusters:
    *   **Cluster 1 (Starts at index 0)**: Checks the top horizontal row, left vertical column, and the top-left to bottom-right diagonal.
    *   **Cluster 2 (Starts at index 2)**: Checks the right vertical column and top-right to bottom-left diagonal.
    *   **Other Cases**: Explicitly checks the middle horizontal row, bottom horizontal row, and middle vertical column.
    
    If any of these sequences match, it sets `WhoWon = CurrentPlayer` and returns `true`.

### C. Game Resolution (Win or Draw)
*   **Winning**: If `CheckWinner` returns a winning condition, it triggers `EndGame()`. This method disables `groupBox1` (preventing any more interactions), highlights the winning player label, and displays the "Restart" button.
*   **Drawing**: If no winner is found by the time `Totalcounter == 9`, the `MadeDraw()` method is invoked, locking the board and displaying a yellow "It's a Draw!" message.

---

## 4. UI and Dynamic Rendering

### Dynamic Grid Drawing
Instead of using a static background image for the Tic-Tac-Toe grid `#`, the application uses the GDI+ `Graphics` library via the `groupBox1_Paint` event.
*   It calculates the horizontal and vertical gaps by finding the midpoint between adjacent `PictureBox` controls (e.g., between `pictureBox1.Right` and `pictureBox2.Left`).
*   It applies a 10px `margin` padding so the lines extend slightly past the grid.
*   It utilizes `SmoothingMode.AntiAlias` to ensure the white, 5px thick lines are drawn cleanly without pixelation.

---

## 5. Application Reset

When a game concludes, the Restart button (`btnRestart_Click`) allows players to reset the game board. 
1.  A standard `MessageBox` asks for confirmation.
2.  **`SetAllPictureBoxesToDefault()`** is called:
    *   Iterates through the 9 picture boxes.
    *   Clears the `.Tag` property to `""`.
    *   Re-loads the default blank image.
    *   Re-enables (`.Enabled = true`) the controls.
3.  Counters (`Totalcounter`, `Check`) are reset to 0, and UI properties (labels, colors) are restored to their initial states.

---

## 6. Important Architectural Notes & Future Considerations
*   **File Paths**: Currently, image resources are loaded using absolute paths (e.g., `@"E:\projects\C#\...\Pics\oPixel.png"`). This tightly couples the application to a specific local file system. 
*   **Data Structure**: The current win detection uses hardcore string matching on `Tag`. A more robust approach might extract the grid array into a decoupled integer array (`0` for empty, `1` for Player1, `2` for Player 2) within a separate logic class.

# Tic-Tac-Toe (Refactored Ver)
<img width="1396" height="860" alt="image" src="https://github.com/user-attachments/assets/c8a1d795-3be9-4120-923f-f1062b89a512" />

## Overview
This project is a classic two-player Tic-Tac-Toe game built using C# and Windows Forms on .NET Framework 4.8.1. It features a graphical user interface where users can click on a 3x3 grid to place their 'X' or 'O'. The game automatically handles turn-switching, evaluates win/draw conditions, and allows players to restart the game at any time.

## Tech Stack
* **Language:** C# 7.3
* **Framework:** .NET Framework 4.8.1
* **UI Technology:** Windows Forms

## Features
* **Two-Player Mode:** Alternates turns between Player 1 (X) and Player 2 (O).
* **Custom Drawn Board:** Uses the `System.Drawing.Graphics` class to paint the Tic-Tac-Toe grid dynamically on the form.
* **Win/Draw Detection:** Continuously evaluates the board state after every move to check for a winner or a draw (when all 9 slots are filled).
* **Reset Functionality:** A restart button resets the game board, turn counter, and UI labels to their default states.
* **Validation:** Prevents players from overwriting a previously selected cell and shows an error message if attempted.

## Architecture and State Management

### Data Structures
The game relies on custom Enums and Structs to track the game state:
* `enPlayer`: Tracks whose turn it is (`Player1`, `Player2`).
* `enWinner`: Represents the game result (`Player1`, `Player2`, `Draw`, `GameInProgress`).
* `stGameStatus`: A struct holding the current state, including the `Winner`, whether the game is `GameOver`, and the `PlayCount` (used to detect a draw).

### UI Components
* **Play Area:** 9 standard Windows Forms `Button` controls (`button1` through `button9`). 
* **Labels:** Uses `lblTurn` to show whose turn it is, and `lblWinner` to display the game outcome.
* **Canvas:** `Form1_Paint` utilizes `Graphics.DrawLine` to draw the 4 intersecting lines representing the game board physically behind the buttons.

## Core Logic and Methods

### 1. Handling Moves
When a player clicks a grid button, the shared `button_Click` event handler captures the sender and passes it to `ChangeImage(Button btn)`.
* **State Updates:** 
  * The button's `Tag` property is updated from `"?"` to either `"X"` or `"O"`.
  * The button's image (`Properties.Resources.X` or `Properties.Resources.O`) and background color update.
  * The `PlayCount` increments.
* **Validation:** If the button's `Tag` is not `"?"`, a `MessageBox` alerts the user that the button is already chosen.

### 2. Evaluating the Winner
`DetermineTheWinnerFromSelectedBtns()` handles the win-check logic. It checks all 8 possible winning line combinations (3 horizontal, 3 vertical, 2 diagonal) by calling `CheckWinnerOrNot`.
* **`CheckWinnerOrNot`:** Compares the `Tag` values of three provided buttons. If they match and are not `"?"`, it returns `true`.

### 3. Ending the Game
`EndGame(enWinner Winner)` processes the end-of-game scenario:
* Accepts the winning condition.
* Displays a `MessageBox` announcing the winner or a draw.
* Calls `DisableAllBtns()`, iterating through all 9 buttons to set `Enabled = false`.
* Updates `lblTurn` to read "Game Over" and changes the generic labels' colors to indicate the match conclusion.

### 4. Restarting the Game
Pressing the Restart button invokes `btnRestart_Click`, which resets the `PlayCount` and `PlayerTurn`. It relies on `RestToDefault()` to:
* Re-enable all grid buttons.
* Reset button tags to `"?"`, background colors to `Transparent`, and restore the default question mark image.
* Reset status labels to their starting text ("Player 1", "In Progress").

## How to Play
1. Launch the application. Player 1 starts automatically.
2. Player 1 (X) clicks an empty square on the grid.
3. The turn automatically switches to Player 2 (O), who clicks a remaining empty square.
4. The first player to align 3 of their symbols vertically, horizontally, or diagonally wins the match.
5. If all 9 squares are filled without a match, the game prompts a Draw.
6. Click the **Restart** button to clear the board and play again.
