using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController_UI : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
        Save();
    }

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
    //[SerializeField] private string mixerParameter;
    //[SerializeField] private AudioMixer audioMixer;
    //[SerializeField] private Slider slider;
    //[SerializeField] private float sliderMultiplier;



    //public void SetUpVolumeSlider()
    //{
    //    slider.onValueChanged.AddListener(SliderValue);
    //    slider.minValue = .001f;
    //    slider.value = PlayerPrefs.GetFloat(mixerParameter, slider.value);
    //}

    //private void OnDisable()
    //{
    //    PlayerPrefs.SetFloat(mixerParameter, slider.value);
    //}

    //private void SliderValue(float value)
    //{
    //    audioMixer.SetFloat(mixerParameter, Mathf.Log10(value) * sliderMultiplier);

    //}




}
