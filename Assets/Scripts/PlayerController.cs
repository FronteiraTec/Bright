using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    // referencia ao character controller 2d
    public CharacterController2D controller;
    // atribuicao da velocidade do personagem
    public float walkSpeed = 20f;
    public float runSpeed = 40f;
    public bool isRunning = false;

    // declaracao do movimento atual do personagem
    float horizontalMove = 0f;
    float velocity;
    // atribuir referencia do objeto no inspector mode
    [SerializeField] private StaminaBar staminaBar;



    private void Update()
    {
        // recebe do input do jogador e calcula se o personagem esta correndo ou andando
        horizontalMove = Input.GetAxisRaw("Horizontal") * velocity;

        if (Input.GetButton("Space") & Input.GetAxisRaw("Horizontal") != 0 & staminaBar.currentStamina > 5)
        {
            isRunning = true;
            staminaBar.UseStamina(0.1f);
            velocity = runSpeed;
        }
        else
        { 
             velocity = walkSpeed;
        }
        

    }

        // exemplo de uso da barra de stamina atravez do player

    void FixedUpdate()
    {
        // movimento da personagem
        controller.Move(horizontalMove * Time.fixedDeltaTime, false);
    }

}