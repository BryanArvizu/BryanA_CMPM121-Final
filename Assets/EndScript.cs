using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    [SerializeField] private GameObject endDoor;
    [SerializeField] private ScreenTintController tint;
    [SerializeField] private GameObject winUI;

    private GameObject player;
    private Vector3 endDoorInitialPos;
    private bool flag = false;
    private float timer = 8f;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        endDoorInitialPos = endDoor.transform.position;
    }

    private void Update()
    {
        if (flag)
        {
            CloseDoor();
            if (timer > 0)
            {
                // print(timer);
                timer -= Time.fixedUnscaledDeltaTime;
                if (timer < 6f && Time.timeScale != 0)
                {
                    if (winUI != null)
                        winUI.SetActive(true);
                    Time.timeScale = 0f;
                    player.GetComponent<PlayerController>().GetWeapon().disable = true;
                }
            }
            else
            {
                timer = 0;
                Time.timeScale = 1.0f;
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            flag = true;
            Fade();
        }
    }

    private void Fade()
    {
        tint.decline = false;
        tint.fadeRate = 0.01f;
        tint.img.color = new Color(0, 0, 0, 0);
    }

    private void CloseDoor()
    {
        if (endDoor.transform.position.y > endDoorInitialPos.y - 0f)
            endDoor.transform.position -= Vector3.up * Time.deltaTime * 4f;
        else
        {
            endDoor.transform.position = endDoorInitialPos - Vector3.up * 3f;
        }
    }
}
