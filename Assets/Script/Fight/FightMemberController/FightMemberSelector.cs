using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightMemberSelector : MemberSelector
{
    void OnClick()
    {
        if (CountCondition == 0)//CountDown.EndCount(倒數結束)為false時，可設定預設出戰怪物
        {
            if (MemberOnFight[memberRank] && CanRetreatMember)
            {
                BtnShower(false, memberRank);//取消顯示已出戰隊員按鈕，顯示未出戰隊員按鈕
                speciesModel.SetSelectedColor(false);//此怪獸顏色壓灰
                MemberOnFight[memberRank] = false;//設定此怪獸退場
                CheckAssignMember();//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
            }
        }
        else if (CountCondition == 2)//戰鬥中
        {
            if (MemberOnFight[memberRank] && CanRetreatMember)
            {
                ToStay();//退場
            }
        }
    }
    void ToStay()//退場
    {
        StayMemberSelectors[memberRank].OnCD = true;//開始計時CD8秒
        StayMemberSelectors[memberRank].LockBtn();//設定按鈕為灰色代表在CD無法招出
        status.transform.position = new Vector3(-4.5f, 3, 0);//設定為出場位置
        speciesModel.SetSelectedColor(false);//此怪獸顏色壓灰
        status.gameObject.SetActive(false);//隱藏怪獸
        BtnShower(false, memberRank);//取消顯示已出戰隊員按鈕，顯示未出戰隊員按鈕
        MemberOnFight[memberRank] = false;//設定此怪獸退場
        CheckAssignMember();//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
    }

    public override void SetMember(int _memberRank)
    {
        base.SetMember(_memberRank);
        //設定非預設出戰怪物壓灰
        if (MemberOnFight[memberRank])
        {
            ShowBtn();
        }
        else
        {
            HideBtn();
        }
        SetBtnIcon();//設置怪獸按鈕Icon
        FightMemberSelectors.Add(memberRank, this);
    }
    protected override void SetBtnIcon()
    {
        base.SetBtnIcon();
        if (!FightHealthSpriteDic.ContainsKey(memberRank))
        {
            FightHealthSpriteDic.Add(memberRank, myTransform.FindChild("health").GetComponent<UISprite>());
            FightHealthSpriteDic[memberRank].atlas = Resources.Load<UIAtlas>("Atlas/FightScene/HealthBar/HealthBar");
        }
    }
}
