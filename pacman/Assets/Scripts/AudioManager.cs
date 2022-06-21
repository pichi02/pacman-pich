using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    void Start()
    {
        if (!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 1f);
            Load();
        }
        else
        {
            Load();
        }
        AudioListener.volume = slider.value;
    }
    public void ChangeVolume()
    {
        AudioListener.volume = slider.value;
        Save();
    }
    private void Load()
    {
        slider.value = PlayerPrefs.GetFloat("volume");
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("volume", slider.value);
    }
}
