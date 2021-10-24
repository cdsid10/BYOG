using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    
    public static SFXManager instance;
    public static SFXManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private AudioSource sFXAudioSource;
    [SerializeField]
    private AudioSource sFXAudioSource2;

    [SerializeField]
    private AudioClip enemyIdealSFX, enemySneezeSFX, playerDeathSFX, areaChangeSFX, pickupSFX, diceRollSFX;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        //player = GameObject.FindGameObjectWithTag("Player");
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        // if (player.transform.position != null)
        // {
        //     transform.position = player.transform.position;
        // }
        
        // if(FindObjectOfType<PlayerMovement>().enabled == true)
        // {
        //     transform.position = player.transform.position;
        // }
        // else
        // {
        //     transform.position = transform.position;
        // }
        
        // if (player.gameObject.activeInHierarchy)
        // {
        //     transform.position = player.transform.position;
        // }
        // else
        // {
        //     
        //     transform.position = transform.position;
        // }
        
        player = GameObject.FindGameObjectWithTag("Player");
        
        if (player == null)
            return;
        else
            transform.position = player.transform.position;
    }

    public void Play_EnemyIdelSFX()
    {
        AudioSource.PlayClipAtPoint(enemyIdealSFX, transform.position);
    }

    public void Play_EnemySneezeSFX()
    {
        AudioSource.PlayClipAtPoint(enemySneezeSFX, transform.position);
    }

    public void Play_PlayerDeathSFX()
    {
        AudioSource.PlayClipAtPoint(playerDeathSFX, player.transform.position);
    }

    public void Play_AreaChangeSFX()
    {
        AudioSource.PlayClipAtPoint(areaChangeSFX, transform.position);
    }
    
    public void Play_PickupSFX()
    {
        AudioSource.PlayClipAtPoint(pickupSFX, transform.position);
    }
    
    public void Play_DiceRollSFX()
    {
        AudioSource.PlayClipAtPoint(diceRollSFX, transform.position);
    }
}