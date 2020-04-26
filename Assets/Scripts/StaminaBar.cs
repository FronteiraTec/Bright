using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private Slider staminaBar;
    [SerializeField] private int maxStamina = 100;
    [Range(0f, 2f)] [SerializeField] private float regenDelay = 0.5f;
    [Range(0f, 4f)] [SerializeField] private float regenAmount = 1f;

    // micro delay no recharge rate
    private WaitForSeconds regenTick = new WaitForSeconds(.05f);
    private float currentStamina;
    
    // processo de regeneracao
    private Coroutine regen;
    public bool staminaEsgotada;
    
    private void Start()
    {
        currentStamina = maxStamina; // inicializa a barra com a stamina cheia
        staminaBar.maxValue = maxStamina; // configura o valor maximo da barra
        staminaBar.value = maxStamina;  // atualiza o preenchimento da barra\
        staminaEsgotada = false;
    }

    public void UseStamina(float amount)
    {
        // Metodo chamado toda vez que uma açao qualquer necessita utilizacao de stamina
        // Verifica se tem stamina suficiente para realizar a acao
        if(true && EnoughStamina())
        {
            currentStamina -= amount; // reduz a qtd usada da stamina atual
            staminaBar.value = currentStamina;  // atualiza o valor na barra

            // Condicional necessaria para que exista o delay apos o uso da stamina
            if(regen != null) // se a regeneracao ja foi inicializada
            {
                // encerra o processo de regeneracao
                StopCoroutine(regen);
            }
            // Chama o metodo de regeneracao
            regen = StartCoroutine(RegenStamina());
        }
    }
    
    public bool EnoughStamina()
    {
      if(staminaBar.value > 1)
      {
        return true;
      }
      return false;
    }
    
    public void Info()
    {
      Debug.Log(("StaminaBar Info = ",maxStamina, regenDelay, regenAmount));
    }
    // public double GetStamina()
    // {
    //   if(staminaEsgotada == true)
    //   {
    //     return -1f;
    //   }
    //   else if(staminaBar.value < 0.1f)
    //   {
    //     staminaEsgotada = true;
    //     Debug.Log("ESGOTOU");
    //     return -1f;
    //   }
    //   else
    //   {
    //     return staminaBar.value;
    //   }
    // }

    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(regenDelay); // delay recharge rate
        // enquanto a barra nao estiver cheia
        while(currentStamina < maxStamina)
        {
            currentStamina += regenAmount; // adiciona a regeneracao para a stamina
            staminaBar.value = currentStamina; // atualiza a barra
            // micro delay no recharge
            yield return regenTick; //se nao iria regenerar 'instantaneamente'
        }
        Debug.Log("CHEIA");
        staminaEsgotada = false;
        regen = null;
    }
}
