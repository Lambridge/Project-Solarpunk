using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModeTrigger : MonoBehaviour {

    [SerializeField] bool becomeFixed;
    [SerializeField] bool becomeFollowing;
    [SerializeField] Vector2 newCameraPosition;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            CameraModes camera;
            camera = GameObject.Find("Main Camera").GetComponent<CameraModes>();
            if (becomeFixed)
            {
                camera.lockedPosition = new Vector3(newCameraPosition.x, newCameraPosition.y, -10f);
                camera.currentCameraMode = CameraModes.CameraMode.locked;
            }
            if (becomeFollowing)
            {
                GameObject player = GameObject.Find("Player");
                camera.objectToFollow = player;
                camera.currentCameraMode = CameraModes.CameraMode.follow;
            }

            
        }
    }

}
