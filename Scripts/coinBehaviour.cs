/* 
 * Author: Raphael Goh Zheng An
 * Date: 2025-06-19
 * Description: Allows the player to collect coins and updates UI.
 */

using UnityEngine;

/// <summary>
/// Handles coin collection behavior.  
/// When collected, updates the player's score and UI, plays a sound, and destroys the coin.
/// </summary>
public class CoinBehaviour : MonoBehaviour
{
    /// <summary>
    /// The value of this coin (amount added to player score).
    /// </summary>
    [SerializeField] int coinValue = 1;

    /// <summary>
    /// Sound played when the coin is collected.
    /// </summary>
    [SerializeField] AudioClip coinSound;

    /// <summary>
    /// Collects the coin: plays sound, adds score, updates UI, and destroys the coin object.
    /// </summary>
    /// <param name="player">The player collecting this coin.</param>
    public void Collect(PlayerBehaviour player)
    {
        // Logic for collecting the coin
        Debug.Log("Coin collected!");

        // Play coin collection sound
        if (coinSound != null)
        {
            AudioSource.PlayClipAtPoint(coinSound, transform.position);
        }

        // Modify player's score
        player.ModifyScore(coinValue);

        // Update UI
        if (UIManager.Instance != null)
        {
            UIManager.Instance.OnCoinCollected();

            // Hide prompt after collecting
            UIManager.Instance.UpdatePromptUI(false);
        }

        // Destroy the coin object
        Destroy(gameObject);
    }
}
