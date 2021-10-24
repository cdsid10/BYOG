using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("purple"))
        {
            Door.hasPurpleKey = true;
            SFXManager.instance.Play_PickupSFX();
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        
        if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("blue"))
        {
            Door.hasBlueKey = true;
            SFXManager.instance.Play_PickupSFX();

            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
