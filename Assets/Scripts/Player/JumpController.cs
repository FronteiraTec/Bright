using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpController : MonoBehaviour
{
  private Rigidbody2D rb;
  private CollisionRay ray;
  private StaminaBar staminaBar;
  private ModifyPhysics physics;
  private bool onGround;

  [Header("Jump Attributes")]
  [SerializeField] private float jumpForce = 80;
  [SerializeField] private float jumpStaminaCost = 5f;
  [Range(0, 1f)] [SerializeField] private float jumpStaminaFactor;
  [SerializeField] private ParticleSystem dust;


  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    ray = GetComponent<CollisionRay>();
    staminaBar = GetComponent<StaminaBar>();
    physics = GetComponent<ModifyPhysics>();
  }

  private void Update()
  {
    onGround = ray.OnGround();
    
    if(Input.GetButton("Jump"))
    {
      // Debug.Log("PULO");
      Jump();
    }
  }
  
  private void FixedUpdate()
  {
    physics.Modify();
  }
  
  private void Jump()
  {
    if((Input.GetButtonDown("Jump")) && onGround)
      {
      CreateDust();
      rb.velocity = new Vector2(rb.velocity.x, 0);
      if(staminaBar.EnoughStamina())
      {
        staminaBar.UseStamina(jumpStaminaCost);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
      }
    }
    // is going up and jump button is still pressed
    else if((Input.GetButton("Jump")) && !onGround && (rb.velocity.y > .1f))
    {
      if(staminaBar.EnoughStamina())
      {
        staminaBar.UseStamina(jumpStaminaFactor);
      }
    }
  }
  
  private void CreateDust()
  {
    dust.Play();
  }
}
