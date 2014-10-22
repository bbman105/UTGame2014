using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnhanceUI : TrainSceneInterfaceUI
{
    //UI物件
    static private Transform ContentObj;
    static private Transform CharaIconObj;
    static private Transform ResourceObj;
    static private Transform ResultCharaDataObj;
    static private Transform ResultMemberTitleObj;
    static private Transform ResultCharaIconObj;
    ////////////////////////腳色資料///////////////////////////
    //腳色IconAtlas
    static private UIAtlas CharaAtlas;
    //讀入目前選擇怪物的class
    static PlayerMonster CurMonster;
    //腳色icon圖的sprite
    static private UISprite charaSprite;
    //腳色icon邊框圖的sprite
    static private UISprite bannerSprite;
    //腳色icon底圖的sprite
    static private UISprite bottomSprite;
    //腳色icon元素icon的sprite
    static private UISprite elementSprite;
    //腳色Icon等級文字
    static private UILabel Lv;
    static private UILabel LvTitle;
    //////////////////////////強化需求素材/////////////////////////////
    static private UILabel needResourceNum;
    static private UILabel needGold;
    static private UILabel ownResourceNum;
    static private UILabel needResourceTypeLabel;
    static private UISprite needResourceTypeSprite;
    static private UISprite enhanceArrow;
    static private EnhanceNeedMaterial NeedMaterial;//需求素材
    ///////////////////////////腳色強化結果各屬性數值UILabel 與 經驗UISprite////////////////////////
    //腳色icon圖的sprite
    static private UISprite ResultCharaSprite;
    //腳色icon邊框圖的sprite
    static private UISprite ResultBannerSprite;
    //腳色icon底圖的sprite
    static private UISprite ResultBottomSprite;
    //腳色icon元素icon的sprite
    static private UISprite ResultElementSprite;
    //腳色Icon等級文字
    static private UILabel ResultLvLabel;
    //腳色資訊
    static private UILabel nameLabel;
    static private UILabel lifeLabel;
    static private UILabel brutalLabel;
    static private UILabel aggressiveLabel;
    static private UILabel resistanceLabel;
    static private UILabel addLifeLabel;
    static private UILabel addBrutalLabel;
    static private UILabel addAggressiveLabel;
    static private UILabel addResistanceLabel;
    static private UISprite lifeArrow;
    static private UISprite brutalArrow;
    static private UISprite resistanceArrow;
    static private UISprite aggressiveArrow;
    static private UISprite memberTitleBg;
    private static int AddMemberBrutal { get; set; }//增加的殘暴
    private static int AddMemberAggressive { get; set; }//增加的侵略
    private static int AddMemberResistance { get; set; }//增加的抵抗
    private static int AddMemberHealth { get; set; }//增加的生命
    static private GameObject maxEnhanceBg;//強化到最高壓灰底圖
    ////////////////////////強化確認訊息物件////////////////////////
    //強化按鈕
    static private GameObject EnhanceBtn;
    static private UILabel EnhanceBtnText;
    static Color32 InactiveColor = new Color32(196, 196, 196, 255);
    static Color ActiveColor = Color.white;
    //強化按鈕觸碰
    static private BoxCollider EnhanceBtnCollider;
    //不可再強化的灰底
    static private GameObject CannotEnhanceBottom;
    //不可強化文字
    static private UILabel CannotEnhanceText;
    //強化確認視窗
    static private GameObject EnhanceMessageBox;
    static private UISprite MessageResourceSprite;
    static private UILabel MessageNeedResourceLabel;
    static private UILabel MessageNeedGoldLabel;

    /// <summary>
    /// 強化介面初始設置
    /// </summary>
    public static void StartSetting()//強化介面初始設置
    {
        //抓取強化怪物頭像物件
        ContentObj = EnhanceMonsterUI.transform.FindChild("EnhanceContent");
        CharaIconObj = ContentObj.transform.FindChild("EhanceChara");
        charaSprite = CharaIconObj.FindChild("chara").GetComponent<UISprite>();
        bannerSprite = CharaIconObj.FindChild("banner").GetComponent<UISprite>();
        bottomSprite = CharaIconObj.FindChild("bottom").GetComponent<UISprite>();
        elementSprite = CharaIconObj.FindChild("element").GetComponent<UISprite>();
        Lv = CharaIconObj.FindChild("lv").GetComponent<UILabel>();
        LvTitle = CharaIconObj.FindChild("lvTitle").GetComponent<UILabel>();
        //抓取強花需求素材物件
        ResourceObj = ContentObj.transform.FindChild("Resource");
        needResourceNum = ResourceObj.FindChild("needResource").GetComponent<UILabel>();
        needGold = ResourceObj.FindChild("needGold").GetComponent<UILabel>();
        ownResourceNum = ResourceObj.FindChild("ownResource").GetComponent<UILabel>();
        needResourceTypeLabel = ResourceObj.FindChild("resourceTypeLabel").GetComponent<UILabel>();
        needResourceTypeSprite = ResourceObj.FindChild("resourceTypeSprite").GetComponent<UISprite>();
        enhanceArrow = ResourceObj.FindChild("enhanceArrow").GetComponent<UISprite>();
        //抓取強化怪物數值物件
        ResultCharaDataObj = EnhanceMonsterUI.transform.FindChild("EnhanceResult");
        ResultMemberTitleObj = ResultCharaDataObj.FindChild("MemberTitle");
        ResultCharaIconObj = ResultMemberTitleObj.FindChild("chara");
        ResultCharaSprite = ResultCharaIconObj.FindChild("chara").GetComponent<UISprite>();
        ResultBannerSprite = ResultCharaIconObj.FindChild("banner").GetComponent<UISprite>();
        ResultBottomSprite = ResultCharaIconObj.FindChild("bottom").GetComponent<UISprite>();
        ResultElementSprite = ResultCharaIconObj.FindChild("element").GetComponent<UISprite>();
        ResultLvLabel = ResultCharaIconObj.FindChild("lv").GetComponent<UILabel>();
        lifeLabel = ResultMemberTitleObj.FindChild("life").GetComponent<UILabel>();
        nameLabel = ResultMemberTitleObj.FindChild("name").GetComponent<UILabel>();
        brutalLabel = ResultMemberTitleObj.FindChild("brutal").GetComponent<UILabel>();
        aggressiveLabel = ResultMemberTitleObj.FindChild("aggressive").GetComponent<UILabel>();
        resistanceLabel = ResultMemberTitleObj.FindChild("resistance").GetComponent<UILabel>();
        addLifeLabel = ResultMemberTitleObj.FindChild("addLife").GetComponent<UILabel>();
        addBrutalLabel = ResultMemberTitleObj.FindChild("addBrutal").GetComponent<UILabel>();
        addAggressiveLabel = ResultMemberTitleObj.FindChild("addAggressive").GetComponent<UILabel>();
        addResistanceLabel = ResultMemberTitleObj.FindChild("addResistance").GetComponent<UILabel>();
        lifeArrow = ResultMemberTitleObj.FindChild("lifeArrow").GetComponent<UISprite>();
        brutalArrow = ResultMemberTitleObj.FindChild("brutalArrow").GetComponent<UISprite>();
        resistanceArrow = ResultMemberTitleObj.FindChild("resistanceArrow").GetComponent<UISprite>();
        aggressiveArrow = ResultMemberTitleObj.FindChild("aggressiveArrow").GetComponent<UISprite>();
        memberTitleBg = ResultMemberTitleObj.FindChild("memberTitleBG").GetComponent<UISprite>();
        maxEnhanceBg = ResultCharaDataObj.FindChild("maxEnhanceBg").gameObject;
        CannotEnhanceText = maxEnhanceBg.transform.FindChild("text").GetComponent<UILabel>();
        //抓取強化確認視窗物件
        EnhanceBtn = EnhanceMonsterUI.transform.FindChild("EnhanceBtn").gameObject;
        EnhanceBtnText = EnhanceBtn.transform.FindChild("text").GetComponent<UILabel>();
        CannotEnhanceBottom = EnhanceBtn.transform.FindChild("maxEnhanceBottom").gameObject;
        EnhanceBtnCollider = EnhanceBtn.GetComponent<BoxCollider>();
        EnhanceMessageBox = EnhanceMonsterUI.transform.FindChild("MessageBox").gameObject;
        MessageResourceSprite = EnhanceMessageBox.transform.FindChild("resourceSprite").GetComponent<UISprite>();
        MessageNeedGoldLabel = EnhanceMessageBox.transform.FindChild("needGold").GetComponent<UILabel>();
        MessageNeedResourceLabel = EnhanceMessageBox.transform.FindChild("needResource").GetComponent<UILabel>();
    }
    /// <summary>
    /// 讀取要強化的怪獸資料
    /// </summary>
    public static void LoadEnhanceMonsterData(int _monsterID)//讀取要強化的怪獸資料
    {
        if (_monsterID != 0)
            CurMonster = Player.MonsterDic[_monsterID];
        else
            CurMonster = null;
        if (CurMonster != null)
        {
            //設定怪獸頭像
            CharaAtlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
            if (CurMonster.Species < 8)
            {
                charaSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                ResultCharaSprite.atlas = charaSprite.atlas;
            }
            else
            {
                charaSprite.atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon2");
                ResultCharaSprite.atlas = charaSprite.atlas;
            }
            charaSprite.spriteName = CurMonster.SpeciesKey;
            charaSprite.MakePixelPerfect();
            elementSprite.atlas = CharaAtlas;
            elementSprite.spriteName = "element" + CurMonster.MainElement.ToString();
            elementSprite.MakePixelPerfect();
            bannerSprite.atlas = CharaAtlas;
            bannerSprite.spriteName = "frame" + CurMonster.MainElement.ToString();
            bannerSprite.MakePixelPerfect();
            bottomSprite.atlas = CharaAtlas;
            bottomSprite.spriteName = "bottom" + CurMonster.MainElement.ToString();
            bottomSprite.MakePixelPerfect();
            Lv.text = CurMonster.Lv.ToString();
            //怪獸強化結果介面
            ResultCharaSprite.spriteName = CurMonster.SpeciesKey;
            ResultCharaSprite.MakePixelPerfect();
            ResultElementSprite.atlas = CharaAtlas;
            ResultElementSprite.spriteName = "element" + CurMonster.MainElement.ToString();
            ResultElementSprite.MakePixelPerfect();
            ResultBannerSprite.atlas = CharaAtlas;
            ResultBannerSprite.spriteName = "frame" + CurMonster.MainElement.ToString();
            ResultBannerSprite.MakePixelPerfect();
            ResultBottomSprite.atlas = CharaAtlas;
            ResultBottomSprite.spriteName = "bottom" + CurMonster.MainElement.ToString();
            ResultBottomSprite.MakePixelPerfect();
            memberTitleBg.spriteName = "enhanceMonsterBottom" + CurMonster.MainElement.ToString();
            ShowEnhanceMonster();//顯示所有怪獸資訊，選擇了要強化的怪獸後被調用
        }
        else
        {
            HideEnhanceMonster();//隱藏所有怪獸資訊，當沒有選擇要強化的怪獸時被調用
        }

    }
    /// <summary>
    /// 檢查是否還可以強化(小於5階)，並顯示強化需求素材，在選擇強化怪獸，或強化後被調用
    /// </summary>
    static void CheckEnhanceAvailable()//檢查是否可以強化，並顯示強化需求素材，在選擇強化怪獸，或強化後被調用
    {
        ShowEnhanceResource();//顯示強化需求素材及擁有素材
        if (CurMonster.Rare >= 5)//如果怪獸已達5階最高強化，壓灰標示為不可再強化
        {
            EnhanceBtnText.color = InactiveColor;
            EnhanceBtnCollider.enabled = false;
            maxEnhanceBg.SetActive(true);
            CannotEnhanceBottom.SetActive(true);
            CannotEnhanceText.text = "已強化到最高階";
        }
        else if (NeedMaterial.ItemNum > Player.OwnResource.GetEnhanceMaterial(NeedMaterial.ItemID))
        {
            EnhanceBtnText.color = InactiveColor;
            EnhanceBtnCollider.enabled = false;
            maxEnhanceBg.SetActive(true);
            CannotEnhanceBottom.SetActive(true);
            CannotEnhanceText.text = "強化素材不足";
        }
        else
        {
            EnhanceBtnText.color = ActiveColor;
            EnhanceBtnCollider.enabled = true;
            maxEnhanceBg.SetActive(false);
            CannotEnhanceBottom.SetActive(false);
        }

    }
    /// <summary>
    /// 顯示所有怪獸資訊，選擇了要強化的怪獸後被調用
    /// </summary>
    static void ShowEnhanceMonster()//顯示所有怪獸資訊，選擇了要強化的怪獸後被調用
    {
        //顯示頭像
        charaSprite.alpha = 100;
        elementSprite.alpha = 100;
        bannerSprite.alpha = 100;
        bottomSprite.alpha = 100;
        Lv.alpha = 100;
        LvTitle.alpha = 100;
        ResultMemberTitleObj.gameObject.SetActive(true);//顯示結果數值面板
        CheckEnhanceAvailable();//檢查是否可以強化，並顯示強化需求素材，在選擇強化怪獸，或強化後被調用
        UpdateEnhanceMonsterData();//更新怪獸數值
        HideEnhanceProperty();//取消顯示強化增加的數值
    }
    /// <summary>
    /// 更新怪獸數值，當選取要強化的怪獸或怪獸升級時被調用
    /// </summary>
    public static void UpdateEnhanceMonsterData()//更新怪獸數值
    {
        //設定強化怪物數值物件
        nameLabel.text = CurMonster.MonsterName;
        ResultLvLabel.text = CurMonster.Lv.ToString();
        lifeLabel.text = CurMonster.Health.ToString();
        brutalLabel.text = CurMonster.Brutal.ToString();
        aggressiveLabel.text = CurMonster.Aggressive.ToString();
        resistanceLabel.text = CurMonster.Resistance.ToString();
    }
    /// <summary>
    /// 隱藏所有怪獸資訊，當沒有選擇要強化的怪獸時被調用
    /// </summary>
    static void HideEnhanceMonster()//隱藏所有怪獸資訊，當沒有選擇要強化的怪獸時被調用
    {
        //隱藏頭像
        charaSprite.alpha = 0;
        elementSprite.alpha = 0;
        bannerSprite.alpha = 0;
        bottomSprite.alpha = 0;
        Lv.alpha = 0;
        LvTitle.alpha = 0;
        //隱藏結果數值面板
        ResultMemberTitleObj.gameObject.SetActive(false);
        maxEnhanceBg.SetActive(false);
        //隱藏需求素材
        ResourceObj.gameObject.SetActive(false);
    }
    /// <summary>
    /// 顯示強化需求素材
    /// </summary>
    static void ShowEnhanceResource()//顯示強化需求素材及擁有素材
    {
        //顯示需求素材
        ResourceObj.gameObject.SetActive(true);
        //取得需求素材
        try
        {
            NeedMaterial = GameDictionary.EnhanceNeedMaterialDic[CurMonster.MainElement][CurMonster.Rare];
            if (NeedMaterial.ItemID != 0)//需求素材ID如果不為0代表不能進化怪獸了
            {
                needResourceNum.text = NeedMaterial.ItemNum.ToString();
                needGold.text = NeedMaterial.NeedGold.ToString();
                needResourceTypeLabel.text = NeedMaterial.ItemName;
                needResourceTypeSprite.spriteName = NeedMaterial.ItemSpriteName;
                needResourceTypeSprite.MakePixelPerfect();
                ownResourceNum.text = Player.OwnResource.GetEnhanceMaterial(NeedMaterial.ItemID).ToString();
            }
            else
            {
                //隱藏需求素材
                ResourceObj.gameObject.SetActive(false);
            }
            //enhanceArrow
        }
        catch
        {
            Debug.LogError("讀取怪獸需求素材時發生錯誤");
        }
    }
    /// <summary>
    /// 取得怪獸強化增加的屬性數值，並顯示出來
    /// </summary>
    public static void ShowEnhanceMonsterAddProperty()//取得怪獸強化增加的屬性數值，並顯示出來
    {
        //顯示怪獸強化增加的屬性
        addLifeLabel.alpha = 255;
        addAggressiveLabel.alpha = 255;
        addBrutalLabel.alpha = 255;
        addResistanceLabel.alpha = 255;
        lifeArrow.alpha = 255;
        aggressiveArrow.alpha = 255;
        brutalArrow.alpha = 255;
        resistanceArrow.alpha = 255;
        //設定怪獸強化增加的屬性數值
        addLifeLabel.text = GameDictionary.SpeciesRarePropertyDic[CurMonster.Species][CurMonster.Rare].BaseHealth.ToString();
        addBrutalLabel.text = GameDictionary.SpeciesRarePropertyDic[CurMonster.Species][CurMonster.Rare].BaseBrutal.ToString();
        addResistanceLabel.text = GameDictionary.SpeciesRarePropertyDic[CurMonster.Species][CurMonster.Rare].BaseResistance.ToString();
        addAggressiveLabel.text = GameDictionary.SpeciesRarePropertyDic[CurMonster.Species][CurMonster.Rare].BaseAggressive.ToString();
    }
    /// <summary>
    /// 取消顯示強化增加的數值
    /// </summary>
    static void HideEnhanceProperty()
    {
        //取消顯示強化增加的數值
        addLifeLabel.alpha = 0;
        addAggressiveLabel.alpha = 0;
        addBrutalLabel.alpha = 0;
        addResistanceLabel.alpha = 0;
        lifeArrow.alpha = 0;
        aggressiveArrow.alpha = 0;
        brutalArrow.alpha = 0;
        resistanceArrow.alpha = 0;
    }
    /// <summary>
    /// 強化並顯示強化後介面
    /// </summary>
    public static void Enhance()//強化並顯示強化後介面
    {
        try
        {
            if (CurMonster.Rare < 5 && NeedMaterial.ItemNum <= Player.OwnResource.GetEnhanceMaterial(NeedMaterial.ItemID))
            {
                Player.OwnResource.AddEnhanceMaterial(NeedMaterial.ItemID, -NeedMaterial.ItemNum);//消耗玩家素材
                CurMonster.EnhanceRare();//怪物生階級
                UpdateEnhanceMonsterData();//更新怪獸數值
                ShowEnhanceResource();//顯示強化需求素材及擁有素材
                ShowEnhanceMonsterAddProperty();//取得怪獸強化增加的屬性數值，並顯示出來
                CheckEnhanceAvailable();//檢查是否可以強化，並顯示強化需求素材，在選擇強化怪獸，或強化後被調用
            }
        }
        catch
        {
            Debug.LogError("強化怪獸時發生錯誤");
        }
    }
    /// <summary>
    /// 隱藏強化確認訊息框
    /// </summary>
    public static void HideEnhanceMessageBox()//隱藏強化確認訊息框
    {
        EnhanceMessageBox.SetActive(false);
    }
    /// <summary>
    /// 顯示強化確認訊息框
    /// </summary>
    public static void ShowEnhanceMessageBox()//顯示強化確認訊息框
    {
        MessageResourceSprite.spriteName = NeedMaterial.ItemSpriteName;
        MessageNeedResourceLabel.text = NeedMaterial.ItemNum.ToString();
        MessageNeedGoldLabel.text = NeedMaterial.NeedGold.ToString();
        EnhanceMessageBox.SetActive(true);
    }
}
