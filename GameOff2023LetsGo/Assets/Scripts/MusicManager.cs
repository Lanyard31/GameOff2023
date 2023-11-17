using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;
    [Tooltip("Add all music tracks here")]
    [SerializeField] AudioClip[] music;
    [SerializeField] AudioClip lastTrack;

    bool firstTimeComplete = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        //hitting M toggles the Mute for music
        if (Input.GetKeyDown(KeyCode.M))
        {
            audioSource.mute = !audioSource.mute;
        }

        //hitting N calls the SwitchTrack function
        if (Input.GetKeyDown(KeyCode.N))
        {
            SwitchTrack();
        }

        //if the music is not playing, it switches the track
        if (!audioSource.isPlaying)
        {
            SwitchTrack();
        }
    }

    private void SwitchTrack()
    {
        if (firstTimeComplete)
        {
            // Play a random track from the music array
            FindNewTrack();
            audioSource.Play();
        }
        else
        {
            audioSource.Play();
            firstTimeComplete = true;
        }
    }

    private void FindNewTrack()
    {
        if (lastTrack != null)
        {
            audioSource.clip = music[Random.Range(0, music.Length)];
            //this makes sure the same track doesn't play twice in a row
            while (audioSource.clip == lastTrack)
            {
                audioSource.clip = music[Random.Range(0, music.Length)];
            }
        }
        else
        {
            audioSource.clip = music[Random.Range(0, music.Length)];
        }
        lastTrack = audioSource.clip;
    }
}
