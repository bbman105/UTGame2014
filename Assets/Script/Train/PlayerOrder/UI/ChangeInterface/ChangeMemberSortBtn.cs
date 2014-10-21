using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ChangeMemberSortBtn : MonoBehaviour
{

    public int SortType;//排序方式 0=品質排序;1=等級排序;2=元素排序
    public static Dictionary<int, UISprite> BtnBottomDic = new Dictionary<int, UISprite>();
    static Color inActiveColor = new Color32(49, 96, 117, 255);
    static Color activeColor = new Color32(55, 186, 202, 255);


    void Awake()
    {
        switch (transform.name)
        {
            case "QualitySort":
                SortType = 0;
                break;
            case "LvSort":
                SortType = 1;
                break;
            case "ElementSort":
                SortType = 2;
                break;
            default:
                Debug.Log("無法辨識排序物件名稱");
                break;
        }
        BtnBottomDic.Add(SortType, transform.GetComponentInChildren<UISprite>());
    }

    void OnClick()
    {
        if (PlayerMonster.SmallToBig)
            PlayerMonster.SmallToBig = false;
        else
            PlayerMonster.SmallToBig = true;
        PlayerMonster.SortType = SortType;
        ChangeMemberList.LoadMonsterSprite();
    }
    public static void SetSpriteColor()//設定排序按鈕顏色
    {
        for (int i = 0; i < BtnBottomDic.Count; i++)//將所有按鈕顯示為未選取顏色
        {
            BtnBottomDic[i].color = inActiveColor;
        }
        
        if (BtnBottomDic.ContainsKey(PlayerMonster.SortType))
        {
            BtnBottomDic[PlayerMonster.SortType].color = activeColor;
        }
        
    }
}
