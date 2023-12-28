using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVolume : MonoBehaviour
{
    public AudioMixer mixer;
    public string audioParameter;
    private float defaultVolume = 0.8f;

    private void Start()
    {
        // Load the saved volume or use the default if not found
        float savedVolume = PlayerPrefs.GetFloat(audioParameter, defaultVolume);
        SetLevel(savedVolume);

        // You can also initialize your UI slider with the saved volume value here
        this.GetComponent<Slider>().value = savedVolume;
    }

    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat(audioParameter, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(audioParameter, sliderValue);
        PlayerPrefs.Save();
    }
}
