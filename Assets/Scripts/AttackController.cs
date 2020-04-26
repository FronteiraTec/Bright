using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
  [Header("Combo Parameters")]
   // how much room until next click to continue combo
  [Range(0f, 2f)] [SerializeField] private float maxComboDelay = 0.5f;
  [SerializeField] private float lastClickedTime = 0;
  private int numberOfClicks = 0;
  
  [Header("Weak Attack Parameters")]
  [Range(0f, 10f)] [SerializeField] private float motionPerAttack = 1f;
  [Range(0f, 100f)] [SerializeField] private float WeakAttackStaminaCost = 0;
  
  // [Header("Components")]
  private Animator anim;
  private Rigidbody2D rb;
  private StaminaBar staminaBar;
  
  private void Start()
  {
    anim = GetComponent<Animator>();
    rb = GetComponent<Rigidbody2D>();
    staminaBar = GetComponent<StaminaBar>();
  }


  private void Update()
  {
    // Checks if the next click is within the chain time, which will proceed the combo
    if(Time.time - lastClickedTime > maxComboDelay)
    {
      numberOfClicks = 0;
    }
    
    // key listener to mouse1/fireButton/MouseLeftClick as you wanna call it
    if(Input.GetMouseButtonDown(0))
    {
      // Call the combo manager which will identify what animation will be played
      Combo();
    }
  }
  
  // responsible for the stamina verification and consuption
  private int staminaUse(float amount)
  {
    if(staminaBar.EnoughStamina())
    {
      staminaBar.UseStamina(amount);
      return 0;
    }
    return 1;
  }
  
  // responsible for moving the character throughout the attack animation
  public void MoveCharacter()
  {
    // Check if there are enough stamina to realize the action (0=able, 1=unable)
    if(staminaUse(WeakAttackStaminaCost) == 0)
    {
      rb.position = new Vector2(rb.position.x + Mathf.Sign(transform.localScale.x) * motionPerAttack, rb.position.y);
    }
  }
  
  // Used to control if next and/or which animation will be played
  private void Combo()
  {
    if(staminaBar.EnoughStamina())
    {
      lastClickedTime = Time.time; // get current time
      numberOfClicks++; // increase clicks counter
      
      // Restrain number of clicks within range(0,2)
      if(numberOfClicks == 1)
      {
        anim.SetBool("Attack1", true);
      }
      if(numberOfClicks % 2 == 0)
      {
        numberOfClicks = 2;
      }
      else if(numberOfClicks % 2 == 1)
      {
        numberOfClicks = 1;
      }
      else
      {
        numberOfClicks = 0;
      }
    }
    else
    {
      numberOfClicks = 0;
    }
  }
  
  // Used as a flag on Weak_Attack_OnGround_1 animation
  // detects if another click was given
  // if yes proceed to the next animation otherwise return to idle
  public void Return1()
  {
    if(numberOfClicks == 2)
    {
      anim.SetBool("Attack2", true);
    }
    else
    {
      anim.SetBool("Attack1", false);
      numberOfClicks = 0;
    }
  }
  
  // Used as a flag on Weak_Attack_OnGround_2 animation
  public void Return2()
  {
    if(numberOfClicks == 1)
    {
      anim.SetBool("Attack1", true);
      anim.SetBool("Attack2", false);
    }
    else
    {
      anim.SetBool("Attack1", false);
      anim.SetBool("Attack2", false);
      numberOfClicks = 0;
    }
  }
}
