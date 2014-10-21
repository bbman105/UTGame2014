using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattleLogPlayer
{
    static GameObject BattleLogUI { get; set; }
    static GameObject[] ArrowLogObj { get; set; }
    static ArrowLog[] Al { get; set; }
    static Transform[] ArrowLeft { get; set; }
    static Transform[] ArrowRight { get; set; }
    static TextMesh[] LSkillName { get; set; }
    static Renderer[] LSkillNameRenderer { get; set; }
    static TextMesh[] RSkillName { get; set; }
    static Renderer[] RSkillNameRenderer { get; set; }
    static TextMesh[] LDamage { get; set; }
    static Renderer[] LDamageRenderer { get; set; }
    static TextMesh[] RDamage { get; set; }
    static Renderer[] RDamageRenderer { get; set; }
    static SpriteRenderer[] LSpecies { get; set; }
    static SpriteRenderer[] RSpecies { get; set; }
    static SpriteRenderer[] LBanner { get; set; }
    static SpriteRenderer[] RBanner { get; set; }
    static SpriteRenderer[] LArrowSprite { get; set; }
    static SpriteRenderer[] RArrowSprite { get; set; }
    static SpriteRenderer[] LChap { get; set; }
    static SpriteRenderer[] RChap { get; set; }
    static ChapLoad[] LChapLoad { get; set; }
    static ChapLoad[] RChapLoad { get; set; }
    static int ArrowLevel { get; set; }
    static int StartArrowLevel { get; set; }
    static int BeatLeft { get; set; }
    static int BeatRight { get; set; }
    static int PreviousElement { get; set; }
    static int NextElement { get; set; }
    static Status LStatus { get; set; }//左方怪獸狀態
    static Status RStatus { get; set; }//右方怪獸狀態
    public static void BattleLogStartSet()
    {
        ArrowLevel = 0;
        StartArrowLevel = 0;
        BeatLeft = 1;
        BeatRight = 1;
        PreviousElement = 1;
        NextElement = 1;
        ArrowLogObj = new GameObject[5];
        Al = new ArrowLog[5];
        ArrowLeft = new Transform[5];
        ArrowRight = new Transform[5];
        LSkillName = new TextMesh[5];
        LSkillNameRenderer = new Renderer[5];
        RSkillName = new TextMesh[5];
        RSkillNameRenderer = new Renderer[5];
        LDamage = new TextMesh[5];
        LDamageRenderer = new Renderer[5];
        RDamage = new TextMesh[5];
        RDamageRenderer = new Renderer[5];
        LSpecies = new SpriteRenderer[5];
        RSpecies = new SpriteRenderer[5];
        LBanner = new SpriteRenderer[5];
        RBanner = new SpriteRenderer[5];
        LArrowSprite = new SpriteRenderer[5];
        RArrowSprite = new SpriteRenderer[5];
        LChap = new SpriteRenderer[5];
        RChap = new SpriteRenderer[5];
        LChapLoad = new ChapLoad[5];
        RChapLoad = new ChapLoad[5];

        BattleLogUI = GameObject.FindGameObjectWithTag("BattleLog");
        for (int i = 0; i < ArrowLogObj.Length; i++)
        {
            ArrowLogObj[i] = BattleLogUI.transform.FindChild(string.Format("ArrowLog{0}", i + 1)).gameObject;
            Al[i] = ArrowLogObj[i].GetComponent<ArrowLog>();
            Al[i].StartSet();
            ArrowLeft[i] = ArrowLogObj[i].transform.FindChild("AniLeft");
            LChapLoad[i] = ArrowLeft[i].GetComponent<ChapLoad>();
            LChapLoad[i].StartSet();
            ArrowRight[i] = ArrowLogObj[i].transform.FindChild("AniRight");
            ArrowRight[i].GetComponent<ChapLoad>().StartSet();
            RChapLoad[i] = ArrowRight[i].GetComponent<ChapLoad>();
            RChapLoad[i].StartSet();
            LSkillName[i] = ArrowLeft[i].FindChild("SkillName").GetComponent<TextMesh>();
            LSkillNameRenderer[i] = LSkillName[i].renderer;
            LSkillNameRenderer[i].sortingLayerName = "UI";
            RSkillName[i] = ArrowRight[i].FindChild("SkillName").GetComponent<TextMesh>();
            RSkillNameRenderer[i] = RSkillName[i].renderer;
            RSkillNameRenderer[i].sortingLayerName = "UI";
            LDamage[i] = ArrowLeft[i].FindChild("Damage").GetComponent<TextMesh>();
            LDamageRenderer[i] = LDamage[i].renderer;
            LDamageRenderer[i].sortingLayerName = "UI";
            RDamage[i] = ArrowRight[i].FindChild("Damage").GetComponent<TextMesh>();
            RDamageRenderer[i] = RDamage[i].renderer;
            RDamageRenderer[i].sortingLayerName = "UI";
            LSpecies[i] = ArrowLeft[i].FindChild("Species").GetComponent<SpriteRenderer>();
            RSpecies[i] = ArrowRight[i].FindChild("Species").GetComponent<SpriteRenderer>();
            LBanner[i] = ArrowLeft[i].FindChild("Banner").GetComponent<SpriteRenderer>();
            RBanner[i] = ArrowRight[i].FindChild("Banner").GetComponent<SpriteRenderer>();
            LChap[i] = ArrowLeft[i].FindChild("Chap").GetComponent<SpriteRenderer>();
            RChap[i] = ArrowRight[i].FindChild("Chap").GetComponent<SpriteRenderer>();
            LArrowSprite[i] = ArrowLeft[i].FindChild("Arrow").GetComponent<SpriteRenderer>();
            RArrowSprite[i] = ArrowRight[i].FindChild("Arrow").GetComponent<SpriteRenderer>();
        }
        ArrowLogStartSet();
    }
    public static void ArrowLogStartSet()//起始設定，防止第一次碰撞LAG
    {
        LStatus = ForceCondition.CharaDic["PlayerMonster"][0].GetComponent<Status>();
        RStatus = ForceCondition.CharaDic["NPCMonster"][0].GetComponent<Status>();
        StatusSet();
        Al[0].PlayAni("Down0");
        Al[1].PlayAni("Down0");
        Al[2].PlayAni("Down0");
        Al[3].PlayAni("Down0");
        Al[4].PlayAni("Down0");
    }

    public static void ShowArrowLog(Status _rStatus, Status _lStatus)
    {
        LStatus = _rStatus;
        RStatus = _lStatus;
        StatusSet();
        ShowArrowLogFunction();
    }
    static Sprite GetSpeciesSprite(string _spriteName)//取得怪物sprite
    {
        return Resources.Load<Sprite>(string.Format("Sprite/MonsterSprite/{0}", _spriteName));
    }
    static Sprite GetArrowSprite(string _spriteName)//取得ArrowLog的sprite
    {
        return Resources.Load<Sprite>(string.Format("Sprite/Arrow/{0}", _spriteName));
    }
    static void StatusSet()
    {
        //設定施法技能名稱ArrowLevel及傷害顯示
        if (LStatus.TmpCanSpell)
        {
            LSkillName[ArrowLevel].text = string.Format("{0}{1}", LStatus.SpellList[LStatus.SelectSpellRank].SkillName, "!");
            LDamage[ArrowLevel].text = ((int)LStatus.KnockDamage).ToString();
        }
        else
        {
            LSkillName[ArrowLevel].text = "無法攻擊!";
            LDamage[ArrowLevel].text = "0";
        }
        //設定施法技能名稱ArrowLevel及傷害顯示
        if (RStatus.TmpCanSpell)
        {
            RSkillName[ArrowLevel].text = string.Format("{0}{1}", RStatus.SpellList[RStatus.SelectSpellRank].SkillName, "!");
            RDamage[ArrowLevel].text = ((int)RStatus.KnockDamage).ToString();
        }
        else
        {
            RSkillName[ArrowLevel].text = "無法攻擊!";
            RDamage[ArrowLevel].text = "0";
        }





        //設定雙方怪物Sprite
        LSpecies[ArrowLevel].sprite = GetSpeciesSprite(LStatus.SpeciesKey);
        RSpecies[ArrowLevel].sprite = GetSpeciesSprite(RStatus.SpeciesKey);
        //設定雙方戰鬥LOG的屬性箭頭
        if (LStatus.SkillElement - 1 < 0) { PreviousElement = 3; }
        else { PreviousElement = LStatus.SkillElement - 1; }
        if (LStatus.SkillElement + 1 > 3) { NextElement = 0; }
        else { NextElement = LStatus.SkillElement + 1; }


        //輸
        if (PreviousElement == RStatus.SkillElement)
        {

            //set chap
            LChap[ArrowLevel].sprite = GetArrowSprite("skillarrow_defeat");
            LChap[ArrowLevel].transform.localScale = new Vector3(-0.7f, 0.7f, 1);
            LChapLoad[ArrowLevel].Show = true;
            LChap[ArrowLevel].transform.localPosition = new Vector3(0.25f, 0, 0);
            RChap[ArrowLevel].sprite = GetArrowSprite("skillarrow_defeat");
            RChapLoad[ArrowLevel].Show = false;
            //set Banner
            LBanner[ArrowLevel].sprite = GetArrowSprite("skillname_bottom");
            RBanner[ArrowLevel].sprite = GetArrowSprite("skillname_bottom");
            //set leftArrow depth
            LArrowSprite[ArrowLevel].sortingOrder = 60 * (ArrowLevel + 1);
            LBanner[ArrowLevel].sortingOrder = 63 * (ArrowLevel + 1);
            LChap[ArrowLevel].sortingOrder = 61 * (ArrowLevel + 1);
            LDamageRenderer[ArrowLevel].sortingOrder = 150 * (ArrowLevel + 1);
            LSkillNameRenderer[ArrowLevel].sortingOrder = 150 * (ArrowLevel + 1);
            LSpecies[ArrowLevel].sortingOrder = 62 * (ArrowLevel + 1);
            //set rightArrow depth
            RArrowSprite[ArrowLevel].sortingOrder = 110 * (ArrowLevel + 1);
            RBanner[ArrowLevel].sortingOrder = 113 * (ArrowLevel + 1);
            RChap[ArrowLevel].sortingOrder = 111 * (ArrowLevel + 1);
            RDamageRenderer[ArrowLevel].sortingOrder = 150 * (ArrowLevel + 1);
            RSkillNameRenderer[ArrowLevel].sortingOrder = 150 * (ArrowLevel + 1);
            RSpecies[ArrowLevel].sortingOrder = 112 * (ArrowLevel + 1);

            BeatLeft = 1;
            BeatRight = 1;
        }
        //贏
        else if (NextElement == RStatus.SkillElement)
        {
            //set chap
            LChap[ArrowLevel].sprite = GetArrowSprite("skillarrow_defeat");
            LChap[ArrowLevel].transform.localScale = new Vector3(-0.7f, 0.7f, 1);
            LChapLoad[ArrowLevel].Show = false;
            RChap[ArrowLevel].sprite = GetArrowSprite("skillarrow_defeat");
            RChapLoad[ArrowLevel].Show = true;
            RChap[ArrowLevel].transform.localPosition = new Vector3(0.25f, 0, 0);
            //set Banner
            LBanner[ArrowLevel].sprite = GetArrowSprite("skillname_bottom");
            RBanner[ArrowLevel].sprite = GetArrowSprite("skillname_bottom");

            //set leftArrow depth
            LArrowSprite[ArrowLevel].sortingOrder = 110 * (ArrowLevel + 1);
            LBanner[ArrowLevel].sortingOrder = 113 * (ArrowLevel + 1);
            LChap[ArrowLevel].sortingOrder = 111 * (ArrowLevel + 1);
            LDamageRenderer[ArrowLevel].sortingOrder = 150 * (ArrowLevel + 1);
            LSkillNameRenderer[ArrowLevel].sortingOrder = 150 * (ArrowLevel + 1);
            LSpecies[ArrowLevel].sortingOrder = 112 * (ArrowLevel + 1);
            //set rightArrow depth
            RArrowSprite[ArrowLevel].sortingOrder = 60 * (ArrowLevel + 1);
            RBanner[ArrowLevel].sortingOrder = 63 * (ArrowLevel + 1);
            RChap[ArrowLevel].sortingOrder = 61 * (ArrowLevel + 1);
            RDamageRenderer[ArrowLevel].sortingOrder = 150 * (ArrowLevel + 1);
            RSkillNameRenderer[ArrowLevel].sortingOrder = 150 * (ArrowLevel + 1);
            RSpecies[ArrowLevel].sortingOrder = 62 * (ArrowLevel + 1);

            //setArrow
            BeatLeft = 1;
            BeatRight = 1;
        }
        //平手
        else
        {

            //set leftArrow depth
            LArrowSprite[ArrowLevel].sortingOrder = 110 * (ArrowLevel + 1);
            LBanner[ArrowLevel].sortingOrder = 113 * (ArrowLevel + 1);
            LChap[ArrowLevel].sortingOrder = 111 * (ArrowLevel + 1);
            LDamageRenderer[ArrowLevel].sortingOrder = 150 * (ArrowLevel + 1);
            LSkillNameRenderer[ArrowLevel].sortingOrder = 150 * (ArrowLevel + 1);
            LSpecies[ArrowLevel].sortingOrder = 112 * (ArrowLevel + 1);
            //set rightArrow depth
            RArrowSprite[ArrowLevel].sortingOrder = 110 * (ArrowLevel + 1);
            RBanner[ArrowLevel].sortingOrder = 113 * (ArrowLevel + 1);
            RChap[ArrowLevel].sortingOrder = 111 * (ArrowLevel + 1);
            RDamageRenderer[ArrowLevel].sortingOrder = 150 * (ArrowLevel + 1);
            RSkillNameRenderer[ArrowLevel].sortingOrder = 150 * (ArrowLevel + 1);
            RSpecies[ArrowLevel].sortingOrder = 112 * (ArrowLevel + 1);

            //set chap
            LChap[ArrowLevel].sprite = GetArrowSprite("skillarrow_deauce_left");
            LChap[ArrowLevel].transform.localScale = new Vector3(0.7f, 0.7f, 1);
            LChapLoad[ArrowLevel].Show = true;
            LChap[ArrowLevel].transform.localPosition = new Vector3(0.7f, 0, 0);
            RChap[ArrowLevel].sprite = GetArrowSprite("skillarrow_deauce_right");
            RChap[ArrowLevel].transform.localScale = new Vector3(-0.7f, -0.7f, 1);
            RChapLoad[ArrowLevel].Show = true;
            RChap[ArrowLevel].transform.localPosition = new Vector3(0.7f, 0, 0);
            //set Banner
            LBanner[ArrowLevel].sprite = GetArrowSprite("skillname_bottom-deuce");
            RBanner[ArrowLevel].sprite = GetArrowSprite("skillname_bottom-deuce");
            //setArrow
            BeatLeft = 2;
            BeatRight = 2;
        }
        LArrowSprite[ArrowLevel].sprite = GetArrowSprite(string.Format("{0}{1}", LStatus.SkillElement, BeatLeft));
        RArrowSprite[ArrowLevel].sprite = GetArrowSprite(string.Format("{0}{1}", RStatus.SkillElement, BeatRight));

    }


    static void ShowArrowLogFunction()
    {
        switch (ArrowLevel)
        {
            case 0:
                Al[0].PlayAni("Down0");
                Al[0].PlayArrowAni("ArrowPlay");
                if (StartArrowLevel > 0) Al[1].PlayAni("Down4");
                if (StartArrowLevel > 1) Al[2].PlayAni("Down3");
                if (StartArrowLevel > 2) Al[3].PlayAni("Down2");
                if (StartArrowLevel > 3) Al[4].PlayAni("Down1");
                break;
            case 1:
                Al[0].PlayAni("Down1");
                if (StartArrowLevel > 0) Al[1].PlayAni("Down0");
                Al[1].PlayArrowAni("ArrowPlay");
                if (StartArrowLevel > 1) Al[2].PlayAni("Down4");
                if (StartArrowLevel > 2) Al[3].PlayAni("Down3");
                if (StartArrowLevel > 3) Al[4].PlayAni("Down2");
                break;
            case 2:
                Al[0].PlayAni("Down2");
                if (StartArrowLevel > 0) Al[1].PlayAni("Down1");
                if (StartArrowLevel > 1) Al[2].PlayAni("Down0");
                Al[2].PlayArrowAni("ArrowPlay");
                if (StartArrowLevel > 2) Al[3].PlayAni("Down4");
                if (StartArrowLevel > 3) Al[4].PlayAni("Down3");
                break;
            case 3:
                Al[0].PlayAni("Down3");
                if (StartArrowLevel > 0) Al[1].PlayAni("Down2");
                if (StartArrowLevel > 1) Al[2].PlayAni("Down1");
                if (StartArrowLevel > 2) Al[3].PlayAni("Down0");
                Al[3].PlayArrowAni("ArrowPlay");
                if (StartArrowLevel > 3) Al[4].PlayAni("Down4");
                break;
            case 4:
                Al[0].PlayAni("Down4");
                if (StartArrowLevel > 0) Al[1].PlayAni("Down3");
                if (StartArrowLevel > 1) Al[2].PlayAni("Down2");
                if (StartArrowLevel > 2) Al[3].PlayAni("Down1");
                if (StartArrowLevel > 3) Al[4].PlayAni("Down0");
                Al[4].PlayArrowAni("ArrowPlay");
                break;
        }

        ArrowLevel++;
        if (StartArrowLevel < 4) StartArrowLevel++;
        if (ArrowLevel > 4) ArrowLevel = 0;
    }
}
