using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    [Tooltip("Add all music tracks here")]
    [SerializeField] AudioClip[] music;
    private AudioSource audioSource;
    int currentTrackIndex = 0;

    private void Awake()
    {
        ThereCanOnlyBeOne();
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
    }

    private void ThereCanOnlyBeOne()
    {
        if (Instance == null)
        {
            // This is the first instance, make it the Singleton.
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            // This is not the first instance, destroy it.
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        // Hitting M toggles the mute for music
        if (Input.GetKeyDown(KeyCode.M))
        {
            audioSource.mute = !audioSource.mute;
        }
    }

    // Call this function when the player completes the level
    public void LevelCompleted()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutAndSwitchTrack());
    }

    private IEnumerator FadeOutAndSwitchTrack()
    {
        // Fade out the volume
        while (audioSource.volume > 0.01f)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime);
            yield return null;
        }
        //set it equal to 0.01f to avoid any weirdness
        audioSource.volume = 0.001f;

        // Pause for a moment
        yield return new WaitForSeconds(1.85f);

        // Queue up the next track
        PlayNextTrack();

        // The volume should lerp up here, right here
        while (audioSource.volume < 1.0f)
        {
            audioSource.volume = Mathf.Lerp(audioSource.volume, 1.0f, Time.deltaTime);
            yield return null;
        }
        //set it equal to 1.0f to avoid any weirdness
        audioSource.volume = 1.0f;
        yield return null;
    }

    private void PlayNextTrack()
    {
        // Get the index of the next track in the music array
        int nextTrackIndex = currentTrackIndex + 1;

        // If it's the last track, don't play anything next
        if (nextTrackIndex >= music.Length)
        {
            return;
        }

        // Play the next track
        audioSource.clip = music[nextTrackIndex];
        //Debug.Log("Playing track " + nextTrackIndex);
        audioSource.Play();

        // Update the current track index
        currentTrackIndex = nextTrackIndex;
    }
}
