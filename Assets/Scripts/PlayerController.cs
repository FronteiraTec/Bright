using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // atribuir referencia do objeto no inspector mode
    [SerializeField] private StaminaBar staminaBar;


    private void Update()
    {
        // exemplo de uso da barra de stamina atravez do player
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // chamada do metodo do uso da stamina
            staminaBar.UseStamina(12f);
        }
    }
}
