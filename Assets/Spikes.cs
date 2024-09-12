using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Spikes : MonoBehaviour
{
    public float damage = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerStats"))
        {
            Debug.Log("Player hit by spikes");
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
        }
    }
}
