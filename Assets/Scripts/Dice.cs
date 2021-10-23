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
            0 => "nORMAL cONDITIONS",
            1 => "Pretty Inverted Conditions",
            2 => "Pretty Fast Conditions",
            3 => "dARKER iNFESTATIONS",
            4 => "iNFECTED, SAY wHAT??!",
            5 => "Oxygen Escape!",
            _ => uILevelName.text
        };
    }

    IEnumerator RollDice()
    {
        randomDiceSide = -1;
        yield return new WaitForSeconds(3f); //change this back to 3
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
