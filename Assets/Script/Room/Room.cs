using UnityEngine;
using System.Collections;
using System.Xml;
using System;
using System.IO;
using System.Collections.Generic;

public class Room : MonoBehaviour
{
    //房間ID，不會重複
    private int roomID;
    public int RoomID
    {
        get { return roomID; }
        set { roomID = value; }
    }
    //房間名稱
    private string roomName;
    public string RoomName
    {
        get { return roomName; }
        set { roomName = value; }
    }
    //房間風格樣式
    private int styleID;
    public int StyleID
    {
        get { return styleID; }
        set { styleID = value; }
    }
    //房間風格檔名稱
    private string styleFileName;
    public string StyleFileName
    {
        get { return styleFileName; }
        set { styleFileName = value; }
    }
    //房間等級
    public byte Lv { get; set; }//房間等級上限
    //紀錄現在房間的快樂度總值
    public int RoomHappy { get; private set; }
    /// <summary>
    /// 更新房間快樂度，當怪獸快樂度改變時呼叫
    /// </summary>
    public void UpdateRoomHappy()//更新房間快樂度，當怪獸快樂度改變時被呼叫
    {
        RoomHappy = 0;
        List<int> monsteBuffer = new List<int>(OwnMonsterDic.Keys);
        foreach (int monsterKey in monsteBuffer)
        {
            RoomHappy += OwnMonsterDic[monsterKey].Happy;
        }
    }
    //紀錄現在房間的最大快樂度
    private int roomMaxHappy;
    public int RoomMaxHappy
    {
        get { return roomMaxHappy; }
        private set
        {
            if (value < 100)
                roomMaxHappy = 100;
            else
            {
                roomMaxHappy = value;
            }
        }
    }
    /// <summary>
    /// 更新房間最大快樂度，當房間成員數量變動時被呼叫
    /// </summary>
    public void UpdateRoomMaxHappy()//更新房間最大快樂度，當房間成員數量變動時被呼叫
    {
        RoomMaxHappy = OwnMonsterDic.Count * 100;
    }
    //房間風格貼圖
    SpriteRenderer RoomStyleSprite;
    Sprite StyleSpriteResource;
    //房間怪獸容量上限
    public byte RoomCapacity { get; set; }//房間的怪獸容量上限
    public int Floor { get; set; }//此房間在第幾層
    public bool OnGhost { get; set; }//此房間是否正在鬧鬼
    public Dictionary<int, PlayerMonster> OwnMonsterDic;//此房間擁有的怪獸字典
    //房間的貼圖層級
    SpriteRenderer[] spritRendererArray;
    Transform myTransform;
    public HappyBar myHappyBar;//房間快樂度介面


    //房間已經擁有的資源物件
    Dictionary<int, RoomResourceData> RoomOwnResourceDic = new Dictionary<int, RoomResourceData>();
    static string Filepath = Application.persistentDataPath + @"/RoomResource.xml";
    static XmlDocument xmlDoc = new XmlDocument();
    static TextAsset XMLFlie_Room;
    static byte[] InBytes;



    public void StartSetRoomAttribute(Dictionary<string, string> _propertyDic, int _floor)
    {
        try
        {
            myTransform = transform;
            myHappyBar = myTransform.FindChild("happyBar").GetComponent<HappyBar>();
            RoomID = int.Parse(_propertyDic["RoomID"]);
            RoomName = _propertyDic["Name"];
            StyleID = int.Parse(_propertyDic["Style"]);
            Lv = byte.Parse(_propertyDic["LV"]);
            Floor = _floor;
            RoomCapacity = 3;
            SetSpriteSortingOrder();//依照房間屬於第幾層設定房間層級
            OwnMonsterDic = new Dictionary<int, PlayerMonster>();//初始化擁有怪獸字典
            //依房間名稱設定物件名稱
            SetName();
            //設定房間風格
            SetRoomStyle();
        }
        catch (Exception ex)
        {
            Debug.LogError("依造從Server取得的屬性設定房間資料時發生錯誤");
            throw ex;
        }
    }
    private void SetName()
    {
        myTransform.name = string.Format("Room{0}", RoomID);
    }
    /// <summary>
    /// 設定房間風格
    /// </summary>
    public void SetRoomStyle()//設定房間風格
    {

        try
        {
            RoomStyleSprite = myTransform.FindChild("style").GetComponent<SpriteRenderer>();
            StyleFileName = GameDictionary.RoomStyleDic[1];
            StyleSpriteResource = Resources.Load<Sprite>(String.Format("Sprite/Room/{0}", StyleFileName));
            RoomStyleSprite.sprite = StyleSpriteResource;
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            Debug.LogError("設定房間造景時發生錯誤");
        }

    }
    /// <summary>
    /// 將房間中加入此怪獸ID的怪獸
    /// </summary>
    public void AddMonster(int _monsterID)//在房間中加入怪獸
    {
        try
        {
            OwnMonsterDic.Add(_monsterID, Player.MonsterDic[_monsterID]);
            OwnMonsterDic[_monsterID].SetMonsterHappyInRoom(this);//取得房間的快樂度需求資訊
            UpdateRoomHappy();//更新房間快樂度，當怪獸快樂度改變時被呼叫
            UpdateRoomMaxHappy();//當怪物被加入到房間時，更新房間最大快樂度
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    /// <summary>
    /// 將房間中的此怪獸ID移除
    /// </summary>
    public void RemoveMonster(int _monsterID)//將怪獸從房間中移出
    {
        try
        {
            OwnMonsterDic.Remove(_monsterID);
            UpdateRoomHappy();//更新房間快樂度，當怪獸快樂度改變時被呼叫
            UpdateRoomMaxHappy();//當怪物被加入到房間時，更新房間最大快樂度
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    /// <summary>
    /// 依照房間屬於第幾層設定房間層級
    /// </summary>
    public void SetSpriteSortingOrder()//依照房間屬於第幾層設定房間層級
    {
        //抓取房間物件裡的子物件貼圖
        spritRendererArray = transform.GetComponentsInChildren<SpriteRenderer>();
        //設定貼圖深度
        foreach (SpriteRenderer sr in spritRendererArray)
        {
            sr.sortingOrder += Floor * 50;
        }
        myHappyBar.SetSortingOrder(Floor);//設定HappyBar貼圖層級
    }
    /// <summary>
    /// 起始設定房間就擁有的資源物件，若有存在String/RoomResource檔案，則讀取目標檔案，若不存在就建立新檔案
    /// </summary>
    public static void StartSetOwnResource()//起始設定房間就擁有的資源物件
    {
        Debug.Log(Filepath);
        if (File.Exists(Filepath))
        {
            xmlDoc.Load(Filepath);
        }
        else
        {
            //创建root节点，也就是最上一层节点
            XmlElement RootElement = xmlDoc.CreateElement("root");
            XmlElement LastSpawnTimeElement = xmlDoc.CreateElement("LastSpawnTime");
            RootElement.AppendChild(LastSpawnTimeElement);
            xmlDoc.AppendChild(RootElement);
            //把XML文件保存至本地
            xmlDoc.Save(Filepath);
        }

    }
    public void SetOwnResource()//從xml檔案中抓取屬於此房間的資源物件
    {
        try
        {
            XmlNodeList ResourceNodes = xmlDoc.SelectNodes(string.Format("root/Room{0}/Resource", RoomID));
            if (ResourceNodes != null)
            {
                for (int i = 0; i < ResourceNodes.Count; i++)
                {
                    if (ResourceNodes[i].InnerText != "")
                    {
                        XmlNode EventTyepNode = ResourceNodes[i].SelectSingleNode("EventType");
                        XmlNode ValueNode = ResourceNodes[i].SelectSingleNode("Value");
                        XmlNode PosNode = ResourceNodes[i].SelectSingleNode("Pos");
                        XmlNode ScaleNode = ResourceNodes[i].SelectSingleNode("Scale");
                        XmlNode MonsterIDNode = ResourceNodes[i].SelectSingleNode("MonsterID");
                        string posStr = PosNode.InnerText;
                        posStr = posStr.Replace("(", "");
                        posStr = posStr.Replace(")", "");
                        string[] posStrs = posStr.Split(',');
                        Vector3 tmpPos = new Vector3(float.Parse(posStrs[0]), float.Parse(posStrs[1]), float.Parse(posStrs[2]));
                        string scaleStr = ScaleNode.InnerText;
                        scaleStr = scaleStr.Replace("(", "");
                        scaleStr = scaleStr.Replace(")", "");
                        string[] scaleStrs = scaleStr.Split(',');
                        Vector3 tmpScale = new Vector3(float.Parse(scaleStrs[0]), float.Parse(scaleStrs[1]), float.Parse(scaleStrs[2]));
                        if (EventTyepNode.InnerText != "")
                        {
                            /*發生過xml資料不齊全問題(目前已解決)
                            Debug.LogError(EventTyepNode.InnerText);
                            Debug.LogError(ValueNode.InnerText);
                            Debug.LogError(PosNode.InnerText);
                            Debug.LogError(ScaleNode.InnerText);
                            Debug.LogError(MonsterIDNode.InnerText);
                            */
                            RoomResourceSpawner.SpawnResourceFromRoom(Player.MonsterDic[int.Parse(MonsterIDNode.InnerText)], this, byte.Parse(EventTyepNode.InnerText), int.Parse(ValueNode.InnerText), tmpPos, tmpScale);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    public static void SetLeaveGameResource()//設定離開遊戲時經過時間所創造的資源物件
    {
        ///////因時間經過生出資源物件
        XmlNode LastSpawnTimeNode = xmlDoc.SelectSingleNode("root/LastSpawnTime");
        if (LastSpawnTimeNode != null && LastSpawnTimeNode.InnerText != "")
        {
            DateTime LastTime = Convert.ToDateTime(LastSpawnTimeNode.InnerText);
            DateTime NoeTime = DateTime.Now;
            TimeSpan DiffTimeSpan = NoeTime - LastTime;
            int SpawnTimes = (int)(DiffTimeSpan.TotalMinutes / 4f);
            //Debug.Log("SpawnTimes=" + SpawnTimes);
            if (SpawnTimes > 0)
            {
                RoomResourceSpawner.AllMonsterSpanw(SpawnTimes);//遊戲離開一段時間回來時生成資源物件，傳入要生產資源物件次數
            }
        }
    }
    void WriteXmlFotAddResource()//當房間物件增加時，寫出xml檔案紀錄房間資源
    {
        try
        {
            XmlNode RootNode = xmlDoc.SelectSingleNode("root");
            XmlNode RoomNode = xmlDoc.SelectSingleNode(string.Format("root/Room{0}", RoomID));
            if (RoomNode != null)
            {
                RoomNode.InnerText = "";
                List<int> DicBuffer = new List<int>(RoomOwnResourceDic.Keys);
                foreach (int key in DicBuffer)
                {
                    XmlElement ResourceElement = xmlDoc.CreateElement("Resource");
                    XmlElement MonsterIDElement = xmlDoc.CreateElement("MonsterID");
                    XmlElement EventTypeElement = xmlDoc.CreateElement("EventType");
                    XmlElement ValueElement = xmlDoc.CreateElement("Value");
                    XmlElement PosElement = xmlDoc.CreateElement("Pos");
                    XmlElement ScaleElement = xmlDoc.CreateElement("Scale");
                    MonsterIDElement.InnerText = RoomOwnResourceDic[key].MonsterID.ToString();
                    EventTypeElement.InnerText = RoomOwnResourceDic[key].EventType.ToString();
                    ValueElement.InnerText = RoomOwnResourceDic[key].Value.ToString();
                    string tmpPos = RoomOwnResourceDic[key].ObjPosition.ToString();
                    PosElement.InnerText = tmpPos;
                    string tmpScale = RoomOwnResourceDic[key].ObjScale.ToString();
                    ScaleElement.InnerText = tmpScale;
                    ResourceElement.AppendChild(MonsterIDElement);
                    ResourceElement.AppendChild(EventTypeElement);
                    ResourceElement.AppendChild(ValueElement);
                    ResourceElement.AppendChild(PosElement);
                    ResourceElement.AppendChild(ScaleElement);
                    RoomNode.AppendChild(ResourceElement);
                }
            }
            else
            {
                XmlElement RoomElement = xmlDoc.CreateElement(string.Format("Room{0}", RoomID));
                List<int> DicBuffer = new List<int>(RoomOwnResourceDic.Keys);
                foreach (int key in DicBuffer)
                {
                    XmlElement ResourceElement = xmlDoc.CreateElement("Resource");
                    XmlElement MonsterIDElement = xmlDoc.CreateElement("MonsterID");
                    XmlElement EventTypeElement = xmlDoc.CreateElement("EventType");
                    XmlElement ValueElement = xmlDoc.CreateElement("Value");
                    XmlElement PosElement = xmlDoc.CreateElement("Pos");
                    XmlElement ScaleElement = xmlDoc.CreateElement("Scale");
                    MonsterIDElement.InnerText = RoomOwnResourceDic[key].MonsterID.ToString();
                    EventTypeElement.InnerText = RoomOwnResourceDic[key].EventType.ToString();
                    ValueElement.InnerText = RoomOwnResourceDic[key].Value.ToString();
                    string tmpPos = RoomOwnResourceDic[key].ObjPosition.ToString();
                    PosElement.InnerText = tmpPos;
                    string tmpScale = RoomOwnResourceDic[key].ObjScale.ToString();
                    ScaleElement.InnerText = tmpScale;
                    ResourceElement.AppendChild(MonsterIDElement);
                    ResourceElement.AppendChild(EventTypeElement);
                    ResourceElement.AppendChild(ValueElement);
                    ResourceElement.AppendChild(PosElement);
                    ResourceElement.AppendChild(ScaleElement);
                    RoomElement.AppendChild(ResourceElement);
                }
                RootNode.AppendChild(RoomElement);
            }
            //紀錄時間
            XmlNode LastSpawnTimeNode = RootNode.SelectSingleNode("LastSpawnTime");
            DateTime NowTime = DateTime.Now;
            string NowTimeString = NowTime.ToString("yyyy-MM-dd HH:mm:ss");
            LastSpawnTimeNode.InnerText = NowTimeString;
            RootNode.AppendChild(LastSpawnTimeNode);
            xmlDoc.Save(Filepath);
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    void WriteXmlFotRemoveResource()//當房間物件減少時，寫出xml檔案紀錄房間資源
    {
        try
        {
            XmlNode RoomNode = xmlDoc.SelectSingleNode(string.Format("root/Room{0}", RoomID));
            if (RoomNode != null)
            {
                RoomNode.InnerText = "";
                List<int> DicBuffer = new List<int>(RoomOwnResourceDic.Keys);
                foreach (int key in DicBuffer)
                {
                    XmlElement ResourceElement = xmlDoc.CreateElement("Resource");
                    XmlElement MonsterIDElement = xmlDoc.CreateElement("MonsterID");
                    XmlElement EventTypeElement = xmlDoc.CreateElement("EventType");
                    XmlElement ValueElement = xmlDoc.CreateElement("Value");
                    XmlElement PosElement = xmlDoc.CreateElement("Pos");
                    XmlElement ScaleElement = xmlDoc.CreateElement("Scale");
                    MonsterIDElement.InnerText = RoomOwnResourceDic[key].MonsterID.ToString();
                    EventTypeElement.InnerText = RoomOwnResourceDic[key].EventType.ToString();
                    ValueElement.InnerText = RoomOwnResourceDic[key].Value.ToString();
                    string tmpPos = RoomOwnResourceDic[key].ObjPosition.ToString();
                    PosElement.InnerText = tmpPos;
                    string tmpScale = RoomOwnResourceDic[key].ObjScale.ToString();
                    ScaleElement.InnerText = tmpScale;
                    ResourceElement.AppendChild(MonsterIDElement);
                    ResourceElement.AppendChild(EventTypeElement);
                    ResourceElement.AppendChild(ValueElement);
                    ResourceElement.AppendChild(PosElement);
                    ResourceElement.AppendChild(ScaleElement);
                    RoomNode.AppendChild(ResourceElement);
                }
                xmlDoc.Save(Filepath);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    /// <summary>
    /// 新增資源物件到房間裡擁有的資源物件清單
    /// </summary>
    public void AddRoomOwnResource(PlayerMonster _resourceOwnMonster, int _resourceID, byte _eventType, int _resourceNum, Vector3 _pos, Vector3 _scale)
    {
        try
        {
            RoomResourceData tmpResource = new RoomResourceData(_resourceOwnMonster, _resourceID, _eventType, _resourceNum, _pos, _scale);
            RoomOwnResourceDic.Add(_resourceID, tmpResource);
            AddResource(_resourceID);//因新增資源物件而影響怪獸大便量及生病，房間是否鬧鬼中
            WriteXmlFotAddResource();//當房間物件增加時，寫出xml檔案紀錄房間資源
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
        }
    }
    /// <summary>
    /// 移除房間裡擁有的資源物件
    /// </summary>
    public void RemoveRoomOwnResource(int _resourceID)
    {
        RemoveResource(_resourceID);//因移除資源物件而影響怪獸大便量及生病，房間是否鬧鬼中
        RoomOwnResourceDic.Remove(_resourceID);
        WriteXmlFotRemoveResource();//當房間物件減少時，寫出xml檔案紀錄房間資源
    }
    /// <summary>
    /// 起始生出的資源物件加入字典中
    /// </summary>
    public void StartAddRoomOwnResource(PlayerMonster _resourceOwnMonster, int _resourceID, byte _eventType, int _resourceNum, Vector3 _pos, Vector3 _scale)
    {
        RoomResourceData tmpResource = new RoomResourceData(_resourceOwnMonster, _resourceID, _eventType, _resourceNum, _pos, _scale);
        RoomOwnResourceDic.Add(_resourceID, tmpResource);
        AddResource(_resourceID);//因新增資源物件而影響怪獸大便量及生病，房間是否鬧鬼中
    }
    /// <summary>
    /// //因新增資源物件而影響怪獸大便量及生病，房間是否鬧鬼中
    /// </summary>
    void AddResource(int _resourceID)//因新增資源物件而影響怪獸大便量及生病，房間是否鬧鬼中
    {
        switch (RoomOwnResourceDic[_resourceID].EventType)
        {
            case 0:
                RoomOwnResourceDic[_resourceID].OwnMonster.ShitCurNum++;
                break;
            case 1:
                RoomOwnResourceDic[_resourceID].OwnMonster.OnVirus = true;
                break;
            case 2:
                OnGhost = true;
                break;
            default:
                Debug.LogError("無此事件種類");
                break;
        }
    }
    /// <summary>
    /// 因移除資源物件而影響怪獸大便量及生病，房間是否鬧鬼中
    /// </summary>
    void RemoveResource(int _resourceID)//因移除資源物件而影響怪獸大便量及生病，房間是否鬧鬼中
    {
        switch (RoomOwnResourceDic[_resourceID].EventType)
        {
            case 0:
                RoomOwnResourceDic[_resourceID].OwnMonster.ShitCurNum--;
                break;
            case 1:
                RoomOwnResourceDic[_resourceID].OwnMonster.OnVirus = false;
                break;
            case 2:
                OnGhost = false;
                break;
            default:
                Debug.LogError("無此事件種類");
                break;
        }
    }
}
