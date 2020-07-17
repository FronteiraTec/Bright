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
    private int SpeedAchievedOnFall;
    private int PrevAnimation;

    [SerializeField] private int highSpeed;
    [SerializeField] private int mortalSpeed;
    
    [HideInInspector] public enum State {idle, walking, running, jumping, falling,
                      landing, attacking, rolling, climbing, emptyState};
                      
    [HideInInspector] public enum Landing {low, high, death};
    [HideInInspector] public Landing landStyle = Landing.low;
    [HideInInspector] public State state = State.emptyState;
    
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
      anim.SetInteger("LandStyle", (int)landStyle);
    }
    
    private void AnimationState()
    {
      // jumping
      if(rb.velocity.y > 0.1f)
      {
        state = State.jumping;
        if(!onGround)
        {
          if((int)(rb.velocity.y) < 4f)
          {
            anim.SetBool("JumpTransition", true);
          }
          else
          {
            anim.SetBool("JumpTransition", false);
          }
        }
      }
      // falling
      else if(rb.velocity.y < -0.1f)
      {
        state = State.falling;
        TrackSpeedOnFall();
        PrevAnimation = (int)state;
      }
      else if(PrevAnimation == 4)
      {
        // TrackSpeedOnFall();
        if(onGround)
        {
          ChooseLandingStyle();
        }
        PrevAnimation = -1;
      }
      else if(state == State.attacking)
      {
        rb.velocity = new Vector2(0, 0);
      }
      // rolling
      else if(state == State.rolling)
      {
        // rb.velocity = new Vector2(0, 0);
        Debug.Log("FSM Roll");
      }
      else if(((int)rb.velocity.x != 0) && onGround)
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
      else if(state == State.emptyState)
      {
        state = State.idle;
      }
      else if(state == State.landing)
      {
        return;
      }
      else
      {
        state = State.idle;
        SpeedAchievedOnFall = 0;
      }
    }
    private void ChooseLandingStyle()
    {
      if(onGround)
      {
        state = State.landing;

        if(SpeedAchievedOnFall > mortalSpeed)
        {
          landStyle = Landing.death;
        }
        else if(SpeedAchievedOnFall > highSpeed)
        {
          landStyle = Landing.high;
        }
        else if(SpeedAchievedOnFall < highSpeed)
        {
          landStyle = Landing.low;
        }
        else
        {
          Debug.Log("Somethings wrong I can feel it");
        }
      }
    }
    private void TrackSpeedOnFall()
    {
      if(!onGround && Mathf.Abs(rb.velocity.y) > SpeedAchievedOnFall)
      {
        SpeedAchievedOnFall = Mathf.Abs((int)rb.velocity.y);
      }
    }
    public void ExitLanding()
    {
      state = State.emptyState;
    }
}
