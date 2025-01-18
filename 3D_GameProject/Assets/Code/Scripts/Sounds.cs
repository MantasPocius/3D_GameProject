using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource audioSource;          // Reference to the AudioSource
    public AudioClip fireSound;              // Firing sound
    public AudioClip reloadOpenSound;        // Sound for opening the gun
    public AudioClip reloadBulletSound;      // Sound for inserting a bullet
    public AudioClip reloadFinalBulletSound; // Sound for inserting the final bullet
    public AudioClip reloadCloseSound;       // Sound for closing the gun
    public AudioClip smallReloadSound;       // Small reload sound after every shot (short reload animation)
    public AudioClip[] footstepSounds;       // Array of footstep sounds

    public Rifle rifleScript;                // Reference to the Rifle script
    public CharacterController characterController; // Reference to CharacterController or similar movement script
    public float footstepInterval = 0.5f;    // Time interval between footstep sounds

    private int previousAmmo = -1;           // Tracks ammo count in the previous frame
    private int bulletsToReload = 0;         // Number of bullets left to reload
    private bool isReloadingSoundPlayed = false;
    private bool isSmallReloadSoundPlayed = false; // Ensures small reload sound plays after each shot
    private bool isJumping = false;         // Tracks if the player is jumping
    private float nextFootstepTime = 0f;    // Timer for the next footstep sound

    void Start()
    {
        // Initialize the grounded state to prevent unnecessary jump/landing sounds
        if (characterController != null)
        {
            isJumping = !characterController.isGrounded; // Set jumping state based on initial grounded state
        }
    }

    void Update()
    {
        if (rifleScript == null || audioSource == null) return;

        // Check if the player has fired
        if (rifleScript.currentAmmo < previousAmmo && rifleScript.currentAmmo >= 0)
        {
            PlayFireSound();
            Invoke(nameof(PlaySmallReloadSound), 0.3f);  // Play a small reload sound after 0.3 seconds delay
        }

        // Trigger reload automatically when reaching 0 ammo
        if (rifleScript.currentAmmo == 0 && !isReloadingSoundPlayed)
        {
            // Play the opening sound immediately, before changing the flag
            if (reloadOpenSound != null)
            {
                audioSource.PlayOneShot(reloadOpenSound);
            }

            // Now, after the sound is triggered, mark the reload as started
            isReloadingSoundPlayed = true;

            bulletsToReload = rifleScript.maxAmmo; // Reload all bullets
            PlayReloadSequence(); // Start the rest of the reload sequence
        }

        // Check for manual reload when pressing "R"
        if (Input.GetKeyDown(KeyCode.R) && rifleScript.currentAmmo < rifleScript.maxAmmo && !isReloadingSoundPlayed)
        {
            bulletsToReload = rifleScript.maxAmmo - rifleScript.currentAmmo; // Calculate how many bullets to reload
            isReloadingSoundPlayed = true;
            PlayReloadSequence(); // Start the reload sequence
        }

        // Reset reload sound when ammo refills to max
        if (rifleScript.currentAmmo == rifleScript.maxAmmo)
        {
            isReloadingSoundPlayed = false; // Allow reload sounds to play again
        }

        // Update the ammo tracker
        previousAmmo = rifleScript.currentAmmo;

        HandleFootsteps();
    }

    private void PlayFireSound()
    {
        if (fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }

    private void PlaySmallReloadSound()
    {
        if (!isSmallReloadSoundPlayed && smallReloadSound != null)
        {
            audioSource.PlayOneShot(smallReloadSound);
            isSmallReloadSoundPlayed = true;
            Invoke(nameof(ResetSmallReloadFlag), smallReloadSound.length); // Reset after the sound finishes
        }
    }

    public void ResetSmallReloadFlag()
    {
        isSmallReloadSoundPlayed = false;
    }

    private void PlayReloadSequence()
    {
        float delay = 0f;

        // Play opening sound
        if (reloadOpenSound != null)
        {
            Invoke(nameof(PlayReloadOpenSound), delay);
            delay += 0.2f;
        }

        // Schedule bullet reload sounds
        for (int i = 0; i < bulletsToReload - 1; i++) // Regular bullets
        {
            Invoke(nameof(PlayReloadBulletSound), delay);
            delay += 0.9f;
        }

        // Play the final bullet sound
        if (reloadFinalBulletSound != null)
        {
            Invoke(nameof(PlayReloadFinalBulletSound), delay);
            delay += 0.9f;
        }

        // Play closing sound
        if (reloadCloseSound != null)
        {
            Invoke(nameof(PlayReloadCloseSound), delay);
            delay += 0.9f;
        }
    }

    private void PlayReloadOpenSound()
    {
        if (audioSource != null && reloadOpenSound != null)
        {
            audioSource.PlayOneShot(reloadOpenSound);
        }
    }

    private void PlayReloadBulletSound()
    {
        if (audioSource != null && reloadBulletSound != null)
        {
            audioSource.PlayOneShot(reloadBulletSound);
        }
    }

    private void PlayReloadFinalBulletSound()
    {
        if (audioSource != null && reloadFinalBulletSound != null)
        {
            audioSource.PlayOneShot(reloadFinalBulletSound);
        }
    }

    private void PlayReloadCloseSound()
    {
        if (audioSource != null && reloadCloseSound != null)
        {
            audioSource.PlayOneShot(reloadCloseSound);
        }
    }

    private void HandleFootsteps()
    {
        if (characterController != null && characterController.isGrounded &&
            (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            if (Time.time >= nextFootstepTime && !audioSource.isPlaying)
            {
                PlayFootstepSound();
                nextFootstepTime = Time.time + footstepInterval;
            }
        }
    }
    private void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepSounds.Length);
            audioSource.PlayOneShot(footstepSounds[randomIndex]);
        }
    }
}