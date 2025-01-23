using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public AudioSource audioSource;          // Reference to the AudioSource
    public AudioClip[] footstepSounds;       // Array of footstep sounds
    public AudioClip landingSound;           // Landing sound
    public AudioClip trenchesSound;          // Trenches background sound (4 minutes long)
    public AudioClip[] gruntSounds;          // Array of grunt sounds for damage

    public CharacterController characterController; // Reference to CharacterController or similar movement script
    public float footstepInterval = 0.5f;    // Time interval between footstep sounds

    private Health playerHealth;             // Reference to the Health script
    private float previousHealth;            // Tracks previous health to detect changes
    private bool isJumping = false;          // Tracks if the player is jumping
    private float nextFootstepTime = 0f;     // Timer for the next footstep sound

    void Start()
    {
        // Initialize the grounded state to prevent unnecessary jump/landing sounds
        if (characterController != null)
        {
            isJumping = !characterController.isGrounded; // Set jumping state based on initial grounded state
        }

        // Get the Health component
        playerHealth = GetComponent<Health>();

        if (playerHealth != null)
        {
            previousHealth = playerHealth.currentHealth; // Initialize health tracker
        }

        // Play trenches sound effect on loop
        StartTrenchesSound();
    }

    private void StartTrenchesSound()
    {
        if (trenchesSound != null)
        {
            // Create a new AudioSource specifically for the trenches sound
            AudioSource trenchesAudioSource = gameObject.AddComponent<AudioSource>();
            trenchesAudioSource.clip = trenchesSound;
            trenchesAudioSource.loop = true; // Loop the trenches sound
            trenchesAudioSource.playOnAwake = true; // Ensure it starts automatically
            trenchesAudioSource.spatialBlend = 0f; // Make it 2D
            trenchesAudioSource.volume = 0.9f; // Set the volume for the trenches sound

            // Play the trenches sound
            trenchesAudioSource.Play();
        }
    }

    void Update()
    {
        HandleFootsteps();
        HandleLanding();
        HandleDamageGrunt(); // Play grunt sound when the player takes damage
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

    private void HandleLanding()
    {
        if (characterController == null) return;

        // Landing logic only
        if (isJumping && characterController.isGrounded)
        {
            isJumping = false; // Player has landed
            PlayLandingSound();
        }
        else if (!characterController.isGrounded)
        {
            isJumping = true; // Player is in the air
        }
    }

    private void HandleDamageGrunt()
    {
        if (playerHealth == null) return;

        // Check if the player's health has decreased
        if (playerHealth.currentHealth < previousHealth)
        {
            PlayGruntSound(); // Play grunt sound
        }

        // Update the previous health value
        previousHealth = playerHealth.currentHealth;
    }

    private void PlayFootstepSound()
    {
        if (footstepSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepSounds.Length);
            SetRandomPitch();
            audioSource.PlayOneShot(footstepSounds[randomIndex]);
        }
    }

    private void PlayLandingSound()
    {
        if (landingSound != null && audioSource != null)
        {
            audioSource.Stop();
            SetRandomPitch();
            audioSource.PlayOneShot(landingSound);
        }
    }

    private void PlayGruntSound()
    {
        if (gruntSounds.Length > 0 && audioSource != null)
        {
            int randomIndex = Random.Range(0, gruntSounds.Length); // Choose a random grunt sound
            SetRandomPitch();
            audioSource.PlayOneShot(gruntSounds[randomIndex]);
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