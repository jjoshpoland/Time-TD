using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeMixer : MonoBehaviour
{
    public AudioMixer mixer;

    private void Awake()
    {
        ResetSFXLowPass();
    }

    public void SetSFXVolume(float level)
    {
        if(mixer == null)
        {
            Debug.LogWarning("No audio mixer assigned");
            return;
        }

        mixer.SetFloat("SFXVolume", level);
    }
    public void SetMusicVolume(float level)
    {
        if(mixer == null)
        {
            Debug.LogWarning("No audio mixer assigned");
            return;
        }

        mixer.SetFloat("MusicVolume", level);
    }
    public void SetMasterVolume(float level)
    {
        if(mixer == null)
        {
            Debug.LogWarning("No audio mixer assigned");
            return;
        }

        mixer.SetFloat("MasterVolume", level);
    }

    public void ResetSFXLowPass( )
    {
        if (mixer == null)
        {
            Debug.LogWarning("No audio mixer assigned");
            return;
        }

        mixer.SetFloat("SFXLowPass", 10000f);
    }

    public void ImposeSFXLowPass()
    {
        if (mixer == null)
        {
            Debug.LogWarning("No audio mixer assigned");
            return;
        }

        mixer.SetFloat("SFXLowPass", 400f);
    }
}
