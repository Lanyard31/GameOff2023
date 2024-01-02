using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TransitionsPlus;

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

            TransitionAnimator.Start(
TransitionType.Dissolve, // transition type
duration: 1.6f, // transition duration in seconds
color: HexToColor("32313B")

);

            // Invoke Load the next scene
            Invoke("LoadScene", 1.62f);
        }
    }

    private void LoadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    Color HexToColor(string hex)
    {
        Color color = new Color();
        ColorUtility.TryParseHtmlString("#" + hex, out color);
        return color;
    }
}

