using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class TextReader : MonoBehaviour
{
    [SerializeField] float lengthOfVoiceRecording = 20f;
    [SerializeField] TextMeshProUGUI textDisplay;
    [SerializeField] TextMeshProUGUI textSkip;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] audioClips;

    Color originalColor;
    string text;
    public bool closingScreen = false;

    void Start()
    {
        text = textDisplay.text;
        originalColor = textSkip.color;
        textSkip.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        StartCoroutine(TypeText());
        StartCoroutine(TextSkipper());
    }
    
    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void Update()
    {
        if (closingScreen)
        {
            return;
        }
        //If the player presses the spacebar or hits Start on their Gamepad, load all the text, and then load the next scene
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit") || Input.GetButtonDown("Start"))
        {
            StopAllCoroutines();
            textDisplay.text = text;
            textSkip.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
            textSkip.text = "Loading...";
            Invoke("LoadNextScene", 0.05f);
        }
    }

    AudioClip GetRandomAudioClip()
    {
        // Select a random AudioClip from the array
        int randomIndex = Random.Range(0, audioClips.Length);
        return audioClips[randomIndex];
    }

    IEnumerator TypeText()
    {
        textDisplay.text = "";
        //Calculate the time to wait based on the number of letters and the length of the voice recording
        float timeToWait = lengthOfVoiceRecording / text.Length;
        foreach (char letter in text.ToCharArray())
        {
            textDisplay.text += letter;
            //insert random AudioClip here
            if (letter == '\n')
            {
                yield return new WaitForSeconds(timeToWait * 3);
            }
            else if (letter == '.')
            {
                yield return new WaitForSeconds(timeToWait * 2);
            }
            else if (letter == ' ')
            {
                yield return new WaitForSeconds(timeToWait);
            }
            else
            {
                AudioClip randomClip = GetRandomAudioClip(); // Define a method to get a random AudioClip
                float randomPitch = Random.Range(0.9f, 1.1f); // Adjust pitch within a range
                float randomVolume = Random.Range(0.8f, 1.0f); // Adjust volume within a range

                // Play the AudioClip with varying pitch and volume
                audioSource.PlayOneShot(randomClip, audioSource.volume * randomVolume);
                audioSource.pitch = randomPitch;

                yield return new WaitForSeconds(timeToWait);
            }
        }
        if (closingScreen)
        {
            yield break;
        }
        textSkip.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
        textSkip.text = "Loading...";
        Invoke("LoadNextScene", 0.05f);
    }

    IEnumerator TextSkipper()
    {
        textSkip.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
        //wait briefly before beginning pulse fade
        yield return new WaitForSeconds(2.5f);


        //fade in the alpha
        float t = 0.0f;
        //pulse the alpha
        while (true)
        {
            t = 0.0f;
            while (t < 1.0f)
            {
                t += Time.deltaTime * 0.2f;
                //preserve color data
                textSkip.color = new Color(originalColor.r, originalColor.g, originalColor.b, t);
                yield return null;
            }
            t = 1.0f;
            while (t > 0.0f)
            {
                t -= Time.deltaTime * 0.2f;
                textSkip.color = new Color(originalColor.r, originalColor.g, originalColor.b, t);
                yield return null;
            }
        }
    }
}
