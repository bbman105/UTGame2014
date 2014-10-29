using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public partial class IODataFromArcalet
{
    /// <summary>
    /// 實例化一個新物品，傳入iGuid
    /// </summary>
    /// <param name="_iGuid"></param>
    static void NewItemInstance(string _iGuid)//實例化一個新物品，傳入iGuid
    {
        ArcaletItem.NewItemInstance(ArcaletSetter.arcaletGame, _iGuid, CallBack_NewItemInstance, null);
    }
    /*******************************************************************
    *	同一個Item可以Instantiate不只一組
    *	每個Item 在實例成功時將會產生一組ItemID
    *	ItemID在寫入資料到Item時被需要當成一個索引
    *******************************************************************/
    static void CallBack_NewItemInstance(int code, object data, object token)
    {
        //Code為0表示建立資料成功
        if (code == 0)
        {
            if (int.TryParse(data.ToString(), out PlayerresourceItemID))
            {
                Debug.Log(string.Format("實例化物品成功!實例化物品ID{0}", PlayerresourceItemID));
                GetPlayerAllResource();//向Server取得玩者所有資源
            }
            else
            {
                Debug.Log(string.Format("取得實例化物品{0}的ID時失敗", data));
            }
        }
        //Code非0表示建立資料失敗
        else
        {
            Debug.LogWarning("實例化物品失敗 Failed - Error:" + code);
        }
    }
}
