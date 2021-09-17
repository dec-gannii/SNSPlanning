using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Manager : MonoBehaviour
{
    public static Sound_Manager instance;

    public AudioSource[] soundEffects;

    public AudioSource title_BGM, badEnding_BGM, goodEnding_BGM;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySFX(int soundToPlay)
    {
        soundEffects[soundToPlay].Stop();
        soundEffects[soundToPlay].pitch = Random.Range(0.9f, 1.1f);

        soundEffects[soundToPlay].Play();
    }

    public void Title_BGM_Stop()
    {
        title_BGM.Stop();
    }

    public void Title_BGM_Play()
    {
        title_BGM.Play();
    }

    public void BadEnding_BGM_Stop()
    {
        badEnding_BGM.Stop();
    }

    public void BadEnding_BGM_Play()
    {
        badEnding_BGM.Play();
    }

    public void GoodEnding_BGM_Play()
    { 
        goodEnding_BGM.Play();
    }

    public void GoodEnding_BGM_Stop()
    {
        goodEnding_BGM.Stop();
    }
}
