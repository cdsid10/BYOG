using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject transitionScreen;  
    
    public void PlayGame()
    {
        StartCoroutine(WaitToLoad(1));
    }

    public void BackToMenu()
    {
        StartCoroutine(WaitToLoad(0));
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    private IEnumerator WaitToLoad(int sceneNumber)
    {
        transitionScreen.SetActive(true);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneNumber);
        yield break;
    }
}
