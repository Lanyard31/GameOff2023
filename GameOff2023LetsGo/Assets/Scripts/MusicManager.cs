using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private AudioSource audioSource;
    [Tooltip("Add all music tracks here")]
    [SerializeField] AudioClip[] music;
    int currentTrackIndex = 0;

    private void Start()
    {
        // Make sure this is the only one
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);
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
    }

    private void PlayNextTrack()
    {
        // Get and play the very next track in the music array
        int nextTrackIndex = (currentTrackIndex + 1) % music.Length;
        audioSource.clip = music[nextTrackIndex];
        Debug.Log("Playing track " + nextTrackIndex);
        audioSource.Play();
        currentTrackIndex = nextTrackIndex;
    }
}
