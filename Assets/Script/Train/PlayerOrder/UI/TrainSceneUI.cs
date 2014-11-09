using UnityEngine;
using System.Collections;

public class TrainSceneUI : MonoBehaviour
{

    public static GameObject UI;

    void Awake()
    {
        UI = gameObject;
        DontDestroyOnLoad(transform.gameObject);//切場景時不清除物件
        LoadingScene.ChangeSceneFunction += ChangeScene;
    }

    public static void ChangeScene()//切換場景
    {
        if (Application.loadedLevelName == "TrainScene")
        {
            UI.SetActive(true);
        }
        else
        {
            UI.SetActive(false);
        }
    }


}
