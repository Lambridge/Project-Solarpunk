using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    //For testing purposes
    [SerializeField] bool healthEnabled;

    //Useful vars start here
    [SerializeField] float maxHealth = 100;
    float currentHealth;

    Slider sliderObject;
    Text healthText;

    // Use this for initialization
    void Start () {
        if (!healthEnabled)
            Destroy(this);

        //sliderObject =
        //    GameObject.Find("HealthBar").GetComponent<Slider>();

        healthText =
            GameObject.Find("HealthHUD").GetComponentInChildren<Text>();

        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    public void UpdateHealth(float damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth < 0) currentHealth = 0;
        UpdateHealthUI();

        //Kill the player if they reach zero health
        if (currentHealth <= 0)
        {
            PlayerDeath();
        }
    }

    private void UpdateHealthUI()
    {
        //sliderObject.value = currentHealth;
        healthText.text = currentHealth.ToString();
    }

    void PlayerDeath()
    {
        SceneManager.LoadScene(0);
    }

}
