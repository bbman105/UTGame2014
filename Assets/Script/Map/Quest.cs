using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public class Quest
{

    int questID;
    public int QuestID
    {
        get { return questID; }
        set { questID = value; }
    }//任務ID
    string questName;
    public string QuestName
    {
        get { return questName; }
        set { questName = value; }
    }//任務名稱
    int[] choiceSummonEvent;
    public int[] ChoiceSummonEvent
    {
        get { return choiceSummonEvent; }
        set { choiceSummonEvent = value; }
    }//選擇事件清單
    int[] choiceEventWeight;
    public int[] ChoiceEventWeight
    {
        get { return choiceEventWeight; }
        set { choiceEventWeight = value; }
    }//選擇事件權重清單
    List<int> AbsoluteSummonEvent;//必出事件清單
    int AllWeight;//召喚事件總權重
    List<int> ownMonsterID;//顯示任務出怪圖像
    public List<int> OwnMonsterID
    {
        get { return ownMonsterID; }
        set { ownMonsterID = value; }
    }
    List<int> OwnItemID { get; set; }//顯示任務物品清單
    int gold;
    public int Gold
    {
        get { return gold; }
        set
        {
            if (value < 0)
                gold = 0;
            else
                gold = value;
        }
    }//任務金錢獎勵
    int exp;
    public int Exp
    {
        get { return exp; }
        set
        {
            if (value < 0)
                exp = 0;
            else
                exp = value;
        }
    }//任務經驗獎勵
    private int onFightMinNumber;
    public int OnFightMinNumber//出戰怪最小數量，不可低於1
    {
        get { return onFightMinNumber; }
        set
        {
            if (value < 1)
                onFightMinNumber = 1;
            else
                onFightMinNumber = value;
        }
    }
    public int OnFightMaxNumber { get; set; }//出戰怪最大數量
    List<int> ResultEvent = new List<int>();//回傳的事件LIST
    public Quest(int _QuestID)
    {
        //讀取地圖資料
        TextAsset XMLFlie_Quest = Resources.Load<TextAsset>("String/Quest");
        //讀取XML文件
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(XMLFlie_Quest.text);
        XmlNode root = doc.DocumentElement;

        try
        {
            this.QuestID = _QuestID;//任務ID
            //讀取xml文件中欲抓取的MonsterID標籤
            XmlNode CurQuestNode = root.SelectSingleNode(string.Format("QID{0}", QuestID));
            this.QuestName = CurQuestNode.SelectSingleNode("QuestName").InnerText;//任務名稱
            string[] absoluteEvent = CurQuestNode.SelectSingleNode("AbsoluteEvent").InnerText.Split(',');//必出事件
            AbsoluteSummonEvent = new List<int>();
            for (int i = 0; i < absoluteEvent.Length; i++)
            {
                if (int.Parse(absoluteEvent[i]) != 0)
                    this.AbsoluteSummonEvent.Add(int.Parse(absoluteEvent[i]));
            }
            string[] eventID = CurQuestNode.SelectSingleNode("ChoiceEvent").InnerText.Split(',');//選擇事件
            this.ChoiceSummonEvent = new int[eventID.Length];
            for (int i = 0; i < eventID.Length; i++)
            {
                this.ChoiceSummonEvent[i] = int.Parse(eventID[i]);
            }
            string[] eventWeight = CurQuestNode.SelectSingleNode("EventWeight").InnerText.Split(',');//召喚事件權重
            this.ChoiceEventWeight = new int[eventWeight.Length];
            for (int i = 0; i < eventWeight.Length; i++)
            {
                this.ChoiceEventWeight[i] = int.Parse(eventWeight[i]);
                AllWeight += this.ChoiceEventWeight[i];
            }
            OwnMonsterID = new List<int>();//讀取任務顯示怪獸圖像
            string[] iconID = CurQuestNode.SelectSingleNode("OwnMonsterID").InnerText.Split(',');
            for (int i = 0; i < iconID.Length; i++)
            {
                if (int.Parse(iconID[i]) != 0)
                    this.OwnMonsterID.Add(int.Parse(iconID[i]));
            }
            OwnItemID = new List<int>();//讀取任務顯示怪獸圖像
            string[] itemID = CurQuestNode.SelectSingleNode("OwnItemID").InnerText.Split(',');
            for (int i = 0; i < itemID.Length; i++)
            {
                if (int.Parse(itemID[i]) != 0)
                    this.OwnItemID.Add(int.Parse(itemID[i]));
            }
            Gold = int.Parse(CurQuestNode.SelectSingleNode("Gold").InnerText);//讀取任務金錢獎勵
            Exp = int.Parse(CurQuestNode.SelectSingleNode("Exp").InnerText);//讀取任務經驗獎勵
            OnFightMinNumber = int.Parse(CurQuestNode.SelectSingleNode("OnFightMinNumber").InnerText);
            OnFightMaxNumber = int.Parse(CurQuestNode.SelectSingleNode("OnFightMaxNumber").InnerText);
        }
        catch
        {
            Debug.Log("讀取Quest.xml時發生錯誤");
        }
    }
    public List<int> GetEvent()//依權重機率挑選事件
    {
        int rndNum = Random.Range(0, AllWeight);
        int tmpWeight = AllWeight;
        int summonEventRank = 0;
        ResultEvent = new List<int>();
        //將必出怪物事件都加入最終出怪事件清單中
        for (int i = 0; i < AbsoluteSummonEvent.Count; i++)
        {
            AddEvent(AbsoluteSummonEvent[i]);
        }
        //依照權重挑選一個擇一岀怪事件
        while (tmpWeight > rndNum && summonEventRank < ChoiceEventWeight.Length)
        {
            tmpWeight -= ChoiceEventWeight[summonEventRank];
            if (tmpWeight <= rndNum)
                break;
            else
                summonEventRank++;
        }
        AddEvent(ChoiceSummonEvent[summonEventRank]);
        return ResultEvent;
    }
    void AddEvent(int _eventID)//判定是否為合法eventID，是的話才加入回傳清單中
    {
        if (GameDictionary.SummonEventDic.ContainsKey(_eventID))
        {
            ResultEvent.Add(_eventID);
        }
        else
        {
            if (_eventID != 0)
                Debug.Log("嘗試讀取不存在的SummonEventID");
        }
    }
}
