using UnityEngine;
using System.Collections;

public partial class ArcaletSetter : MonoBehaviour
{

    void OnArcaletStateChanged(int state, int code, ArcaletGame game)
    {
        //當連線狀態發生改變時，OnStateChange會通知程式
        Debug.Log("Arcalet狀態改變 - State:" + state + " Code:" + code);
        if (state <= 600)//未登入
        {
            UnLoginState(state);//未連線執行
        }
        else if (state >= 900)//斷線
        {
            //一旦state大於900，表示目前已經處於斷線狀態
            ConnectStatusText.text = "與伺服器連線失敗";
        }

    }
    void UnLoginState(int _state)//未連線時執行
    {
        switch (_state)
        {
            case 0:
                ConnectStatusText.text = "尚未連線...";
                break;
            case 100:
                ConnectStatusText.text = "嘗試與伺服器連線中...";
                break;
            case 200:
                ConnectStatusText.text = "已連線至伺服器...";
                break;
            case 300:
                ConnectStatusText.text = "進入 Binary Mode...";
                break;
            case 400:
                ConnectStatusText.text = "已登入至伺服器...";
                break;
            case 500:
                ConnectStatusText.text = "進入遊戲場景...";
                break;
            case 600:
                ConnectStatusText.text = "向伺服器取得資料...";
                break;
            default:
                break;
        }
    }

}
