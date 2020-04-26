using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRay : MonoBehaviour
{
    [Header("Attributes")]
    [Range(0, 2f)]   [SerializeField] private float rayLenght;
    [SerializeField] private Vector3 colliderOffset;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool onGround;

    private void Start()
    {
        
    }

    private void Update()
    {
      onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, rayLenght, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, rayLenght, groundLayer);
    }
    
    private void OnDrawGizmos()
    {
      Gizmos.color = Color.green;
      Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * rayLenght);
      Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * rayLenght);
    }
    
    public bool OnGround()
    {
      return onGround;
    }
}
