/* 
 * Author: Raphael Goh Zheng An
 * Date: 2025-06-19
 * Description: UI Manager with dynamic prompt message
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the game's UI, including score display, dynamic interaction prompts,
/// and a congratulations panel when all coins are collected.
/// </summary>
public class UIManager : MonoBehaviour
{
    /// <summary>
    /// Singleton instance of the UIManager.
    /// </summary>
    public static UIManager Instance;
    
    [Header("UI References")]

    /// <summary>
    /// UI text showing the current score.
    /// </summary>
    public TextMeshProUGUI scoreText;

    /// <summary>
    /// UI text showing a dynamic interaction prompt.
    /// </summary>
    public TextMeshProUGUI promptText;

    /// <summary>
    /// Panel displayed when all coins are collected.
    /// </summary>
    public GameObject congratulationsPanel;

    /// <summary>
    /// Text displayed inside the congratulations panel.
    /// </summary>
    public TextMeshProUGUI congratsText;
    
    [Header("Game Settings")]

    /// <summary>
    /// Total number of coins in the scene.
    /// </summary>
    public int totalCoins = 5;

    /// <summary>
    /// Current number of coins collected.
    /// </summary>
    private int coinsCollected = 0;

    /// <summary>
    /// Initialize singleton instance and UI elements.
    /// </summary>
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            Debug.Log("UIManager Instance created successfully");
        }
        else
        {
            Debug.LogWarning("Multiple UIManager instances detected! Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Initializes the UI at the start of the game.
    /// </summary>
    void Start()
    {
        UpdateScoreUI();
        UpdatePromptUI(false);
        
        if (congratulationsPanel != null)
            congratulationsPanel.SetActive(false);
    }

    /// <summary>
    /// Call this method when a coin is collected.
    /// Updates the score UI and checks if all coins are collected.
    /// </summary>
    public void OnCoinCollected()
    {
        coinsCollected++;
        Debug.Log($"Coin collected! Total: {coinsCollected}/{totalCoins}");
        UpdateScoreUI();
        
        // Check if all coins are collected
        if (coinsCollected >= totalCoins)
        {
            ShowCongratulations();
        }
    }

    /// <summary>
    /// Updates the score display in the UI.
    /// </summary>
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            string scoreString = $"Score: {coinsCollected}/{totalCoins}";
            scoreText.text = scoreString;
            Debug.Log($"Score UI updated: {scoreString}");
        }
    }

    /// <summary>
    /// Updates the interaction prompt with an optional dynamic message.
    /// </summary>
    /// <param name="showPrompt">Whether to show or hide the prompt.</param>
    /// <param name="message">The message to display in the prompt (optional).</param>
    public void UpdatePromptUI(bool showPrompt, string message = "")
    {
        if (promptText != null)
        {
            if (showPrompt)
            {
                promptText.text = message;
                promptText.gameObject.SetActive(true);
                Debug.Log("Prompt UI shown: " + message);
            }
            else
            {
                promptText.gameObject.SetActive(false);
                Debug.Log("Prompt UI hidden");
            }
        }
    }

    /// <summary>
    /// Displays the congratulations panel when all coins are collected.
    /// </summary>
    void ShowCongratulations()
    {
        Debug.Log("Showing congratulations!");
        
        if (congratulationsPanel != null)
        {
            congratulationsPanel.SetActive(true);
            
            if (congratsText != null)
            {
                congratsText.text = "Congratulations!\nYou collected all 5 coins!";
            }
            else
            {
                Debug.LogWarning("Congratulations Text is not assigned!");
            }
        }
        else
        {
            Debug.LogWarning("Congratulations Panel is not assigned!");
        }
    }
}



