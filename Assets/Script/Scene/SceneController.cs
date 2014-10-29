using UnityEngine;
using System.Collections;

public class SceneController : MonoBehaviour
{
    public static SceneController instance = null;
    //異步對像
    static AsyncOperation async;
    //讀取場景的進度，它的取值範圍在0 - 1 之間。
    int progress = 0;
    //是否要切換場景
    static bool ChanceScene;
    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Update()
    {
        if (ChanceScene)
        {
            //.在這裡計算讀取的進度，
            //progress 的取值範圍在0.1 - 1之間， 但是它不會等於1
            //也就是說progress可能是0.9的時候就直接進入新場景了
            //所以在寫進度條的時候需要注意一下。
            //為了計算百分比 所以直接乘以100即可
            ArcaletSetter.ConnectStatusText.text = "Progress..." + progress + "%";
            progress = (int)(async.progress * 100);
            //有了讀取進度的數值，大家可以自行製作進度條啦。
            //Debug.Log("Progress..." + progress + "%");            
            if (progress == 100)
                ChanceScene = false;
        }
    }
    //注意這裡返回值一定是 IEnumerator
    static IEnumerator loadScene()
    {
        //異步讀取場景。
        async = Application.LoadLevelAsync("TrainScene");
        //讀取完畢後返回， 系統會自動進入C場景
        yield return async;
    }
    public static void GoTrainScene()
    {
        //切換到培養介面
        //Application.LoadLevel("TrainScene");
        //在這裡開啟一個異步任務，
        //進入loadScene方法。
        instance.StartCoroutine(loadScene());
        ChanceScene = true;
        Debug.Log("test1");
    }
}
