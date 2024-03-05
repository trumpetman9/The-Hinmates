using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityIcon : MonoBehaviour
{
    public float cooldownTimer;
    public Image cooldownBar; 

    public Image InsufficientMana;
    
    public float cooldownTime;

    // Start is called before the first frame update
    void Start()
    {
        cooldownTimer = 0;
        cooldownBar.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        

        // Update cooldown progress
        if (cooldownTimer > 0)
        {
            cooldownTimer -= Time.deltaTime;
            cooldownBar.fillAmount = cooldownTimer / cooldownTime;
        }
        else
        {
            cooldownBar.fillAmount = 0;
        }
    }
    public void StartCooldown(float time)
    {
        cooldownTime = time;
        cooldownTimer = time;
    }

    public void SufficientMana(bool isSufficient){
        if(isSufficient){
            InsufficientMana.gameObject.SetActive(false);
        } else {
            InsufficientMana.gameObject.SetActive(true);
        }
    }

}
