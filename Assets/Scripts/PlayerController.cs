using UnityEngine.SceneManagement;
using System.Collections.Generic;
﻿using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  // Inspector Variables
  [Header("Horizontal Movement")]
  [SerializeField] private float walkSpeed = 5f;
  [SerializeField] private float runSpeed = 5f;
  [SerializeField] private float runStaminaCost = 1f;
  
  [Header("Vertical Movement")]
  [SerializeField] private float jumpForce = 80;
  [SerializeField] private float jumpStaminaCost = 5f;
  [Range(0, 1f)][SerializeField] private float jumpStaminaFactor;

  [Header("Collision")]
  [SerializeField] private bool onGround = false;
  [SerializeField] private float rayLenght = 1f;
  
  [Header("Physics")]
  [SerializeField] private float fallMultiplier = 4f;
  [SerializeField] private float linearDrag = 2f;
  [SerializeField] private float gravity = 3f;

  [Header("Components")]
  [SerializeField] private LayerMask groundLayer;
  [SerializeField] private StaminaBar staminaBar;
  [SerializeField] private Collider2D coll;
  [SerializeField] private Rigidbody2D rb;
  [SerializeField] private Animator anim;

  [Header("Info")]
  public Vector2 direction;
  public bool facingRight = true;
  
  // Finite State Machine
  private enum State {idle, walking, running, jumping, falling};
  private State state = State.idle;

  // Update is called once per frame
  private void Update()
  {
    onGround = Physics2D.Raycast(transform.position, Vector2.down, rayLenght, groundLayer);
    direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    AnimationState();
    anim.SetInteger("state", (int)state);
  }
  
  private void FixedUpdate()
  {
    if(Input.GetButton("Jump"))
    {
      Jump();
    }
    Movement();
    // Reload the Scene for testing purposes
    if (Input.GetKeyDown(KeyCode.R) || Input.GetKey(KeyCode.JoystickButton6))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    ModifyPhysics();
  }
  
  private void ModifyPhysics()
  {
    bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);
    if(onGround)
    {
      if(Mathf.Abs(direction.x) < 0.4f || changingDirections)
      {
        rb.drag = linearDrag;
      }
      else
      {
        rb.drag = 0f;
      }
      rb.gravityScale = 0;
    }
    else
    {
      rb.gravityScale = gravity;
      rb.drag = linearDrag * 0.15f;
      if(rb.velocity.y < 0)
      {
        rb.gravityScale = gravity * (fallMultiplier / 2);
      }
      else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
      {
        rb.gravityScale = gravity * fallMultiplier;
      }
    }
  }
  
  private void Jump()
  {
    if((Input.GetButtonDown("Jump")) && onGround)
      {
      rb.velocity = new Vector2(rb.velocity.x, 0);
      if(staminaBar.GetStamina() > jumpStaminaCost)
      {
        staminaBar.UseStamina(jumpStaminaCost);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        state = State.jumping;
      }
    }
    else if((Input.GetButton("Jump")) && !onGround && (rb.velocity.y > .1f))
    {
      if(staminaBar.GetStamina() > (jumpStaminaFactor))
      {
        staminaBar.UseStamina(jumpStaminaFactor);
      }
    }
  }

  private void Movement()
  {
    if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.JoystickButton1))
    {
      if(direction.x != 0)
      {
        if(staminaBar.GetStamina() > runStaminaCost){
          staminaBar.UseStamina(runStaminaCost);
          rb.velocity = new Vector2(direction.x * runSpeed, rb.velocity.y);
        }
      }
    }
    else 
    {
      rb.velocity = new Vector2(direction.x * walkSpeed, rb.velocity.y);
    }

    if((direction.x > 0 && !facingRight) || (direction.x < 0 && facingRight))
    {
      Flip();
    }
  }
  private void Flip()
  {
    facingRight = !facingRight;
    transform.localScale = new Vector2(Mathf.Sign(direction.x), transform.localScale.y);
  }

  
  private void AnimationState()
  {
    if(state == State.jumping)
    {
      if(rb.velocity.y < .1f)
      {
        state = State.falling;
      }
    }
    else if(state == State.falling)
    {
      if(onGround)
      {
        state = State.idle;
      }
    }

    else if(0.1f <= Mathf.Abs(rb.velocity.x) && Mathf.Abs(rb.velocity.x) <= walkSpeed)
    {
      state = State.walking;
    }
    else if(walkSpeed < Mathf.Abs(rb.velocity.x) && Mathf.Abs(rb.velocity.x) <= runSpeed)
    {
      state = State.running;
    }
    else
    {
      state = State.idle;
    }
  }
  
  private void OnDrawGizmos()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawLine(transform.position, transform.position + Vector3.down * rayLenght);
  }
}


