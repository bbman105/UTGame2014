using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.Collections.Generic;

public delegate void ChangeSceneDelegate();


public partial class TrainScene : MonoBehaviour
{

    public static ChangeSceneDelegate ChangeSceneFunction;//場警變化委託
    public static bool StartSet { get; set; }//是否已初始設置

    //事件對話字典
    public static List<Dialogue> EventList;
    public static Dictionary<int, Dialogue> EventDic;
    private static int eventNum;
    public static int EventNum
    {
        get { return TrainScene.eventNum; }
        set { TrainScene.eventNum = value; }
    }

    //事件觸碰事件回饋字典
    public static Dictionary<int, TouchFeedBack> TouchFeedBackDic;//[TouchFeedbackID]
    public static Dictionary<int, Dictionary<int, TouchFeedBack>> OwnPTouchFeedBack;
    public static Dictionary<int, Dictionary<int, TouchFeedBack>> PFeedBackDic;
    private static int touchFeedBackNum;
    public static int TouchFeedBackNum
    {
        get { return TrainScene.touchFeedBackNum; }
        set { TrainScene.touchFeedBackNum = value; }
    }
    private static int personalityNum;
    public static int PersonalityNum
    {
        get { return TrainScene.personalityNum; }
        set { TrainScene.personalityNum = value; }
    }
    //事件回答類型字典
    public static Dictionary<string, EventFeedBack> EventFeedBackDic;
    private static int eventFeedBackNum;
    public static int EventFeedBackNum
    {
        get { return TrainScene.eventFeedBackNum; }
        set { TrainScene.eventFeedBackNum = value; }
    }
    //總技能字典
    public static Dictionary<int, Skill> SkillDic;
    public static int SkillNum { get; set; }


    private static int curEventID;
    public static int CurEventID
    {
        get { return TrainScene.curEventID; }
        set { TrainScene.curEventID = value; }
    }

    GameObject RoomResourceSpawnerObj { get; set; }//房間物件產生器
    GameObject RoomResourceSpawnerResourceObj { get; set; }//房間物件產生器的位置物件


    public struct Dialogue
    {
        public string id;
        public string dialogue;
        public string dialogueIcon;
        public string ans1;
        public string ans2;
        public string ans3;
        public string ans1Icon;
        public string ans2Icon;
        public string ans3Icon;
        public int action;
    }
    public struct TouchFeedBack
    {
        public int id;
        public string feedBackDialogue;
        public int ownPersonality;
        public int familiarMin;
        public int familiarMax;
        public string textAni;
    }
    public struct EventFeedBack
    {
        public int personalityID;
        public int eventID;
        public int[][] ansFeedback;//[第幾個回答][回答影響的三屬性]
        public string[] ansDialogue;//[第幾個回答]
        public string[] ansTextAni;//[第幾個回答]
    }

    private static void LoadSkillXml()
    {
        SkillDic = new Dictionary<int, Skill>();
        //依照法術編號讀取法術資料中的法術各屬性
        TextAsset XMLFlie = Resources.Load<TextAsset>("String/Spell");
        //讀取XML文件
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(XMLFlie.text);
        XmlNodeList SPIDxml = doc.GetElementsByTagName("SpellID");
        SkillNum = SPIDxml.Count;
        for (int i = 0; i < SkillNum; i++)
        {
            int spellID = int.Parse(SPIDxml[i].InnerText);
            Skill fs = new Skill(spellID);
            SkillDic.Add(spellID, fs);
        }
    }


    private void LoadEventFeedBack()
    {
        EventFeedBackDic = new Dictionary<string, EventFeedBack>();
        TextAsset textXML = Resources.Load<TextAsset>("String/EventFeedback");
        XmlDocument eventFeedBackXml = new XmlDocument();
        eventFeedBackXml.LoadXml(textXML.text);
        XmlNodeList PIDxml = eventFeedBackXml.GetElementsByTagName("PersonalityID");
        XmlNodeList EIDxml = eventFeedBackXml.GetElementsByTagName("EventID");
        XmlNodeList Ans1Feedbackxml = eventFeedBackXml.GetElementsByTagName("Ans1Feedback");
        XmlNodeList Ans1Dialoguexml = eventFeedBackXml.GetElementsByTagName("Ans1Dialogue");
        XmlNodeList Ans1TextAnixml = eventFeedBackXml.GetElementsByTagName("Ans1TextAni");
        XmlNodeList Ans2Feedbackxml = eventFeedBackXml.GetElementsByTagName("Ans2Feedback");
        XmlNodeList Ans2Dialoguexml = eventFeedBackXml.GetElementsByTagName("Ans2Dialogue");
        XmlNodeList Ans2TextAnixml = eventFeedBackXml.GetElementsByTagName("Ans2TextAni");
        XmlNodeList Ans3Feedbackxml = eventFeedBackXml.GetElementsByTagName("Ans3Feedback");
        XmlNodeList Ans3Dialoguexml = eventFeedBackXml.GetElementsByTagName("Ans3Dialogue");
        XmlNodeList Ans3TextAnixml = eventFeedBackXml.GetElementsByTagName("Ans3TextAni");
        EventFeedBackNum = EIDxml.Count;
        for (int i = 0; i < EventFeedBackNum; i++)
        {
            EventFeedBack efb = new EventFeedBack();
            //宣告初始化回答的交錯陣列
            efb.ansFeedback = new int[3][];
            efb.ansFeedback[0] = new int[3];
            efb.ansFeedback[1] = new int[3];
            efb.ansFeedback[2] = new int[3];
            efb.ansDialogue = new string[3];
            efb.ansTextAni = new string[3];


            //////////////////給予讀取的數值//////////////////////////
            efb.personalityID = int.Parse(PIDxml[i].InnerText);
            efb.eventID = int.Parse(EIDxml[i].InnerText);
            //將eventFeedBack的影響三項屬性用逗號分隔出來
            string[][] s = new string[3][];
            s[0] = Ans1Feedbackxml[i].InnerText.Split(',');
            s[1] = Ans2Feedbackxml[i].InnerText.Split(',');
            s[2] = Ans3Feedbackxml[i].InnerText.Split(',');
            //給予三項屬性的影響數值
            for (int j = 0; j < 3; j++)//三個回答
            {
                for (int k = 0; k < s[j].Length; k++)//三個屬性
                {
                    efb.ansFeedback[j][k] = int.Parse(s[j][k]);
                }
            }
            efb.ansDialogue[0] = Ans1Dialoguexml[i].InnerText;
            efb.ansDialogue[1] = Ans2Dialoguexml[i].InnerText;
            efb.ansDialogue[2] = Ans3Dialoguexml[i].InnerText;
            efb.ansTextAni[0] = Ans1TextAnixml[i].InnerText;
            efb.ansTextAni[1] = Ans2TextAnixml[i].InnerText;
            efb.ansTextAni[2] = Ans3TextAnixml[i].InnerText;
            EventFeedBackDic.Add(string.Format("{0}-{1}", efb.personalityID, efb.eventID), efb);
        }
    }

    private void LoadTouchFeedBackXml()
    {
        try
        {
            TouchFeedBackDic = new Dictionary<int, TouchFeedBack>();//TouchFeedBack的ID索引
            OwnPTouchFeedBack = new Dictionary<int, Dictionary<int, TouchFeedBack>>();//個性索引
            PFeedBackDic = new Dictionary<int, Dictionary<int, TouchFeedBack>>();
            TextAsset textXML = Resources.Load<TextAsset>("String/TouchFeedBack");
            XmlDocument touchFeedBackXml = new XmlDocument();
            touchFeedBackXml.LoadXml(textXML.text);
            XmlNodeList IDxml = touchFeedBackXml.GetElementsByTagName("ID");
            XmlNodeList FBDxml = touchFeedBackXml.GetElementsByTagName("FeedBackText");
            XmlNodeList TextAnixml = touchFeedBackXml.GetElementsByTagName("TextAni");
            XmlNodeList familiarxml = touchFeedBackXml.GetElementsByTagName("Familiar");
            XmlNodeList OwnPersonalityxml = touchFeedBackXml.GetElementsByTagName("OwnPersonality");

            TouchFeedBackNum = IDxml.Count;
            for (int i = 0; i < TouchFeedBackNum; i++)
            {


                TouchFeedBack touchFeedback = new TouchFeedBack();
                touchFeedback.id = int.Parse(IDxml[i].InnerText);
                touchFeedback.feedBackDialogue = FBDxml[i].InnerText;
                touchFeedback.textAni = TextAnixml[i].InnerText;
                string[] familiar = familiarxml[i].InnerText.Split(',');
                touchFeedback.familiarMin = int.Parse(familiar[0]);
                touchFeedback.familiarMax = int.Parse(familiar[1]);
                touchFeedback.ownPersonality = int.Parse(OwnPersonalityxml[i].InnerText);


                TouchFeedBackDic.Add(touchFeedback.id, touchFeedback);
                if (!PFeedBackDic.ContainsKey(touchFeedback.ownPersonality))
                {
                    PFeedBackDic.Add(touchFeedback.ownPersonality, new Dictionary<int, TouchFeedBack>());
                }
                PFeedBackDic[touchFeedback.ownPersonality].Add(touchFeedback.id, touchFeedback);
                if (!OwnPTouchFeedBack.ContainsKey(touchFeedback.ownPersonality))
                {
                    OwnPTouchFeedBack.Add(touchFeedback.ownPersonality, PFeedBackDic[touchFeedback.ownPersonality]);
                }
            }
            PersonalityNum = PFeedBackDic.Count;
        }
        catch(Exception ex)
        {
            Debug.Log(ex);
        }
    }


    private static void LoadDialogueXml()
    {
        EventDic = new Dictionary<int, Dialogue>();
        EventList = new List<Dialogue>();
        TextAsset textXML = Resources.Load<TextAsset>("String/EventDialogue");
        XmlDocument eventLodXml = new XmlDocument();
        eventLodXml.LoadXml(textXML.text);
        XmlNodeList IDinXml = eventLodXml.GetElementsByTagName("ID");
        XmlNodeList DinXml = eventLodXml.GetElementsByTagName("DiolgueText");
        XmlNodeList Ans1 = eventLodXml.GetElementsByTagName("Ans1");
        XmlNodeList Ans2 = eventLodXml.GetElementsByTagName("Ans2");
        XmlNodeList Ans3 = eventLodXml.GetElementsByTagName("Ans3");
        XmlNodeList Ans1Icon = eventLodXml.GetElementsByTagName("Ans1Icon");
        XmlNodeList Ans2Icon = eventLodXml.GetElementsByTagName("Ans2Icon");
        XmlNodeList Ans3Icon = eventLodXml.GetElementsByTagName("Ans3Icon");
        XmlNodeList DialogueIcon = eventLodXml.GetElementsByTagName("DiolgueIcon");
        XmlNodeList Action = eventLodXml.GetElementsByTagName("ActID");

        EventNum = IDinXml.Count;

        for (int i = 0; i < EventNum; i++)
        {
            Dialogue dlg = new Dialogue();
            dlg.id = IDinXml[i].InnerText;
            dlg.dialogue = DinXml[i].InnerText;
            dlg.dialogueIcon = DialogueIcon[i].InnerText;
            dlg.ans1 = Ans1[i].InnerText;
            dlg.ans2 = Ans2[i].InnerText;
            dlg.ans3 = Ans3[i].InnerText;
            dlg.ans1Icon = Ans1Icon[i].InnerText;
            dlg.ans2Icon = Ans2Icon[i].InnerText;
            dlg.ans3Icon = Ans3Icon[i].InnerText;
            dlg.action = int.Parse(Action[i].InnerText);

            EventDic.Add(int.Parse(dlg.id), dlg);
            EventList.Add(dlg);
        }
    }

    //物件讀入Scripts
    void LoadScripts()
    {
        gameObject.AddComponent<ResourceUI>();
        gameObject.AddComponent<TrainSceneInterfaceUI>();
        gameObject.AddComponent<TrainSceneUICtrl>();
        //gameObject.AddComponent<PlayerCtrl>();
    }
    void SpawnRoomResourceObjList()//建立資源清單物件
    {
        ///////////////////////////////////////////////LoadEventUI////////////////////////////////////////
        RoomResourceSpawnerResourceObj = Resources.Load<GameObject>("GameObject/RoomCom/RoomResourceSpawner");//資源物件清單位置物件
        Vector3 ResourceListPos = Vector3.zero;
        Quaternion eventUIRot = Quaternion.Euler(0, 0, 0);
        RoomResourceSpawnerObj = Instantiate(RoomResourceSpawnerResourceObj, ResourceListPos, eventUIRot) as GameObject;
        RoomResourceSpawnerObj.name = "RoomResourceSpawner";
        RoomResourceSpawner.StartSetRoomResourceSpawner();//起始設定
    }
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(gameObject);//切場景時不清除物件
        if (ChangeSceneFunction != null)
            ChangeSceneFunction();
        if (!TrainScene.StartSet)
        {
            LoadDialogueXml();
            LoadTouchFeedBackXml();
            LoadEventFeedBack();
            LoadSkillXml();
            SpawnRoomResourceObjList();//建立資源清單物件
            Player.PlayerSourceSetting(1);//設定玩者資源
            GameDictionary.LoadData();//讀取xml檔案，並建立遊戲字典
            TrainSceneInterfaceUI.StartSetUI();//建立UI物件
            Player.StartSet();
            LoadScripts();
        }
        else
        {
            Destroy(gameObject);
        }
        StartSet = true;//已初始設置
    }
}
