using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_run : StateMachineBehaviour
{
  DogController dog;
  Transform player;
  Rigidbody2D rb;

  // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    player = GameObject.FindGameObjectWithTag("Player").transform;
    dog = animator.GetComponent<DogController>();
    rb = animator.GetComponent<Rigidbody2D>();
  }

  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    // flip dog to face player
    dog.LookAtPlayer();
    // get player position
    Vector2 target = new Vector2(player.position.x, rb.position.y);    
    Vector2 newPos = Vector2.MoveTowards(rb.position, target, dog.runSpeed * Time.fixedDeltaTime);
    rb.MovePosition(newPos);
  }
}
