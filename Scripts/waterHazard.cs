using UnityEngine;

/* 
 * Author: Raphael Goh Zheng An
 * Date: 2025-06-16
 * Description: Kills player and respawns when player touches hazard.
 */

public class Hazard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerBehaviour player = other.GetComponent<PlayerBehaviour>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
