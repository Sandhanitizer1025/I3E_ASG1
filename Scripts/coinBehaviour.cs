/* 
 * Author: Raphael Goh Zheng An
 * Date: 2025-06-16
 * Description: Allows the player to collect coins.
 */

using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    // Coin value that will be added to the player's score
    [SerializeField]
    int coinValue = 1;

    public void Collect(PlayerBehaviour player)
    {
        // Logic for collecting the coin
        Debug.Log("Coin collected!");
        
        // Add the coin value to the player's score
        // This is done by calling the ModifyScore method on the player object
        // The coinValue is passed as an argument to the method
        // This allows the player to gain points when they collect the coin
        player.ModifyScore(coinValue);

        if (UIManager.Instance != null)
            UIManager.Instance.OnCoinCollected();
        Destroy(gameObject); // Destroy the coin object
    }
}
