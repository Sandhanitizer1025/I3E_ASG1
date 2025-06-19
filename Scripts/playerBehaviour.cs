/* 
 * Author: Raphael Goh Zheng An
 * Date: 2025-06-19
 * Description: Player behaviour
 */

using UnityEngine;

/// <summary>
/// Handles player behavior including interaction with collectibles and doors,
/// managing score, handling death and respawn.
/// </summary>
public class PlayerBehaviour : MonoBehaviour
{
    /// <summary>
    /// Maximum health of the player.
    /// </summary>
    int maxHealth = 100;

    /// <summary>
    /// Current health of the player.
    /// </summary>
    int currentHealth = 100;

    /// <summary>
    /// Current score of the player.
    /// </summary>
    int currentScore = 0;

    /// <summary>
    /// Whether the player can currently interact with an object.
    /// </summary>
    bool canInteract = false;

    /// <summary>
    /// The current coin the player is able to interact with.
    /// </summary>
    CoinBehaviour currentCoin = null;

    /// <summary>
    /// The current door the player is able to interact with.
    /// </summary>
    DoorBehaviour currentDoor = null;

    /// <summary>
    /// Transform where the player will respawn upon death.
    /// </summary>
    [SerializeField] private Transform respawnPoint;

    /// <summary>
    /// Update is called once per frame to check for interaction input.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract();
        }
    }

    /// <summary>
    /// Called when the player presses the interact key (E).
    /// Handles interactions with coins and doors.
    /// </summary>
    void OnInteract()
    {
        if (canInteract)
        {
            if (currentCoin != null)
            {
                Debug.Log("Interacting with coin");
                currentCoin.Collect(this);
            }
            else if (currentDoor != null)
            {
                Debug.Log("Interacting with door");
                currentDoor.Interact();

                // Hide prompt after door interaction
                if (UIManager.Instance != null)
                    UIManager.Instance.UpdatePromptUI(false);
            }
        }
    }

    /// <summary>
    /// Modifies the player's score by a specified amount.
    /// </summary>
    /// <param name="amt">Amount to add to the current score (can be negative).</param>
    public void ModifyScore(int amt)
    {
        currentScore += amt;
    }

    /// <summary>
    /// Called when the player enters a trigger collider.
    /// Detects interactions with coins and doors.
    /// </summary>
    /// <param name="other">The collider the player entered.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.CompareTag("Collectible"))
        {
            canInteract = true;
            currentCoin = other.GetComponent<CoinBehaviour>();
            currentDoor = null;

            if (UIManager.Instance != null)
                UIManager.Instance.UpdatePromptUI(true, "Press E to collect coin");
        }
        else if (other.CompareTag("Door"))
        {
            canInteract = true;
            currentDoor = other.GetComponent<DoorBehaviour>();
            currentCoin = null;

            if (UIManager.Instance != null)
                UIManager.Instance.UpdatePromptUI(true, "Press E to open door");
        }
    }

    /// <summary>
    /// Called when the player exits a trigger collider.
    /// Updates interaction state.
    /// </summary>
    /// <param name="other">The collider the player exited.</param>
    void OnTriggerExit(Collider other)
    {
        if (currentCoin != null && other.gameObject == currentCoin.gameObject)
        {
            canInteract = false;
            currentCoin = null;

            if (UIManager.Instance != null)
                UIManager.Instance.UpdatePromptUI(false);
        }
        else if (currentDoor != null && other.gameObject == currentDoor.gameObject)
        {
            canInteract = false;
            currentDoor = null;

            if (UIManager.Instance != null)
                UIManager.Instance.UpdatePromptUI(false);
        }
    }

    /// <summary>
    /// Handles player death and respawn.
    /// Moves player to respawn point and resets relevant components.
    /// </summary>
    public void Die()
    {
        Debug.Log("Player Died!");

        if (respawnPoint != null)
        {
            // Disable CharacterController
            CharacterController cc = GetComponent<CharacterController>();
            if (cc != null)
                cc.enabled = false;

            // Move player
            transform.position = respawnPoint.position;
            transform.rotation = respawnPoint.rotation;

            // Reset Rigidbody velocity
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            // Re-enable CharacterController
            if (cc != null)
                cc.enabled = true;

            Debug.Log("Player respawned.");
        }
        else
        {
            Debug.LogWarning("Respawn point not assigned!");
        }
    }
}
