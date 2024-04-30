using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    private GameObject hiddenObject; // Obiekt, który chcemy ukryć/pokazać

    private void Start()
    {
        // Przy starcie gry znajdź obiekt w hierarchii (np. po tagu lub nazwie)
        hiddenObject = GameObject.FindWithTag("HiddenObject");
        // Ukryj obiekt
        hiddenObject.SetActive(true);
    }

    public void ToggleHiddenObject()
    {
        // Przełącz stan obiektu (pokaż/ukryj)
        hiddenObject.SetActive(!hiddenObject.activeSelf);
    }
}