using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public Logic logic;
    public bool playerISAlive = true;
    public float maxHealth;
    public float health;

    private bool canTakeDamage = true;
    private Animator anim;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<Logic>();
        anim = GetComponentInParent<Animator>();
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (!canTakeDamage)
        {
            return;
        }

        health -= damage;
        anim.SetBool("Damage", true);

        if (health <= 0)
        {
            playerISAlive = false;
            logic.gameOver();
            // Disable player movement and controls
            GetComponentInParent<GatherInput>().DisableControls();

            // Disable player collider to prevent further interactions
            GetComponent<PolygonCollider2D>().enabled = false;

            // Optionally freeze Rigidbody to stop any further movement
            Rigidbody2D rb = GetComponentInParent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
            }

            Debug.Log("Player is dead");
        }
        
        StartCoroutine(DamagePrevention());
    }

    private IEnumerator DamagePrevention()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(0.15f);
        
        if (health > 0)
        {
            canTakeDamage = true;
            anim.SetBool("Damage", false);
        }
        else
        {
            anim.SetBool("Death", true);
        }
    }
}
