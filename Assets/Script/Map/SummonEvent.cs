using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class SummonEvent
{


    public int EventID { get; set; }//召喚事件ID
    public int ChoiceUnderlingNumber { get; set; }//出場嘍囉數量
    public int[] ChoiceUnderlingID { get; set; }//選擇嘍囉ID清單
    public int[] ChoiceUnderlingWeight { get; set; }//嘍囉出場權重
    public List<int> AbsoluteUnderling { get; set; }//必出嘍囉ID清單
    List<int> ResultMonsterID;//最終出怪ID清單
    //public Tactics[] AllTactics { get; set; }//戰術清單
    //public float TacticsCD { get; set; }//戰術CD
    //public int OnFightMinNumber { get; set; }//出戰怪最小數量
    //public int OnFightMaxNumber { get; set; }//出戰怪最大數量


    int AllWeight;//召喚事件總權重

    public SummonEvent(int _summonEventID)
    {

        //讀取地圖資料
        TextAsset XMLFlie_Event = Resources.Load<TextAsset>("String/SummonEvent");
        //讀取XML文件
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(XMLFlie_Event.text);
        XmlNode root = doc.DocumentElement;

        try
        {
            EventID = _summonEventID;//事件ID
            //讀取xml文件中欲抓取的MonsterID標籤
            XmlNode CurEventNode = root.SelectSingleNode(string.Format("SEID{0}", EventID));
            ChoiceUnderlingNumber = int.Parse(CurEventNode.SelectSingleNode("ChoiceUnderlingNumber").InnerText);
            string[] underling = CurEventNode.SelectSingleNode("ChoiceUnderling").InnerText.Split(',');
            ChoiceUnderlingID = new int[underling.Length];
            for (int i = 0; i < ChoiceUnderlingID.Length; i++)
            {
                ChoiceUnderlingID[i] = int.Parse(underling[i]);
            }
            string[] weight = CurEventNode.SelectSingleNode("Weight").InnerText.Split(',');
            ChoiceUnderlingWeight = new int[weight.Length];
            for (int i = 0; i < weight.Length; i++)
            {
                ChoiceUnderlingWeight[i] = int.Parse(weight[i]);
                AllWeight += ChoiceUnderlingWeight[i];
            }
            string[] absoluteUnderling = CurEventNode.SelectSingleNode("AbsoluteUnderling").InnerText.Split(',');//必出怪物
            AbsoluteUnderling = new List<int>();
            for (int i = 0; i < absoluteUnderling.Length; i++)
            {
                if (int.Parse(absoluteUnderling[i]) != 0)
                    this.AbsoluteUnderling.Add(int.Parse(absoluteUnderling[i]));
            }
            /*
            //讀取戰術
            List<string[]> tacticsNameList = new List<string[]>();
            for (int i = 1; i < 6; i++)//目前最多只能填5個戰術
            {
                string[] bossTactics = CurEventNode.SelectSingleNode(string.Format("BossTactics{0}", i)).InnerText.Split(',');
                tacticsNameList.Add(bossTactics);
            }
            SetTactics(tacticsNameList);
            TacticsCD = float.Parse(CurEventNode.SelectSingleNode("ColdDown").InnerText);
            OnFightMinNumber = int.Parse(CurEventNode.SelectSingleNode("OnFightMinNumber").InnerText);
            OnFightMaxNumber = int.Parse(CurEventNode.SelectSingleNode("OnFightMaxNumber").InnerText);
            */
        }
        catch
        {
            Debug.Log("讀取SummonEvent.xml時發生錯誤");
        }
    }
    void SetTactics(List<string[]> _tacticsNameList)
    {
        /*
        AllTactics = new Tactics[_tacticsNameList.Count];
        for (int i = 0; i < _tacticsNameList.Count; i++)
        {
            switch (_tacticsNameList[i][0])
            {
                case "Health":
                    AllTactics[i] = new HealthTactics(_tacticsNameList[i][1]);
                    break;
                case "SingleSuppress":
                    break;
                case "Suppress":
                    break;
                case "0":
                    break;
                default:
                    Debug.Log("關卡召喚事件傳入的戰術錯誤，無此戰術");
                    break;
            }
        }
        */
    }

    public List<int> GetUnderlings()//依權重機率挑選怪獸
    {
        ResultMonsterID = new List<int>();
        //將必出怪物事件都加入最終出怪事件清單中
        for (int i = 0; i < AbsoluteUnderling.Count; i++)
        {
            AddMonster(AbsoluteUnderling[i]);
        }
        for (int i = 0; i < ChoiceUnderlingNumber; i++)
        {
            int rndNum = Random.Range(0, AllWeight);
            int tmpWeight = AllWeight;//3,1
            int summonMonsterRank = 0;
            //加入選擇性出怪
            while (tmpWeight > rndNum && summonMonsterRank < ChoiceUnderlingWeight.Length)
            {
                tmpWeight -= ChoiceUnderlingWeight[summonMonsterRank];
                if (tmpWeight <= rndNum)
                {
                    if (ChoiceUnderlingID[summonMonsterRank] != 0)
                        AddMonster(ChoiceUnderlingID[summonMonsterRank]);
                    break;
                }
                else
                {
                    summonMonsterRank++;
                }
            }
        }
        return ResultMonsterID;
    }
    void AddMonster(int _monsterID)//判定是否為合法monsterID，是的話才加入回傳清單中
    {
        if (GameDictionary.NPCDic.ContainsKey(_monsterID))
        {
            ResultMonsterID.Add(_monsterID);
        }
        else
        {
            if (_monsterID != 0)
                Debug.Log("嘗試召喚不存在的的NPCMonsterID");
        }
    }
}



