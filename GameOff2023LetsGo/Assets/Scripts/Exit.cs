using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{

    SaveData saveData;


    private void Start()
    {
        saveData = FindObjectOfType<SaveData>();
    }

    // This method is called when another collider enters the trigger
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other collider is the player
        if (other.CompareTag("Player"))
        {
            //save all data
            saveData.SaveScrapCount();
            saveData.SaveTierData();
            saveData.SaveCurrentWeapon();

            // Invoke Load the next scene
            Invoke("LoadScene", 0.2f);
        }
    }

    private void LoadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}

