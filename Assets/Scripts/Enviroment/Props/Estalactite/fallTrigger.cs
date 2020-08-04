using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fallTrigger : MonoBehaviour
{
  GameObject estalactite;

  private void OnTriggerEnter2D(Collider2D col)
  {
    if(col.gameObject.tag == "Player")
    {
      estalactite = GameObject.Find("body");
      estalactite.BroadcastMessage("TrembleAndFall");
      Destroy(this);
    }
  }
}