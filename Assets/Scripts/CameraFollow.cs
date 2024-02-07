using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    public Vector3 cameraDistance;

    // Use this for initialization
    void Start () {
    }

    void LateUpdate ()
    {
        transform.position = player.transform.position + cameraDistance;
        transform.Rotate(0, 0, 0);
    }
}