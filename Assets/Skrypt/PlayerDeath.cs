using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    public GameOverScreen gameOverScreen;
    public List<GameManager> gameManagers = new List<GameManager>();
    public int health = 100;
    public List<Collider2D> deathZones = new List<Collider2D>();
    public Movement player;
    Animator animator;

    private void Start()
    {
        GameManager[] foundManagers = FindObjectsOfType<GameManager>();
        gameManagers.AddRange(foundManagers);
        player = FindObjectOfType<Movement>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (deathZones.Contains(collision))
        {
            TakeDamage(100);
        }
    }

    private void TakeDamage(int damage)
    {
        if (gameOverScreen != null)
        {
            health -= damage;

            if (health <= 0)
            {
                animator.SetBool("Death", true);
                Invoke("DeactivateAnimationAndDie", 0.5f);
            }
        }
        else
        {
            Debug.LogError("gameOverScreen is not assigned!");
        }
    }

    private void Die()
    {
        if (gameOverScreen != null)
        {
            // Code to handle character death
            gameOverScreen.Setup();
            player.isDead = true;
        }
        else
        {
            Debug.LogError("gameOverScreen is not assigned!");
        }
    }

    void DeactivateAnimationAndDie()
    {
        animator.SetBool("Death", true);
        Die();
    }
}