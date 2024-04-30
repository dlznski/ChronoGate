using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Nazwa sceny, do której chcesz przenieść gracza
    public string sceneName;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Sprawdź, czy obiekt, który wszedł w strefę, to gracz
        if (other.gameObject.tag == "Player")
        {
            // Przełącz na nową scenę
            SceneManager.LoadScene(sceneName);
            Teleporter.canTeleport = true;
            PlayerTeleporter.isTeleporting = false;
        }
    }
}
