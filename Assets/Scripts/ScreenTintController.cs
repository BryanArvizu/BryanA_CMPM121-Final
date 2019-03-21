using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenTintController : MonoBehaviour
{
    public RawImage img;

    public float fadeRate = 0.1f;
    public bool decline = true;

    private void Start()
    {
        img = GetComponent<RawImage>();
    }

    private void Update()
    {
        if(decline)
        {
            if (img.color.a > 0)
                img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - fadeRate);
            else if (img.color.a < 0)
                img.color = new Color(img.color.r, img.color.g, img.color.b, 0);
        }
        else
        {
            if (img.color.a < 1.0f)
                img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + fadeRate);
            else if (img.color.a > 1.0f)
                img.color = new Color(img.color.r, img.color.g, img.color.b, 1.0f);
        }
    }

    void TintUpdateHealth(float health)
    {
        print(health);
        if (health == 0f)
        {
            print("wow");
            decline = false;
            fadeRate = 0.01f;
            img.color = new Color(0, 0, 0, 0);
        }
        else
        {
            img.color = Color.red;
        }
    }

    private void OnEnable()
    {
        EventManager.StartListening("Health", TintUpdateHealth);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Health", TintUpdateHealth);
    }
}
