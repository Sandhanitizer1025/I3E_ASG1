/* 
 * Author: Raphael Goh Zheng An
 * Date: 2025-06-19
 * Description: Door behaviour
 */

using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public Transform doorHinge; // Assign DoorHinge object here
    private bool isOpen = false;

    public void Interact()
    {
        if (!isOpen)
        {
            Debug.Log("Opening door...");
            doorHinge.Rotate(0f, 90f, 0f); // Rotate hinge 90 degrees Y
            isOpen = true;
        }
    }
}