using UnityEngine;
using UnityEngine.SceneManagement;

public class CheatCode : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        // If the N key is pressed
        if (Input.GetKeyDown(KeyCode.N))
        {
            // Load the next scene
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;

            // If the next scene index is greater than the number of scenes, reset it to 0
            if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }

            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}