using UnityEngine;

public class WeaponSwapSounds : MonoBehaviour
{
    public AudioSource audioSource;          // Reference to the AudioSource for playing sounds
    public AudioClip mp28SwapSound;         // Sound for swapping to MP28
    public AudioClip smleMk3SwapSound;      // Sound for swapping to SMLE MK3

    private bool isMP28Equipped = false;    // Tracks if the MP28 is equipped
    private bool isSMLEMK3Equipped = false; // Tracks if the SMLE MK3 is equipped

    void Update()
    {
        // Check if the player swaps to MP28 (key 2)
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // If MP28 is not already equipped, play the MP28 swap sound and mark it as equipped
            if (!isMP28Equipped)
            {
                PlaySwapSound(mp28SwapSound);
                isMP28Equipped = true;
                isSMLEMK3Equipped = false; // Ensure SMLE MK3 is marked as not equipped
            }
        }

        // Check if the player swaps to SMLE MK3 (key 1)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // If SMLE MK3 is not already equipped, play the SMLE MK3 swap sound and mark it as equipped
            if (!isSMLEMK3Equipped)
            {
                PlaySwapSound(smleMk3SwapSound);
                isSMLEMK3Equipped = true;
                isMP28Equipped = false; // Ensure MP28 is marked as not equipped
            }
        }
    }

    // Method to play the swap sound
    private void PlaySwapSound(AudioClip sound)
    {
        if (audioSource != null && sound != null)
        {
            audioSource.PlayOneShot(sound);
        }
    }
}