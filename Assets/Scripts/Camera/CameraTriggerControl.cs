using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTriggerControl : MonoBehaviour {
    public Transform cam_1;
    public float cam_zoomspeed=0.2f;
    public float cam_dist = 8;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cam_1")
        {
            Camera.main.gameObject.GetComponent<CameraController>().Zoom(cam_1.transform, cam_zoomspeed, cam_dist);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cam_1")
        {
            Camera.main.gameObject.GetComponent<CameraController>().Zoom(this.transform, 2f, 5f);
        }
    }

}
