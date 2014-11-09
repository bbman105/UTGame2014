using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System;

public class GameDictionary
{
    //預設怪獸字典
    public static Dictionary<int, DefaultMonsterData> DefaultMonsterDic { get; set; }
    //特效字典
    public static Dictionary<int, string> EffectDic { get; set; }
    //總技能字典
    public static Dictionary<int, Skill> SkillDic;
    //天賦字典
    public static Dictionary<int, Talent> TalentDic { get; set; }
    //房間風格字典
    public static Dictionary<int, string> RoomStyleDic { get; set; }
    //經驗表字典
    public static Dictionary<int, Dictionary<string, int>> MonsterExperienceDic { get; set; }
    //物種名稱字典
    public static Dictionary<string, string> SpeciesNameDic { get; set; }
    //依照強化怪物素材ID取的素材
    public static Dictionary<int, EnhanceMaterial> EnhanceMaterialDic { get; set; }//依照[怪獸強化素材ID]取得強化素材
    //元素物種進化需求素材
    public static Dictionary<int, Dictionary<int, EnhanceNeedMaterial>> EnhanceNeedMaterialDic { get; set; }//依照[元素][怪獸品質]取得需求素材
    //物種品質屬性字典
    public static Dictionary<int, Dictionary<int, RareBaseProperty>> SpeciesRarePropertyDic { get; set; }//[物種DI][品質]
    //物種等級增加屬性字典
    //public static Dictionary<int, Dictionary<int, LevelProperty>> SpeciesLevelPropertyDic { get; set; }//[物種DI][等級]
    //物種進化取得技能字典
    public static Dictionary<int, Dictionary<int, Skill[]>> SpeciesEvolutionSkillDic { get; set; }//[物種DI][第幾階]
    //物種進化取得天賦字典
    public static Dictionary<int, Dictionary<int, Talent>> SpeciesEvolutionTalentDic { get; set; }//[物種DI][第幾階]
    //物種進化增加屬性字典
    public static Dictionary<int, Dictionary<int, EvolutionProperty>> SpeciesEvolutionPropertyDic { get; set; }//[物種DI][第幾階]
    //地圖關卡字典
    public static Dictionary<int, Map> MapDic { get; set; }
    //任務成就字典
    public static Dictionary<int, AchievementType> AchievementTypeDic { get; set; }
    //進化樹字典，給予SpeciesID找到可進化的物種
    public static Dictionary<string, SpeciesEvolution> EvolutionTreeDic { get; set; }
    //關卡任務字典
    public static Dictionary<int, Quest> QuestDic { get; set; }
    //關卡召喚事件字典
    public static Dictionary<int, SummonEvent> SummonEventDic { get; set; }
    //NPC字典
    public static Dictionary<int, NPC> NPCDic { get; set; }

    public static void LoadData()
    {
        LoadDefaultMonster();//讀取預設怪獸xml檔案
        LoadSkillFile();//讀取技能xml檔案，並設定技能字典
        LoadEffectFile();//讀取特效xml檔案，並設定特效字典
        LoadRoomStyleFile();//讀取房間風格xml檔案，並設定字典
        LoadTalent();//讀取天賦XML檔案，並設定天賦字典
        LoadMonsterExperience();//讀取怪物經驗xml檔案，並設定怪物經驗字典
        LoadEnhanceItemData();//讀取強化素材資料
        LoadEnhanceNeedMaterialData();//讀取元素物種需求素材
        LoadMonsterRareProperty();//讀取物種品質屬性xml檔案，並設物種物品質屬性字典
        LoadMonsterSpeciesName();//讀取物種名稱xml檔案，並設定物種字典
        LoadMonsterEvolutionProperty();//讀取物種進化增加屬性xml檔案，並設定物種等級屬性字典
        LoadMonsterEvolutionSkill();//讀取物種進化取得技能
        LoadMonsterTalent();//讀取物種進化取得天賦
        LoadMapQuestData();//讀取地圖任務xml檔案，並設定任務資料
        LoadAchievementTypeData();//讀取任務成就種類xml檔案，並設定成就種類資料
        LoadEvolutionTreeData();//讀取進化表xml檔案，並設定進化資料
        LoadQuestDic();//讀取任務xml檔案
        LoadSummonEventDic();//讀取召喚事件xml檔案
        LoadNPCDic();//讀取NPC怪物字典
    }
    public static void LoadDefaultMonster()//讀取預設怪獸xml檔案
    {
        DefaultMonsterDic = new Dictionary<int, DefaultMonsterData>();
        try
        {
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/PlayerDefaultMonster");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList IDNodes = doc.GetElementsByTagName("DefaultMonsterID");
            for (int i = 0; i < IDNodes.Count; i++)
            {
                int monsterID = int.Parse(IDNodes[i].InnerText);
                string name = IDNodes[i].ParentNode.SelectSingleNode("Name").InnerText;
                int species = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("Species").InnerText);
                int speciesLevel = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("SpeciesLevel").InnerText);
                int rare = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("Rare").InnerText);
                int lv = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("LV").InnerText);
                int health = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("Health").InnerText);
                int wild = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("Wild").InnerText);
                int mutation = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("Mutation").InnerText);
                int natural = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("Natural").InnerText);
                int happy = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("Happy").InnerText);
                int brutal = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("Brutal").InnerText);
                int aggressive = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("Aggressive").InnerText);
                int resistance = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("Resistance").InnerText);
                int mainElement = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("MainElement").InnerText);
                int exp = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("EXP").InnerText);
                int personality = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("Personality").InnerText);
                int selectedSkill = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("SelectedSkill").InnerText);
                int level2Species = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("Level2Species").InnerText);
                int level3Speices = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("Level3Species").InnerText);
                int inRoomID = int.Parse(IDNodes[i].ParentNode.SelectSingleNode("InRoomID").InnerText);
                DefaultMonsterData monsterData = new DefaultMonsterData();
                monsterData.DefaultSetMonsterData(monsterID, name, species, speciesLevel, rare, lv, health, wild, mutation,
                    natural, happy, brutal, aggressive, resistance, mainElement, exp, personality, selectedSkill,
                    level2Species, level3Speices, inRoomID);
                DefaultMonsterDic.Add(monsterID, monsterData);
            }
        }
        catch
        {
            Debug.LogError("讀入預設怪獸資料(PlayerDefaultMonster.xml)時，發生錯誤");
        }
    }
    public static void LoadEffectFile()//讀取特效xml檔案，並設定特效字典
    {
        EffectDic = new Dictionary<int, string>();
        try
        {
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/EffectFile");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList FileNameNodes = doc.GetElementsByTagName("EffectFile");
            XmlNodeList IDNodes = doc.GetElementsByTagName("EffectID");
            for (int i = 0; i < FileNameNodes.Count; i++)
            {

                EffectDic.Add(int.Parse(IDNodes[i].InnerText), FileNameNodes[i].InnerText);
            }
        }
        catch
        {
            Debug.LogError("讀入特效資料(EffectFile.xml)時，發生錯誤");
        }
    }
    public static void LoadRoomStyleFile()//讀取房間風格xml檔案，並設定字典
    {
        try
        {
            RoomStyleDic = new Dictionary<int, string>();
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/RoomStyleFile");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList FileNameNodes = doc.GetElementsByTagName("StyleFile");
            XmlNodeList IDNodes = doc.GetElementsByTagName("StyleID");
            for (int i = 0; i < FileNameNodes.Count; i++)
            {
                RoomStyleDic.Add(int.Parse(IDNodes[i].InnerText), FileNameNodes[i].InnerText);
            }
        }
        catch
        {
            Debug.LogError("讀入房間風格資料(RoomStyleFile.xml)時，發生錯誤");
        }
    }
    private static void LoadSkillFile()//讀取技能xml檔案，並設定技能字典
    {
        SkillDic = new Dictionary<int, Skill>();
        //依照法術編號讀取法術資料中的法術各屬性
        TextAsset XMLFlie = Resources.Load<TextAsset>("String/Spell");
        //讀取XML文件
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(XMLFlie.text);
        XmlNodeList SPIDxml = doc.GetElementsByTagName("SpellID");
        for (int i = 0; i < SPIDxml.Count; i++)
        {
            int spellID = int.Parse(SPIDxml[i].InnerText);
            Skill fs = new Skill(spellID);
            SkillDic.Add(spellID, fs);
        }
    }
    private static void LoadTalent()//讀取天賦XML檔案，並設定天賦字典
    {
        TalentDic = new Dictionary<int, Talent>();
        //依照法術編號讀取法術資料中的法術各屬性
        TextAsset XMLFlie = Resources.Load<TextAsset>("String/Talent");
        //讀取XML文件
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(XMLFlie.text);
        XmlNodeList TIDxml = doc.GetElementsByTagName("TalentID");
        for (int i = 0; i < TIDxml.Count; i++)
        {
            int talentID = int.Parse(TIDxml[i].InnerText);
            Talent talent = new Talent(talentID);
            TalentDic.Add(talentID, talent);
        }
    }
    public static void LoadMonsterExperience()//讀取怪物經驗xml檔案，並設定怪物經驗字典
    {
        MonsterExperienceDic = new Dictionary<int, Dictionary<string, int>>();
        try
        {
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/MonsterExperience");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList LvNodes = doc.GetElementsByTagName("LV");
            XmlNodeList CopperNodes = doc.GetElementsByTagName("Copper");
            XmlNodeList SilverNodes = doc.GetElementsByTagName("Silver");
            XmlNodeList GoldNodes = doc.GetElementsByTagName("Gold");
            XmlNodeList DimandNodes = doc.GetElementsByTagName("Dimand");
            XmlNodeList CrystalNodes = doc.GetElementsByTagName("Crystal");
            for (int i = 0; i < LvNodes.Count; i++)
            {
                Dictionary<string, int> MonsterExperienceChildDic = new Dictionary<string, int>();
                MonsterExperienceChildDic.Add("Copper", int.Parse(CopperNodes[i].InnerText));
                MonsterExperienceChildDic.Add("Silver", int.Parse(SilverNodes[i].InnerText));
                MonsterExperienceChildDic.Add("Gold", int.Parse(GoldNodes[i].InnerText));
                MonsterExperienceChildDic.Add("Dimand", int.Parse(DimandNodes[i].InnerText));
                MonsterExperienceChildDic.Add("Crystal", int.Parse(CrystalNodes[i].InnerText));
                try
                {
                    MonsterExperienceDic.Add(int.Parse(LvNodes[i].InnerText), MonsterExperienceChildDic);
                }
                catch
                {
                    Debug.LogError("讀入怪物經驗表(MonsterExperience.xml)時，等級欄位重複，資料錯誤");
                }
            }
        }
        catch
        {
            Debug.LogError("讀入怪物經驗表(MonsterExperience.xml)時，發生錯誤");
        }
    }
    public static void LoadMonsterRareProperty()//讀取物種品質屬性xml檔案，並設定物種品質屬性字典
    {
        SpeciesRarePropertyDic = new Dictionary<int, Dictionary<int, RareBaseProperty>>();
        try
        {
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/SpeciesProperty");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList SpeciesNodes = doc.GetElementsByTagName("Species");
            XmlNodeList CopperNodes = doc.GetElementsByTagName("Copper");
            XmlNodeList SilverNodes = doc.GetElementsByTagName("Silver");
            XmlNodeList GoldNodes = doc.GetElementsByTagName("Gold");
            XmlNodeList DimandNodes = doc.GetElementsByTagName("Dimand");
            XmlNodeList CrystalNodes = doc.GetElementsByTagName("Crystal");
            for (int i = 0; i < SpeciesNodes.Count; i++)
            {
                int speciesID = int.Parse(SpeciesNodes[i].InnerText);
                string[] CopperProperty = CopperNodes[i].InnerText.Split(',');
                string[] SilverProperty = SilverNodes[i].InnerText.Split(',');
                string[] GoldProperty = GoldNodes[i].InnerText.Split(',');
                string[] DimandProperty = DimandNodes[i].InnerText.Split(',');
                string[] CrystalProperty = CrystalNodes[i].InnerText.Split(',');
                Dictionary<int, RareBaseProperty> MonsterRarePropertyChildDic = new Dictionary<int, RareBaseProperty>();
                RareBaseProperty copperBaseProperty = new RareBaseProperty();//紅銅各屬性
                copperBaseProperty.BaseHealth = int.Parse(CopperProperty[0]);
                copperBaseProperty.BaseBrutal = int.Parse(CopperProperty[1]);
                copperBaseProperty.BaseResistance = int.Parse(CopperProperty[2]);
                copperBaseProperty.BaseAggressive = int.Parse(CopperProperty[3]);
                MonsterRarePropertyChildDic.Add(1, copperBaseProperty);
                RareBaseProperty silverBaseProperty = new RareBaseProperty();//白銀各屬性
                silverBaseProperty.BaseHealth = int.Parse(SilverProperty[0]);
                silverBaseProperty.BaseBrutal = int.Parse(SilverProperty[1]);
                silverBaseProperty.BaseResistance = int.Parse(SilverProperty[2]);
                silverBaseProperty.BaseAggressive = int.Parse(SilverProperty[3]);
                MonsterRarePropertyChildDic.Add(2, silverBaseProperty);
                RareBaseProperty goldBaseProperty = new RareBaseProperty();//黃金各屬性
                goldBaseProperty.BaseHealth = int.Parse(GoldProperty[0]);
                goldBaseProperty.BaseBrutal = int.Parse(GoldProperty[1]);
                goldBaseProperty.BaseResistance = int.Parse(GoldProperty[2]);
                goldBaseProperty.BaseAggressive = int.Parse(GoldProperty[3]);
                MonsterRarePropertyChildDic.Add(3, goldBaseProperty);
                RareBaseProperty dimandBaseProperty = new RareBaseProperty();//藍鑽各屬性
                dimandBaseProperty.BaseHealth = int.Parse(DimandProperty[0]);
                dimandBaseProperty.BaseBrutal = int.Parse(DimandProperty[1]);
                dimandBaseProperty.BaseResistance = int.Parse(DimandProperty[2]);
                dimandBaseProperty.BaseAggressive = int.Parse(DimandProperty[3]);
                MonsterRarePropertyChildDic.Add(4, dimandBaseProperty);
                RareBaseProperty crystalBaseProperty = new RareBaseProperty();//紫晶各屬性
                crystalBaseProperty.BaseHealth = int.Parse(CrystalProperty[0]);
                crystalBaseProperty.BaseBrutal = int.Parse(CrystalProperty[1]);
                crystalBaseProperty.BaseResistance = int.Parse(CrystalProperty[2]);
                crystalBaseProperty.BaseAggressive = int.Parse(CrystalProperty[3]);
                MonsterRarePropertyChildDic.Add(5, crystalBaseProperty);
                SpeciesRarePropertyDic.Add(speciesID, MonsterRarePropertyChildDic);
            }
        }
        catch
        {
            Debug.LogError("讀入怪物品質屬性(SpeciesProperty.xml)時，發生錯誤");
        }
    }
    /*
    public static void LoadMonsterLevelProperty()//讀取物種等級增加屬性xml檔案，並設定物種等級增加屬性字典
    {
        //給予[物種][等級]找出升級的屬性
        SpeciesLevelPropertyDic = new Dictionary<int, Dictionary<int, LevelProperty>>();

        try
        {
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/LevelProperty");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList SpeciesNodes = doc.GetElementsByTagName("Species");
            for (int i = 1; i <= SpeciesNodes.Count; i++)
            {
                XmlNode SpeciesNode = doc.SelectSingleNode(string.Format("root/SpeciesID{0}", i));
                Dictionary<int, LevelProperty> MonsterLevelPropertyChildDic = new Dictionary<int, LevelProperty>();
                for (int j = 1; j <= 30; j++)
                {
                    XmlNode levelNode = SpeciesNode.SelectSingleNode(string.Format("LV{0}", j));
                    string[] LevelProperty = levelNode.InnerText.Split(',');
                    LevelProperty levelProperty = new LevelProperty();
                    levelProperty.AddHealth = int.Parse(LevelProperty[0]);
                    levelProperty.AddBrutal = int.Parse(LevelProperty[1]);
                    levelProperty.AddResistance = int.Parse(LevelProperty[2]);
                    levelProperty.AddAggressive = int.Parse(LevelProperty[3]);
                    MonsterLevelPropertyChildDic.Add(j, levelProperty);
                }
                SpeciesLevelPropertyDic.Add(i, MonsterLevelPropertyChildDic);
            }
        }
        catch
        {
            Debug.LogError("讀入怪物等級增加屬性(LevelProperty.xml)時，發生錯誤");
        }
    }
    */
    public static void LoadMonsterSpeciesName()//讀取物種名稱xml檔案，並設定物種字典
    {
        SpeciesNameDic = new Dictionary<string, string>();
        try
        {
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/SpeciesName");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList SpeciesIDNodes = doc.GetElementsByTagName("SpeciesID");
            XmlNodeList SpeciesNameNodes = doc.GetElementsByTagName("SpeciesName");
            for (int i = 0; i < SpeciesIDNodes.Count; i++)
            {
                string speciesKey = SpeciesIDNodes[i].InnerText;
                string speciesName = SpeciesNameNodes[i].InnerText;
                SpeciesNameDic.Add(speciesKey, speciesName);
            }
        }
        catch
        {
            Debug.LogWarning("讀入物種名稱(SpeciesName.xml)時，發生錯誤");
        }
    }
    public static void LoadMonsterEvolutionProperty()//讀取物種進化屬性xml檔案，並設定怪物進化屬性字典
    {
        SpeciesEvolutionPropertyDic = new Dictionary<int, Dictionary<int, EvolutionProperty>>();

        try
        {
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/EvolutionProperty");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList SpeciesNodes = doc.GetElementsByTagName("Species");
            XmlNodeList BaseNodes = doc.GetElementsByTagName("Base");
            XmlNodeList Natural2Nodes = doc.GetElementsByTagName("Natural2");
            XmlNodeList Mutation2Nodes = doc.GetElementsByTagName("Mutation2");
            XmlNodeList Wild2Nodes = doc.GetElementsByTagName("Wild2");
            XmlNodeList Natural3Nodes = doc.GetElementsByTagName("Natural3");
            XmlNodeList Mutation3Nodes = doc.GetElementsByTagName("Mutation3");
            XmlNodeList Wild3Nodes = doc.GetElementsByTagName("Wild3");
            for (int i = 0; i < SpeciesNodes.Count; i++)
            {
                int speciesID = int.Parse(SpeciesNodes[i].InnerText);
                string[] BaseProperty = BaseNodes[i].InnerText.Split(',');
                string[] Natural2Property = Natural2Nodes[i].InnerText.Split(',');
                string[] Mutation2Property = Mutation2Nodes[i].InnerText.Split(',');
                string[] Wild2Property = Wild2Nodes[i].InnerText.Split(',');
                string[] Natural3Property = Natural3Nodes[i].InnerText.Split(',');
                string[] Mutation3Property = Mutation3Nodes[i].InnerText.Split(',');
                string[] Wild3Property = Wild3Nodes[i].InnerText.Split(',');
                Dictionary<int, EvolutionProperty> MonsterEvolutionPropertyChildDic = new Dictionary<int, EvolutionProperty>();
                EvolutionProperty baseProperty = new EvolutionProperty();//原生2階各屬性
                baseProperty.AddHealth = int.Parse(BaseProperty[0]);
                baseProperty.AddBrutal = int.Parse(BaseProperty[1]);
                baseProperty.AddResistance = int.Parse(BaseProperty[2]);
                baseProperty.AddAggressive = int.Parse(BaseProperty[3]);
                baseProperty.AddSize = float.Parse(BaseProperty[4]);
                MonsterEvolutionPropertyChildDic.Add(0, baseProperty);
                EvolutionProperty natural2Property = new EvolutionProperty();//原生2階各屬性
                natural2Property.AddHealth = int.Parse(Natural2Property[0]);
                natural2Property.AddBrutal = int.Parse(Natural2Property[1]);
                natural2Property.AddResistance = int.Parse(Natural2Property[2]);
                natural2Property.AddAggressive = int.Parse(Natural2Property[3]);
                natural2Property.AddSize = float.Parse(Natural2Property[4]);
                MonsterEvolutionPropertyChildDic.Add(1, natural2Property);
                EvolutionProperty mutation2Property = new EvolutionProperty();//突變2階各屬性
                mutation2Property.AddHealth = int.Parse(Mutation2Property[0]);
                mutation2Property.AddBrutal = int.Parse(Mutation2Property[1]);
                mutation2Property.AddResistance = int.Parse(Mutation2Property[2]);
                mutation2Property.AddAggressive = int.Parse(Mutation2Property[3]);
                mutation2Property.AddSize = float.Parse(Mutation2Property[4]);
                MonsterEvolutionPropertyChildDic.Add(2, mutation2Property);
                EvolutionProperty wild2Property = new EvolutionProperty();//野性2階各屬性
                wild2Property.AddHealth = int.Parse(Wild2Property[0]);
                wild2Property.AddBrutal = int.Parse(Wild2Property[1]);
                wild2Property.AddResistance = int.Parse(Wild2Property[2]);
                wild2Property.AddAggressive = int.Parse(Wild2Property[3]);
                wild2Property.AddSize = float.Parse(Wild2Property[4]);
                MonsterEvolutionPropertyChildDic.Add(3, wild2Property);
                EvolutionProperty natural3Property = new EvolutionProperty();//原生3階各屬性
                natural3Property.AddHealth = int.Parse(Natural3Property[0]);
                natural3Property.AddBrutal = int.Parse(Natural3Property[1]);
                natural3Property.AddResistance = int.Parse(Natural3Property[2]);
                natural3Property.AddAggressive = int.Parse(Natural3Property[3]);
                natural3Property.AddSize = float.Parse(Natural3Property[4]);
                MonsterEvolutionPropertyChildDic.Add(4, natural3Property);
                EvolutionProperty mutation3Property = new EvolutionProperty();//突變3階各屬性
                mutation3Property.AddHealth = int.Parse(Mutation3Property[0]);
                mutation3Property.AddBrutal = int.Parse(Mutation3Property[1]);
                mutation3Property.AddResistance = int.Parse(Mutation3Property[2]);
                mutation3Property.AddAggressive = int.Parse(Mutation3Property[3]);
                mutation3Property.AddSize = float.Parse(Mutation3Property[4]);
                MonsterEvolutionPropertyChildDic.Add(5, mutation3Property);
                EvolutionProperty wild3Property = new EvolutionProperty();//野性3階各屬性
                wild3Property.AddHealth = int.Parse(Wild3Property[0]);
                wild3Property.AddBrutal = int.Parse(Wild3Property[1]);
                wild3Property.AddResistance = int.Parse(Wild3Property[2]);
                wild3Property.AddAggressive = int.Parse(Wild3Property[3]);
                wild3Property.AddSize = float.Parse(Wild3Property[4]);
                MonsterEvolutionPropertyChildDic.Add(6, wild3Property);
                SpeciesEvolutionPropertyDic.Add(speciesID, MonsterEvolutionPropertyChildDic);
            }
        }
        catch
        {
            Debug.LogError("讀入物種進化屬性(EvolutionProperty.xml)時，發生錯誤");
        }
    }

    public static void LoadEnhanceNeedMaterialData()//讀取元素物種需求素材
    {
        EnhanceNeedMaterialDic = new Dictionary<int, Dictionary<int, EnhanceNeedMaterial>>();
        try
        {
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/EnhanceMaterial");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList ElementIDNodes = doc.GetElementsByTagName("ElementID");
            XmlNodeList CopperNodes = doc.GetElementsByTagName("Copper");
            XmlNodeList SilverNodes = doc.GetElementsByTagName("Silver");
            XmlNodeList GoldNodes = doc.GetElementsByTagName("Gold");
            XmlNodeList DimandNodes = doc.GetElementsByTagName("Dimand");
            for (int i = 0; i < ElementIDNodes.Count; i++)
            {
                int elementID = int.Parse(ElementIDNodes[i].InnerText);
                string[] copperNeed = CopperNodes[i].InnerText.Split(',');
                string[] silverNeed = SilverNodes[i].InnerText.Split(',');
                string[] goldNeed = GoldNodes[i].InnerText.Split(',');
                string[] dimandNeed = DimandNodes[i].InnerText.Split(',');
                Dictionary<int, EnhanceNeedMaterial> EnhanceNeedMaterialChildDic = new Dictionary<int, EnhanceNeedMaterial>();
                EnhanceNeedMaterial copperNeedMaterial = new EnhanceNeedMaterial(int.Parse(copperNeed[0]), int.Parse(copperNeed[1]), int.Parse(copperNeed[2]));
                EnhanceNeedMaterial silverNeedMaterial = new EnhanceNeedMaterial(int.Parse(silverNeed[0]), int.Parse(silverNeed[1]), int.Parse(copperNeed[2]));
                EnhanceNeedMaterial goldNeedMaterial = new EnhanceNeedMaterial(int.Parse(goldNeed[0]), int.Parse(goldNeed[1]), int.Parse(copperNeed[2]));
                EnhanceNeedMaterial dimandNeedMaterial = new EnhanceNeedMaterial(int.Parse(dimandNeed[0]), int.Parse(dimandNeed[1]), int.Parse(copperNeed[2]));
                EnhanceNeedMaterial crystalNeedMaterial = new EnhanceNeedMaterial(0, 0, 0);
                EnhanceNeedMaterialChildDic.Add(1, copperNeedMaterial);
                EnhanceNeedMaterialChildDic.Add(2, silverNeedMaterial);
                EnhanceNeedMaterialChildDic.Add(3, goldNeedMaterial);
                EnhanceNeedMaterialChildDic.Add(4, dimandNeedMaterial);
                EnhanceNeedMaterialChildDic.Add(5, crystalNeedMaterial);
                EnhanceNeedMaterialDic.Add(elementID, EnhanceNeedMaterialChildDic);
            }
        }
        catch
        {
            Debug.LogError("讀入怪獸需求強化(EnhanceMaterial.xml)時，發生錯誤");
        }
    }
    public static void LoadEnhanceItemData()//讀取強化素材資料
    {
        EnhanceMaterialDic = new Dictionary<int, EnhanceMaterial>();
        try
        {
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/Item");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList ItemIDNode = doc.GetElementsByTagName("ItemID");
            XmlNodeList NameNode = doc.GetElementsByTagName("Name");
            XmlNodeList SpriteNameNode = doc.GetElementsByTagName("SpriteName");
            for (int i = 0; i < ItemIDNode.Count; i++)
            {
                int itemID = int.Parse(ItemIDNode[i].InnerText);
                string name = NameNode[i].InnerText;
                int spriteNameID = int.Parse(SpriteNameNode[i].InnerText);
                EnhanceMaterial enhanceMaterial = new EnhanceMaterial(itemID, name, spriteNameID);
                EnhanceMaterialDic.Add(itemID, enhanceMaterial);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("讀入物品表(Item.xml)時，發生錯誤");
            Debug.LogError(ex);
        }
    }
    public static void LoadMonsterEvolutionSkill()//讀取物種進化習得技能xml檔案，並設定怪物進化技能字典
    {
        SpeciesEvolutionSkillDic = new Dictionary<int, Dictionary<int, Skill[]>>();

        try
        {
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/EvolutionSkill");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList SpeciesNodes = doc.GetElementsByTagName("Species");
            XmlNodeList BaseNodes = doc.GetElementsByTagName("Base");
            XmlNodeList Natural2Nodes = doc.GetElementsByTagName("Natural2");
            XmlNodeList Mutation2Nodes = doc.GetElementsByTagName("Mutation2");
            XmlNodeList Wild2Nodes = doc.GetElementsByTagName("Wild2");
            XmlNodeList Natural3Nodes = doc.GetElementsByTagName("Natural3");
            XmlNodeList Mutation3Nodes = doc.GetElementsByTagName("Mutation3");
            XmlNodeList Wild3Nodes = doc.GetElementsByTagName("Wild3");
            for (int i = 0; i < SpeciesNodes.Count; i++)
            {
                int speciesID = int.Parse(SpeciesNodes[i].InnerText);
                string[] BaseSkill = BaseNodes[i].InnerText.Split(',');
                string[] Natural2Skill = Natural2Nodes[i].InnerText.Split(',');
                string[] Mutation2Skill = Mutation2Nodes[i].InnerText.Split(',');
                string[] Wild2Skill = Wild2Nodes[i].InnerText.Split(',');
                string[] Natural3Skill = Natural3Nodes[i].InnerText.Split(',');
                string[] Mutation3Skill = Mutation3Nodes[i].InnerText.Split(',');
                string[] Wild3Skill = Wild3Nodes[i].InnerText.Split(',');
                Dictionary<int, Skill[]> MonsterEvolutionSkillChildDic = new Dictionary<int, Skill[]>();
                Skill[] baseSkills = new Skill[BaseSkill.Length];
                for (int j = 0; j < baseSkills.Length; j++)
                {
                    baseSkills[j] = SkillDic[int.Parse(BaseSkill[j])];
                }
                MonsterEvolutionSkillChildDic.Add(0, baseSkills);
                Skill[] nutural2Skills = new Skill[Natural2Skill.Length];
                for (int j = 0; j < nutural2Skills.Length; j++)
                {
                    nutural2Skills[j] = SkillDic[int.Parse(Natural2Skill[j])];
                }
                MonsterEvolutionSkillChildDic.Add(1, nutural2Skills);
                Skill[] mutation2Skills = new Skill[Mutation2Skill.Length];
                for (int j = 0; j < mutation2Skills.Length; j++)
                {
                    mutation2Skills[j] = SkillDic[int.Parse(Mutation2Skill[j])];
                }
                MonsterEvolutionSkillChildDic.Add(2, mutation2Skills);
                Skill[] wild2Skills = new Skill[Wild2Skill.Length];
                for (int j = 0; j < wild2Skills.Length; j++)
                {
                    wild2Skills[j] = SkillDic[int.Parse(Wild2Skill[j])];
                }
                MonsterEvolutionSkillChildDic.Add(3, wild2Skills);
                Skill[] nutural3Skills = new Skill[Natural3Skill.Length];
                for (int j = 0; j < nutural3Skills.Length; j++)
                {
                    nutural3Skills[j] = SkillDic[int.Parse(Natural3Skill[j])];
                }
                MonsterEvolutionSkillChildDic.Add(4, nutural3Skills);
                Skill[] mutation3Skills = new Skill[Mutation3Skill.Length];
                for (int j = 0; j < mutation3Skills.Length; j++)
                {
                    mutation3Skills[j] = SkillDic[int.Parse(Mutation3Skill[j])];
                }
                MonsterEvolutionSkillChildDic.Add(5, mutation3Skills);
                Skill[] wild3Skills = new Skill[Wild3Skill.Length];
                for (int j = 0; j < wild3Skills.Length; j++)
                {
                    wild3Skills[j] = SkillDic[int.Parse(Wild3Skill[j])];
                }
                MonsterEvolutionSkillChildDic.Add(6, wild3Skills);

                SpeciesEvolutionSkillDic.Add(speciesID, MonsterEvolutionSkillChildDic);
            }
        }
        catch
        {
            Debug.LogError("讀入物種進化技能(EvolutionSkill.xml)時，發生錯誤");
        }
    }
    public static void LoadMonsterTalent()//讀取物種進化習得天賦xml檔案，並設定怪物進化天賦字典
    {
        SpeciesEvolutionTalentDic = new Dictionary<int, Dictionary<int, Talent>>();
        try
        {
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/EvolutionTalent");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList SpeciesNodes = doc.GetElementsByTagName("Species");
            XmlNodeList Natural2Nodes = doc.GetElementsByTagName("Natural2");
            XmlNodeList Mutation2Nodes = doc.GetElementsByTagName("Mutation2");
            XmlNodeList Wild2Nodes = doc.GetElementsByTagName("Wild2");
            XmlNodeList Natural3Nodes = doc.GetElementsByTagName("Natural3");
            XmlNodeList Mutation3Nodes = doc.GetElementsByTagName("Mutation3");
            XmlNodeList Wild3Nodes = doc.GetElementsByTagName("Wild3");
            for (int i = 0; i < SpeciesNodes.Count; i++)
            {
                int speciesID = int.Parse(SpeciesNodes[i].InnerText);
                int Natural2Skill = int.Parse(Natural2Nodes[i].InnerText);
                int Mutation2Skill = int.Parse(Mutation2Nodes[i].InnerText);
                int Wild2Skill = int.Parse(Wild2Nodes[i].InnerText);
                int Natural3Skill = int.Parse(Natural3Nodes[i].InnerText);
                int Mutation3Skill = int.Parse(Mutation3Nodes[i].InnerText);
                int Wild3Skill = int.Parse(Wild3Nodes[i].InnerText);
                Dictionary<int, Talent> MonsterTalentChildDic = new Dictionary<int, Talent>();
                MonsterTalentChildDic.Add(1, TalentDic[Natural2Skill]);//原生2階天賦
                MonsterTalentChildDic.Add(2, TalentDic[Mutation2Skill]);//突變2階天賦
                MonsterTalentChildDic.Add(3, TalentDic[Wild2Skill]);//野性2階天賦
                MonsterTalentChildDic.Add(4, TalentDic[Natural3Skill]);//原生3階天賦
                MonsterTalentChildDic.Add(5, TalentDic[Mutation3Skill]);//突變3階天賦
                MonsterTalentChildDic.Add(6, TalentDic[Wild3Skill]);//野性3階天賦
                SpeciesEvolutionTalentDic.Add(speciesID, MonsterTalentChildDic);
            }
        }
        catch
        {
            Debug.LogError("讀入物種進化天賦(Talent.xml)時，發生錯誤");
        }
    }
    public static void LoadMapQuestData()//讀取地圖任務xml檔案，並設定任務資料
    {


        try
        {
            MapDic = new Dictionary<int, Map>();
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/Map");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList PlanetIDNodes = doc.GetElementsByTagName("PlanetID");
            XmlNodeList MapIDNodes = doc.GetElementsByTagName("MapID");
            XmlNodeList PlanetNameNodes = doc.GetElementsByTagName("PlanetName");
            XmlNodeList MapNameNodes = doc.GetElementsByTagName("MapName");
            XmlNodeList Quest1IDNodes = doc.GetElementsByTagName("Quest1ID");
            XmlNodeList Quest2IDNodes = doc.GetElementsByTagName("Quest2ID");
            XmlNodeList Quest3IDNodes = doc.GetElementsByTagName("Quest3ID");
            XmlNodeList Quest4IDNodes = doc.GetElementsByTagName("Quest4ID");
            XmlNodeList Quest5IDNodes = doc.GetElementsByTagName("Quest5ID");
            XmlNodeList Quest6IDNodes = doc.GetElementsByTagName("Quest6ID");
            XmlNodeList Quest7IDNodes = doc.GetElementsByTagName("Quest7ID");
            XmlNodeList Quest8IDNodes = doc.GetElementsByTagName("Quest8ID");
            XmlNodeList Quest9IDNodes = doc.GetElementsByTagName("Quest9ID");
            XmlNodeList Quest10IDNodes = doc.GetElementsByTagName("Quest10ID");
            for (int i = 0; i < PlanetIDNodes.Count; i++)
            {
                int planetID = int.Parse(PlanetIDNodes[i].InnerText);
                int mapID = int.Parse(MapIDNodes[i].InnerText);
                int mapKey = int.Parse(planetID.ToString() + mapID.ToString());
                string planetName = PlanetNameNodes[i].InnerText;
                string mapName = MapNameNodes[i].InnerText;
                int[] questIDs = new int[10];
                questIDs[0] = int.Parse(Quest1IDNodes[i].InnerText);
                questIDs[1] = int.Parse(Quest2IDNodes[i].InnerText);
                questIDs[2] = int.Parse(Quest3IDNodes[i].InnerText);
                questIDs[3] = int.Parse(Quest4IDNodes[i].InnerText);
                questIDs[4] = int.Parse(Quest5IDNodes[i].InnerText);
                questIDs[5] = int.Parse(Quest6IDNodes[i].InnerText);
                questIDs[6] = int.Parse(Quest7IDNodes[i].InnerText);
                questIDs[7] = int.Parse(Quest8IDNodes[i].InnerText);
                questIDs[8] = int.Parse(Quest9IDNodes[i].InnerText);
                questIDs[9] = int.Parse(Quest10IDNodes[i].InnerText);
                List<int> QuestIDList=new List<int>();
                for(int j=0;j<questIDs.Length;j++)
                {
                    if (questIDs[j] != 0)
                        QuestIDList.Add(questIDs[j]);
                }
                Map map = new Map(planetID, mapID, planetName, mapName, QuestIDList);
                MapDic.Add(mapKey, map);
            }
        }
        catch
        {
            Debug.LogError("讀入地圖任務相關資料(Map.xml)時，發生錯誤");
        }
    }
    public static void LoadAchievementTypeData()//讀取任務成就種類xml檔案，並設定成就種類資料
    {
        AchievementTypeDic = new Dictionary<int, AchievementType>();
        try
        {
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/AchievementType");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNodeList typeIDNodes = doc.GetElementsByTagName("AchievementTypeID");
            XmlNodeList nameNodes = doc.GetElementsByTagName("AchievementName");
            XmlNodeList detailNodes = doc.GetElementsByTagName("AchievementDetail");
            for (int i = 0; i < typeIDNodes.Count; i++)
            {
                int achievementID = int.Parse(typeIDNodes[i].InnerText);
                string achievementName = nameNodes[i].InnerText;
                string achievementDetail = detailNodes[i].InnerText;
                AchievementType at = new AchievementType(achievementID, achievementName, achievementDetail);
                AchievementTypeDic.Add(achievementID, at);
            }
        }
        catch
        {
            Debug.LogError("讀入任務成就類型資料(AchievementType.xml)時，發生錯誤");
        }

    }

    public static void LoadEvolutionTreeData()//讀取進化表xml檔案，並設定進化資料
    {
        EvolutionTreeDic = new Dictionary<string, SpeciesEvolution>();
        try
        {
            //讀取xml文件
            TextAsset xmlFile = Resources.Load<TextAsset>("String/EvolutionTree");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlFile.text);
            XmlNode rootNode = doc.DocumentElement;
            string speciesID = "";
            SpeciesEvolution se;
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                string[] evoSpecies = new string[3];
                speciesID = node.SelectSingleNode("Species").InnerText;
                evoSpecies[0] = node.SelectSingleNode("Natural").InnerText;
                evoSpecies[1] = node.SelectSingleNode("Mutation").InnerText;
                evoSpecies[2] = node.SelectSingleNode("Wild").InnerText;
                se = new SpeciesEvolution(speciesID, evoSpecies);
                try
                {
                    EvolutionTreeDic.Add(speciesID, se);
                }
                catch
                {
                    Debug.LogError("將物種進化表存入字典時發生key值重複");
                }
            }
        }
        catch
        {
            Debug.LogError("讀入怪獸進化表資料(EvolutionTree.xml)時，發生錯誤");
        }
    }
    public static void LoadQuestDic()//讀取任務xml檔案
    {
        QuestDic = new Dictionary<int, Quest>();//初始化任務字典
        //讀取任務資料
        TextAsset XMLFlie_Quest = Resources.Load<TextAsset>("String/Quest");
        //讀取XML文件
        XmlDocument Questdoc = new XmlDocument();
        Questdoc.LoadXml(XMLFlie_Quest.text);
        XmlNodeList QuestsNodes = Questdoc.GetElementsByTagName("QuestID");
        try
        {
            for (int i = 0; i < QuestsNodes.Count; i++)
            {
                int questID = int.Parse(QuestsNodes[i].InnerText);
                QuestDic.Add(questID, new Quest(questID));
            }
        }
        catch
        {
            Debug.LogError("讀取String/Quest時發生問題");
        }
    }
    public static void LoadSummonEventDic()//讀取召喚事件Xml檔案
    {
        SummonEventDic = new Dictionary<int, SummonEvent>();//初始化召喚事件字典
        //讀取任務資料
        TextAsset XMLFlie_SummonEvent = Resources.Load<TextAsset>("String/SummonEvent");
        //讀取XML文件
        XmlDocument SummonEventdoc = new XmlDocument();
        SummonEventdoc.LoadXml(XMLFlie_SummonEvent.text);
        XmlNodeList EventNodes = SummonEventdoc.GetElementsByTagName("EventID");

        try
        {
            for (int i = 0; i < EventNodes.Count; i++)
            {
                int eventID = int.Parse(EventNodes[i].InnerText);
                SummonEventDic.Add(eventID, new SummonEvent(eventID));
            }
        }
        catch
        {
            Debug.LogError("讀取String/SummonEvent時發生問題");
        }
    }

    public static void LoadNPCDic()//讀取NPC怪物字典
    {
        NPCDic = new Dictionary<int, NPC>();//初始化NPC字典
        //讀取任務資料
        TextAsset XMLFlie_NPC = Resources.Load<TextAsset>("String/NPCMonster");
        //讀取XML文件
        XmlDocument NPCdoc = new XmlDocument();
        NPCdoc.LoadXml(XMLFlie_NPC.text);
        XmlNodeList NPCNodes = NPCdoc.GetElementsByTagName("MonsterID");
        try
        {
            for (int i = 0; i < NPCNodes.Count; i++)
            {
                int NPCID = int.Parse(NPCNodes[i].InnerText);
                NPCDic.Add(NPCID, new NPC(NPCID));
            }
        }
        catch
        {
            Debug.LogError("讀取String/NPCMonster時發生問題");
        }
    }


}
