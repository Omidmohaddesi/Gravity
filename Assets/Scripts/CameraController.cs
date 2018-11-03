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
    public Animator Curtain_animator;
    #endregion 
    private void Start()
    {
        if (DialogueManager.dialogue_ended == true)
        {
            Curtain_animator.SetBool("dialogue_ended", true); 
            Camera.main.orthographicSize = 5;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        temp = Vector2.Lerp(new Vector2(transform.position.x, transform.position.y), new Vector2(Target.position.x, Target.position.y), FollowSpeed * Time.deltaTime);
        transform.position = new Vector3(temp.x,temp.y,-10);

        if (DialogueManager.dialogue_ended == true)
        {
            Curtain_animator.SetBool("dialogue_ended", DialogueManager.dialogue_ended);
            Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, Camera_dist, ZoomSpeed);
        }

        //updating background xy but not z 
        background.position = new Vector3(transform.position.x,transform.position.y,background.position.z);
    }

    public void Zoom(float target_size,float zoom_speed)
    {
        Camera_dist = target_size;
        ZoomSpeed = zoom_speed;
    }
}
