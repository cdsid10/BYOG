using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tutorial : MonoBehaviour
{
    private TextMeshPro tutTextTMP;

    [SerializeField] float vanishTutTextAfterTime = 3.0f;
    [SerializeField] KeyCode keyToModifyText;
    [SerializeField] private string tagName;

    // Start is called before the first frame update
    void Start()
    {
        tutTextTMP = gameObject.transform.GetChild(0).GetComponent<TextMeshPro>();

        gameObject.transform.GetChild(0).gameObject.name = gameObject.name + "_Text";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToModifyText))
        {
            StartCoroutine(VanishTutTextAfterTime());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagName))
        {
            tutTextTMP.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(tagName))
        {
            tutTextTMP.text = "";
            // tutTextTMP.enabled = false;
        }
    }

    IEnumerator VanishTutTextAfterTime()
    {
        yield return new WaitForSeconds(vanishTutTextAfterTime);
        tutTextTMP.text = "";
    }
}
