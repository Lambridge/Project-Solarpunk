using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashHitbox : MonoBehaviour {

    [SerializeField] float lifetimeInSeconds = 0.3f;
	
	// Update is called once per frame
	void Update () {
        lifetimeInSeconds += Time.deltaTime * -1;
        if(lifetimeInSeconds <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
