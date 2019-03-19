using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_AmmoMax : MonoBehaviour
{
    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void UpdateText(float x)
    {
        if (text != null)
        {
            text.text = "/ " + x;
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening("AmmoMax", UpdateText);
    }

    private void OnDisable()
    {
        EventManager.StopListening("AmmoMax", UpdateText);
    }
}
