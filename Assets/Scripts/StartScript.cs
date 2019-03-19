using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    void Update()
    {
        if(Input.GetButtonUp("Jump"))
        {
            print("Starting");
            SceneManager.LoadScene("Game");
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            print("Exiting");
            Application.Quit();
        }
    }
}
