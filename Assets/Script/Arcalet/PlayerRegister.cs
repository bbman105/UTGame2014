using UnityEngine;
using System.Collections;

public partial class ArcaletSetter : MonoBehaviour
{
    //系統自動取得帳號宣告
    string sysAccount;//系統取得帳號
    string sysPassword;//系統取得密碼

    /// <summary>
    /// 註冊功能必須要有 帳號/密碼/E-Mail
    /// </summary>
    public void Regist(string _accountStr, string _passwordStr, string _mailStr)
    {
        /*******************************************************************
        * 註冊功能必須要有 帳號/密碼/E-Mail
        * 註冊資訊限制可參考以下網址
        * http://developer.arcalet.com/tutorials/index.asp?maintitleclass=3&dirid=229
        * Error Code : 10035~10050
        *******************************************************************/
        //在此製作一個Token, 當程式呼叫CallBack時可以再將Token取出
        string[] registToken = new string[] { _accountStr, _passwordStr, _mailStr };
        ArcaletSystem.ApplyNewUser(gguid, certificate, _accountStr, _passwordStr,
         _mailStr, CB_Regist, registToken);
    }

    /// <summary>
    /// 註冊結果的CallBack
    /// </summary>
    void CB_Regist(int code, object token)
    {
        /*******************************************************************
        * 註冊結果的CallBack
        *******************************************************************/
        //Code為0表示註冊成功
        if (code == 0)
        {
            string[] reg = token as string[]; //取回Token
            string acc = reg[0];
            string pw = reg[1];
            string mail = reg[2];
            Debug.Log("註冊成功 - 帳號:" + acc + " / 密碼:" +
             pw + " E-Mail" + mail);

            //如果希望玩家在註冊完成之後自動登入可以利用Token取得玩家的註冊資訊並進行登入
            if (autoLogin) ArcaletLaunch(acc, pw);
        }
        //Code非0表示註冊失敗
        else
        {
            Debug.LogWarning("註冊失敗 - Error:" + code);
        }
    }
    /// <summary>
    /// 由Client端紀錄的帳號自動登入，若沒有帳號紀錄則生成的一個帳號
    /// </summary>
    public void AutoSysAccount()
    {
        if (!PlayerPrefs.HasKey("PlayerID") || !PlayerPrefs.HasKey("PlayerPassword"))//如果玩家Client端沒有登入帳號或密碼
        {
            /*******************************************************************
            * 系統帳號是由arcaet自動生成的一個帳號
            * 系統帳號可轉成正式帳號,轉換帳號時可以自行命名,並且保留所有玩家資料
            *******************************************************************/
            ArcaletSystem.ApplyNewUser(gguid, certificate, CallBack_AutoSysAccount, null);
        }
        else
        {
            Debug.Log(PlayerPrefs.GetString("PlayerID"));
            Debug.Log(PlayerPrefs.GetString("PlayerPassword"));
            ArcaletLaunch(PlayerPrefs.GetString("PlayerID"), PlayerPrefs.GetString("PlayerPassword")); //使用Client帳號密碼進行登入
        }
    }
    /// <summary>
    /// 系統帳號是由arcaet自動生成的一個帳號
    /// </summary>
    public void ReAutoSysAccount()
    {
        /*******************************************************************
        * 系統帳號是由arcaet自動生成的一個帳號
        * 系統帳號可轉成正式帳號,轉換帳號時可以自行命名,並且保留所有玩家資料
        *******************************************************************/
        ArcaletSystem.ApplyNewUser(gguid, certificate, CallBack_AutoSysAccount, null);
    }
    /// <summary>
    /// 取得系統帳號的CallBack
    /// </summary>
    void CallBack_AutoSysAccount(int code, object data, object token)
    {
        //Code為0表示取得帳號成功
        if (code == 0)
        {
            Hashtable ht = data as Hashtable; //取得帳號成功時將返回一組Hashtable
            sysAccount = ht["userid"].ToString();
            sysPassword = ht["passwd"].ToString();
            PlayerPrefs.SetString("PlayerID", sysAccount);
            PlayerPrefs.SetString("PlayerPassword", sysPassword);
            Debug.Log(sysAccount);
            Debug.Log(sysPassword);
            ArcaletLaunch(sysAccount, sysPassword); //可以在取得系統帳號之後執行自動登入
        }
        //Code非0表示取得帳號失敗
        else
        {
            Debug.LogWarning("自動取得帳號失敗 - Error:" + code);
        }
    }
    //升級為正式帳號
    void UpgardeSysAccount(string _accountStr, string _passwordStr, string mail)
    {
        ArcaletSystem.UpgradeUser(arcaletGame, _accountStr, _passwordStr, mail, CallBack_UpgardeSysAccount, null);
    }

    //升級帳號的CallBack
    void CallBack_UpgardeSysAccount(int code, object token)
    {
        //Code為0表示升級帳號成功
        if (code == 0)
        {
            Debug.Log("成功將系統帳戶轉為正式Arcalet帳戶");
        }
        //Code非0表示升級帳號失敗
        else
        {
            Debug.LogWarning("將系統帳戶轉為正式Arcalet帳戶時發生錯誤! - Error:" + code);
        }
    }
}
