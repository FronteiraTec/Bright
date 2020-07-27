using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollController : MonoBehaviour
{
  [Range(1f, 50f)] [SerializeField] private float rollMovement;
  
  // private StaminaBar staminaBar;
  private Rigidbody2D rb;
  private CollisionRay ray;
  private FiniteStateMachine fsm;
  private bool onGround;
  
  private void Start()
  {
    // staminaBar = GetComponent<StaminaBar>();
    rb = GetComponent<Rigidbody2D>();
    ray = GetComponent<CollisionRay>();    
    fsm = GetComponent<FiniteStateMachine>();
  }

  private void Update()
  {
    onGround = ray.OnGround();
    
    if(onGround)
    {
      if(Input.GetKeyDown(KeyCode.LeftControl))
      {
        Roll();
      }
    }
  }
  
  private void Roll()
  {
    Debug.Log("Roll");
    fsm.state = FiniteStateMachine.State.rolling;
  }
  
  public void RollMove()
  {
    rb.velocity = new Vector2(rollMovement *  Mathf.Sign(transform.localScale.x), rb.velocity.y);
  }
}
