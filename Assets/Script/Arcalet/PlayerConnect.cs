using UnityEngine;
using System.Collections;

public partial class ArcaletSetter : MonoBehaviour
{

    void OnArcaletStateChanged(int state, int code, ArcaletGame game)
    {
        //當連線狀態發生改變時，OnStateChange會通知程式
        Debug.Log("Arcalet狀態改變 - State:" + state + " Code:" + code);


        if (state >= 100 && state <= 600)
        {
            //ConnectStatusText.text = "與伺服器連線中...";
        }

        if (state >= 900)
        {
            //一旦state大於900，表示目前已經處於斷線狀態
            ConnectStatusText.text = "與伺服器連線失敗";
            //Debug.LogWarning("Arcalet game disconnect");
            //barLeagth = 0;
        }

    }
}
