using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerControl : MonoBehaviour {
    public float cam_zoomspeed=0.2f;
    public float cam_dist = 8;
    public Camera cam;
    public Transform player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            cam.GetComponent<CameraController>().Zoom(this.transform, cam_zoomspeed, cam_dist);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            cam.GetComponent<CameraController>().Zoom(player.transform, 2f, 5f);
        }
    }

}
