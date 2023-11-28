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
    Color originalColor;
    bool skipText = false;
    float timeElapsed = 0f;
    string text;

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
        //If the player presses the spacebar or hits Start on their Gamepad, load all the text, and then load the next scene
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Submit"))
        {
            StopAllCoroutines();
            skipText = true;
            textDisplay.text = text;
            textSkip.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);
            textSkip.text = "Loading...";
            Invoke("LoadNextScene", 0.05f);
        }
    }

    IEnumerator TypeText()
    {
        textDisplay.text = "";
        //Calculate the time to wait based on the number of letters and the length of the voice recording
        float timeToWait = lengthOfVoiceRecording / text.Length;
        foreach (char letter in text.ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(timeToWait);
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
