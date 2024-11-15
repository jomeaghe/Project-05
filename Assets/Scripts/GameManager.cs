using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;  // Singleton instance

    // Game variables
    public int totalCoins = 5;  // Total number of coins to collect
    public int coinsCollected = 0;  // Coins collected by the player

    // UI references
    public TextMeshProUGUI coinText;  // Reference to the UI Text component to display the coins collected
    public GameObject winScreen;  // Reference to the Win Screen UI element
    public GameObject gameOverScreen;  // Reference to the Game Over Screen UI element
    public TextMeshProUGUI gameOverText;  // Reference to the Game Over Text UI element (optional)
    public Button restartButton;  // Reference to the restart button (drag and drop from Inspector)

    private bool gameOver = false;  // Flag to check if the game is over

    private void Awake()
    {
        // Singleton pattern to ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate GameManager instances
        }
    }

    private void Start()
    {
        // Ensure that coinText, winScreen, and gameOverScreen are assigned in the Inspector
        if (coinText == null)
        {
            Debug.LogError("Coin Text is not assigned in the Inspector!");
        }
        if (winScreen == null)
        {
            Debug.LogError("Win Screen is not assigned in the Inspector!");
        }
        if (gameOverScreen == null)
        {
            Debug.LogError("Game Over Screen is not assigned in the Inspector!");
        }

        // Initialize the coin display at the start of the game
        if (coinText != null)
        {
            coinText.text = "Coins Collected: " + coinsCollected;
        }

        // Ensure the win and game over screens are inactive at the start
        if (winScreen != null)
        {
            winScreen.SetActive(false);
        }
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(false);
        }

        // Set up the restart button
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);  // Add listener to restart the game on button click
        }
    }

    // Call this method when a coin is collected
    public void AddCoin(int value)
    {
        // Don't allow coin collection if the game is over
        if (gameOver) return;

        // Increment the coin count
        coinsCollected += value;

        // Update the coin display text
        if (coinText != null)
        {
            coinText.text = "Coins Collected: " + coinsCollected;
        }

        // Check if the player has collected all the coins and trigger the win condition
        if (coinsCollected >= totalCoins)
        {
            WinGame();
        }
    }

    // Handle winning the game
    private void WinGame()
    {
        if (winScreen != null)
        {
            // Show the win screen
            winScreen.SetActive(true);
            Time.timeScale = 0f;  // Pause the game
        }
    }

    // Call this method when the player is hit by the enemy (game over)
    public void GameOver()
    {
        if (gameOver) return;  // Prevent calling multiple times

        gameOver = true;  // Set the game over flag

        // Show the game over screen
        if (gameOverScreen != null)
        {
            gameOverScreen.SetActive(true);
        }

        // Optionally, update the text on the game over screen
        if (gameOverText != null)
        {
            gameOverText.text = "You Were Caught! Game Over.";
        }

        Time.timeScale = 0f;  // Pause the game when the game is over
    }

    // Restart the game when the restart button is clicked
    public void RestartGame()
    {
        Time.timeScale = 1f;  // Resume the game time
        gameOver = false;  // Reset game over flag
        coinsCollected = 0;  // Reset coin count

        // Reload the current scene to restart the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Optional: Function to exit to the main menu
    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;  // Ensure game is running when exiting
        SceneManager.LoadScene("MainMenu");  // Replace with your main menu scene name
    }

    // Optional: Function to unpause the game (if you have a pause menu)
    public void UnpauseGame()
    {
        Time.timeScale = 1f;  // Resume the game time
        gameOver = false;  // Reset game over flag
    }
}
