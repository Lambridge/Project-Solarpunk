using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForAndAddGameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (GameObject.Find("GameManager") == null)
        {
            DontDestroyOnLoad(gameObject);
            gameObject.name = "GameManager";
            gameObject.AddComponent<GameManager>();
            Destroy(this);
        }
        else
        {
            Destroy(gameObject);
        }
	}
}
