using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicIterator : MonoBehaviour
{
    // Awake is called when the script instance is being loaded
    void Start()
    {
        // Find the music manager
        MusicManager musicManager = MusicManager.Instance;

        // Check null
        if (musicManager != null)
        {
            // Call the level completed method
            musicManager.LevelCompleted();
        }
    }
}
