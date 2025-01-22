using UnityEngine;

public class SMLE_MK3_Sounds : MonoBehaviour
{
    public AudioSource audioSource;              // Reference to the AudioSource
    public AudioClip fireSound;                  // Firing sound
    public AudioClip reloadOpenSound;            // Sound for opening the gun
    public AudioClip reloadBulletSound;          // Sound for inserting a bullet
    public AudioClip reloadFinalBulletSound;     // Sound for inserting the final bullet
    public AudioClip reloadCloseSound;           // Sound for closing the gun
    public AudioClip smallReloadSound;           // Small reload sound after every shot
    public AudioClip concreteHitSound;           // Sound for hitting concrete surfaces

    public Rifle rifleScript;                    // Reference to the Rifle script

    private int previousAmmo = -1;               // Tracks ammo count in the previous frame
    private int bulletsToReload = 0;             // Number of bullets left to reload
    private bool isReloadingSoundPlayed = false;
    private bool isSmallReloadSoundPlayed = false; // Ensures small reload sound plays after each shot

    void Update()
    {
        if (rifleScript == null || audioSource == null) return;

        // Check if the player has fired
        if (rifleScript.currentAmmo < previousAmmo && rifleScript.currentAmmo >= 0)
        {
            PlayFireSound();
            Invoke(nameof(PlaySmallReloadSound), 0.3f);  // Play a small reload sound after 0.3 seconds delay
        }

        HandleRayHitSounds(); // Check and play hit sounds when shooting

        // Trigger reload automatically when reaching 0 ammo
        if (rifleScript.currentAmmo == 0 && !isReloadingSoundPlayed)
        {
            isReloadingSoundPlayed = true;
            bulletsToReload = rifleScript.maxAmmo; // Reload all bullets
            PlayReloadSequence();
        }

        // Check for manual reload when pressing "R"
        if (Input.GetKeyDown(KeyCode.R) && rifleScript.currentAmmo < rifleScript.maxAmmo && !isReloadingSoundPlayed)
        {
            bulletsToReload = rifleScript.maxAmmo - rifleScript.currentAmmo; // Calculate how many bullets to reload
            isReloadingSoundPlayed = true;
            PlayReloadSequence();
        }

        // Reset reload sound when ammo refills to max
        if (rifleScript.currentAmmo == rifleScript.maxAmmo)
        {
            isReloadingSoundPlayed = false; // Allow reload sounds to play again
        }

        // Update the ammo tracker
        previousAmmo = rifleScript.currentAmmo;
    }

    private void HandleRayHitSounds()
    {
        if (rifleScript.currentAmmo < previousAmmo) // Check if the rifle has just fired
        {
            Ray ray = rifleScript.playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                // Skip playing sounds for the player, enemies, or key enemies.
                if (!hit.collider.transform.root.CompareTag("Player") &&
                    !hit.collider.transform.root.CompareTag("KeyEnemy") &&
                    !hit.collider.transform.root.CompareTag("Enemy"))
                {
                    PlayConcreteHitSound(); // Play a default hit sound for all other surfaces
                }
            }
        }
    }

    private void PlayFireSound()
    {
        if (fireSound != null)
        {
            SetRandomPitch();
            audioSource.PlayOneShot(fireSound);
        }
    }

    private void PlaySmallReloadSound()
    {
        if (!isSmallReloadSoundPlayed && smallReloadSound != null)
        {
            SetRandomPitch();
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

        if (reloadOpenSound != null)
        {
            Invoke(nameof(PlayReloadOpenSound), delay);
            delay += 0.2f;
        }

        for (int i = 0; i < bulletsToReload - 1; i++)
        {
            Invoke(nameof(PlayReloadBulletSound), delay);
            delay += 0.9f;
        }

        if (reloadFinalBulletSound != null)
        {
            Invoke(nameof(PlayReloadFinalBulletSound), delay);
            delay += 1f;
        }

        if (reloadCloseSound != null)
        {
            Invoke(nameof(PlayReloadCloseSound), delay);
        }
    }

    private void PlayReloadOpenSound()
    {
        if (reloadOpenSound != null)
        {
            SetRandomPitch();
            audioSource.PlayOneShot(reloadOpenSound);
        }
    }

    private void PlayReloadBulletSound()
    {
        if (reloadBulletSound != null)
        {
            SetRandomPitch();
            audioSource.PlayOneShot(reloadBulletSound);
        }
    }

    private void PlayReloadFinalBulletSound()
    {
        if (reloadFinalBulletSound != null)
        {
            SetRandomPitch();
            audioSource.PlayOneShot(reloadFinalBulletSound);
        }
    }

    private void PlayReloadCloseSound()
    {
        if (reloadCloseSound != null)
        {
            SetRandomPitch();
            audioSource.PlayOneShot(reloadCloseSound);
        }
    }

    private void PlayConcreteHitSound()
    {
        if (concreteHitSound != null)
        {
            SetRandomPitch();
            audioSource.PlayOneShot(concreteHitSound);
        }
    }

    private void SetRandomPitch()
    {
        if (audioSource != null)
        {
            audioSource.pitch = Random.Range(0.9f, 1.1f); // Random pitch between 0.9 and 1.1
        }
    }
}