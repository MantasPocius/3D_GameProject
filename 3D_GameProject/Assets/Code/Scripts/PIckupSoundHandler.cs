using UnityEngine;

public class PickupSoundHandler : MonoBehaviour
{
    public AudioClip pickupSound; // Assign the sound for picking up items
    public ItemPickup itemPickup; // Reference to the ItemPickup script
    public Sounds soundsScript;   // Reference to the Sounds script

    private bool soundPlayed = false; // Flag to ensure the sound only plays once
    void Update()
    {
        // Check if the key has been picked up
        if (itemPickup != null && itemPickup.KeyPickedUp && !soundPlayed)
        {
            if (soundsScript != null && pickupSound != null)
            {
                soundsScript.audioSource.PlayOneShot(pickupSound); // Play the sound
                soundPlayed = true; // Mark the sound as played
            }
        }
    }
}