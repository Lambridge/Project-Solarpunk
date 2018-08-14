using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    BoxCollider2D collider;
    SpriteRenderer spriteRenderer;

    Color closedColor;
    Color openColor = Color.yellow;

	// Use this for initialization
	void Start () {
        collider = gameObject.GetComponent<BoxCollider2D>();
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        closedColor = gameObject.GetComponent<SpriteRenderer>().color;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            collider.enabled = false;
            spriteRenderer.color = openColor;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            collider.enabled = true;
            spriteRenderer.color = closedColor;
        }
    }

}
