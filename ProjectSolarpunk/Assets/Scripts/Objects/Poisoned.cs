using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poisoned : MonoBehaviour {

    [SerializeField] bool poisoned = true;

    SpriteRenderer spriteRenderer;
    [SerializeField] Color poisonedColor;
    [SerializeField] Color restoredColor;

    BoxCollider2D boxCollider;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        boxCollider = gameObject.GetComponent<BoxCollider2D>();

        if (poisoned)
        {
            spriteRenderer.color = poisonedColor;
        }
        else
        {
            spriteRenderer.color = restoredColor;
            boxCollider.isTrigger = true;
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Water" && poisoned == true)
        {
            poisoned = false;
            spriteRenderer.color = restoredColor;
            boxCollider.isTrigger = true;
        }
    }
}
