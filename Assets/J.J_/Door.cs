using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Door : MonoBehaviour
{
    public static bool hasPurpleKey;
    public static bool hasBlueKey;

    [SerializeField] GameObject key;
    [SerializeField] GameObject key_UIPurple;
    [SerializeField] GameObject key_UIBlue;
    [SerializeField] GameObject keyDialogue_UIPurple;
    [SerializeField] GameObject keyDialogue_UIBlue;
    [SerializeField] GameObject PurpleTrigger;
    [SerializeField] GameObject BlueTrigger;

    private void Start()
    {
        //key.GetComponent<SpriteRenderer>().color = key_UI.gameObject.GetComponent<Image>().color;
    }

    private void Update()
    {
        if (hasPurpleKey)
        {
            key_UIPurple.SetActive(true);
        }
        else
        {
            key_UIPurple.SetActive(false);
        }
        
        if (hasBlueKey)
        {
            key_UIBlue.SetActive(true);
        }
        else
        {
            key_UIBlue.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (hasPurpleKey && gameObject.CompareTag("PurpleDoor"))
            {
                OpenDoorPurple();
                keyDialogue_UIPurple.SetActive(false);
            }

            if (hasBlueKey && gameObject.CompareTag("YellowDoor"))
            {
                OpenDoorBlue();
                keyDialogue_UIBlue.SetActive(false);
            }
        }
    }

    public void OpenDoorPurple()
    {
        SFXManager.instance.Play_AreaChangeSFX();
        hasPurpleKey = false;
        PurpleTrigger.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
    }
    
    public void OpenDoorBlue()
    {
        SFXManager.instance.Play_AreaChangeSFX();
        hasBlueKey = false;
        BlueTrigger.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        
    }
}
