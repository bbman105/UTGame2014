using UnityEngine;
using System.Collections;

public class CtrlPower
{

    private static bool[] CanCtrl;
    public static bool GetCanCtrl(int _index)
    {
        if (_index >= CanCtrl.Length || _index < 0)
            return false;
        return CanCtrl[_index];
    }
    static GameObject playerTouchCtrl;

    public static void StartSetting()
    {
        //0=培養介面 , 1=事件介面 , 2=冒險介面 , 3=怪獸編輯介面 , 4=進化介面, 7=編輯房間介面,20=底下UI控制介面 -1=關閉所有控制權
        CanCtrl = new bool[21];
        playerTouchCtrl = GameObject.FindGameObjectWithTag("PlayerCtrl");
        StartCtrl(0);//控制權回到培養界面
        TrainScene.ChangeSceneFunction += ChangeScene;
    }

    public static void ChangeScene()//切換場景
    {
        if (Application.loadedLevelName == "TrainScene")
        {
            if (!GetCanCtrl(4))
                StartCtrl(2);//控制權回到冒險界面
        }
    }
    public static void StopCtrl(int _index)//關閉介面
    {
        if (_index < CanCtrl.Length && _index >= 0)
            CanCtrl[_index] = false;
    }

    public static void StartCtrl(int startNum, bool onlyStart)//開始控制介面，開啟onlyStart時，底下UI控制介面控制權也會被關閉
    {
        if (onlyStart)
        {
            if (startNum < CanCtrl.Length - 1 && startNum >= 0)
            {
                for (int i = 0; i < CanCtrl.Length; i++)
                {
                    CanCtrl[i] = false;
                }
                CanCtrl[startNum] = true;
                if (CanCtrl[0])
                {
                    PlayerCtrl.CanCtrl = true;
                    playerTouchCtrl.SetActive(true);
                }
                else
                {
                    PlayerCtrl.CanCtrl = false;
                    playerTouchCtrl.SetActive(false);
                }
            }
        }
        else
        {
            if (startNum < CanCtrl.Length - 1 && startNum >= 0)
            {
                for (int i = 0; i < CanCtrl.Length; i++)
                {
                    CanCtrl[i] = false;
                }
                CanCtrl[startNum] = true;
                CanCtrl[20] = true;
                if (CanCtrl[0])
                {
                    PlayerCtrl.CanCtrl = true;
                    playerTouchCtrl.SetActive(true);
                }
                else
                {
                    PlayerCtrl.CanCtrl = false;
                    playerTouchCtrl.SetActive(false);
                }

            }
            else
            {
                if (startNum == -1)
                {
                    for (int i = 0; i < CanCtrl.Length; i++)
                    {
                        CanCtrl[i] = false;
                    }
                }
            }
        }
    }
    public static void StartCtrl(int startNum)//開始控制介面，除非startNum為-1，否則不會關掉底下UI控制介面
    {
        if (startNum < CanCtrl.Length - 1 && startNum >= 0)
        {
            for (int i = 0; i < CanCtrl.Length; i++)
            {
                CanCtrl[i] = false;
            }
            CanCtrl[startNum] = true;
            CanCtrl[20] = true;
            if (CanCtrl[0])
            {
                PlayerCtrl.CanCtrl = true;
                playerTouchCtrl.SetActive(true);
            }
            else
            {
                PlayerCtrl.CanCtrl = false;
                playerTouchCtrl.SetActive(false);
            }
        }
        else
        {
            if (startNum == -1)
            {
                for (int i = 0; i < CanCtrl.Length; i++)
                {
                    CanCtrl[i] = false;
                }
            }
        }
    }
}
