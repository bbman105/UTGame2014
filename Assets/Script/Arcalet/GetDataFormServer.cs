using UnityEngine;
using System.Collections;

public partial class ArcaletSetter : MonoBehaviour
{
    static bool IsGetPlayerResourceData;//是否已從Server取得玩家資源
    static bool IsGetEnhanceResourceData;//是否已從Server取得怪獸強化素材
    //場景轉換
    static SceneController scaneController;
    void StartSetGetDataFromServer()//初始設定GetDataFormServer
    {
        IsGetPlayerResourceData = false;
        IsGetEnhanceResourceData = false;
        //場景轉換物件
        scaneController = GameObject.FindGameObjectWithTag("SceneController").GetComponent<SceneController>();
    }
    /// <summary>
    /// 起始向Server取得所有資料
    /// </summary>
    void GetGameDataFormArcaletServer()//起始向Server取得所有資料
    {
        //向Server取得並設定基本資源
        IODataFromArcalet.CheckMonsterInstance();//向Server檢查實例化玩者資源物件
        //向Server取得並設定怪獸強化素材
        IODataFromArcalet.CheckEnhanceResourceInstance();//向Server檢查實例化玩者資源物件
    }
    /// <summary>
    /// 傳入完成的讀取進度種類，1=玩家資源,2=怪獸強化素材
    /// </summary>
    /// <param name="_packageType"></param>
    public static void SetDataProgressCheck(int _packageType)//設定資料進度
    {
        switch(_packageType)
        {
            case 1://玩家資源
                IsGetPlayerResourceData = true;
                break;
            case 2://怪物強化素材
                IsGetEnhanceResourceData = true;
                break;
            default:
                Debug.Log("設定資料讀取進度的類型無法辨識");
                break;
        }
        CheckProgress();//檢查進度，若已經讀取完畢進入遊戲
    }
    static void CheckProgress()//檢查進度，若已經讀取完畢進入遊戲
    {
        Debug.Log("bb");
        if (IsGetPlayerResourceData && IsGetEnhanceResourceData)//已從Server取得玩家資源
        {
            Debug.Log("cc");
            scaneController.GoTrainScene();//進入培養場景
        }
    }
}