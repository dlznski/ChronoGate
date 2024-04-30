using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private Vector3 originalPosition;
    public static bool canTeleport = true; // Flaga określająca, czy postać może się teleportować

    // Czas opóźnienia przed powrotem do oryginalnej pozycji
    public float returnDelay = 4f;

    // Czas opóźnienia przed kolejnym teleportem
    public float teleportDelay = 10f;

    // Referencja do obiektu, który chcesz ujawnić
    public GameObject objectToReveal; 

    // Referencja do obiektu ikony umiejętności
    public GameObject skillIcon;
    public GameObject skillIcon2;

    // Referencja do nowego pustego obiektu
    private GameObject newObject;


    // Aktualizacja jest wywoływana raz na klatkę
    void Update()
    {
        // Sprawdzamy, czy został naciśnięty klawisz "Z" i czy postać może się teleportować
        if (Input.GetKeyDown(KeyCode.Z) 
        && canTeleport 
        && !PlayerTeleporter.isTeleporting)
        {
            AudioManager.instance.PlaySFX("rewind");
            SavePositionAndReturn();

            // Ujawniamy obiekt
            objectToReveal.SetActive(true);

            // Ukrywamy ikonę umiejętności
            skillIcon.SetActive(false);
            skillIcon2.SetActive(false);

            // Uruchamiamy timer dla kolejnego teleportu
            Invoke("EnableTeleport", teleportDelay);
        }
    }

    void SavePositionAndReturn()
{
    // Pobieramy aktualną pozycję gracza
    originalPosition = transform.position;

    // Tworzymy nowy pusty obiekt w miejscu, w którym gracz się teleportował
    newObject = new GameObject("TeleportMarker");
    newObject.transform.position = originalPosition;

    // Ładujemy grafikę "Owlet_Time" jako zasób
    Sprite owletTimeSprite = Resources.Load<Sprite>("Owlet_Time");

    // Dodajemy komponent SpriteRenderer do nowego obiektu i przypisujemy mu grafikę "Owlet_Time"
    SpriteRenderer spriteRenderer = newObject.AddComponent<SpriteRenderer>();
    spriteRenderer.sprite = owletTimeSprite;

    // Ustawiamy Order in Layer na 2
    spriteRenderer.sortingOrder = 1;

    // Ustawiamy flagę, że postać nie może się teleportować
    canTeleport = false;

    // Uruchamiamy timer dla powrotu do oryginalnej pozycji
    Invoke("ReturnToOriginalPosition", returnDelay);
}



    void ReturnToOriginalPosition()
    {
        // Ustawiamy pozycję gracza na jego oryginalną pozycję
        transform.position = originalPosition;

        // Deaktywujemy obiekt
        objectToReveal.SetActive(false);

        // Usuwamy nowy pusty obiekt
        Destroy(newObject);

        
    }


    void EnableTeleport()
    {
        // Ustawiamy flagę umożliwiającą kolejne teleportacje
        AudioManager.instance.PlaySFX("rewindrestore");
        canTeleport = true;
        // Pokazujemy ikonę umiejętności
        skillIcon.SetActive(true);
        skillIcon2.SetActive(true);
    }
}
