using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PulseTextSize : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float minSize = 30f;
    public float maxSize = 40f;
    public float speed = 1f;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        //pulses the text size up and down
        text.fontSize = Mathf.PingPong(Time.time * speed, maxSize - minSize) + minSize;
    }

    private void OnDisable() {
        //resets the text size to the original size
        text.fontSize = minSize;
    }
}
