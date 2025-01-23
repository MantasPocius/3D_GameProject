using UnityEngine;

public class MP28Sounds : MonoBehaviour
{
    public AudioSource audioSource;          // Reference to the AudioSource (on the player character)
    public AudioClip fireSound;              // Sound for the MP28 firing (looping/bursting sound)
    public AudioClip reloadSound;            // Sound for reloading the MP28

    public SMG smgScript;                    // Reference to the SMG script (or MP28 script)

    private bool isFiring = false;           // Is the player holding the fire button (left-click)
    private float fireInterval = 0.075f;     // Interval in seconds between each shot sound (adjusted for realistic SMG fire rate)
    private float fireTimer = 0f;            // Timer to track the interval between shots
    private bool reloadSoundPlayed = false;  // To track if the reload sound has already played

    void Start()
    {
        // Ensure the SMG script's variables are properly initialized
        if (smgScript != null)
        {
            smgScript.currentAmmo = smgScript.maxAmmo;  // Start with a full magazine
            smgScript.isReloading = false;             // Ensure not reloading at the start
        }
    }

    void Update()
    {
        // Check if ammo is more than 0 before allowing fire
        bool canFire = smgScript.currentAmmo > 0;

        // If the player presses left mouse button (left-click) and has ammo, and is not reloading
        if (Input.GetMouseButtonDown(0) && canFire && !smgScript.isReloading)
        {
            if (!isFiring)
            {
                StartFiring();  // Start the firing sound spam
            }
        }

        // If the player releases left mouse button (left-click)
        if (Input.GetMouseButtonUp(0) && isFiring)
        {
            StopFiring();  // Stop the firing sound spam
        }

        // If the player is firing, handle sound at regular intervals
        if (isFiring && !smgScript.isReloading)  // Don't play firing sound when reloading
        {
            // If the player has ammo, continue playing the firing sound
            if (canFire)
            {
                fireTimer += Time.deltaTime;

                if (fireTimer >= fireInterval)  // Check if it's time for the next shot sound
                {
                    PlayFireSound();  // Play the fire sound continuously
                    fireTimer = 0f;    // Reset the timer for the next shot sound
                }
            }
            else
            {
                // Stop firing when ammo is empty
                StopFiring();
            }
        }

        // Check for reload sound when the gun is reloading
        if (smgScript.isReloading && !reloadSoundPlayed)
        {
            PlayReloadSound();  // Play reload sound if reloading
            reloadSoundPlayed = true;  // Mark reload sound as played
        }

        // Check for manual reload input (R key) or auto reload when ammo is empty
        if ((Input.GetKeyDown(KeyCode.R) || smgScript.currentAmmo <= 0) && !smgScript.isReloading)
        {
            smgScript.isReloading = true;  // Start reloading when the player presses R or runs out of ammo
            reloadSoundPlayed = false;     // Reset the reload sound flag when reloading starts
            Invoke(nameof(CompleteReload), 2f);  // Simulate a 2-second reload duration
        }
    }

    // Start firing sound spam
    private void StartFiring()
    {
        isFiring = true;
    }

    // Stop firing sound spam (when releasing left-click or ammo reaches 0)
    private void StopFiring()
    {
        isFiring = false;
    }

    // Play the fire sound once
    private void PlayFireSound()
    {
        if (audioSource != null && fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }

    // Play reload sound
    private void PlayReloadSound()
    {
        if (audioSource != null && reloadSound != null)
        {
            audioSource.PlayOneShot(reloadSound);  // Ensure the reload sound plays when triggered
        }
    }

    // Simulate completing the reload after the reload duration
    private void CompleteReload()
    {
        smgScript.currentAmmo = smgScript.maxAmmo;  // Refill ammo to the maximum
        smgScript.isReloading = false;             // Reset the reloading flag
        reloadSoundPlayed = false;                 // Allow the reload sound to play again
    }
}