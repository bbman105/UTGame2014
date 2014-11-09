using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;


public class TrainScene : MonoBehaviour
{

    static bool IsStartSet;
    //起始設定
    void Start()
    {
        if (LoadingScene.ChangeSceneFunction != null)
            LoadingScene.ChangeSceneFunction();
        if (!IsStartSet)
        {
            LoadingScene.LoadDialogueXml();
            LoadingScene.LoadTouchFeedBackXml();
            LoadingScene.LoadEventFeedBack();
            LoadingScene.SpawnRoomResourceObjList();//建立資源清單物件
            Player.PlayerSourceSetting(1);//設定玩者資源
            TrainSceneInterfaceUI.StartSetUI();//建立UI物件
            SetPlayerMonster();//初始設定玩者擁有的所有怪獸
            LoadingScene.AddScripts();
            Player.StartSet();
            IsStartSet = true;
        }
    }
    void SetPlayerMonster()//初始設定玩者擁有的所有怪獸
    {
        List<int> dicBuffer = new List<int>(Player.MonsterDic.Keys);
        foreach (int key in dicBuffer)
        {
            Player.MonsterDic[key].StartSetMonster();//初始設定怪獸
        }
    }
}
