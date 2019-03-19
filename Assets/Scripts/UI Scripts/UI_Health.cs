using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;

public class UI_Health : MonoBehaviour
{
    private UnityAction listener;

    private Text text;

    void Awake()
    {
        text = GetComponent<Text>();
    }

    void UpdateText(float x)
    {
        if(text != null)
        {
            text.text = x + " hp";
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening("Health", UpdateText);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Health", UpdateText);
    }
}