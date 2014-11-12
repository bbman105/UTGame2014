using UnityEngine;
using System.Collections;

public class TrainSceneCamera : MonoBehaviour
{

    public static GameObject Camera;

    void Awake()
    {
        Camera = gameObject;
        DontDestroyOnLoad(transform.gameObject);//切場景時不清除物件
        LoadingScene.ChangeSceneFunction += ChangeScene;
    }

    public static void ChangeScene()//切換場景
    {
        if (Application.loadedLevelName == "TrainScene")
        {
            Camera.SetActive(true);
        }
        else
        {
            Camera.SetActive(false);
        }
    }
}
