using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    private static bool created = false;
    public Transform player;
    public Transform[] checkpoints;
    [HideInInspector]
    public static Vector3 spawnLocation = new Vector3(2,7,0);
    public static bool StartDialogueFinished = false;

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
        //Teleport();
    }

    public void Teleport()
    {
        //teleport for dev
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (checkpoints[0] != null)
            {
                player.position = checkpoints[0].position;
                Camera.main.transform.position = player.position;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && checkpoints.Length >= 2)
        {
            if (checkpoints[1] != null)
            {
                player.position = checkpoints[1].position;
                Camera.main.transform.position = player.position;
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha3)&&checkpoints.Length>=3)
        {
            if (checkpoints[2] != null)
            {
                player.position = checkpoints[2].position;
                Camera.main.transform.position = player.position;
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha4) && checkpoints.Length >= 4)
        {
            if (checkpoints[3] != null)
            {
                player.position = checkpoints[3].position;
                Camera.main.transform.position = player.position;
            }
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5) && checkpoints.Length >= 5)
        {
            if (checkpoints[4] != null)
            {
                player.position = checkpoints[4].position;
                Camera.main.transform.position = player.position;
            }
        }
        else
            return;
    }

    public void LoadFirstScene()
    {
        Load0_1();
    }

    public void LoadNextScene()
    {
        if (SceneManager.GetActiveScene().name == "Level_0_1")
            Load0_2();
        else if (SceneManager.GetActiveScene().name == "Level_0_2")
            Load0_3();
        else if (SceneManager.GetActiveScene().name == "Level_0_3")
            Load1_1();
        else if (SceneManager.GetActiveScene().name == "Level_1_1")
            Load1_2();
        else if (SceneManager.GetActiveScene().name == "Level_1_2")
            Load2_1();
        else if (SceneManager.GetActiveScene().name == "Level_2_1")
            Load2_2();        
    }

    public void ReloadCurrentScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);       
    }

    #region Helper function
    //dumb ways to do it
    public void Load0_1()
    {
        SceneManager.LoadScene("Level_0_1");
    }

    public void Load0_2()
    {
        SceneManager.LoadScene("Level_0_2");
    }

    public void Load0_3()
    {
        SceneManager.LoadScene("Level_0_3");
    }

    public void Load1_1()
    {
        SceneManager.LoadScene("Level_1_1");
    }

    public void Load1_2()
    {
        SceneManager.LoadScene("Level_1_2");
    }

    public void Load2_1()
    {
        SceneManager.LoadScene("Level_2_1");
    }

    public void Load2_2()
    {
        SceneManager.LoadScene("Level_2_2");
    }
    #endregion
}