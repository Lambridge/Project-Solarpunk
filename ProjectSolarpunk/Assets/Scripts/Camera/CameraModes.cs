using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraModes : MonoBehaviour {

    public enum CameraMode { none, locked, follow }
    public CameraMode currentCameraMode;

    [SerializeField] public Vector3 lockedPosition;
    [SerializeField] public GameObject objectToFollow;

    [SerializeField] bool followXPosition;
    [SerializeField] bool followYPosition;

    // Update is called once per frame
    void Update()
    {
        if (currentCameraMode == CameraMode.follow)
        {
            FollowObject();
        }
        else if (currentCameraMode == CameraMode.locked)
        {
            transform.position = lockedPosition;
        }
    }

    void FollowObject()
    {
        Vector3 newCameraPosition = 
            transform.position;

        if (followXPosition)
        {
            newCameraPosition.x = objectToFollow.transform.position.x;
        }
        if (followYPosition)
        {
            newCameraPosition.y = objectToFollow.transform.position.y;
        }
        newCameraPosition.z = -10f;

        transform.position = newCameraPosition;
    }
}
