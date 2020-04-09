using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
  // Inspector Variables
  [SerializeField]private int speed = 5;
  [SerializeField]private float jumpForce = 20f;
  [SerializeField]private LayerMask ground;
  [SerializeField]private StaminaBar staminaBar;

  // Finite State Machine
  private enum State {idle, running, jumping, falling, hurt};
  private State state = State.idle;

  // Start() Variables
  private Rigidbody2D rb;
  private Animator anim;
  private Collider2D coll;

  private void Start()
  {
      rb = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
      coll = GetComponent<Collider2D>();
  }

  // Update is called once per frame
  private void Update()
  {
    Movement();
    AnimationState();
    anim.SetInteger("state",(int)state);
  }

  private void Jump()
  {
    staminaBar.UseStamina(12f);
    rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    state = State.jumping;
  }

  private void Movement()
  {
    if (Input.GetKeyDown(KeyCode.R))
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    float HDirection = Input.GetAxis("Horizontal");
    // Moving Left
    if(HDirection < 0)
    {
      rb.velocity = new Vector2(-speed, rb.velocity.y);
      transform.localScale = new Vector2(-1, 1);
    }
    // Moving Right
    else if(HDirection > 0)
    {
      rb.velocity = new Vector2(speed, rb.velocity.y);
      transform.localScale = new Vector2(1, 1);
    }
    
    if(Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
    {
      Jump();
    }
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
      if(coll.IsTouchingLayers(ground))
      {
        state = State.idle;
      }
    }

    else if(Mathf.Abs(rb.velocity.x) > 2f)
    {
      // Moving
      state = State.running;
    }
    else
    {
      state = State.idle;
    }
  }
}