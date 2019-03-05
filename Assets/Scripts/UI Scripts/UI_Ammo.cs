using UnityEngine;
using UnityEngine.UI;
using System;

public class UI_Ammo : MonoBehaviour
{
    public event Action<float> magEvent;

    private Text text;

    void Awake()
    {
        magEvent = UpdateText;
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
        EventManager.StartListening("MagCount", magEvent);
    }

    private void OnDisable()
    {
        EventManager.StopListening("magCount", magEvent);
    }
}
