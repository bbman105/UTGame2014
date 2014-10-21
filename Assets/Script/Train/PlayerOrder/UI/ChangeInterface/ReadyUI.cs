using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ReadyUI : TrainSceneInterfaceUI
{

    //隊伍List
    static List<PlayerTeam> playerTeamList;
    //讀入的prefab物件
    static GameObject teamObj;
    //隊伍icon gameobj LIst
    static List<GameObject> team;
    static GameObject[] chara;
    static int[] monsterNum;
    static int teamNum;
    public static PlayerTeam curTeam;
    public static PlayerMonster curMember;
    static int theLargestNum;//取team.count/teamNum中較大的數
    //每個隊伍卡的位置
    static List<Vector3> resourceUIPos;
    //每隻隊伍icon物件的基本座標
    static Vector3 baseResourceUIPos;
    //每隻隊伍icon物件的Y軸位移座標
    static Vector3 addResourceUIPOsY;
    //每隻隊伍icon物件的基本縮放
    static Vector3 baseResourceUIScale;
    //每隻隊伍icon物件的基本旋轉
    static Quaternion baseResourceUIRot;
    //隊伍名稱標籤
    static private List<UILabel> teamNameLabel;
    //隊伍icon圖的sprite
    static private UISprite[] charaSprite;
    //邊框圖的sprite
    static private UISprite[] bannerSprite;
    //底圖的sprite
    static private UISprite[] bottomSprite;
    //元素icon的sprite
    static private UISprite[] elementSprite;
    //腳色等級Label
    static private UILabel[] lvLabel;


    public void StartSetting()
    {
        resourceUIPos = new List<Vector3>();
        monsterNum = new int[3];
        chara = new GameObject[3];
        team = new List<GameObject>();
        baseResourceUIPos = new Vector3(0, 300, 0);
        addResourceUIPOsY = new Vector3(0, -270, 0);
        baseResourceUIScale = new Vector3(1, 1, 1);
        baseResourceUIRot = Quaternion.Euler(0, 0, 0);
        teamNameLabel = new List<UILabel>();
        charaSprite = new UISprite[3];
        bannerSprite = new UISprite[3];
        bottomSprite = new UISprite[3];
        elementSprite = new UISprite[3];
        lvLabel = new UILabel[3];
        teamObj = Resources.Load<GameObject>("GameObject/RoomUI/memberList/teamCard");

        
        //建立玩者隊伍清單
        playerTeamList = new List<PlayerTeam>();

        /*
                Dictionary<int, PlayerTeam>.KeyCollection TeamDickeys = Player.TeamDic.Keys;
        foreach (int key in TeamDickeys)
        {
            int nullMemberNum = 0;
            for (int i = 0; i < Player.TeamDic[key].Member.Length; i++)
            {
                if (Player.TeamDic[key].Member[i] == 0)
                    nullMemberNum++;
            }
            if (nullMemberNum < 3)//如果隊伍中空成員數小於3(沒成員的意思)則加入可選取出戰的隊伍清單中
                playerTeamList.Add(Player.TeamDic[key]);
        }
        
        //建立隊伍頭像icon物件
        teamNum = playerTeamList.Count;
        for (int i = 0; i < teamNum; i++)
        {
            //設定隊伍卡位置
            resourceUIPos.Add(baseResourceUIPos + (addResourceUIPOsY * i));
            //創建隊伍
            team.Add(Instantiate(teamObj, resourceUIPos[i], baseResourceUIRot) as GameObject);
            //給予創建的隊伍卡的隊伍UID
            team[i].transform.FindChild("Start").GetComponent<GoFightScene>().FightTeamID = playerTeamList[i].TeamID;
            teamNameLabel.Add(team[i].transform.FindChild("teamName").GetComponent<UILabel>());
            //把剛創建的隊伍物件丟到Ready底下
            team[i].transform.parent = RoomInterfaceUI.readyUI.transform;
            //設定隊伍到對的位置上
            team[i].transform.localPosition = resourceUIPos[i];
            //設定隊伍縮放
            team[i].transform.localScale = baseResourceUIScale;
            //設定每個隊伍的隊員數
            //monsterNum[i] = playerTeamList[i].MemberNum;
            //curTeam = playerTeamList[i];
            for (int j = 0; j < 3; j++)
            {
                //抓取隊伍卡上腳色
                chara[j] = team[i].transform.FindChild((j + 1).ToString()).gameObject;
                //抓取隊伍卡上腳色ICON的物件
                charaSprite[j] = chara[j].transform.FindChild("sprite").GetComponent<UISprite>();
                bannerSprite[j] = chara[j].transform.FindChild("banner").GetComponent<UISprite>();
                bottomSprite[j] = chara[j].transform.FindChild("bg").GetComponent<UISprite>();
                elementSprite[j] = chara[j].transform.FindChild("element").GetComponent<UISprite>();
                lvLabel[j] = chara[j].transform.FindChild("lv").GetComponent<UILabel>();
            }
        }
        */

    }

    public static void LoadTeamData()
    {
        playerTeamList.Clear();
        Dictionary<int, PlayerTeam>.KeyCollection TeamDickeys = Player.TeamDic.Keys;
        foreach (int key in TeamDickeys)
        {
            int nullMemberNum = 0;
            for (int i = 0; i < Player.TeamDic[key].Member.Length; i++)
            {
                if (Player.TeamDic[key].Member[i] == 0)
                    nullMemberNum++;
            }
            if (nullMemberNum < 3)//如果隊伍中空成員數小於3(沒成員的意思)則加入可選取出戰的隊伍清單中
                playerTeamList.Add(Player.TeamDic[key]);
        }
        teamNum = playerTeamList.Count;//設定可出戰隊伍數量


        if (teamNum == team.Count)
        {
            Debug.Log("玩家可出戰隊伍量不變");
            theLargestNum = teamNum;
            SetTeamData();
        }
        else if (teamNum > team.Count)
        {
            Debug.Log("玩家可出戰隊伍量變多需擴充物件");
            theLargestNum = teamNum;
            SetTeamData();
        }
        else
        {
            Debug.Log("玩家可出戰隊伍量變少需取消顯示多出來的icon");
            theLargestNum = team.Count;
            SetTeamData();
        }
    }

    static void SetTeamData()
    {

        for (int i = 0; i < theLargestNum; i++)
        {

            if (i >= team.Count)//可出戰隊伍數量超過現有物件量，建立新物件並讀取資料
            {
                //設定隊伍卡位置
                resourceUIPos.Add(baseResourceUIPos + (addResourceUIPOsY * i));
                //創建隊伍
                team.Add(Instantiate(teamObj, resourceUIPos[i], baseResourceUIRot) as GameObject);
                //給予創建的隊伍卡的隊伍UID
                team[i].transform.FindChild("Start").GetComponent<GoFightScene>().FightTeamID = playerTeamList[i].TeamID;
                teamNameLabel.Add(team[i].transform.FindChild("teamName").GetComponent<UILabel>());
                //把剛創建的隊伍物件丟到Ready底下
                team[i].transform.parent = readyUI.transform;
                //設定隊伍到對的位置上
                team[i].transform.localPosition = resourceUIPos[i];
                //設定隊伍縮放
                team[i].transform.localScale = baseResourceUIScale;
                //設定每個隊伍的隊員數
                monsterNum[i] = playerTeamList[i].MemberNum;
                for (int j = 0; j < 3; j++)
                {
                    //抓取隊伍卡上腳色
                    chara[j] = team[i].transform.FindChild((j + 1).ToString()).gameObject;
                    //抓取隊伍卡上腳色ICON的物件
                    charaSprite[j] = chara[j].transform.FindChild("sprite").GetComponent<UISprite>();
                    bannerSprite[j] = chara[j].transform.FindChild("banner").GetComponent<UISprite>();
                    bottomSprite[j] = chara[j].transform.FindChild("bg").GetComponent<UISprite>();
                    elementSprite[j] = chara[j].transform.FindChild("element").GetComponent<UISprite>();
                    lvLabel[j] = chara[j].transform.FindChild("lv").GetComponent<UILabel>();
                }
            }

            if (i < teamNum)
            {
                monsterNum[i] = playerTeamList[i].MemberNum;
                curTeam = playerTeamList[i];
                team[i].SetActive(true);
                teamNameLabel[i].text = curTeam.TeamName;
                for (int j = 0; j < 3; j++)
                {
                    //抓取隊伍卡上腳色
                    chara[j] = team[i].transform.FindChild((j + 1).ToString()).gameObject;
                    if (curTeam.Member[j] != 0)//如果該位置有怪物的話
                    {
                        //抓取隊伍卡上腳色ICON的物件
                        charaSprite[j] = chara[j].transform.FindChild("sprite").GetComponent<UISprite>();
                        bannerSprite[j] = chara[j].transform.FindChild("banner").GetComponent<UISprite>();
                        bottomSprite[j] = chara[j].transform.FindChild("bg").GetComponent<UISprite>();
                        elementSprite[j] = chara[j].transform.FindChild("element").GetComponent<UISprite>();
                        lvLabel[j] = chara[j].transform.FindChild("lv").GetComponent<UILabel>();

                        chara[j].SetActive(true);
                        curMember = Player.MonsterDic[curTeam.Member[j]];
                        //選擇對的物種頭像
                        if (curMember.Species < 8)
                        {
                            charaSprite[j].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                        }
                        else
                        {
                            charaSprite[j].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon2");
                        }
                        charaSprite[j].spriteName = curMember.SpeciesKey;
                        charaSprite[j].MakePixelPerfect();
                        elementSprite[j].spriteName = "element" + curMember.MainElement.ToString();
                        elementSprite[j].MakePixelPerfect();
                        bannerSprite[j].spriteName = "frame" + curMember.MainElement.ToString();
                        bannerSprite[j].MakePixelPerfect();
                        bottomSprite[j].spriteName = "bottom" + curMember.MainElement.ToString();//curMember.Rare.ToString();
                        bottomSprite[j].MakePixelPerfect();
                        lvLabel[j].text = "LV" + curMember.Lv.ToString();
                    }
                    else
                    {
                        chara[j].SetActive(false);
                    }
                }
            }
            else//如果i大於等於可出戰隊伍清單代表是多的物件需隱藏
            {
                team[i].SetActive(false);
            }


        }
    }

}
