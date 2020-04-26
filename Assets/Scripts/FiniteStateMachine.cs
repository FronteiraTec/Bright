using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private MovementController movement;
    private CollisionRay ray;
    private bool onGround;

    private enum State {idle, walking, running, jumping, falling};
    private State state = State.idle;
    
    private void Start()
    {
      movement = GetComponent<MovementController>();
      ray = GetComponent<CollisionRay>();
      rb = GetComponent<Rigidbody2D>();
      anim = GetComponent<Animator>();
    }

    private void Update()
    {
      onGround = ray.OnGround();
      AnimationState();
      anim.SetInteger("state", (int)state);
    }
    
    private void AnimationState()
    {
      // jumping
      if(rb.velocity.y > 0.1f)
      {
        state = State.jumping;
      }
      // falling
      else if(rb.velocity.y < -0.1f)
      {
        state = State.falling;
      }
  
      else if(Mathf.Abs(rb.velocity.x) > .5f && onGround)
      {
        if(Mathf.Abs(rb.velocity.x) <= movement.walkSpeed)
        {
          state = State.walking;
        }
        else if(Mathf.Abs(rb.velocity.x) <= movement.runSpeed)
        {
          state = State.running;
        }
      }
      else
      {
        state = State.idle;
      }
    }
}
