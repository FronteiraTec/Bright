using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingController : MonoBehaviour
{
    private CollisionRay ray;
    private FiniteStateMachine fsm;
    private Rigidbody2D rb;
    private bool onGround;
    private int PrevAnimation;
    private int SpeedAchievedOnFall;

    [SerializeField] private int highSpeed;
    [SerializeField] private int mortalSpeed;
    
    private void Start()
    {
      ray = GetComponent<CollisionRay>();
      fsm = GetComponent<FiniteStateMachine>();
      rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
      onGround = ray.OnGround();
      if(PrevAnimation != 4)
      {
        PrevAnimation = (int)fsm.state;
        SpeedAchievedOnFall = 0;
      }
      else
      {
        ChooseLandingStyle();
        PrevAnimation = 1000;
      }
    }
    
    private void ChooseLandingStyle()
    {
      TrackSpeedOnFall(); //assign to variable SpeedAchievedOnFall
      // Debug.Log(SpeedAchievedOnFall);

      fsm.state = FiniteStateMachine.State.landing;
      if(onGround)
      {
        if(SpeedAchievedOnFall > mortalSpeed)
        {
          fsm.landStyle = FiniteStateMachine.Landing.death;
        }
        else if(SpeedAchievedOnFall > highSpeed)
        {
          fsm.landStyle = FiniteStateMachine.Landing.high;
        }
        else if(SpeedAchievedOnFall < highSpeed)
        {
          fsm.landStyle = FiniteStateMachine.Landing.low;
        }
      }
    }
    private void TrackSpeedOnFall()
    {
      if(Mathf.Abs((int)rb.velocity.y) > SpeedAchievedOnFall)
      {
        SpeedAchievedOnFall = Mathf.Abs((int)rb.velocity.y);
      }
    }
    // public void 
}
