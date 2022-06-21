using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadVolume : MonoBehaviour
{
    private void Awake()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
        }

    }
}
