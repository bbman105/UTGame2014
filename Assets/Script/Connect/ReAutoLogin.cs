using UnityEngine;
using System.Collections;

public class ReAutoLogin : MonoBehaviour
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
        arcaletSetter.ReAutoSysAccount();//由系統自動取得帳戶
    }
}
