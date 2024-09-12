using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

     private void OnTriggerEnter2D(Collider2D collision)
    { 
        if(collision.CompareTag("Player"))
        {
         collision.GetComponent<PlayCollectibles>().GemCollected();

         GetComponent<AudioSource>().Play();
         GetComponent<SpriteRenderer>().enabled = false;
         GetComponent<Collider2D>().enabled = false; 
        }
    }
}