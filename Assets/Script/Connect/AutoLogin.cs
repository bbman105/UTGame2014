using UnityEngine;
using System.Collections;

public class AutoLogin : MonoBehaviour
{
    ArcaletSetter arcaletSetter;
    void Awake()
    {
        //抓取ArcaletSet Component
        arcaletSetter = GameObject.FindGameObjectWithTag("ConnectToServer").GetComponent<ArcaletSetter>();
    }
    //按下登入
    void OnClick()
    {
        ArcaletSetter.ConnectStatusText.text = "與伺服器連線中...";
        arcaletSetter.AutoSysAccount();//由Client端紀錄的帳號自動登入，若沒有帳號紀錄則生成的一個帳號
    }
}
