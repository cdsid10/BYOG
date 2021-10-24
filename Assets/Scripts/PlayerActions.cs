using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActions : MonoBehaviour
{
    private Dice _dice;

    [SerializeField] private Animator _animator;

    [SerializeField] private TextMeshProUGUI textInfo;
    [SerializeField] private TextMeshProUGUI overallText;
    
    [Header("Oxygen")]
    [SerializeField] private float maxOxygen;
    [SerializeField] private float depleteRate;
    [SerializeField] private float currentOxygen;
    [SerializeField] private float pickupValue;
    
    [Header("Infected")] 
    [SerializeField] private float timeRemaining;
    [SerializeField] private float maxTime;

    [SerializeField] private GameObject controls;
    [SerializeField] private GameObject dialoguePurpleText;
    [SerializeField] private GameObject dialogueYellowText;
    [SerializeField] private GameObject blackScreen;
    [SerializeField] private GameObject lightsMap;


    // Start is called before the first frame update
    void Start()
    {
        _dice = FindObjectOfType<Dice>();
        currentOxygen = maxOxygen;
        timeRemaining = maxTime;
        if (_dice.canPlayerMove)
        {
            StartCoroutine(HideControls());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_dice.randomDiceSide == 3)
        {
            lightsMap.SetActive(false);
        }
    }

    public void OxygenMode()
    {
        if (_dice.randomDiceSide == 4)
        {
            currentOxygen -= depleteRate * Time.deltaTime;
            textInfo.text = "OXYGEN REMAINING: ";
            overallText.text = Mathf.RoundToInt(currentOxygen) + "%";
        }

        if (currentOxygen <= 0)
        {
            Reset();
        }
    }

    public void InfectedMode()
    {
        if (_dice.randomDiceSide == 5)
        {
            timeRemaining -= Time.deltaTime;
            textInfo.text = "DECONTAMINATION IN: ";
            overallText.text = timeRemaining.ToString("f0") + " s";
            
            if (timeRemaining <= 0)
            {
                //ui show that time out and then restart
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PurpleDoor"))
        {
            dialoguePurpleText.SetActive(true);
        }
            
        if (other.CompareTag("YellowDoor"))
        {
            dialogueYellowText.SetActive(true);
        }
        
        if (_dice.randomDiceSide == 4)
        {
            if (!other.CompareTag("OxygenPickup")) return;
            currentOxygen += pickupValue;
            if (currentOxygen >= maxOxygen)
            {
                currentOxygen = maxOxygen;
            }
            other.gameObject.SetActive(false);
        }

        if (_dice.randomDiceSide == 5)
        {
            // if (other.CompareTag("PurpleDoor"))
            // {
            //     dialoguePurpleText.SetActive(true);
            // }
            //
            // if (other.CompareTag("YellowDoor"))
            // {
            //     dialogueYellowText.SetActive(true);
            // }
            
            if (!other.CompareTag("InfectedGoo")) return;
            timeRemaining = maxTime;
            other.gameObject.SetActive(false);
        }

        if (_dice.randomDiceSide > -1 & _dice.randomDiceSide < 6)
        {
            // if (other.CompareTag("PurpleDoor"))
            // {
            //     dialoguePurpleText.SetActive(true);
            // }
            //
            // if (other.CompareTag("YellowDoor"))
            // {
            //     dialogueYellowText.SetActive(true);
            // }
        }
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PurpleDoor"))
        {
            dialoguePurpleText.SetActive(false);
        }
        
        if (other.CompareTag("YellowDoor"))
        {
            dialogueYellowText.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("infected"))
        {
            Reset();
        }
    }

    IEnumerator HideControls()
    {
        yield return new WaitForSeconds(5f);
        controls.SetActive(false);
        yield break;
    }

    public void Reset()
    {
        StartCoroutine(ResetLoop());
    }

    private IEnumerator ResetLoop()
    {
        SFXManager.instance.Play_PlayerDeathSFX();
        _animator.SetTrigger("Death");
        yield return new WaitForSeconds(1f);
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        yield break;
    }

    public void EndGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
