using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EditMemberData : TrainSceneInterfaceUI
{

    //讀入目前選擇怪物的class
    public static PlayerMonster curMonster;
    //技能卡 gameobj LIst
    public static Skill SelectedSkill;
    public static GameObject DataUI { get; set; }
    public static GameObject SkillUI { get; set; }
    static GoSkillList goSkillList;

    //腳色icon圖的atlas
    static private UIAtlas charaAtlas;
    //腳色icon圖的sprite
    static private UISprite charaSprite;
    //腳色icon邊框圖的sprite
    static private UISprite bannerSprite;
    //腳色icon底圖的sprite
    static private UISprite bottomSprite;
    //腳色icon元素icon的sprite
    static private UISprite elementSprite;
    //Title面板的背景顏色
    static private UISprite bgSprite;
    //品質星星
    static private UISprite[] rareSprite;

    //腳色各屬性數值UILabel 與 經驗UISprite
    static private ViewSkill viewSkill;
    static private ViewData viewData;
    static private UILabel lvLabel;
    static private UILabel nameLabel;
    static private UILabel speciesLabel;
    static private UILabel lifeLabel;
    static private UILabel brutalLabel;
    static private UILabel aggressiveLabel;
    static private UILabel resistanceLabel;
    static private UISprite expSprite;
    //技能各屬性數值UILabel & Sprite
    static private UILabel skillName;
    static private UILabel skillDmg;
    static private UILabel skillDescription;
    static private UISprite skillBG;
    //特徵&天賦
    static private UILabel[] talentLabel;
    static private UILabel[] featureLabel;
    public void StartSetting()
    {
        ///////////////////////初始化型別///////////////////////////

        //抓取EditMemberData上腳色ICON的物件
        charaAtlas = memberTitleUI.transform.FindChild("chara").FindChild("chara").GetComponent<UIAtlas>();
        charaSprite = memberTitleUI.transform.FindChild("chara").FindChild("chara").GetComponent<UISprite>();
        bannerSprite = memberTitleUI.transform.FindChild("chara").FindChild("banner").GetComponent<UISprite>();
        bottomSprite = memberTitleUI.transform.FindChild("chara").FindChild("bottom").GetComponent<UISprite>();
        elementSprite = memberTitleUI.transform.FindChild("chara").FindChild("element").GetComponent<UISprite>();

        //抓取EditMemberData上腳色各屬性數值的物件
        viewSkill = memberTitleUI.transform.FindChild("skillBTN").GetComponent<ViewSkill>();
        viewSkill.StartSet();
        viewData = memberTitleUI.transform.FindChild("DataBTN").GetComponent<ViewData>();
        viewData.StartSet();
        nameLabel = memberTitleUI.transform.FindChild("name").GetComponent<UILabel>();
        speciesLabel = memberTitleUI.transform.FindChild("speciesName").GetComponent<UILabel>();
        lvLabel = memberTitleUI.transform.FindChild("lv").GetComponent<UILabel>();
        lifeLabel = memberTitleUI.transform.FindChild("life").GetComponent<UILabel>();
        brutalLabel = memberTitleUI.transform.FindChild("brutal").GetComponent<UILabel>();
        aggressiveLabel = memberTitleUI.transform.FindChild("aggressive").GetComponent<UILabel>();
        resistanceLabel = memberTitleUI.transform.FindChild("resistance").GetComponent<UILabel>();
        expSprite = memberTitleUI.transform.FindChild("exp").GetComponent<UISprite>();
        bgSprite = memberTitleUI.transform.FindChild("bg").GetComponent<UISprite>();
        DataUI = memberDataUI;
        SkillUI = memberSkillUI;
        //抓取技能版上的數值與屬性
        goSkillList = SkillUI.transform.FindChild("changeSkill").GetComponent<GoSkillList>();
        skillName = SkillUI.transform.FindChild("skillName").GetComponent<UILabel>();
        skillDescription = SkillUI.transform.FindChild("skillDescription").GetComponent<UILabel>();
        skillDmg = SkillUI.transform.FindChild("damage").GetComponent<UILabel>();
        skillBG = SkillUI.transform.FindChild("skillBG").GetComponent<UISprite>();
        //抓取特徵天賦版上的數
        talentLabel = new UILabel[2];
        talentLabel[0] = DataUI.transform.FindChild("talent").FindChild("talent1").GetComponent<UILabel>();
        talentLabel[1] = DataUI.transform.FindChild("talent").FindChild("talent2").GetComponent<UILabel>();
        talentLabel[0].gameObject.SetActive(false);
        talentLabel[1].gameObject.SetActive(false);
        featureLabel = new UILabel[5];
        for (int i = 0; i < featureLabel.Length; i++)
        {
            featureLabel[i] = DataUI.transform.FindChild("feature").FindChild(string.Format("feature{0}", i)).GetComponent<UILabel>();
            featureLabel[i].gameObject.SetActive(false);
        }
    }


    public static void LoadMonsterData()
    {
        SkillUI.SetActive(true);
        DataUI.SetActive(false);//預設取消顯示腳色
        ViewSkill.SetGray();
        ViewData.SetWhite();
        ///////////////////////讀取目前選取怪物的資料/////////////////////////////
        //設置怪物Icon的各種資料(等級、怪物、元素、邊框、底圖)
        lvLabel.text = curMonster.Lv.ToString();
        //判斷物種頭像放在哪個atlas
        charaAtlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
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
        charaSprite.MakePixelPerfect();
        elementSprite.atlas = charaAtlas;
        elementSprite.spriteName = "element" + curMonster.MainElement.ToString();
        elementSprite.MakePixelPerfect();
        bannerSprite.atlas = charaAtlas;
        bannerSprite.spriteName = "frame" + curMonster.MainElement.ToString();
        bannerSprite.MakePixelPerfect();
        bottomSprite.atlas = charaAtlas;
        bottomSprite.spriteName = "bottom" + curMonster.MainElement.ToString(); ; //curMonster.Rare.ToString();
        bottomSprite.MakePixelPerfect();
        //設置怪物的各種屬性資料(名稱、種族、體型、生命、殘暴、侵略、抵抗)
        nameLabel.text = curMonster.MonsterName;
        speciesLabel.text = curMonster.SpeciesName;
        lifeLabel.text = curMonster.Health.ToString();
        brutalLabel.text = curMonster.Brutal.ToString();
        aggressiveLabel.text = curMonster.Aggressive.ToString();
        resistanceLabel.text = curMonster.Resistance.ToString();
        expSprite.width = (int)(68 + ((float)curMonster.CurExp / curMonster.NeedExp) * 460);
        bgSprite.spriteName = "elementBG" + curMonster.MainElement.ToString();

        //建立怪物擁有技能
        SelectedSkill = Player.MonsterDic[Player.curMonsterID].SelectedSkill;
        skillName.text = SelectedSkill.SkillName;
        skillDmg.text = ((int)(SelectedSkill.Damage * curMonster.Brutal)).ToString();
        skillDescription.text = SelectedSkill.Description;
        skillBG.spriteName = "skillElementBG" + curMonster.MainElement.ToString();
        goSkillList.SelectedMonsterID = curMonster.MonsterID;
        //讀取怪獸擁有天賦
        for (int i = 0; i < talentLabel.Length; i++)
        {
            if (i < curMonster.OwnTalentList.Count)
            {
                talentLabel[i].gameObject.SetActive(true);
                talentLabel[i].text = string.Format("{0}:{1}", curMonster.OwnTalentList[i].TalentName, curMonster.OwnTalentList[i].Description);
            }
            else
            {
                talentLabel[i].gameObject.SetActive(false);
            }
        }
        //讀取怪獸擁有特徵
        for (int i = 0; i < featureLabel.Length; i++)
        {
            if (i < curMonster.OwnFeatureList.Count)
            {
                featureLabel[i].gameObject.SetActive(true);
                featureLabel[i].text = string.Format("{0}{1}:{2}", curMonster.OwnFeatureList[i].GetDominantString(), curMonster.OwnFeatureList[i].OwnTalent.TalentName, curMonster.OwnFeatureList[i].OwnTalent.Description);
            }
            else
            {
                featureLabel[i].gameObject.SetActive(false);
            }
        }

    }

}
