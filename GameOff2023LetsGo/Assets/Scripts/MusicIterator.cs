using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicIterator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Find the music manager
        GameObject musicManager = GameObject.FindObjectOfType<MusicManager>().gameObject;
        //check null
        if (musicManager != null)
        {
            //call the level completed method
            musicManager.GetComponent<MusicManager>().LevelCompleted();
        }
    }
}
