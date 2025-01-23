using UnityEngine;

public class SMLESounds : MonoBehaviour
{
    public AudioSource audioSource;              // Reference to the AudioSource
    public AudioClip fireSound;                  // Normal firing sound
    public AudioClip chargedFireSound;           // Charged mode firing sound
    public AudioClip chargeActivateSound;        // Sound for activating charged mode
    public AudioClip reloadOpenSound;            // Sound for opening the gun
    public AudioClip reloadBulletSound;          // Sound for inserting a bullet
    public AudioClip reloadFinalBulletSound;     // Sound for inserting the final bullet
    public AudioClip reloadCloseSound;           // Sound for closing the gun
    public AudioClip smallReloadSound;           // Small reload sound after every shot
    public AudioClip concreteHitSound;           // Sound for hitting concrete surfaces
    public AudioClip[] enemyHurtSounds;          // Array for enemy and key enemy hurt sounds

    public Rifle rifleScript;                    // Reference to the Rifle script

    private int previousAmmo = -1;               // Tracks ammo count in the previous frame
    private int bulletsToReload = 0;             // Number of bullets left to reload
    private bool isReloadingSoundPlayed = false;
    private bool isSmallReloadSoundPlayed = false; // Ensures small reload sound plays after each shot
    private bool isChargedMode = false;          // Is the gun in charged mode?

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

        // Toggle charged mode when pressing "Q"
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleChargedMode();
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
                // Check if the hit object is tagged as "Enemy" or "KeyEnemy"
                if (hit.collider.transform.root.CompareTag("Enemy") || hit.collider.transform.root.CompareTag("KeyEnemy"))
                {
                    PlayRandomEnemyHurtSound(); // Play random hurt/death sound
                }
                else if (!hit.collider.transform.root.CompareTag("Player")) // Skip player sounds
                {
                    PlayConcreteHitSound(); // Play default concrete sound
                }
            }
        }
    }

    private void ToggleChargedMode()
    {
        isChargedMode = !isChargedMode;

        if (isChargedMode)
        {
            // Play the charge activation sound when entering charged mode
            if (chargeActivateSound != null)
            {
                audioSource.PlayOneShot(chargeActivateSound);
            }
        }
    }

    private void PlayRandomEnemyHurtSound()
    {
        if (enemyHurtSounds.Length > 0) // Ensure there are sounds in the array
        {
            int randomIndex = Random.Range(0, enemyHurtSounds.Length); // Pick a random sound
            SetRandomPitch();
            audioSource.PlayOneShot(enemyHurtSounds[randomIndex]); // Play the selected sound
        }
    }

    private void PlayFireSound()
    {
        if (isChargedMode && chargedFireSound != null)
        {
            // Play the charged mode firing sound
            SetRandomPitch();
            audioSource.PlayOneShot(chargedFireSound);
        }
        else if (fireSound != null)
        {
            // Play the normal firing sound
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