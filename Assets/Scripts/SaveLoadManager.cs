using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    private static bool created = false;
    public Transform player;
    public Transform[] checkpoints;

    [HideInInspector]
    public static Vector3 spawnLocation = new Vector3(2,7,0);
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(this.gameObject);
            created = true;
        }
    }

    private void Update()
    {
        //teleport for dev
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            player.position = checkpoints[0].position;
            Camera.main.transform.position = player.position;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2)){
            player.position = checkpoints[1].position;
            Camera.main.transform.position = player.position;

        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            player.position = checkpoints[2].position;
            Camera.main.transform.position = player.position;

        }
    }
}