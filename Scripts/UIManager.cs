/* 
 * Author: Raphael Goh Zheng An
 * Date: 2025-06-16
 * Description: Player behaviour
 */

// This will handle the score display and congratulations message
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    [Header("UI References")]
    public Text scoreText; // UI text showing current score
    public Text promptText; // UI text showing "Press E to collect"
    public GameObject congratulationsPanel; // Panel to show when all coins collected
    public Text congratulationsText; // Text inside the congratulations panel
    
    [Header("Game Settings")]
    public int totalCoins = 5; // Total number of coins in the scene
    private int coinsCollected = 0;
    
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        UpdateScoreUI();
        UpdatePromptUI(false);
        
        if (congratulationsPanel != null)
            congratulationsPanel.SetActive(false);
    }
    
    // Call this method when a coin is collected
    public void OnCoinCollected()
    {
        coinsCollected++;
        UpdateScoreUI();
        
        // Check if all coins are collected
        if (coinsCollected >= totalCoins)
        {
            ShowCongratulations();
        }
    }
    
    // Update the score display
    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"Score: {coinsCollected}/{totalCoins}";
        }
    }
    
    // Update the interaction prompt
    public void UpdatePromptUI(bool showPrompt)
    {
        if (promptText != null)
        {
            if (showPrompt)
            {
                promptText.text = "Press E to collect coin";
                promptText.gameObject.SetActive(true);
            }
            else
            {
                promptText.gameObject.SetActive(false);
            }
        }
    }
    
    // Show congratulations when all coins are collected
    void ShowCongratulations()
    {
        if (congratulationsPanel != null)
        {
            congratulationsPanel.SetActive(true);
            
            if (congratulationsText != null)
            {
                congratulationsText.text = "Congratulations!\nYou collected all 5 coins!";
            }
        }
    }
}

// Modified PlayerBehaviour.cs - Add these modifications to your existing script:
/*
Add this to your OnTriggerEnter method after setting currentCoin:
if (UIManager.Instance != null)
    UIManager.Instance.UpdatePromptUI(true);

Add this to your OnTriggerExit method after setting currentCoin to null:
if (UIManager.Instance != null)
    UIManager.Instance.UpdatePromptUI(false);
*/

// Modified CoinBehaviour.cs - Add this modification to your existing script:
/*
Add this line to your Collect method before destroying the game object:
if (UIManager.Instance != null)
    UIManager.Instance.OnCoinCollected();
*/
