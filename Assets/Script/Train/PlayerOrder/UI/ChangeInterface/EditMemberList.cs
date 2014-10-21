using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditMemberList : TrainSceneInterfaceUI
{

    //讀入的prefab物件
    static GameObject charaObj;
    static GameObject charaBottomObj;
    //腳色icon gameobj LIst
    static List<GameObject> chara;
    static Transform Grid;
    //腳色icon底圖 gameobj LIst
    static List<GameObject> charaBottom;
    //玩者隊伍List
    public static List<PlayerMonster> PlayerMonsterList;
    static int curMonsterNum;
    static PlayerMonster curMonster;
    static int theLargestNum;//取chara.count/curMonsterNum中較大的數
    //每隻腳色的位置
    static List<Vector3> resourceUIPos;
    //每隻腳色icon物件的基本座標
    static Vector3 baseResourceUIPos;
    //每隻腳色icon物件的X軸位移座標
    static Vector3 addResourceUIPOsX;
    //每隻腳色icon物件的Y軸位移座標
    static Vector3 addResourceUIPOsY;
    private static int row;
    private static int column;
    //每隻腳色icon物件的基本縮放
    static Vector3 baseResourceUIScale;
    //每隻腳色icon物件的基本旋轉
    static Quaternion baseResourceUIRot;
    //腳色icon圖的sprite
    static private UISprite charaSprite;
    //邊框圖的sprite
    static private UISprite bannerSprite;
    //底圖的sprite
    static private UISprite bottomSprite;
    //元素icon的sprite
    static private UISprite elementSprite;
    //品質星星
    static private UISprite[] rareSprite;


    public void StartSetting()
    {
        resourceUIPos = new List<Vector3>();
        chara = new List<GameObject>();
        Grid = editMemberListUI.transform.FindChild("ScrollView").FindChild("Grid");
        charaBottom = new List<GameObject>();
        PlayerMonsterList = new List<PlayerMonster>();
        curMonsterNum = new int();
        row = new int();
        column = new int();
        baseResourceUIPos = new Vector3(-240, -160, 0);
        addResourceUIPOsX = new Vector3(160, 0, 0);
        addResourceUIPOsY = new Vector3(0, -160, 0);
        baseResourceUIScale = new Vector3(1, 1, 1);
        baseResourceUIRot = Quaternion.Euler(0, 0, 0);
        charaObj = Resources.Load<GameObject>("GameObject/RoomUI/CharaList/chara");
        charaBottomObj = Resources.Load<GameObject>("GameObject/RoomUI/CharaList/charaChangeBottom");
    }

    public static void LoadMonsterSprite()
    {
        //將玩者隊伍字典存入List中
        PlayerMonsterList.Clear();
        Dictionary<int, PlayerMonster>.KeyCollection MonsterDickeys = Player.MonsterDic.Keys;
        theLargestNum = Player.MonsterCapacity;//玩家可擁有的怪物上限
        foreach (int key in MonsterDickeys)
        {
            PlayerMonsterList.Add(Player.MonsterDic[key]);
        }
        PlayerMonsterList.Sort();
        curMonsterNum = PlayerMonsterList.Count;
        if (curMonsterNum == chara.Count)
        {
            Debug.Log("玩家擁有的怪物量不變");
            SetMonster();
        }
        else if (curMonsterNum > chara.Count)
        {
            Debug.Log("玩家擁有的怪物量變多需擴充物件");
            SetMonster();
        }
        else
        {
            Debug.Log("玩家擁有的怪物量變少需取消顯示多出來的icon");
            SetMonster();
        }
    }

    static void SetMonster()
    {
        EditMonsterSortBtn.SetSpriteColor();//設定排序按鈕顏色
        for (int i = 0; i < theLargestNum; i++)
        {
            if (i >= chara.Count)//怪物數量超過現有物件量，建立新物件並讀取怪物
            {
                //判斷此怪物要擺在第幾行
                column = i / 4;
                //判斷此怪物要擺在第幾列
                row = i % 4;
                //設定怪物位置
                resourceUIPos.Add(baseResourceUIPos + (addResourceUIPOsX * row) + (addResourceUIPOsY * column));
                //創建怪物
                chara.Add(Instantiate(charaObj, resourceUIPos[i], baseResourceUIRot) as GameObject);
                charaBottom.Add(Instantiate(charaBottomObj, resourceUIPos[i], baseResourceUIRot) as GameObject);
                //把怪物頭像丟到memberUI底下
                chara[i].transform.parent = Grid;
                charaBottom[i].transform.parent = Grid;
                //設定怪物到對的位置上
                chara[i].transform.localPosition = resourceUIPos[i];
                charaBottom[i].transform.localPosition = resourceUIPos[i];
                //設定怪物縮放
                chara[i].transform.localScale = baseResourceUIScale;
                charaBottom[i].transform.localScale = baseResourceUIScale;
            }



            if (i < curMonsterNum)
            {
                chara[i].SetActive(true);
                //charaBottom[i].SetActive(true);
                //判斷此怪物要擺在第幾行
                column = i / 4;
                //判斷此怪物要擺在第幾列
                row = i % 4;
                //設定怪物位置
                resourceUIPos[i] = baseResourceUIPos + (addResourceUIPOsX * row) + (addResourceUIPOsY * column);
                //設定怪物到對的位置上
                chara[i].transform.localPosition = resourceUIPos[i];
                //設定怪物縮放
                chara[i].transform.localScale = baseResourceUIScale;
                //給予剛創建怪物的怪物ID，用來做點取判斷
                chara[i].GetComponent<MonsterChoiceListBtn>().MonsterID = PlayerMonsterList[i].MonsterID;
                /////////////設定怪物頭像//////////////
                curMonster = PlayerMonsterList[i];
                //設置怪物等級label
                chara[i].transform.FindChild("lv").GetComponent<UILabel>().text = curMonster.Lv.ToString();
                //抓取物件上的atlas & sprite
                charaSprite = chara[i].transform.FindChild("chara").GetComponent<UISprite>();
                bannerSprite = chara[i].transform.FindChild("banner").GetComponent<UISprite>();
                bottomSprite = chara[i].transform.FindChild("bottom").GetComponent<UISprite>();
                elementSprite = chara[i].transform.FindChild("element").GetComponent<UISprite>();
                //讀取怪物atlas
                if (curMonster.Species <= 13)
                {
                    charaSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                }
                //選擇對的物種頭像
                if (curMonster.Species < 8)
                {
                    charaSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                }
                else
                {
                    charaSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon2");
                }
                charaSprite.spriteName = curMonster.SpeciesKey;
                //歸正縮放比例
                charaSprite.MakePixelPerfect();

                bannerSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                bannerSprite.spriteName = "frame" + curMonster.MainElement.ToString();
                bannerSprite.MakePixelPerfect();
                bottomSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                bottomSprite.spriteName = "bottom" + curMonster.MainElement.ToString();
                bottomSprite.MakePixelPerfect();
                elementSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                elementSprite.spriteName = "element" + curMonster.MainElement.ToString();
                elementSprite.MakePixelPerfect();
            }
            else//如果怪物清單變少，則隱藏多的怪物
            {
                chara[i].SetActive(false);
                //charaBottom[i].SetActive(false);
            }
        }
    }
}
