using UnityEngine;
using System.Collections;

public partial class ArcaletSetter : MonoBehaviour
{
    //登入需求宣告
    bool onLogin = false;//是否登入中
    bool autoLogin = true;	//是否允許玩家在註冊帳號之後自動登入

    public void StartSetPlayerLogin()//起始設定PlayerLogin
    {
        onLogin = false;
    }

    //使用者登入
    public void ArcaletLaunch(string _accountStr, string _passwordStr)
    {
        if (arcaletGame != null)
            arcaletGame.Dispose();
        arcaletGame = new ArcaletGame(_accountStr, _passwordStr, gguid, sguid, certificate); //輸入所需要的參數
        //偵測連線狀態
        arcaletGame.onStateChanged += OnArcaletStateChanged;
        //指定 CallBack function
        arcaletGame.onCompletion += CallBack_ArcaletLaunch;
        arcaletGame.Launch(); //啟動 arcalet
    }

    /*******************************************************************
    //	當ArcaletGame執行之後,會在CallBack接收到是否成功登入的訊息
    //	code為0表示登入成功 code非0表示登入失敗
    //	登入失敗可參考 Error Code
    //	http://developer.arcalet.com/tutorials/index.asp?maintitleclass=3&dirid=229
    *******************************************************************/
    void CallBack_ArcaletLaunch(int code, ArcaletGame game)
    {
        if (code == 0)
        {
            onLogin = true;
            GetGameDataFormArcaletServer();//起始向Server取得所有資料
            //登入成功時執行的區段
            Debug.Log("登入成功!");
        }
        else
        {
            //登入失敗時執行的區段
            Debug.LogWarning("登入失敗 - Code:" + code);
        }
    }
}
