using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoSettings : MonoBehaviour
{
    Resolution[] resolutions;
    public Dropdown resolutionDropdown;

    private void Start()
    {
      // gather information about the supported resolutions
      resolutions = Screen.resolutions;
      resolutionDropdown.ClearOptions(); // erase default component options
      
      // create options list, string type to be used on the dropdown
      List<string> options = new List<string>();
      
      int currentResolutionIndex = 0;
      
      // loop through the availables resolutions
      for(int i=0; i<resolutions.Length; i++)
      {
        // format the string
        string option = resolutions[i].width + " x " + resolutions[i].height +
                        " : " + resolutions[i].refreshRate;
        // add the resolution to the list
        options.Add(option);
        
        if(resolutions[i].width == Screen.currentResolution.width &&
          resolutions[i].height == Screen.currentResolution.height)
        {
          currentResolutionIndex = i;
        }
      }
      // add the options list to the dropdown
      resolutionDropdown.AddOptions(options);
      resolutionDropdown.value = currentResolutionIndex;
      resolutionDropdown.RefreshShownValue();
    }
    
    public void SetResolution(int resolutionIndex)
    {
      Resolution resolution = resolutions[resolutionIndex];
      Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    
    public void SetFullscreen(bool isFullscreen)
    {
      Screen.fullScreen = isFullscreen;
    }

}
