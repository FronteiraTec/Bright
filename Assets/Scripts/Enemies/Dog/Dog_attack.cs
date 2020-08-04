using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_attack : StateMachineBehaviour
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
  }

  // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
     
  }
}
