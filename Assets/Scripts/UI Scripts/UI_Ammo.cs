using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Ammo : MonoBehaviour
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
            text.text = x + "";
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening("MagCount", UpdateText);
    }

    private void OnDisable()
    {
        EventManager.StopListening("magCount", UpdateText);
    }
}
