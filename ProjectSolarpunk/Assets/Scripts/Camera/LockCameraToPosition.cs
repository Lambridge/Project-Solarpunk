using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockCameraToPosition : MonoBehaviour {

    [SerializeField] Vector2 positionToLockTo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraModes camera;
            camera = GameObject.Find("Main Camera").GetComponent<CameraModes>();
            camera.lockedPosition = new Vector3(17.5f, 0f, -10f);
            camera.currentCameraMode = CameraModes.CameraMode.locked;
        }
    }

}
