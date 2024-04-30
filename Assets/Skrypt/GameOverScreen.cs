using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public void Setup()
    {
        gameObject.SetActive(true);
    }
    public void RestartButton()
    {
        SceneManager.LoadScene(1);
        Teleporter.canTeleport = true;
        PlayerTeleporter.isTeleporting = false;
    }

    public void RestartButton2()
    {
        SceneManager.LoadScene(2);
        Teleporter.canTeleport = true;
        PlayerTeleporter.isTeleporting = false;
    }

    public void MainMenuButon()
    {
        SceneManager.LoadScene(0);
    }
}
