using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EvolutionLoad
{

    GameObject EvoUI;
    PlayerMonster Pm;
    int EvoType;

    UISprite MonsterSprite;//怪獸
    UISprite EvoTitleSprite;//標題

    //怪獸資料
    UILabel NameLabel;//名子
    UILabel HealthLabel;//生命
    UILabel BrutalLabel;//殘暴
    UILabel AggressiveLabel;//侵略
    UILabel ResistanceLabel;//抵抗
    UILabel HealthAddLabel;//生命
    UILabel BrutalAddLabel;//殘暴
    UILabel AggressiveAddLabel;//侵略
    UILabel ResistanceAddLabel;//抵抗
    UILabel TalentLabel;//獲取的天賦
    UILabel SkillLabel;//獲取的技能
    int addHealth;
    public int AddHealth
    {
        get { return addHealth; }
        set { addHealth = value; }
    }
    int addBrutal;
    public int AddBrutal
    {
        get { return addBrutal; }
        set { addBrutal = value; }
    }
    int addAggressive;
    public int AddAggressive
    {
        get { return addAggressive; }
        set { addAggressive = value; }
    }
    int addResistance;
    public int AddResistance
    {
        get { return addResistance; }
        set { addResistance = value; }
    }

    //怪獸Icon物件
    UISprite IconSprite;//Icon
    UISprite ElementSprite;//元素
    UISprite BannerSprite;//品質框
    UISprite BottomSprite;//底圖


    public static bool OnEvolutionUI { get; set; }//是否再進化介面
    public static List<EvolutionGettor> EvoList { get; set; }//進化排程


    public static void CheckEvolutionSchedule(EvolutionGettor _evolutionGettor)//傳入一個進化控制器，如果目前正有怪獸在進化中，加入一個進化排成，否則就進化
    {
        if (EvoList == null)
            EvoList = new List<EvolutionGettor>();
        if (_evolutionGettor.CheckEvolution())//如果可進化，則進化進化排程
        {
            if (OnEvolutionUI)//如果有複數的進化怪物則加入排程
            {
                EvoList.Add(_evolutionGettor);//新增一個進化行程
            }
            else//只有一隻怪獸可進化則直接進化
            {
                _evolutionGettor.GetEvolution();//進行進化
            }
        }

    }
    public static void NextEvolution()//若有進化排成則執行下一個進化
    {
        OnEvolutionUI = false;
        if (EvoList.Count > 0)//還有怪獸等待進化就繼續下一隻怪獸的進化
        {
            Debug.Log("進化怪獸");
            EvoList[0].GetEvolution();//進行進化
            EvoList.Remove(EvoList[0]);//移出待進化清單
        }
        else//排程中沒有可進化怪獸就關閉介面
        {
            Debug.Log("無可進化怪獸");
            CloseEvolutionUI.CloseEvoUI();
        }
    }



    public EvolutionLoad(PlayerMonster _pm)
    {
        EvoUI = TrainSceneInterfaceUI.evoUI;
        Pm = _pm;
        EvoType = 1;
        //怪獸圖像
        MonsterSprite = EvoUI.transform.FindChild("character").FindChild("character").GetComponent<UISprite>();
        //標題
        EvoTitleSprite = EvoUI.transform.FindChild("evolutionTitle").GetComponent<UISprite>();
        EvoTitleSprite.atlas = Resources.Load<UIAtlas>("Atlas/TrainScene/Event/Event");
        //各種屬性
        NameLabel = EvoUI.transform.FindChild("MemberTitle").FindChild("name").GetComponent<UILabel>();
        HealthLabel=EvoUI.transform.FindChild("MemberTitle").FindChild("life").GetComponent<UILabel>();
        BrutalLabel = EvoUI.transform.FindChild("MemberTitle").FindChild("brutal").GetComponent<UILabel>();
        AggressiveLabel = EvoUI.transform.FindChild("MemberTitle").FindChild("aggressive").GetComponent<UILabel>();
        ResistanceLabel = EvoUI.transform.FindChild("MemberTitle").FindChild("resistance").GetComponent<UILabel>();
        HealthAddLabel=EvoUI.transform.FindChild("MemberTitle").FindChild("lifeAdd").GetComponent<UILabel>();
        BrutalAddLabel = EvoUI.transform.FindChild("MemberTitle").FindChild("brutalAdd").GetComponent<UILabel>();
        AggressiveAddLabel = EvoUI.transform.FindChild("MemberTitle").FindChild("aggressiveAdd").GetComponent<UILabel>();
        ResistanceAddLabel = EvoUI.transform.FindChild("MemberTitle").FindChild("resistanceAdd").GetComponent<UILabel>();
        TalentLabel = EvoUI.transform.FindChild("MemberTitle").FindChild("talent").GetComponent<UILabel>();
        SkillLabel = EvoUI.transform.FindChild("MemberTitle").FindChild("skill").GetComponent<UILabel>();
        //頭像
        IconSprite = EvoUI.transform.FindChild("MemberTitle").FindChild("chara").FindChild("chara").GetComponent<UISprite>();
        ElementSprite = EvoUI.transform.FindChild("MemberTitle").FindChild("chara").FindChild("element").GetComponent<UISprite>();
        BannerSprite = EvoUI.transform.FindChild("MemberTitle").FindChild("chara").FindChild("banner").GetComponent<UISprite>();
        BottomSprite = EvoUI.transform.FindChild("MemberTitle").FindChild("chara").FindChild("bottom").GetComponent<UISprite>();
        ElementSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
        BannerSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
        BottomSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
    }

    public void ShowEvolution(int _preHealth, int _preBrutal,int _preAggressive,int _preResistance,string _talent,string _skill)
    {
        //怪物圖像
        MonsterSprite.atlas = Resources.Load<UIAtlas>(string.Format("Atlas/Character/{0}", Pm.Species));
        MonsterSprite.spriteName = Pm.SpeciesLevel.ToString();
        MonsterSprite.MakePixelPerfect();
        //進化標題
        EvoTitleSprite.spriteName = string.Format("evoTitle{0}", EvoType);
        //計算各屬性增加的數值
        AddHealth = Pm.Health - _preHealth;
        AddBrutal = Pm.Brutal - _preBrutal;
        AddAggressive = Pm.Aggressive - _preAggressive;
        AddResistance = Pm.Resistance - _preResistance;
        //各種屬性
        NameLabel.text = Pm.MonsterName;
        HealthLabel.text = Pm.Health.ToString();
        BrutalLabel.text = Pm.Brutal.ToString();
        AggressiveLabel.text = Pm.Aggressive.ToString();
        ResistanceLabel.text = Pm.Resistance.ToString();
        HealthAddLabel.text = AddHealth.ToString();
        BrutalAddLabel.text = AddBrutal.ToString();
        AggressiveAddLabel.text = AddAggressive.ToString();
        ResistanceAddLabel.text = AddResistance.ToString();
        TalentLabel.text = _talent;
        SkillLabel.text = _skill;
        //選擇對的物種頭像
        if (Pm.Species < 8)
        {
            IconSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
        }
        else
        {
            IconSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon2");
        }
        IconSprite.spriteName = Pm.SpeciesKey;
        IconSprite.MakePixelPerfect();
        ElementSprite.spriteName = "element" + Pm.MainElement.ToString();
        ElementSprite.MakePixelPerfect();
        BannerSprite.spriteName = "frame" + Pm.Rare.ToString();
        BannerSprite.MakePixelPerfect();
        BottomSprite.spriteName = "bottom" + Pm.Rare.ToString();
        BottomSprite.MakePixelPerfect();
        OnEvolutionUI = true;//設定目前已經顯示進化介面，若有其他怪物可進化不會立刻跳出
        EvoUI.SetActive(true);//顯示進化介面
        CtrlPower.StartCtrl(4, true);//設定進化介面控制權
    }

}
