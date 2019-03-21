using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    private Text txt;

    private void Start()
    {
        txt = GetComponent<Text>();
    }

    void Update()
    {
        print(txt.color.a);
        if(txt.color.a < 1.0f)
        {
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, txt.color.a + Time.fixedUnscaledDeltaTime);
        }
        else
            txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, 1.0f);
    }
}
