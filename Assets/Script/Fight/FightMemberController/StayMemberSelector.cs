using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StayMemberSelector : MemberSelector
{
    //腳色Icon冷卻Label
    private UILabel cdLabel;
    private int showCdNumber;//頭像上顯示的CD值

    void Update()
    {
        ToFightCDFunction();
    }

    void ToFightCDFunction()
    {
        if(OnCD)
        {
            if(CDTimer>0)
            {
                if (showCdNumber != (int)CDTimer)
                {
                    showCdNumber = (int)CDTimer;
                    cdLabel.text = showCdNumber.ToString();
                }
                CDTimer -= Time.deltaTime;                
            }
            else
            {
                CDTimer = CDTime;
                UnLockBtn();//設定按鈕為原來顏色，代表CD結束可以再次出戰
                OnCD = false;
            }
        }
    }
    void OnClick()
    {
        if (CountCondition == 0)//CountDown.EndCount(倒數結束)為false時，可設定預設出戰怪物
        {
            if (!MemberOnFight[memberRank] && CanAddMember)
            {
                BtnShower(true, memberRank);//顯示已出戰隊員按鈕，取消顯示未出戰隊員按鈕
                speciesModel.SetSelectedColor(true);//設定此怪獸顏色恢復正常
                MemberOnFight[memberRank] = true;//設定此怪獸出場
                CheckAssignMember();//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
            }
        }
        else if (CountCondition == 2)//戰鬥中
        {
            if (!MemberOnFight[memberRank] && CanAddMember && !OnCD)
            {
                ToFight();//隊員出戰
            }
        }
    }

    void ToFight()//隊員出戰
    {
        SetCantCollideToWall();//設定怪獸進場，不受到周圍碰撞影響，且擁有剛體狀態，不會被擊退
        status.transform.position = new Vector3(-4.5f, 3, 0);//設定為出場位置
        speciesModel.SetSelectedColor(true);//設定此怪獸顏色恢復正常
        status.gameObject.SetActive(true);//顯示此怪獸
        BtnShower(true, memberRank);//顯示已出戰隊員按鈕，取消顯示未出戰隊員按鈕
        MemberOnFight[memberRank] = true;//設定此怪獸出場
        CheckAssignMember();//檢查出場怪獸數量，判斷是否可指派隊員撤退或隊員出戰
    }
    void SetCantCollideToWall()//設定怪獸進場，不受到周圍碰撞影響，且擁有剛體狀態，不會被擊退
    {
        status.rigidStatus.SetEnterRigid();//給予鋼體狀態
        memberGameobject.layer = LayerMask.NameToLayer("P1CharaEnter");//設定圖層為可穿透牆壁
    }
    public override void SetMember(int _memberRank)
    {
        base.SetMember(_memberRank);
        //設定非預設出戰怪物壓灰
        if (MemberOnFight[memberRank])
        {
            HideBtn();
            speciesModel.SetSelectedColor(true);
        }
        else
        {
            ShowBtn();
            speciesModel.SetSelectedColor(false);
        }
        SetBtnIcon();//設置怪獸按鈕Icon
        StayMemberSelectors.Add(memberRank, this);
        MemberStatusDic.Add(this.memberRank, status);//將此隊員狀態加入字典
    }
    protected override void SetBtnIcon()
    {
        base.SetBtnIcon();
        cdLabel = myTransform.FindChild("cd").GetComponent<UILabel>();
        cdLabel.enabled = false;//隱藏CD秒數
        if (!StayHealthSpriteDic.ContainsKey(memberRank))
        {
            StayHealthSpriteDic.Add(memberRank, myTransform.FindChild("health").GetComponent<UISprite>());
            StayHealthSpriteDic[memberRank].atlas = Resources.Load<UIAtlas>("Atlas/FightScene/HealthBar/HealthBar");
        }
    }
    public override void LockBtn()
    {
        base.LockBtn();
        cdLabel.enabled = true;//顯示CD秒數
    }
    public override void UnLockBtn()
    {
        base.UnLockBtn();
        cdLabel.enabled = false;//隱藏CD秒數
    }
}
