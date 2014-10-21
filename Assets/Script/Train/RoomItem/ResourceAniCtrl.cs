using UnityEngine;
using System.Collections;

public class ResourceAniCtrl : MonoBehaviour
{
    public string AniStr;
    Animator myAni;
    void Awake()
    {
        myAni = transform.GetComponent<Animator>();
    }
    public void SetAni()//設定動畫
    {
        try
        {
            myAni.SetTrigger(AniStr);
        }
        catch
        {
            Debug.Log("SetTrigger不存在的動畫名稱");
        }
    }
}
