using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region game varaible
    public float FollowSpeed = 2f;
    public float ZoomSpeed = 0.1f;
    public float Camera_dist = 5f;
    private Vector2 temp;
    #endregion

    #region reference
    public Transform background;
    public Transform Target;
    public DialogueManager dialogueManager;
    #endregion 
    private void Start()
    {
        this.transform.position = Target.transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        temp = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), new Vector2(Target.position.x, Target.position.y), FollowSpeed * Time.deltaTime);
        transform.position = new Vector3(temp.x,temp.y,-10);
        this.GetComponent<Camera>().orthographicSize = Mathf.Lerp(this.GetComponent<Camera>().orthographicSize, Camera_dist, ZoomSpeed);
        //updating background xy but not z 
        background.position = new Vector3(transform.position.x,transform.position.y,background.position.z);
        if (Target.tag != "Player")
        {
            background.localScale = Vector3.Lerp(background.localScale, new Vector3(2.4f,2.1f,1),0.01f);
        }
        else
            background.localScale = Vector3.Lerp(background.localScale, new Vector3(1.6f, 1.4f, 1), 0.01f);
    }

    public void Zoom(Transform _target,float _zoomspeed,float _camdist)
    {
        Target = _target;
        FollowSpeed = _zoomspeed;
        Camera_dist = _camdist;
    }
}
