using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // Referencja do Transform gracza
    private Vector3 offset; // Przesunięcie względem gracza

    void Start()
    {
        // Ustawiamy przesunięcie na początku gry
        offset = new Vector3(-9.5f, -4f, -0.02229161f);
    }

    void Update()
    {
        // Ustawiamy pozycję obiektu na pozycję gracza plus przesunięcie
        transform.position = playerTransform.position + offset;
    }
}
