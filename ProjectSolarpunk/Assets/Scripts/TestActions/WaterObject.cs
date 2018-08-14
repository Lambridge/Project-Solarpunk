using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObject : MonoBehaviour {

    private void Awake()
    {
        gameObject.name = "WaterBullet";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }

}
