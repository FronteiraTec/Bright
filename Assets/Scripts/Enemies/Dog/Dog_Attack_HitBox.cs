using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_Attack_HitBox : MonoBehaviour
{
  
  [SerializeField] private LayerMask layer;
  private DogController dog;
  private Collider2D col;
  private GameObject player;

  private void Start()
  {
    dog = GameObject.Find("Dog").GetComponent<DogController>();
    col = GetComponent<Collider2D>();
  }

  private void OnTriggerEnter2D(Collider2D collider)
  {
    if(collider.transform.name == "Player")
    {
      // get player's health bar script and TakeDamage()
      player = GameObject.Find("Player");
      player.BroadcastMessage("TakeDamage", dog.attackDamage);
    }
  }
}
