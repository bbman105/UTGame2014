using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class NPCMonsterSpawner : MonoBehaviour, ISpawn
{

    List<GameObject> chara;
    GameObject charaObj;
    public static int QuestID { get; set; }
    //召喚事件參數
    public int UnderlingNumber { get; set; }//出場嘍囉數量
    public int[] UnderlingWeight { get; set; }//嘍囉出場權重
    public Tactics[] AllTactics { get; set; }//戰術清單
    public float TacticsCD { get; set; }//戰術CD
    public int OnFightMinNumber { get; set; }//出戰怪最小數量
    public int OnFightMaxNumber { get; set; }//出戰怪最大數量



    Vector3 monsterBasePos;
    Vector3 monsterAddPos;
    Quaternion monsterRot;
    Vector3 monsterScale;
    string monsterTag;
    int monsterLayer;
    public int SpawnMonsterID { get; set; }


    public void NPCMonsterSpawnerSet(int _questID)
    {
        //設定怪物建立位置
        monsterBasePos = new Vector3(0.5f, 4.5f, 0);
        //設定怪物遞增位置
        monsterAddPos = new Vector3(0.5f, 0, 0);
        //設定怪物建立角度
        monsterRot = Quaternion.Euler(0f, 0f, 0f);
        //設定怪物縮放大小
        monsterScale = new Vector3(-1, 1, 1);
        //設定讀取怪物物件
        charaObj = Resources.Load<GameObject>("GameObject/FightScene/Chara/chara");
        //建立怪物GameObject列表
        chara = new List<GameObject>();
        monsterTag = "NPCMonster";
        monsterLayer = 14;//14 is NPCmonser
        QuestID = _questID;
    }

    public void Spawn()
    {
        try
        {
            StageControler.OnFightMaxNumber = GameDictionary.QuestDic[QuestID].OnFightMaxNumber;//設定最大出場怪數
            StageControler.OnFightMinNumber = GameDictionary.QuestDic[QuestID].OnFightMinNumber;//設定最小出場怪數
            ForceCondition.BaseGold = GameDictionary.QuestDic[QuestID].Gold;
            ForceCondition.BaseExp = GameDictionary.QuestDic[QuestID].Exp;
            List<int> eventID = GameDictionary.QuestDic[QuestID].GetEvent();//此關卡依照權重挑取召喚事件
            List<int> ResultSummonMID = new List<int>();
            for (int i = 0; i < eventID.Count; i++)
            {
                UnderlingNumber = GameDictionary.SummonEventDic[eventID[i]].ChoiceUnderlingNumber;
                UnderlingWeight = GameDictionary.SummonEventDic[eventID[i]].ChoiceUnderlingWeight;
                //AllTactics = GameDictionary.SummonEventDic[eventID[i]].AllTactics;
                //TacticsCD = GameDictionary.SummonEventDic[eventID[i]].TacticsCD;
                //OnFightMaxNumber = GameDictionary.SummonEventDic[eventID[i]].OnFightMaxNumber;
                //OnFightMinNumber = GameDictionary.SummonEventDic[eventID[i]].OnFightMinNumber;
                List<int> monsterIDs = GameDictionary.SummonEventDic[eventID[i]].GetUnderlings();//將此事件的怪物都加入欲創建清單中
                for (int j = 0; j < monsterIDs.Count; j++)
                {
                    ResultSummonMID.Add(monsterIDs[j]);
                }
            }
            if (ResultSummonMID.Count != 0)//召喚怪獸
            {
                for (int j = 0; j < ResultSummonMID.Count; j++)//創造挑出來的怪物
                {
                    //建立怪物GameObject
                    chara.Add(Instantiate(charaObj, monsterBasePos + (monsterAddPos * j), monsterRot) as GameObject);
                    //設定怪物縮放
                    chara[j].transform.localScale = monsterScale;
                    //設定怪物標籤
                    chara[j].tag = monsterTag;
                    //設定怪物圖層
                    chara[j].layer = monsterLayer;
                    //給予物件名稱
                    chara[j].name = "NPCMonster" + (j + 1);
                    //設定怪物的的ID並讀取怪物資料
                    SpawnMonsterID = ResultSummonMID[j];
                    chara[j].GetComponent<Status>().SetMonsterAttribute(SpawnMonsterID, new SetNpcMonsterAttribute());
                    chara[j].GetComponent<Status>().MovSetup();
                    //設定SpriteRenderer;
                    SpeciesModel sm = new SpeciesModel(
                        chara[j].transform,
                        chara[j].GetComponent<Status>().CharaSpriteRenderer,
                        chara[j].GetComponent<Status>().SpeciesKey,
                        chara[j].GetComponent<Status>().Size);
                    sm.SetSpeciesModel();
                    //加入腳色清單
                    if (!ForceCondition.CharaDic.ContainsKey(monsterTag))
                    {
                        ForceCondition.CharaDic.Add(monsterTag, new List<GameObject>());
                    }
                    ForceCondition.CharaDic[monsterTag].Add(chara[j]);
                    if (!ForceCondition.CharaDic.ContainsKey(monsterTag))
                    {
                        ForceCondition.CharaDic.Add(monsterTag, ForceCondition.CharaDic[monsterTag]);
                    }
                }
            }
            else//企劃資料錯誤，沒有召喚怪獸
            {
                //建立怪物GameObject
                chara.Add(Instantiate(charaObj, monsterBasePos + (monsterAddPos), monsterRot) as GameObject);
                //設定怪物縮放
                chara[0].transform.localScale = monsterScale;
                //設定怪物標籤
                chara[0].tag = monsterTag;
                //設定怪物圖層
                chara[0].layer = monsterLayer;
                //給予物件名稱
                chara[0].name = "NPCMonster" + 1;
                //設定怪物的的ID並讀取怪物資料
                SpawnMonsterID = 1;
                //chara[0].GetComponent<Status>().SetSource(Resources.Load<TextAsset>("String/NPCMonster"), new SetNpcMonsterAttribute());
                chara[0].GetComponent<Status>().SetMonsterAttribute(SpawnMonsterID, new SetNpcMonsterAttribute());
                chara[0].GetComponent<Status>().MovSetup();
                //加入腳色清單
                if (!ForceCondition.CharaDic.ContainsKey(monsterTag))
                {
                    ForceCondition.CharaDic.Add(monsterTag, new List<GameObject>());
                }
                ForceCondition.CharaDic[monsterTag].Add(chara[0]);
                if (!ForceCondition.CharaDic.ContainsKey(monsterTag))
                {
                    ForceCondition.CharaDic.Add(monsterTag, ForceCondition.CharaDic[monsterTag]);
                }
                Debug.Log("企劃資料錯誤，此關卡沒有召喚怪物");
            }
        }
        catch
        {
            Debug.Log("召喚怪物時發生錯誤");
        }
    }
}
