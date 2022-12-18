using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatMenu : MonoBehaviour
{
    public PlayerController playerController;
    public Slider HealthSlider;
    public Slider SpeedSlider;
    public Slider AttackSlider;
    
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI attackText;
    
    public int increment = 1;
    public int cost = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        HealthSlider.maxValue = playerController.gameMaxHealth;
        SpeedSlider.maxValue = playerController.gameMaxSpeed;
        AttackSlider.maxValue = playerController.gameMaxAttack;
        
        HealthSlider.value = playerController.maxHealth;
        SpeedSlider.value = playerController.speed;
        AttackSlider.value = playerController.attack;
        
        healthText.text = playerController.maxHealth.ToString();
        speedText.text = playerController.speed.ToString();
        attackText.text = playerController.attack.ToString();
        xpText.text = playerController.xps.ToString();
    }
    
    public void AddHealth()
    {
        if (playerController.xps < cost)
        {
            return;
        }
        playerController.xps -= cost;
        xpText.text = playerController.xps.ToString();
        playerController.maxHealth += 1;
        HealthSlider.value = playerController.maxHealth;
    }
    
    public void AddSpeed()
    {
        if (playerController.xps < cost)
        {
            return;
        }
        playerController.xps -= cost;
        xpText.text = playerController.xps.ToString();
        playerController.speed += 1;
        SpeedSlider.value = playerController.speed;
    }
    
    public void AddAttack()
    {
        if (playerController.xps < cost)
        {
            return;
        }
        playerController.xps -= cost;
        xpText.text = playerController.xps.ToString();
        playerController.attack += 1;
        AttackSlider.value = playerController.attack;
    }
    
}
