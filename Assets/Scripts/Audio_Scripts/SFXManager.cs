using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public static SFXManager Instance
    {
        get { return instance; }
    }

    [SerializeField]
    private AudioSource sFXAudioSource;

    [SerializeField]
    private AudioClip playerWalkSFX, playerDeathSFX;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void Play_PlayerDeathSFX()
    {
        AudioSource.PlayClipAtPoint(playerDeathSFX, transform.position);
    }
}
