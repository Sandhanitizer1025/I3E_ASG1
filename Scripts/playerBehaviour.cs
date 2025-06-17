/* 
 * Author: Raphael Goh Zheng An
 * Date: 2025-06-16
 * Description: Player behaviour
 */

using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Player's maximum health
    int maxHealth = 100;
    // Player's current health
    int currentHealth = 100;
    // Player's current score
    int currentScore = 0;
    // Flag to check if the player can interact with objects
    bool canInteract = false;
    // Stores the current coin object the player has detected
    CoinBehaviour currentCoin = null;

    void Update()
    {
        // Check for E key input
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    // The Interact callback for the Interact Input Action
    // This method is called when the player presses the interact button
    void OnInteract()
    {
        // Check if the player can interact with objects
        if (canInteract)
        {
            // Check if the player has detected a coin or a door
            if (currentCoin != null)
            {
                Debug.Log("Interacting with coin");
                // Call the Collect method on the coin object
                // Pass the player object as an argument
                currentCoin.Collect(this);
            }
        }
    }

    // Method to modify the player's score
    // This method takes an integer amount as a parameter
    // It adds the amount to the player's current score
    // The method is public so it can be accessed from other scripts
    public void ModifyScore(int amt)
    {
        // Increase currentScore by the amount passed as an argument
        currentScore += amt;
    }

    


    // Trigger Callback for when the player enters a trigger collider
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        // Check if the player detects a trigger collider tagged as "Collectible" or "Door"
        if (other.CompareTag("Collectible"))
        {
            // Set the canInteract flag to true
            // Get the CoinBehaviour component from the detected object
            canInteract = true;
            currentCoin = other.GetComponent<CoinBehaviour>();
            if (UIManager.Instance != null)
                UIManager.Instance.UpdatePromptUI(true);
        }
    }

    // Trigger Callback for when the player exits a trigger collider
    void OnTriggerExit(Collider other)
    {
        // Check if the player has a detected coin or door
        if (currentCoin != null)
        {
            // If the object that exited the trigger is the same as the current coin
            if (other.gameObject == currentCoin.gameObject)
            {
                // Set the canInteract flag to false
                // Set the current coin to null
                // This prevents the player from interacting with the coin
                canInteract = false;
                currentCoin = null;
                if (UIManager.Instance != null)
                    UIManager.Instance.UpdatePromptUI(false);
            }
        }
    }
}
