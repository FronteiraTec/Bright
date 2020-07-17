using UnityEngine.SceneManagement;
using System.Collections.Generic;
﻿using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

  private void FixedUpdate()
  {
    // Reload the Scene for testing purposes
    if (Input.GetKeyDown(KeyCode.R))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    // Return to the main menu
    else if (Input.GetKeyDown(KeyCode.Escape))
    {
      SceneManager.LoadScene(0);
    }
  }
}


