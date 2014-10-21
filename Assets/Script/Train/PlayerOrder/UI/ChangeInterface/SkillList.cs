using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillList : TrainSceneInterfaceUI
{

    static public int SelectedMonsterID { get; set; }
    static public PlayerMonster CurMonster { get; set; }
    //讀入的prefab物件
    static GameObject SkillCardObj;
    static int cardNum;
    //讀入玩家的技能class
    private static Skill curSkill;
    public static Skill CurSkill
    {
        get { return SkillList.curSkill; }
        set { SkillList.curSkill = value; }
    }
    //技能卡 gameobj LIst
    static List<Skill> TmpSkillList;
    static List<GameObject> SkillObjList;
    static Transform Grid;
    //static int cardNum;
    static int theLargestNum;//取cardNum/SkillObjList中較大的數當最大數
    //每張卡的的位置
    static List<Vector3> cardPos;
    //技能卡物件的基本座標
    static Vector3 baseCardPos;
    //技能卡物件的Y軸位移座標
    static Vector3 addCardPOsY;
    //技能卡物件的基本縮放
    static Vector3 baseCardScale;
    //技能卡物件的基本旋轉
    static Quaternion baseCardRot;
    //技能各屬性數值UILabel & Sprite
    static private List<ChangeSkill> changeSkill;
    static private List<UILabel> skillName;
    static private List<UILabel> skillDmg;
    static private List<UILabel> skillDescription;
    static private List<UISprite> SkillBottomSprite;


    public void StartSetting()
    {
        ///////////////////////初始化型別///////////////////////////
        //介面數值初始化
        SkillObjList = new List<GameObject>();
        Grid = skillListUI.transform.FindChild("ScrollView").FindChild("Grid");
        cardPos = new List<Vector3>();
        //cardNum = new int();
        baseCardPos = new Vector3(0, 330, 0);
        addCardPOsY = new Vector3(0, -240, 0);
        baseCardScale = new Vector3(1, 1, 1);
        baseCardRot = Quaternion.Euler(0, 0, 0);
        //技能卡數值初始化
        changeSkill = new List<ChangeSkill>();
        skillName = new List<UILabel>();
        skillDmg = new List<UILabel>();
        skillDescription = new List<UILabel>();
        SkillBottomSprite = new List<UISprite>();
        SkillCardObj = Resources.Load<GameObject>("GameObject/RoomUI/SkillList/skillData");

    }

    public static void LoadSkillData(int _selectedMonsterID)
    {
        SelectedMonsterID = _selectedMonsterID;
        CurMonster = Player.MonsterDic[SelectedMonsterID];

        //抓取擁有的技能數
        TmpSkillList = CurMonster.OwnSkillList;
        cardNum = TmpSkillList.Count;
        if (cardNum == SkillObjList.Count)
        {
            Debug.Log("玩家擁有的技能量不變");
            theLargestNum = cardNum;
            SetSkill();
        }
        else if (cardNum > SkillObjList.Count)
        {
            Debug.Log("玩家擁有的技能量變多需擴充物件");
            theLargestNum = cardNum;
            SetSkill();
        }
        else
        {
            Debug.Log("玩家擁有的技能量變少需取消顯示多出來的物件");
            theLargestNum = SkillObjList.Count;
            SetSkill();
        }

    }
    static void SetSkill()
    {
        ///////////////////////讀取目前玩家卡片的資料/////////////////////////////
        for (int i = 0; i < theLargestNum; i++)
        {
            if (i >= SkillObjList.Count)//擁有技能數量超過現有物件量，建立新物件並讀取技能資料
            {
                //////////////////////////////創建技能卡片，並抓取技能卡及各屬性物件////////////////////////////////
                //設定技能卡位置
                cardPos.Add(baseCardPos + (addCardPOsY * i));
                //創建技能卡
                SkillObjList.Add(Instantiate(SkillCardObj, cardPos[i], baseCardRot) as GameObject);
                //把技能丟到SkillList底下
                //SkillObjList[i].transform.parent = skillListUI.transform;
                SkillObjList[i].transform.parent = Grid;
                //設定技能卡到對的位置上
                SkillObjList[i].transform.localPosition = cardPos[i];
                //設定技能卡縮放
                SkillObjList[i].transform.localScale = baseCardScale;
                //設定技能卡名稱
                SkillObjList[i].name = "skill" + i;
                //抓取SkillList上技能卡各屬性數值的物件
                skillName.Add(SkillObjList[i].transform.FindChild("skillName").GetComponent<UILabel>());
                skillDmg.Add(SkillObjList[i].transform.FindChild("damageNum").GetComponent<UILabel>());
                skillDescription.Add(SkillObjList[i].transform.FindChild("introduction").GetComponent<UILabel>());
                SkillBottomSprite.Add(SkillObjList[i].transform.FindChild("bottom").GetComponent<UISprite>());
                changeSkill.Add(SkillObjList[i].GetComponent<ChangeSkill>());
            }

            if (i < cardNum)
            {

                //顯示技能卡
                SkillObjList[i].SetActive(true);
                //設定技能卡上各屬性
                skillName[i].text = CurMonster.OwnSkillList[i].SkillName;
                skillDmg[i].text = string.Format("{0}", (int)(CurMonster.OwnSkillList[i].Damage * CurMonster.Brutal));
                skillDescription[i].text = CurMonster.OwnSkillList[i].Description;
                changeSkill[i].ChangeMonsterID = SelectedMonsterID;
                changeSkill[i].ChangeSpellID = CurMonster.OwnSkillList[i].SpellID;

                //設定技能卡元素背景
                SkillBottomSprite[i].spriteName = "skillchangeBG" + CurMonster.MainElement.ToString();
            }
            else//如果技能清單變少，則隱藏多的技能
            {
                //取消顯示多的技能卡
                SkillObjList[i].SetActive(false);
            }

        }
    }
}
