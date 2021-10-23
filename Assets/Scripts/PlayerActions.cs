using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private Dice _dice;

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
        
    }

    public void OxygenMode()
    {
        if (_dice.randomDiceSide == 5)
        {
            currentOxygen -= depleteRate * Time.deltaTime;
            textInfo.text = "OXYGEN REMAINING: ";
            overallText.text = Mathf.RoundToInt(currentOxygen) + "%";
        }

        if (currentOxygen <= 0)
        {
            //Reset with ui first
        }
    }

    public void InfectedMode()
    {
        if (_dice.randomDiceSide == 4)
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
        if (_dice.randomDiceSide == 5)
        {
            if (!other.CompareTag("OxygenPickup")) return;
            currentOxygen += pickupValue;
            if (currentOxygen >= maxOxygen)
            {
                currentOxygen = maxOxygen;
            }
            other.gameObject.SetActive(false);
        }

        if (_dice.randomDiceSide != 4) return;
        if (!other.CompareTag("InfectedGoo")) return;
        timeRemaining = maxTime;
        other.gameObject.SetActive(false);
    }

    IEnumerator HideControls()
    {
        yield return new WaitForSeconds(5f);
        controls.SetActive(false);
        yield break;
    }
}
