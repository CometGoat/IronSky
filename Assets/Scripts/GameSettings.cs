using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/* ----- Settings Script ------
This script is responsible for loading and saving game settings using PlayerPrefs. 
It is meant to behave the same way whether it is persistent through scenes or reloaded every time.


Please note, every time you wish to add a settings to be loaded/saved,
make sure to add its name to the list and to add its loading logic in RefreshSettings().
*/

/*  ----- How to use it ------

-- Adding a Set/Get setting

Create your regular methods with the setting's logic within it.
If you wish the setting to be saved add the following in the Set method:

    PlayerPrefs.SetFloat("SettingName", volume);

-- Set up automatic load on start

Add the SettingName string to the settingsName list in the Start() method.
Then, add a case to the switch in the RefreshSettings() method. This is where
you will need to input a default value in case the saved setting could be loaded.
It is up to you if you wish to create a variable or hardcode the default value.


*/


public class GameSettings : MonoBehaviour
{
    //////////////////
    /// VARIABLES ////
    //////////////////

    [Header("Default Settings")]

    [Range(0, 1)]public float defaultVolume = 1; // Default volume value.
    [Range(0, 3)] public int defaultGraphicsQuality = 3; // Default graphics quality.
    public bool defaultRumbleEnabled = true; // Default rumble settings.

    private float volume; // Current value
    private int graphicsQualityIndex; // Current value
    private bool isRumbleEnabled; // Current value

    private List<string> settingsName; // This list created on Start(), is a way for the script which settings to load from PlayerPrefs.

    /////////////////
    //// METHODS ////
    /////////////////

    void Start()
    {
        // Creating the list of settings to load/save
        settingsName.Add("volume");
        settingsName.Add("rumble");
        settingsName.Add("graphicsQuality");

        // Loading saved settings
    }

    private void RefreshSettings()
    {
        // For each settings we want to load (list created on Start() )
        foreach (string settingName in settingsName)
        {

            // Attempt to load the saved value or apply the default one.
            switch (settingName)
            {
                case "volume":
                    SetVolume(PlayerPrefs.GetFloat("settingName", defaultVolume));
                    break;

                case "rumble":
                    SetRumbleEnabled(PlayerPrefs.GetInt("settingName", defaultRumbleEnabled) != 0); //This is a quick conversion from 0/1 to false/true
                    break;

                case "graphicsQuality":
                    SetGraphicQuality(PlayerPrefs.GetInt("settingName", defaultGraphicsQuality));
                    break;
            }
        }

        GameEventManager.current.SettingsRefreshed();

    }


    public void SetVolume(float newVolume)
    {
        volume = Mathf.Clamp(newVolume, 0, 1);
        PlayerPrefs.SetFloat("volume", volume);

        //Actually applying the setting
        /* @TODO: Use project's audio mixer to set volume */

        GameEventManager.current.VolumeChanged(volume);
        
    }
    public void SetVolume(bool isVolumeEnabled)
    {
        if(isVolumeEnabled)
        {
            volume = 1;
            PlayerPrefs.SetFloat("volume", 1);
        }
        else
        {
            volume = 0;
            PlayerPrefs.SetFloat("volume", 0);
        }

        SetVolume(volume); // Call the regular float based SetVolume for simplicity.
    }
    public float GetVolume()
    {
        return volume;
    }


    public void SetGraphicQuality(int QualityIndex)
    {
        graphicsQualityIndex = Mathf.Clamp(QualityIndex, 0, 3); // Clamping 0-3 because there are only 3 Graphic Settings
        PlayerPrefs.SetInt("graphicsQuality", graphicsQualityIndex);

        //Actually applying the setting
        QualitySettings.SetQualityLevel(graphicsQualityIndex);

        GameEventManager.current.GraphicQualityChanged(graphicsQualityIndex);
    }
    public int GetGraphicsQualityIndex()
    {
        return graphicsQualityIndex;
    }


    public void SetRumbleEnabled(bool RumbleEnabled)
    {
        isRumbleEnabled = RumbleEnabled;
        PlayerPrefs.SetInt("rumble", isRumbleEnabled ? 1 : 0);

        GameEventManager.current.RumbleEnabledChanged(isRumbleEnabled);
    }
    public bool GetRumbleEnabled()
    {
        return isRumbleEnabled;
    }


  
}
