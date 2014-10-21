using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Formation : TrainSceneInterfaceUI
{
    //隊伍List
    static List<PlayerTeam> playerTeamList;
    //讀入的prefab物件
    static GameObject MemeberCardObj;
    static GameObject MemeberObj;
    static GameObject EmptyObj;
    //目前選擇的隊伍
    public static PlayerTeam curTeam;
    public static PlayerMonster curMember;
    //隊員 gameobj LIst
    static GameObject[] memberCard;
    static GameObject[] member;
    static GameObject[] empty;
    //交換後的怪物ID
    static int changeMonstID;
    public static int ChangeMonstID
    {
        get { return Formation.changeMonstID; }
        set { Formation.changeMonstID = value; }
    }

    private static int curTmpTeamID;
    public static int CurTmpTeamID
    {
        get { return Formation.curTmpTeamID; }
        set
        {
            if (value > (Player.TeamNum - 1))
            {
                Formation.curTmpTeamID = 0;
            }
            else if (value < 0)
            {
                Formation.curTmpTeamID = (Player.TeamDic.Count - 1);
            }
            else { Formation.curTmpTeamID = value; }
        }
    }
    //每個隊員、隊員卡顯示的的位置
    static Vector3[] charaPos;
    static Vector3[] cardPos;
    //腳色卡物件的基本座標
    static Vector3 baseCharaPos;
    static Vector3 baseCardPos;
    //腳色卡物件的Y軸位移座標
    static Vector3 addCharaPosX;
    static Vector3 addCardPOsY;
    //腳色卡物件的基本縮放
    static Vector3 baseCharaScale;
    static Vector3 baseCardScale;
    //腳色卡物件的基本旋轉
    static Quaternion baseCharaRot;
    static Quaternion baseCardRot;
    //腳色icon圖的sprite
    static private UISprite[] charaSprite;
    static private UISprite[] iconCharaSprite;
    //腳色icon邊框圖的sprite
    static private UISprite[] bannerSprite;
    //腳色icon底圖的sprite
    static private UISprite[] bottomSprite;
    //腳色icon元素icon的sprite
    static private UISprite[] elementSprite;
    //腳色icon等級label
    static private UILabel[] lvLabel;
    //腳色各屬性數值UILabel 與 經驗UISprite
    static private UILabel[] nameLabel;
    //static private UILabel[] raceLabel;
    //static private UILabel[] sizeLabel;
    static private UILabel[] lifeLabel;
    static private UILabel[] brutalLabel;
    static private UILabel[] aggressiveLabel;
    static private UILabel[] resistanceLabel;
    static private UISprite[] expSprite;
    //隊伍
    static private UILabel teamNo;
    static private UILabel teamName;

    public void StartSetting()
    {
        ///////////////////////初始化型別///////////////////////////
        //介面數值初始化
        member = new GameObject[3];
        memberCard = new GameObject[3];
        empty = new GameObject[3];
        cardPos = new Vector3[3];
        charaPos = new Vector3[3];
        baseCardPos = new Vector3(0, 110, 0);
        baseCharaPos = new Vector3(-160, 350, 0);
        addCardPOsY = new Vector3(0, -210, 0);
        addCharaPosX = new Vector3(160, 0, 0);
        baseCardScale = new Vector3(1, 1, 1);
        baseCardRot = Quaternion.Euler(0, 0, 0);
        baseCharaScale = new Vector3(1, 1, 1);
        baseCharaRot = Quaternion.Euler(0, 0, 0);

        //初始化腳色卡上的ICon各屬性數值的物件
        iconCharaSprite = new UISprite[3];
        bannerSprite = new UISprite[3];
        bottomSprite = new UISprite[3];
        elementSprite = new UISprite[3];
        lvLabel = new UILabel[3];

        //初始化腳色卡各屬性數值的物件
        nameLabel = new UILabel[3];
        //raceLabel = new UILabel[3];
        //sizeLabel = new UILabel[3];
        lifeLabel = new UILabel[3];
        brutalLabel = new UILabel[3];
        aggressiveLabel = new UILabel[3];
        resistanceLabel = new UILabel[3];
        expSprite = new UISprite[3];

        //初始化腳色圖像
        charaSprite = new UISprite[3];


        MemeberCardObj = Resources.Load<GameObject>("GameObject/RoomUI/memberList/memberCard");
        MemeberObj = Resources.Load<GameObject>("GameObject/RoomUI/memberList/member");
        EmptyObj = Resources.Load<GameObject>("GameObject/RoomUI/memberList/empty");
        CurTmpTeamID = 0;
        //將玩者隊伍字典存入List中
        playerTeamList = new List<PlayerTeam>();
        //////////////////////////////創建腳色卡片，並抓取腳色卡及各屬性物件////////////////////////////////
        for (int i = 0; i < 3; i++)
        {
            //////////////////////////////創建腳色卡片，並抓取腳色卡及各屬性物件////////////////////////////////
            //設定腳色卡位置
            cardPos[i] = baseCardPos + (addCardPOsY * i);
            //創建腳色卡
            memberCard[i] = Instantiate(MemeberCardObj, cardPos[i], baseCardRot) as GameObject;
            //創建空腳色卡
            empty[i] = Instantiate(EmptyObj, cardPos[i], baseCardRot) as GameObject;
            //把腳色丟到formation底下
            memberCard[i].transform.parent = formationUI.transform;
            empty[i].transform.parent = formationUI.transform;
            //設定腳色卡到對的位置上
            memberCard[i].transform.localPosition = cardPos[i];
            empty[i].transform.localPosition = cardPos[i];
            //設定腳色卡縮放
            memberCard[i].transform.localScale = baseCardScale;
            empty[i].transform.localScale = baseCardScale;
            //設定腳色卡名稱
            memberCard[i].name = i.ToString();
            empty[i].name = "empty" + i.ToString();
            //設定此怪物卡在隊伍中的排序
            memberCard[i].GetComponent<GoChangeMemberList>().ChangeMemberRank = i;
            empty[i].GetComponent<GoChangeMemberList>().ChangeMemberRank = i;
            //抓取formation上腳色卡的ICon各屬性數值的物件
            iconCharaSprite[i] = formationUI.transform.FindChild(i.ToString()).FindChild("chara").FindChild("chara").GetComponent<UISprite>();
            bannerSprite[i] = formationUI.transform.FindChild(i.ToString()).FindChild("chara").FindChild("banner").GetComponent<UISprite>();
            bottomSprite[i] = formationUI.transform.FindChild(i.ToString()).FindChild("chara").FindChild("bottom").GetComponent<UISprite>();
            elementSprite[i] = formationUI.transform.FindChild(i.ToString()).FindChild("chara").FindChild("element").GetComponent<UISprite>();
            elementSprite[i].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
            bannerSprite[i].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
            bottomSprite[i].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");

            //抓取formation裡腳色卡各屬性數值的物件
            nameLabel[i] = formationUI.transform.FindChild(i.ToString()).FindChild("name").GetComponent<UILabel>();
            //raceLabel[i] = RoomInterfaceUI.formationUI.transform.FindChild(i.ToString()).FindChild("race").GetComponent<UILabel>();
            //sizeLabel[i] = RoomInterfaceUI.formationUI.transform.FindChild(i.ToString()).FindChild("size").GetComponent<UILabel>();
            lifeLabel[i] = formationUI.transform.FindChild(i.ToString()).FindChild("life").GetComponent<UILabel>();
            brutalLabel[i] = formationUI.transform.FindChild(i.ToString()).FindChild("brutal").GetComponent<UILabel>();
            aggressiveLabel[i] = formationUI.transform.FindChild(i.ToString()).FindChild("aggressive").GetComponent<UILabel>();
            resistanceLabel[i] = formationUI.transform.FindChild(i.ToString()).FindChild("resistance").GetComponent<UILabel>();
            expSprite[i] = formationUI.transform.FindChild(i.ToString()).FindChild("exp").GetComponent<UISprite>();
            lvLabel[i] = formationUI.transform.FindChild(i.ToString()).FindChild("lv").GetComponent<UILabel>();
            //////////////////////////////創建腳色，並抓取腳色及各屬性物件////////////////////////////////
            //設定腳色位置
            charaPos[i] = baseCharaPos + (addCharaPosX * (2 - i));
            //創建腳色
            member[i] = Instantiate(MemeberObj, charaPos[i], baseCharaRot) as GameObject;
            //設定腳色物件名稱
            member[i].name = "Member" + i.ToString();
            //把腳色丟到formation底下
            member[i].transform.parent = formationUI.transform;
            //抓取formation上腳色的物件
            charaSprite[i] = formationUI.transform.FindChild(string.Format("Member{0}", i)).FindChild("chara").GetComponent<UISprite>();
            //設定腳色卡縮放
            member[i].transform.localScale = baseCharaScale;
            //設定腳色到對的位置上
            member[i].transform.localPosition = charaPos[i];
        }
        teamNo = formationUI.transform.FindChild("team").FindChild("teamNum").GetComponent<UILabel>();
        teamName = formationUI.transform.FindChild("team").FindChild("teamName").GetComponent<UILabel>();
    }


    public static void LoadMemberData()
    {
        //清除玩者隊伍並重新讀取玩者隊伍
        playerTeamList.Clear();
        Dictionary<int, PlayerTeam>.KeyCollection TeamDickeys = Player.TeamDic.Keys;
        foreach (int key in TeamDickeys)
        {
            playerTeamList.Add(Player.TeamDic[key]);
        }
        curTeam = playerTeamList[CurTmpTeamID];
        //取得目前的隊伍
        curTeam = playerTeamList[CurTmpTeamID];
        for (int i = 0; i < 3; i++)
        {
            //設定目前編輯的隊伍ID
            memberCard[i].GetComponent<GoChangeMemberList>().ChangeTeamID = curTeam.TeamID;
            empty[i].GetComponent<GoChangeMemberList>().ChangeTeamID = curTeam.TeamID;
            if (curTeam.Member[i] != 0)//如果該位置有怪物的話
            {
                memberCard[i].SetActive(true);
                empty[i].SetActive(false);
                curMember = Player.MonsterDic[curTeam.Member[i]];
                ///////////////////////讀取目前選取腳色的資料/////////////////////////////
                //設置腳色Icon的各種資料(怪物、元素、邊框、底圖)
                //選擇對的物種頭像
                if (curMember.Species < 8)
                {
                    iconCharaSprite[i].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                }
                else
                {
                    iconCharaSprite[i].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon2");
                }
                iconCharaSprite[i].spriteName = curMember.SpeciesKey;
                iconCharaSprite[i].MakePixelPerfect();
                elementSprite[i].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                elementSprite[i].spriteName = "element" + curMember.MainElement.ToString();
                elementSprite[i].MakePixelPerfect();
                bannerSprite[i].spriteName = "frame" + curMember.MainElement.ToString();
                bannerSprite[i].MakePixelPerfect();
                bottomSprite[i].spriteName = "bottom" + curMember.MainElement.ToString();//curMember.Rare.ToString();
                bottomSprite[i].MakePixelPerfect();
                //設置怪物的各種屬性資料(名稱、等級、經驗、種族、體型、生命、殘暴、侵略、抵抗)
                lvLabel[i].text = curMember.Lv.ToString();
                expSprite[i].width = (int)(70 + ((float)curMember.CurExp / curMember.NeedExp) * 385);
                nameLabel[i].text = curMember.MonsterName;
                //raceLabel[i].text = curMember.RaceToString(curMember.Race);
                //sizeLabel[i].text = curMember.SizeToString((curMember.Size));
                lifeLabel[i].text = curMember.Health.ToString();
                brutalLabel[i].text = curMember.Brutal.ToString();
                aggressiveLabel[i].text = curMember.Aggressive.ToString();
                resistanceLabel[i].text = curMember.Resistance.ToString();


                //判斷物種貼圖放在哪個atlas
                member[i].transform.FindChild("chara").gameObject.SetActive(true);
                member[i].transform.FindChild("shadow").gameObject.SetActive(true);
                charaSprite[i].atlas = Resources.Load<UIAtlas>(string.Format("Atlas/Character/{0}", curMember.Species));
                charaSprite[i].spriteName = curMember.SpeciesLevel.ToString();
                charaSprite[i].MakePixelPerfect();

            }
            else//如果該位置沒有怪物
            {
                memberCard[i].SetActive(false);
                empty[i].SetActive(true);
                member[i].transform.FindChild("chara").gameObject.SetActive(false);
                member[i].transform.FindChild("shadow").gameObject.SetActive(false);
            }
        }
        //判斷是第幾隊及設定隊名
        teamNo.text = string.Format("{0}", CurTmpTeamID + 1);
        teamName.text = curTeam.TeamName;
    }

}
