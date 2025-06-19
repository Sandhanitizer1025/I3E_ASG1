/* 
 * Author: Raphael Goh Zheng An
 * Date: 2025-06-19
 * Description: Water/spike hazard that kills player on touch.
 */

using UnityEngine;

/// <summary>
/// Hazard that kills the player on contact (e.g., water or spikes).
/// </summary>
public class Hazard : MonoBehaviour
{
    /// <summary>
    /// Trigger event called when another collider enters this hazard's trigger.
    /// If the player enters, it will call their Die() method.
    /// </summary>
    /// <param name="other">The collider that entered the hazard trigger.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Attempt to get the PlayerBehaviour from the player's parent object
            PlayerBehaviour player = other.GetComponentInParent<PlayerBehaviour>();

            if (player != null)
            {
                player.Die();
            }
            else
            {
                Debug.LogWarning("Hazard touched Player tag, but no PlayerBehaviour found on parent!");
            }
        }
    }
}

