using UnityEngine;
using System.Collections;

public class EnhanceOKButton : MonoBehaviour
{

    void OnClick()
    {
        EnhanceUI.HideEnhanceMessageBox();//隱藏強化確認視窗
        EnhanceUI.Enhance();//強化並顯示介面
    }

}
