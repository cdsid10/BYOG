using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenuPanel;
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private int startMenuBuildNum = 0;

    private bool isGamePaused;

    [SerializeField] private GameObject loadingScreenPanel;
    [SerializeField] private Slider loadingScreenSlider;
    [SerializeField] private Text loadingScreenProgressText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseUnpause();
    }

    public void PauseUnpause()
    {
        if (!isGamePaused)
        {
            pauseMenuPanel.SetActive(true);
            isGamePaused = true;

            Time.timeScale = 0f;
        }
        else
        {
            if (optionsMenuPanel.activeSelf == true)
            {
                optionsMenuPanel.SetActive(false);
            }
            else
            {
                pauseMenuPanel.SetActive(false);
                isGamePaused = false;

                Time.timeScale = 1f;
            }            
        }
    }

    public void OpenOptions()
    {
        optionsMenuPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsMenuPanel.SetActive(false);
    }

    public void StartMenu()
    {
        StartCoroutine(LoadStartMenu(startMenuBuildNum));

        BGMusicManager.instance.StopSound("In_Game_BG_Music");
        BGMusicManager.instance.PlaySound("Main_Menu_BG_Music");
    }

    IEnumerator LoadStartMenu(int sceneBuildNum)
    {
        Time.timeScale = 1f;

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneBuildNum);

        loadingScreenPanel.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingScreenSlider.value = progress;
            loadingScreenProgressText.text = progress * 100f + "%";

            yield return null;
        }
    }
}
