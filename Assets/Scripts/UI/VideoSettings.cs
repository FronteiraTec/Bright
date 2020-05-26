using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    Resolution[] resolutions; // list of avaiable resolutions
    public Dropdown resolutionDropdown; // dropdown button reference
    
    public Text qualityText; // text which holds the quality settings text
    private int qualityIndex; // used to control increase and decrease level of quality
    public Button IncreaseQualityButton; 
    public Button DecreaseQualityButton;
    // list with all avaiable quality settings 
    private string[] qualities = new string[6]
    {
      "Very Low", "Low", "Medium",
      "High", "Very High", "Ultra"
    };
    
    private void Start()
    {
      // gather information about the supported resolutions
      resolutions = Screen.resolutions;
      resolutionDropdown.ClearOptions(); // erase default component options
      SetResolutionOptions(); // add options to the dropdown menu
      resolutionDropdown.RefreshShownValue(); // update default value of dropdown
      ChangeQualityText(); // update the text to the current quality setting
    }
    
    private void FixedUpdate()
    {
      CheckQualityOption();
    }

    // add options to the dropdown menu
    private void SetResolutionOptions()
    {      
      // create options list, string type to be used on the dropdown
      List<string> options = new List<string>();
      
      // standart native display resolution
      int currentResolutionIndex = 0;
      
      // loop through the availables resolutions
      for(int i=0; i<resolutions.Length; i++)
      {
        // format the string
        string option = resolutions[i].width + " x " + resolutions[i].height +
        " : " + resolutions[i].refreshRate;
        // add the resolution to the list
        options.Add(option);
        
        // check for current native display resolution
        if(resolutions[i].width == Screen.currentResolution.width &&
        resolutions[i].height == Screen.currentResolution.height)
        {
          // if resolution on dropdown matches update the index
          currentResolutionIndex = i;
        }
      }
      // add the options list to the dropdown
      resolutionDropdown.AddOptions(options);
      resolutionDropdown.value = currentResolutionIndex;
    }

    // change game resolution from dropdown selection
    public void SetResolution(int resolutionIndex)
    {
      Resolution resolution = resolutions[resolutionIndex];
      Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    // change window mode, between FULLSCREEN and WINDOWED
    public void SetFullscreen(bool isFullscreen)
    {
      Screen.fullScreen = isFullscreen;
    }

    // attached to button that will increase quality settings
    public void IncreaseQuality()
    {
      // increase current quality level on settings
      QualitySettings.IncreaseLevel(false);
    }
    
    // attached to button that will decrease quality settings
    public void DecreaseQuality()
    {
      // decrease current quality level on settings
      QualitySettings.DecreaseLevel(false);
    }
    
    // change middle button text, which holds quality setting text
    private void ChangeQualityText()
    {
      // assign text from qualities list based on the current quality index
      qualityText.text = qualities[QualitySettings.GetQualityLevel()];
    }
    
    // verify current quality settings and disable button if needed
    private void CheckQualityOption()
    {
      ChangeQualityText();
      // get current quality index from quality settings
      qualityIndex = QualitySettings.GetQualityLevel();

      // if it's NOT possible to increase
      if(qualityIndex == qualities.Length-1)
      {
        IncreaseQualityButton.interactable = false;
      }
      // if it's NOT possible to decrease
      else if(qualityIndex < 1)
      {
        DecreaseQualityButton.interactable = false;
      }
      // if it's possible to do both actions
      else
      {
        // enable interactability of both buttons
        IncreaseQualityButton.interactable = true;
        DecreaseQualityButton.interactable = true;
      }
    }
}
