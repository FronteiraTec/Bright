using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // referencia ao character controller 2d
    public CharacterController2D controller;
    // atribuicao da velocidade do personagem
    public float runSpeed = 40f;
    // declaracao do movimento atual do personagem
    float horizontalMove = 0f;

    // atribuir referencia do objeto no inspector mode
    [SerializeField] private StaminaBar staminaBar;


   
    private void Update()
    {
        // receptor do input do jogador
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;


        // exemplo de uso da barra de stamina atravez do player
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // chamada do metodo do uso da stamina
            staminaBar.UseStamina(12f);
        }
    }


    void FixedUpdate()
    {
        // movimento da personagem
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
}