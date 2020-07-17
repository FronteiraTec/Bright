using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollController : MonoBehaviour
{
  [Header("Roll Movement")]
  [SerializeField] private float rollMovement = 1.5f;
  // [SerializeField] public float runSpeed = 5f;
  // [SerializeField] private float runStaminaCost = 1f;
  
  // private StaminaBar staminaBar;
  private Rigidbody2D rb;
  private CollisionRay ray;
  private FiniteStateMachine fsm;
  private bool onGround;
  private bool isRolling = false;
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
      // isRolling = true;
      if(Input.GetKeyDown(KeyCode.LeftControl))
      {
        Roll();
        // fsm.state = FiniteStateMachine.State.emptyState;

        // isRolling = false;
      }
    }
  }
  
  private void Roll()
  {
    Debug.Log("Roll");
    fsm.state = FiniteStateMachine.State.rolling;
    rb.velocity = new Vector2(rollMovement *  Mathf.Sign(transform.localScale.x), rb.velocity.y);
    // rb.velocity = new Vector2(0, 0);
    // fsm.state = FiniteStateMachine.State.emptyState;
    return;
  }
}
