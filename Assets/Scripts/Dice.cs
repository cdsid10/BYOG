using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Dice : MonoBehaviour
{
    [SerializeField] private Sprite[] diceSides;

    [SerializeField] private GameObject uITransition;
    [SerializeField] private GameObject beforeUITransition;

    [SerializeField]
    private TextMeshProUGUI uILevelName;
    
    //image component of the dice ui image
    private Image _image;
    
    public int randomDiceSide;
    public bool canPlayerMove = false;
    
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        StartCoroutine(RollDice());
    }

    // Update is called once per frame
    void Update()
    {
        uILevelName.text = randomDiceSide switch
        {
            0 => "tHE dEFAULT wAY TO eSCAPE.",
            1 => "tHE iNVERTED wAY TO eSCAPE.",
            2 => "tHE fASTEST wAY TO eSCAPE.",
            3 => "dARKER iNFESTATIONS.",
            4 => "iNFECTED, SAY wHAT??!",
            5 => "lIMITED oXYGEN sUPPLY!",
            _ => uILevelName.text
        };
    }

    IEnumerator RollDice()
    {
        randomDiceSide = -1;
        yield return new WaitForSeconds(2.8f); //change this back to 2.8f
        beforeUITransition.SetActive(false);
        randomDiceSide = Random.Range(0, 6); 
        //randomDiceSide = 5;
        _image.enabled = true;
        _image.sprite = diceSides[randomDiceSide];
        yield return new WaitForSeconds(4f); //change this back to 3
        uITransition.SetActive(false);
        canPlayerMove = true;
    }
}
