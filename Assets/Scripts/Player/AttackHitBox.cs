using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitBox : MonoBehaviour
{
    Collider2D collider;
  
    private void Start()
    {
      collider = GetComponent<BoxCollider2D>();
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
      if(collider.gameObject.tag == "Prop")
      {
        collider.gameObject.BroadcastMessage("TakeDamage");
      }
    }
}
