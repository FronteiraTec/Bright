using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class estalactiteController : MonoBehaviour
{
  
  [SerializeField] private float delayToFall;
  [SerializeField] private float damageDealt;
  [SerializeField] private float fallSpeed;
  
  [SerializeField] private Vector3 colliderOffset;
  [SerializeField] private float rayLenght;
  [SerializeField] private LayerMask groundLayer;
  [SerializeField] private bool onGround;
  [SerializeField] private ParticleSystem explosionParticles;

  private Rigidbody2D rb;
  private Collider2D col;
  private Animator anim;
  GameObject player;
  
  private void Start()
  {
    rb = GetComponent<Rigidbody2D>();
    col = GetComponent<BoxCollider2D>();
    anim = GetComponent<Animator>();
  }

  private void Update()
  {
    onGround = Physics2D.Raycast(transform.position + colliderOffset, Vector2.down, rayLenght, groundLayer) || Physics2D.Raycast(transform.position - colliderOffset, Vector2.down, rayLenght, groundLayer);
    
    if (onGround)
    {
      this.Die();
    }
  }
  
  private void OnDrawGizmos()
  {
    Gizmos.color = Color.green;
    Gizmos.DrawLine(transform.position + colliderOffset, transform.position + colliderOffset + Vector3.down * rayLenght);
    Gizmos.DrawLine(transform.position - colliderOffset, transform.position - colliderOffset + Vector3.down * rayLenght);
  }
  
  void TrembleAndFall()
  {
    StartCoroutine(this.Fall());
  }
  
  private IEnumerator Fall()
  {
    anim.SetTrigger("crack");
    yield return new WaitForSeconds(delayToFall);
    rb.velocity = new Vector2(0, fallSpeed);
  }
  
  private void Die()
  {
    explosionParticles.Play();
    anim.SetBool("IsDestroyed", true);
    rb.velocity = new Vector2(0,0);
    col.enabled = false;
    this.enabled = false;
    // Destroy(this);
  }
  
  void OnCollisionEnter2D(Collision2D collision2D)
  {
    if(collision2D.transform.name == "Player")
    {
      // get player's health bar script and TakeDamage()
      player = GameObject.Find("Player");
      player.BroadcastMessage("TakeDamage", damageDealt);
    }
  }
}
