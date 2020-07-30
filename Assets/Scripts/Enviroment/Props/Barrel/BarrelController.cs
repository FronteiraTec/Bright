using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelController : MonoBehaviour
{
  [SerializeField] private int HealthPoints = 3;
  [SerializeField] private bool destroyable;
  [SerializeField] private Animator animator;
  
  private void TakeDamage()
  {
    animator.SetTrigger("Hurt");
    
    if(destroyable)
    {
      HealthPoints -= 1;
    }
      
    if(HealthPoints <= 0)
    {
      Die();
    }
  }
  
  private void Die()
  {
    animator.SetBool("IsDestroyed", true);
    GetComponent<Collider2D>().enabled = false;
    this.enabled = false;
  }
}
