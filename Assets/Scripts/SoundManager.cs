using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    // Start is called before the first frame update
    void Start()
    {
     if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1); //creates a player pref for game volume that will save across all scenes and sets the default value to 1
            Load();//load player pref
        }

     else
        {
            Load(); //load the default player pref (1)
        }
    }

    public void ChangeVolume()//function allows volume to change
    {
        AudioListener.volume = volumeSlider.value;//the volume for the audio listener will be set to whatever the value of the slider is (0-1)
        Save();//saves the value
    }    

    private void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");//loads the player pref's saved volume
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);//saves the float value for the game volume player pref
    }
}
