using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScreen : MonoBehaviour
{
    [SerializeField] int menuBuildIndex = 0;

    public void Menu()
    {
        SceneManager.LoadScene(menuBuildIndex);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
