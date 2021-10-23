using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    // Graphics Settings Variables
    [SerializeField] private Toggle fullscreenTog;
    [SerializeField] private Toggle vSyncTog;

    [SerializeField] private Button leftResBtn;
    [SerializeField] private TMP_Text resLabelTxt;
    [SerializeField] private Button rightResBtn;
    [SerializeField] private List<Resolution> resolutions;

    private int selectedRes;

    // Audio Settings Variables
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private Slider masterVolSlider;
    [SerializeField] private Slider musicVolSlider;
    [SerializeField] private Slider sFXVolSlider;

    [SerializeField] private TMP_Text masterVolSliderNumValueText;
    [SerializeField] private TMP_Text musicVolSliderNumValueText;
    [SerializeField] private TMP_Text sFXVolSliderNumValueText;

    // [SerializeField] public AudioSource sFXMusicChecker;

    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
            vSyncTog.isOn = false;
        else
            vSyncTog.isOn = true;
        
        // Search for (previously defined) resolution in list
        bool foundLastSavedRes = false;
        for (int i = 0; i < resolutions.Count; i++)
        {
            if(Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundLastSavedRes = true;

                selectedRes = i;

                UpdateResolutionText();
            }
        }
        // If a different resolution is set which is absent from our resolutions List
        if (!foundLastSavedRes)
        {
            resLabelTxt.text = Screen.width.ToString() + " X " + Screen.height.ToString();

            Resolution newRes = new Resolution();
            newRes.horizontal = Screen.width;
            newRes.vertical = Screen.height;

            resolutions.Add(newRes);
            selectedRes = resolutions.Count - 1;

            UpdateResolutionText();
        }

        // Setting saved Audio Mixer values at the start of the game
        if (PlayerPrefs.HasKey("MasterVol"))
        {
            audioMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            masterVolSlider.value = PlayerPrefs.GetFloat("MasterVol");
        }
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            audioMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            musicVolSlider.value = PlayerPrefs.GetFloat("MusicVol");
        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            audioMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
            sFXVolSlider.value = PlayerPrefs.GetFloat("SFXVol");
        }

        masterVolSliderNumValueText.text = (masterVolSlider.value + 80).ToString();
        musicVolSliderNumValueText.text = (musicVolSlider.value + 80).ToString();
        sFXVolSliderNumValueText.text = (sFXVolSlider.value + 80).ToString();
    }

    void Update()
    {
        if (selectedRes == 0)
            leftResBtn.interactable = false;
        else if (selectedRes == resolutions.Count - 1)
            rightResBtn.interactable = false;
        else
            leftResBtn.interactable = rightResBtn.interactable = true;

        SetMasterVolume();
        SetMusicVolume(); 
        SetSFXVolume();
    }

    // It works When we click on left button of resolution setting
    public void LeftResolution()
    {
        selectedRes--;
        if(selectedRes < 0)
            selectedRes = 0;
        
        UpdateResolutionText();
    }
    // It works When we click on right button of resolution setting
    public void RightResolution()
    {
        selectedRes++;
        if(selectedRes > resolutions.Count - 1)
            selectedRes = resolutions.Count - 1;
        
        UpdateResolutionText();
    }

    public void UpdateResolutionText()
    {
        resLabelTxt.text = resolutions[selectedRes].horizontal.ToString() + " X " + resolutions[selectedRes].vertical.ToString();
    }

    public void ApplyGraphics()
    {
        // Apply VSync
        if (vSyncTog.isOn)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
        
        // Set Resolution (& Also take care of Fullscreen setting at startup)
        Screen.SetResolution(resolutions[selectedRes].horizontal, resolutions[selectedRes].vertical, fullscreenTog.isOn);
    }

    public void SetMasterVolume()
    {
        masterVolSliderNumValueText.text = Mathf.RoundToInt(masterVolSlider.value + 80).ToString();

        audioMixer.SetFloat("MasterVol", masterVolSlider.value);

        PlayerPrefs.SetFloat("MasterVol", masterVolSlider.value);
    }

    public void SetMusicVolume()
    {
        musicVolSliderNumValueText.text = Mathf.RoundToInt(musicVolSlider.value + 80).ToString();

        audioMixer.SetFloat("MusicVol", musicVolSlider.value);

        PlayerPrefs.SetFloat("MusicVol", musicVolSlider.value);
    }

    public void SetSFXVolume()
    {
        sFXVolSliderNumValueText.text = Mathf.RoundToInt(sFXVolSlider.value + 80).ToString();

        audioMixer.SetFloat("SFXVol", sFXVolSlider.value);

        PlayerPrefs.SetFloat("SFXVol", sFXVolSlider.value);
    }

    /*public void PlaySFXLoop()
    {
        sFXMusicChecker.Play();
    }

    public void StopSFXLoop()
    {
        sFXMusicChecker.Stop();
    }*/
}

[System.Serializable]
public class Resolution
{
    public int horizontal;
    public int vertical;
}
