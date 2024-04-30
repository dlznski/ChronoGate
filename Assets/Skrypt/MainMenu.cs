using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
        Teleporter.canTeleport = true;
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void startowamuzyka()
    {
    AudioManager.instance.PlayMusic("light");
    }
}
