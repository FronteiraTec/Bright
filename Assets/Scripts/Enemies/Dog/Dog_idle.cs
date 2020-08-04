using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dog_idle : StateMachineBehaviour
{
  Rigidbody2D rb;
  DogController dog;

  // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    rb = animator.GetComponent<Rigidbody2D>();
    dog = animator.GetComponent<DogController>();
  }

  // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    dog.LookAtPlayer();
  }
}
