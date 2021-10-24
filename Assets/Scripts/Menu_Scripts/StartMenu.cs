using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private int firstLvlBuildIndex = 1;

    [SerializeField] private GameObject optionsMenuPanel;

    // [SerializeField] private GameObject loadingScreenPanel;
    // [SerializeField] private Slider loadingScreenSlider;
    // [SerializeField] private Text loadingScreenProgressText;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsMenuPanel.activeSelf)
            {
                optionsMenuPanel.SetActive(false);
            }
        }
    }

    public void StartGame()
    {
        StartCoroutine(LoadScene(firstLvlBuildIndex));

        BGMusicManager.instance.StopSound("Main_Menu_BG_Music");
        BGMusicManager.instance.PlaySound("In_Game_BG_Music");
    }

    public void OptionsMenu()
    {
        optionsMenuPanel.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        optionsMenuPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUIT!");
    }

    IEnumerator LoadScene(int sceneBuildNum)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneBuildNum);

        //loadingScreenPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            //loadingScreenSlider.value = progress;
            //loadingScreenProgressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
