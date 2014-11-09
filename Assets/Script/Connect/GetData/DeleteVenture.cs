using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public partial class IODataFromArcalet
{

    /// <summary>
    /// 移除冒險進度Item
    /// </summary>
    static void CallBack_DeleteVentureInstance(int code, object token)
    {
        //Code為0表示建立資料成功
        if (code == 0)
        {
            Debug.Log("移除冒險物件成功");
        }
        //Code非0表示建立資料失敗
        else
        {
            Debug.LogWarning("移除PlayerVenture Item失敗 Failed - Error:" + code);
        }
    }
}
