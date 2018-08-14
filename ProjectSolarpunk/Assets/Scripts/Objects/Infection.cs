using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infection : MonoBehaviour {

    bool infected = true;
    SpriteRenderer spriteRenderer;
    [SerializeField] Color infectionTint;
    BoxCollider2D boxCollider;

    // Use this for initialization
    void Start () {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = false;
        spriteRenderer.color = infectionTint;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water" && infected == true)
        {
            infected = false;
            spriteRenderer.color = Color.white;
            boxCollider.isTrigger = true;
            
            gameObject.AddComponent<CuttableObject>();
            Destroy(this);
        }
    }

}
