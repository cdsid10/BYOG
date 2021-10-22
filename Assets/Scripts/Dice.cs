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

    [SerializeField]
    private TextMeshProUGUI _textMeshPro;
    
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
        _textMeshPro.text = randomDiceSide switch
        {
            0 => "Pretty Normal Conditions",
            1 => "Pretty Inverted Conditions",
            2 => "Pretty Fast Conditions",
            3 => "Pretty Dark Conditions",
            _ => _textMeshPro.text
        };
    }

    IEnumerator RollDice()
    {
        randomDiceSide = -1;
        randomDiceSide = Random.Range(0, 6);
        _image.sprite = diceSides[randomDiceSide];
        yield return new WaitForSeconds(3f);
        canPlayerMove = true;
        uITransition.SetActive(false);
    }
}
