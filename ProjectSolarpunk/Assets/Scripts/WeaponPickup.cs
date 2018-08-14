using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {

    //Pickup doesn't work if you don't set one of these to be true !!
    [SerializeField] bool isBladePickup;
    [SerializeField] bool isGunPickup;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            if (isBladePickup)
                gameManager.BladeObtained = true;
            if (isGunPickup)
                gameManager.GunObtained = true;

            Destroy(gameObject);
        }
    }

}
