using System.Collections;
using UnityEngine;

public class PlayerTeleporter : MonoBehaviour
{
    public float teleportDelay = 15f;
    private Vector3 teleportOffset = new Vector3(0, -100, 0);
    public static bool isTeleporting = false;
    public GameObject skillIcon2;
    public GameObject skillIcon;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) 
        && !isTeleporting 
        && Teleporter.canTeleport)
        {
            StartCoroutine(TeleportPlayer());
            skillIcon2.SetActive(false);
            skillIcon.SetActive(false);
        }
    }

    IEnumerator TeleportPlayer()
    {
        
        AudioManager.instance.PlayMusic("dark");
        isTeleporting = true;

        // Teleportacja o 100 jednostek w dół
        transform.position += teleportOffset;

        // Czekamy 15 sekund
        yield return new WaitForSeconds(teleportDelay);

        // Teleportacja o 100 jednostek w górę
        transform.position -= teleportOffset;
        AudioManager.instance.PlayMusic("light");
        yield return new WaitForSeconds(10f);
        isTeleporting = false;
        skillIcon2.SetActive(true);
        skillIcon.SetActive(true);
    }
}
