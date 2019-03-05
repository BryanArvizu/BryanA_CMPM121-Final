using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_AmmoMax : MonoBehaviour
{
    public event Action<float> ammoMaxEvent;

    private Text text;

    void Awake()
    {
        ammoMaxEvent = UpdateText;
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
        EventManager.StartListening("AmmoMax", ammoMaxEvent);
    }

    private void OnDisable()
    {
        EventManager.StopListening("AmmoMax", ammoMaxEvent);
    }
}
