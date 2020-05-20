using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyPhysics : MonoBehaviour
{
  [Header("Attributes")]
  [SerializeField] private float fallMultiplier = 4f;
  [SerializeField] private float linearDrag = 2f;
  [SerializeField] private float gravity = 3f;

  private Rigidbody2D rb;
  private CollisionRay ray;
  private bool onGround;
  private Vector2 direction;

  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    ray = GetComponent<CollisionRay>();    
  }

  private void Update()
  {
    onGround = ray.OnGround();
    direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
  }
  
  public void Modify()
  {
    bool changingDirections = (direction.x > 0 && rb.velocity.x < 0) || (direction.x < 0 && rb.velocity.x > 0);
    if(onGround)
    {
      if(Mathf.Abs(direction.x) < 2f || changingDirections)
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
      // responsible for the dinamic jump height
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
}
