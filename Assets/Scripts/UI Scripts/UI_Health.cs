using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Health : MonoBehaviour
{
    public event Action<float> healthEvent;

    private Text text;

    void Awake()
    {
        healthEvent = UpdateText;
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
        EventManager.StartListening("Health", healthEvent);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Health", healthEvent);
    }
}