using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public partial class ForceCondition : MonoBehaviour
{

    QuestAchievementGettor AchievementGettor;//成就取得器
    UnlockQuestGettor QuestUnlockGettor;//關卡解鎖器
    //結算UI
    static GameObject battleEndPannel;//UGUI物件
    static GameObject battleEndExtraReward;//額外獎勵標籤
    static GameObject battleEndDetail;//戰鬥結算畫面物件
    static GameObject battleEndExperience;//戰鬥經驗結算畫面物件
    static GameObject battleEndBG;//BattleEnd黑背景
    //獎勵文字UI
    static Animator extraRewardAni;
    static Animator goldPlusAni;
    static Animator expPlusAni;
    static UILabel rewardLabel;
    static UILabel expLabel;
    static UILabel goldLabel;
    static UILabel goldPlusLabel;
    static UILabel expPlusLabel;
    static UILabel remainHealthLabel;
    static UILabel elementHitLabel;
    static UILabel dealthNumLabel;
    static UILabel spendTimeLabel;
    static Vector3 plusPos = new Vector3(30, 0, 0);
    //取得獎勵物品
    static Transform[] ItemTransformArray;
    static UISprite[] ItemSpriteArray;
    static UILabel[] ItemLabelArray;
    //經驗介面
    static BoxCollider continueToTrainSceneCollider;

    private static void StartSetBattleEndUI()//戰鬥結算UI設置
    {
        //結算面板
        battleEndPannel = GameObject.FindGameObjectWithTag("BattleEnd");//抓取battleEndPannel物件
        battleEndDetail = battleEndPannel.transform.FindChild("BattleEndDetail").gameObject;
        battleEndBG = battleEndPannel.transform.FindChild("BG").gameObject;
        battleEndExtraReward = battleEndDetail.transform.FindChild("ExtraReward").gameObject;

        extraRewardAni = battleEndExtraReward.GetComponent<Animator>();
        rewardLabel = battleEndExtraReward.GetComponent<UILabel>();
        expLabel = battleEndDetail.transform.FindChild("Exp").GetComponent<UILabel>();
        goldLabel = battleEndDetail.transform.FindChild("Gold").GetComponent<UILabel>();
        expPlusLabel = battleEndDetail.transform.FindChild("ExpPlus").FindChild("ExpPlus").GetComponent<UILabel>();
        goldPlusLabel = battleEndDetail.transform.FindChild("GoldPlus").FindChild("GoldPlus").GetComponent<UILabel>();
        expPlusAni = battleEndDetail.transform.FindChild("ExpPlus").GetComponent<Animator>();
        goldPlusAni = battleEndDetail.transform.FindChild("GoldPlus").GetComponent<Animator>();
        remainHealthLabel = battleEndDetail.transform.FindChild("Health").GetComponent<UILabel>();
        elementHitLabel = battleEndDetail.transform.FindChild("ElementHit").GetComponent<UILabel>();
        spendTimeLabel = battleEndDetail.transform.FindChild("Time").GetComponent<UILabel>();
        dealthNumLabel = battleEndDetail.transform.FindChild("Death").GetComponent<UILabel>();
        //取得獎勵物品物件
        ItemTransformArray = new Transform[8];
        ItemSpriteArray = new UISprite[8];
        ItemLabelArray = new UILabel[8];
        for (int i = 0; i < ItemSpriteArray.Length; i++)
        {
            ItemTransformArray[i] = battleEndDetail.transform.FindChild("ItemList").FindChild(string.Format("Item{0}", i));
            ItemSpriteArray[i] = ItemTransformArray[i].FindChild("item").GetComponent<UISprite>();
            ItemLabelArray[i] = ItemTransformArray[i].FindChild("num").GetComponent<UILabel>();
        }

        //////////////////////////////抓取經驗介面腳色卡片，並抓取腳色卡及各屬性物件////////////////////////////////
        battleEndExperience = battleEndPannel.transform.FindChild("BattleEndExperience").gameObject;
        continueToTrainSceneCollider = battleEndExperience.transform.FindChild("continueToTrainScene").GetComponent<BoxCollider>();
        //創建腳色卡
        memberCard = new GameObject[3];
        //初始化腳色卡上的ICon各屬性數值的物件
        iconCharaAtlas = new UIAtlas[3];
        iconCharaSprite = new UISprite[3];
        bannerSprite = new UISprite[3];
        bottomSprite = new UISprite[3];
        elementSprite = new UISprite[3];
        lvLabel = new UILabel[3];
        deathLabel = new UILabel[3];
        deathMaskSprite = new UISprite[3];
        //初始化腳色卡各屬性數值的物件
        reRollProperty = new ReRollLvPropertyBtn[3];
        nameLabel = new UILabel[3];
        lifeLabel = new UILabel[3];
        brutalLabel = new UILabel[3];
        aggressiveLabel = new UILabel[3];
        resistanceLabel = new UILabel[3];
        addLifeLabel = new UILabel[3];
        addBrutalLabel = new UILabel[3];
        addAggressiveLabel = new UILabel[3];
        addResistanceLabel = new UILabel[3];
        lifeArrow = new UISprite[3];
        brutalArrow = new UISprite[3];
        resistanceArrow = new UISprite[3];
        aggressiveArrow = new UISprite[3];
        expSprite = new UISprite[3];
        levelUpSprite = new Animator[3];

        int pos = 0;
        for (int i = 0; i < 3; i++)
        {
            memberCard[i] = battleEndExperience.transform.FindChild(string.Format("MemberTitle{0}", i + 1)).gameObject;

            //抓取腳色卡的ICon各屬性數值的物件
            iconCharaAtlas[i] = memberCard[i].transform.FindChild("chara").FindChild("chara").GetComponent<UIAtlas>();
            deathLabel[i] = memberCard[i].transform.FindChild("chara").FindChild("deathText").GetComponent<UILabel>();
            iconCharaSprite[i] = memberCard[i].transform.FindChild("chara").FindChild("chara").GetComponent<UISprite>();
            bannerSprite[i] = memberCard[i].transform.FindChild("chara").FindChild("banner").GetComponent<UISprite>();
            bottomSprite[i] = memberCard[i].transform.FindChild("chara").FindChild("bottom").GetComponent<UISprite>();
            elementSprite[i] = memberCard[i].transform.FindChild("chara").FindChild("element").GetComponent<UISprite>();
            deathMaskSprite[i] = memberCard[i].transform.FindChild("chara").FindChild("mask").GetComponent<UISprite>();
            //抓取腳色卡上的怪物屬性物件
            nameLabel[i] = memberCard[i].transform.FindChild("name").GetComponent<UILabel>();
            lifeLabel[i] = memberCard[i].transform.FindChild("life").GetComponent<UILabel>();
            brutalLabel[i] = memberCard[i].transform.FindChild("brutal").GetComponent<UILabel>();
            aggressiveLabel[i] = memberCard[i].transform.FindChild("aggressive").GetComponent<UILabel>();
            resistanceLabel[i] = memberCard[i].transform.FindChild("resistance").GetComponent<UILabel>();
            addLifeLabel[i] = memberCard[i].transform.FindChild("addLife").GetComponent<UILabel>();
            addBrutalLabel[i] = memberCard[i].transform.FindChild("addBrutal").GetComponent<UILabel>();
            addAggressiveLabel[i] = memberCard[i].transform.FindChild("addAggressive").GetComponent<UILabel>();
            addResistanceLabel[i] = memberCard[i].transform.FindChild("addResistance").GetComponent<UILabel>();
            lifeArrow[i] = memberCard[i].transform.FindChild("lifeArrow").GetComponent<UISprite>();
            brutalArrow[i] = memberCard[i].transform.FindChild("brutalArrow").GetComponent<UISprite>();
            resistanceArrow[i] = memberCard[i].transform.FindChild("resistanceArrow").GetComponent<UISprite>();
            aggressiveArrow[i] = memberCard[i].transform.FindChild("aggressiveArrow").GetComponent<UISprite>();
            expSprite[i] = memberCard[i].transform.FindChild("exp").GetComponent<UISprite>();
            lvLabel[i] = memberCard[i].transform.FindChild("lv").GetComponent<UILabel>();
            levelUpSprite[i] = memberCard[i].transform.FindChild("levelUp").GetComponent<Animator>();
            reRollProperty[i] = memberCard[i].transform.FindChild("rollBtn").GetComponent<ReRollLvPropertyBtn>();
            reRollProperty[i].StartSet();
            reRollProperty[i].EnableButton(false);//將按鈕設為不啟動
            if (Player.TeamDic[PlayerTeamID].Member[i] != 0)//如果該隊伍位置有怪物
            {
                CurMember = Player.MonsterDic[(Player.TeamDic[PlayerTeamID].Member[i])];
                reRollProperty[pos].playerMonster = CurMember;
                reRollProperty[pos].RollBtnID = pos;
                pos++;
            }
        }
        //校正點擊底部繼續的按鈕觸碰範圍
        switch (pos)
        {
            case 1:
                continueToTrainSceneCollider.size = new Vector3(480, 1000, 0);
                break;
            case 2:
                continueToTrainSceneCollider.size = new Vector3(480, 560, 0);
                break;
            case 3:
                continueToTrainSceneCollider.size = new Vector3(480, 130, 0);
                break;
            default:
                break;
        }
    }
    void BattleEndAnim()
    {
        battleEndBG.SetActive(true);
        battleEndDetail.SetActive(true);
        //play anim
        BattleEnd be = (BattleEnd)battleEndPannel.GetComponent("BattleEnd");
        be.PlayBattleEndAni("BattleEnd1");
    }
    void DetailUIShow()//展示結算畫面
    {
        if (RewardShow)
        {
            switch (Result)
            {
                case 1:
                    battleEndDetail.transform.FindChild("Title").GetComponent<UISprite>().spriteName = "win";
                    break;
                case 2:
                    battleEndDetail.transform.FindChild("Title").GetComponent<UISprite>().spriteName = "defeat";
                    break;
                case 3:
                    battleEndDetail.transform.FindChild("Title").GetComponent<UISprite>().spriteName = "deauce";
                    break;
                default:
                    break;
            }
            expLabel.text = GainExp.ToString();
            goldLabel.text = GainGold.ToString();
            remainHealthLabel.text = string.Format("剩餘血量: {0}%", ForceStatusDic["PlayerMonster"].CurHealthPercent.ToString("#0"));
            elementHitLabel.text = string.Format("元素爆擊率: {0}%", ForceStatusDic["PlayerMonster"].ElementHitRate.ToString("#0"));
            spendTimeLabel.text = string.Format("花費時間: {0}S", SpentTime);
            dealthNumLabel.text = string.Format("死亡數: {0}", ForceStatusDic["PlayerMonster"].DeathCharaNum);
            ////////////獎勵物品////////////
            if (KillLootDic.Count <= ItemSpriteArray.Length)
            {
                List<int> dicKeys = new List<int>(KillLootDic.Keys);
                for (int i = 0; i < ItemTransformArray.Length; i++)
                {
                    if (i < dicKeys.Count)
                    {
                        ItemTransformArray[i].gameObject.SetActive(true);
                        ItemSpriteArray[i].spriteName = string.Format("item{0}", dicKeys[i]);//顯示物品圖像
                        ItemLabelArray[i].text = KillLootDic[dicKeys[i]].ToString();//顯示數量
                    }
                    else
                    {
                        ItemTransformArray[i].gameObject.SetActive(false);
                    }
                }
            }
            RewardShow = false;
            StartCoroutine("ChangeRank");
            StartCoroutine("EndRankShow");
        }
    }

    IEnumerator ChangeRank()//每0.05秒亂數改變一次Rank圖像
    {
        while (!CalculateTimeUp)
        {
            switch (RankTmpShow)//依照戰鬥結果取得正確的評價標題
            {
                case 1:
                    battleEndDetail.transform.FindChild("Rank").GetComponent<UISprite>().spriteName = "s";
                    break;
                case 2:
                    battleEndDetail.transform.FindChild("Rank").GetComponent<UISprite>().spriteName = "a";
                    break;
                case 3:
                    battleEndDetail.transform.FindChild("Rank").GetComponent<UISprite>().spriteName = "b";
                    break;
                case 4:
                    battleEndDetail.transform.FindChild("Rank").GetComponent<UISprite>().spriteName = "c";
                    break;
                case 5:
                    battleEndDetail.transform.FindChild("Rank").GetComponent<UISprite>().spriteName = "d";
                    break;
                default:
                    break;
            }
            RankTmpShow++;
            RewardPerTime += 0.01f;
            yield return new WaitForSeconds(RewardPerTime);
        }
    }
    void ExtraRewardShow()
    {
        StartCoroutine("AddReward");
    }
    IEnumerator AddReward()//每0.05秒增加獎賞直到數值達到實際獲得獎賞值後停止
    {
        rewardLabel.text = string.Format("獎勵+{0}%", RankExtraPercent * 100);
        extraRewardAni.SetTrigger("ExtraReward");
        yield return new WaitForSeconds(0.2f);

        /*
        Debug.Log("GainGold=" + GainGold);
        Debug.Log("GainExp=" + GainExp);
        Debug.Log("FinalGold=" + FinalGold);
        Debug.Log("FinalExp=" + FinalExp);
        */

        TmpGold = GainGold;
        TmpExp = GainExp;
        int extraGold = (FinalGold - GainGold);
        int extraExp = (FinalExp - GainExp);
        int perGoldAdd = (int)(extraGold / 20) + 1;
        int perExpAdd = (int)(extraExp / 20) + 1;
        expPlusLabel.text = string.Format("+{0}", extraExp);
        goldPlusLabel.text = string.Format("+{0}", extraGold);
        int expColumn = 0;
        int goldColumn = 0;
        if (FinalExp > 1000)
        {
            expColumn = 3;
        }
        else if (FinalExp > 100)
        {
            expColumn = 2;
        }
        else if (FinalExp > 10)
        {
            expColumn = 1;
        }
        else
        {
            expColumn = 0;
        }
        if (FinalGold > 1000)
        {
            goldColumn = 3;
        }
        else if (FinalGold > 100)
        {
            goldColumn = 2;
        }
        else if (FinalGold > 10)
        {
            goldColumn = 1;
        }
        else
        {
            goldColumn = 0;
        }
        expPlusLabel.transform.localPosition = Vector3.zero + plusPos * expColumn;
        goldPlusLabel.transform.localPosition = Vector3.zero + plusPos * goldColumn;

        if (extraGold > 0)
            goldPlusAni.SetTrigger("Plus");
        if (extraExp > 0)
            expPlusAni.SetTrigger("Plus");

        yield return new WaitForSeconds(0.2f);
        while (TmpGold < FinalGold || TmpExp < FinalExp)
        {
            TmpGold += perGoldAdd;
            TmpExp += perExpAdd;
            battleEndDetail.transform.FindChild("Exp").GetComponent<UILabel>().text = TmpExp.ToString();
            battleEndDetail.transform.FindChild("Gold").GetComponent<UILabel>().text = TmpGold.ToString();
            yield return new WaitForSeconds(0.05f);
        }
        battleEndDetail.transform.FindChild("Exp").GetComponent<UILabel>().text = FinalExp.ToString();
        battleEndDetail.transform.FindChild("Gold").GetComponent<UILabel>().text = FinalGold.ToString();
    }
    IEnumerator EndRankShow()//時間到後顯示正確的Rank圖像
    {
        yield return new WaitForSeconds(CalculateTime);
        CalculateTimeUp = true;//時間到顯示正確的Rank圖像
        yield return new WaitForSeconds(RewardPerTime);
        //show Rank UISprite
        switch (Rank)//依照評價取得正確評價標題
        {
            case 1:
                battleEndDetail.transform.FindChild("Rank").GetComponent<UISprite>().spriteName = "s";
                break;
            case 2:
                battleEndDetail.transform.FindChild("Rank").GetComponent<UISprite>().spriteName = "a";
                break;
            case 3:
                battleEndDetail.transform.FindChild("Rank").GetComponent<UISprite>().spriteName = "b";
                break;
            case 4:
                battleEndDetail.transform.FindChild("Rank").GetComponent<UISprite>().spriteName = "c";
                break;
            case 5:
                battleEndDetail.transform.FindChild("Rank").GetComponent<UISprite>().spriteName = "d";
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.5f);
        ExtraRewardShow();//顯示額外獎勵
    }

    public void ExperienceUIShow()//展示經驗畫面
    {
        if (ShowExperience)
        {
            ShowExperience = false;
            battleEndDetail.SetActive(false);
            battleEndExperience.SetActive(true);
            int pos = 0;//控制隊員顯示位置，如果隊員2空出，則把隊員3顯示位置補在隊員2的位置
            int inActive = 0;
            for (int i = 0; i < 3; i++)//最多3個隊員
            {
                if (Player.TeamDic[PlayerTeamID].Member[i] != 0)//如果該隊伍位置有怪物
                {
                    memberCard[pos].SetActive(true);
                    CurMember = Player.MonsterDic[(Player.TeamDic[PlayerTeamID].Member[i])];
                    //選擇對的物種頭像
                    if (CurMember.Species < 8)
                    {
                        iconCharaSprite[pos].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                    }
                    else
                    {
                        iconCharaSprite[pos].atlas = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon2");
                    }
                    //設定外框元素及底圖的atlas
                    iconCharaAtlas[pos] = Resources.Load<UIAtlas>("Atlas/Character/Icon/CharaIcon");
                    iconCharaSprite[pos].spriteName = CurMember.SpeciesKey;
                    iconCharaSprite[pos].MakePixelPerfect();
                    elementSprite[pos].atlas = iconCharaAtlas[pos];
                    elementSprite[pos].spriteName = "element" + CurMember.MainElement.ToString();
                    elementSprite[pos].MakePixelPerfect();
                    bannerSprite[pos].atlas = iconCharaAtlas[pos];
                    bannerSprite[pos].spriteName = "frame" + CurMember.MainElement.ToString();
                    bannerSprite[pos].MakePixelPerfect();
                    bottomSprite[pos].atlas = iconCharaAtlas[pos];
                    bottomSprite[pos].spriteName = "bottom" + CurMember.MainElement.ToString();
                    bottomSprite[pos].MakePixelPerfect();
                    //設置怪物的各種屬性資料(名稱、等級、經驗、種族、體型、生命、殘暴、侵略、抵抗)
                    lvLabel[pos].text = TmpMemberLv[i].ToString();
                    expSprite[pos].width = (int)(10 + ((float)TmpMemberExp[i] / (float)TmpMemberNeedExp[i]) * 305);
                    nameLabel[pos].text = CurMember.MonsterName;
                    brutalLabel[pos].text = TmpMemberBrutal[i].ToString();
                    aggressiveLabel[pos].text = TmpMemberAggressive[i].ToString();
                    resistanceLabel[pos].text = TmpMemberResistance[i].ToString();
                    addLifeLabel[pos].alpha = 0;
                    addBrutalLabel[pos].alpha = 0;
                    addAggressiveLabel[pos].alpha = 0;
                    addResistanceLabel[pos].alpha = 0;
                    lifeArrow[pos].alpha = 0;
                    brutalArrow[pos].alpha = 0;
                    aggressiveArrow[pos].alpha = 0;
                    resistanceArrow[pos].alpha = 0;
                    pos++;
                }
                else
                {
                    memberCard[2 - inActive].SetActive(false);
                    inActive++;
                }
            }
            StartCoroutine("AddExp");
            StartCoroutine("ReCheckEndExperienceShow");
        }
    }

    void CalculatePerMemberExperience()//計算每個隊員取得的經驗
    {
        int pos = 0;
        for (int i = 0; i < 3; i++)//最多3個隊員
        {
            if (Player.TeamDic[PlayerTeamID].Member[i] != 0)//如果該隊伍位置有怪物
            {
                CurMember = Player.MonsterDic[(Player.TeamDic[PlayerTeamID].Member[i])];
                TmpMemberExp[i] = CurMember.CurExp;//將得到經驗獎勵前的經驗數值記錄起來
                TmpMemberNeedExp[i] = CurMember.NeedExp;//將得到經驗獎勵前的經驗數值記錄起來
                TmpMemberLv[i] = CurMember.Lv;
                TmpMemberBrutal[i] = CurMember.Brutal;
                TmpMemberAggressive[i] = CurMember.Aggressive;
                TmpMemberResistance[i] = CurMember.Resistance;
                TmpMemberHealth[i] = CurMember.Health;

                expSprite[pos].width = (int)(10 + ((float)TmpMemberExp[i] / (float)TmpMemberNeedExp[i]) * 305);
                lvLabel[pos].text = TmpMemberLv[i].ToString();
                lifeLabel[pos].text = TmpMemberHealth[i].ToString();
                brutalLabel[pos].text = TmpMemberBrutal[i].ToString();
                aggressiveLabel[pos].text = TmpMemberAggressive[i].ToString();
                resistanceLabel[pos].text = TmpMemberResistance[i].ToString();
                if (ForceCondition.CharaStatusDic["PlayerMonster"][i].Alive)//如果該隊員活著
                {
                    //CurMember.GetExp(PerFinalExp);
                    deathLabel[pos].enabled = false;
                    deathMaskSprite[pos].enabled = false;
                }
                else
                {
                    deathLabel[pos].enabled = true;
                    deathMaskSprite[pos].enabled = true;
                }
                pos++;
            }
        }
    }
    IEnumerator AddExp()//每0.05秒增加經驗直到數值達到實際獲得經驗值後停止
    {
        float times = (float)ExperienceTime / ExpPerTime;
        float perExpAdd = ((float)(PerFinalExp) / times);//計算出動畫每次會加的經驗數值
        float curAddExp = 0;//目前動畫已經增加的經驗值
        int pos = 0;//控制隊員顯示位置，如果隊員2空出，則把隊員3顯示位置補在隊員2的位置
        while (curAddExp < PerFinalExp && !ExperienceTimeUp)//時間未到，且目前已經增加的經驗值小於應得到的經驗值總數
        {
            pos = 0;
            curAddExp += perExpAdd;//目前動畫已增加的經驗值增加
            for (int i = 0; i < 3; i++)//最多3個隊員
            {
                if (Player.TeamDic[PlayerTeamID].Member[i] != 0)//如果該隊伍位置有怪物
                {
                    if (ForceCondition.CharaStatusDic["PlayerMonster"][i].Alive)//如果該隊員活著
                    {
                        CurMember = Player.MonsterDic[(Player.TeamDic[PlayerTeamID].Member[i])];

                        if (CanLvUp[i])
                        {
                            TmpMemberExp[i] += perExpAdd;//暫時經驗值增加
                            expSprite[pos].width = 10 + (int)(((float)TmpMemberExp[i] / (float)TmpMemberNeedExp[i]) * 305);//經驗條增加
                            if (TmpMemberExp[i] >= TmpMemberNeedExp[i])//如果戰時經驗值>暫時等級所需經驗代表升級
                            {
                                CanLvUp[i] = false;
                                TmpMemberLv[i]++;//暫時等級+1
                                TmpMemberExp[i] = 0;//升級時，將暫時經驗值顯示條回0
                                TmpMemberNeedExp[i] = GameDictionary.MonsterExperienceDic[TmpMemberLv[i]][CurMember.RareToString(CurMember.Rare)];//取得下一等級的所需經驗值
                                TmpMemberHealth[i] += CurMember.AddLvHealthPlus;
                                TmpMemberBrutal[i] += CurMember.AddLvBrutalPlus;
                                TmpMemberResistance[i] += CurMember.AddLvResistancePlus;
                                TmpMemberAggressive[i] += CurMember.AddLvAggressivePlus;
                                AddMemberHealth[i] += CurMember.AddLvHealthPlus;
                                AddMemberBrutal[i] += CurMember.AddLvBrutalPlus;
                                AddMemberResistance[i] += CurMember.AddLvResistancePlus;
                                AddMemberAggressive[i] += CurMember.AddLvAggressivePlus;
                                reRollProperty[pos].EnableButton(true);
                                //更新升級後的顯示數值UI
                                LevelUpChangeLabelValue(pos, i);
                                expSprite[pos].width = 10;
                            }
                        }
                    }
                    pos++;
                }
            }
            yield return new WaitForSeconds(ExpPerTime);
        }
        EndExperienceShow();//顯示正確的經驗值
    }
    public static void ReRollChangeLabelValue(int pos, PlayerMonster _curMonster)//重骰更新UI數值
    {
        CurMember = _curMonster;
        //設置怪物的各種屬性資料(名稱、等級、經驗、種族、體型、生命、殘暴、侵略、抵抗)
        expSprite[pos].width = (int)(10 + ((float)CurMember.CurExp / CurMember.NeedExp) * 305);//顯示正確的經驗條長度
        lvLabel[pos].text = CurMember.Lv.ToString();
        lifeLabel[pos].text = CurMember.Health.ToString();
        brutalLabel[pos].text = CurMember.Brutal.ToString();
        aggressiveLabel[pos].text = CurMember.Aggressive.ToString();
        resistanceLabel[pos].text = CurMember.Resistance.ToString();
        if (CurMember.AddLvHealthPlus != 0)//如果升級增加的血量不是0，則顯示數值及箭頭
        {
            addLifeLabel[pos].text = CurMember.AddLvHealthPlus.ToString();
            addLifeLabel[pos].alpha = 255;
            lifeArrow[pos].alpha = 255;
        }
        else
        {
            addLifeLabel[pos].alpha = 0;
            lifeArrow[pos].alpha = 0;
        }
        if (CurMember.AddLvBrutalPlus != 0)//如果升級增加的殘暴不是0，則顯示數值及箭頭
        {
            addBrutalLabel[pos].text = CurMember.AddLvBrutalPlus.ToString();
            addBrutalLabel[pos].alpha = 255;
            brutalArrow[pos].alpha = 255;
        }
        else
        {
            addBrutalLabel[pos].alpha = 0;
            brutalArrow[pos].alpha = 0;
        }
        if (CurMember.AddLvAggressivePlus != 0)//如果升級增加的侵略不是0，則顯示數值及箭頭
        {
            addAggressiveLabel[pos].text = CurMember.AddLvAggressivePlus.ToString();
            addAggressiveLabel[pos].alpha = 255;
            aggressiveArrow[pos].alpha = 255;
        }
        else
        {
            addAggressiveLabel[pos].alpha = 0;
            aggressiveArrow[pos].alpha = 0;
        }
        if (CurMember.AddLvResistancePlus != 0)//如果升級增加的血量不是0，則顯示數值及箭頭
        {
            addResistanceLabel[pos].text = CurMember.AddLvResistancePlus.ToString();
            addResistanceLabel[pos].alpha = 255;
            resistanceArrow[pos].alpha = 255;
        }
        else
        {
            addResistanceLabel[pos].alpha = 0;
            resistanceArrow[pos].alpha = 0;
        }
    }
    static void LevelUpChangeLabelValue(int pos, int i)//升級時更新成員屬性，pos是指定哪個物件,i是指定哪個隊員的資料
    {
        reRollProperty[pos].CanReroll = true;//開啟按鈕功能
        CurMember = Player.MonsterDic[(Player.TeamDic[PlayerTeamID].Member[i])];
        //設置怪物的各種屬性資料(名稱、等級、經驗、種族、體型、生命、殘暴、侵略、抵抗)
        expSprite[pos].width = (int)(10 + ((float)TmpMemberExp[i] / (float)TmpMemberNeedExp[i]) * 305);//經驗條增加
        lvLabel[pos].text = TmpMemberLv[i].ToString();
        lifeLabel[pos].text = TmpMemberHealth[i].ToString();
        brutalLabel[pos].text = TmpMemberBrutal[i].ToString();
        aggressiveLabel[pos].text = TmpMemberAggressive[i].ToString();
        resistanceLabel[pos].text = TmpMemberResistance[i].ToString();
        levelUpSprite[pos].SetTrigger("levelUp");
        if (AddMemberHealth[i] != 0)//如果升級增加的血量不是0，則顯示數值及箭頭
        {
            addLifeLabel[pos].text = AddMemberHealth[i].ToString();
            addLifeLabel[pos].alpha = 255;
            lifeArrow[pos].alpha = 255;
        }
        if (AddMemberBrutal[i] != 0)//如果升級增加的殘暴不是0，則顯示數值及箭頭
        {
            addBrutalLabel[pos].text = AddMemberBrutal[i].ToString();
            addBrutalLabel[pos].alpha = 255;
            brutalArrow[pos].alpha = 255;
        }
        if (AddMemberAggressive[i] != 0)//如果升級增加的侵略不是0，則顯示數值及箭頭
        {
            addAggressiveLabel[pos].text = AddMemberAggressive[i].ToString();
            addAggressiveLabel[pos].alpha = 255;
            aggressiveArrow[pos].alpha = 255;
        }
        if (AddMemberResistance[i] != 0)//如果升級增加的血量不是0，則顯示數值及箭頭
        {
            addResistanceLabel[pos].text = AddMemberResistance[i].ToString();
            addResistanceLabel[pos].alpha = 255;
            resistanceArrow[pos].alpha = 255;
        }
    }
    public static void EndExperienceShow()//時間到後顯示正確的經驗值
    {

        ExperienceTimeUp = true;//時間到顯示正確的成員數值
        int pos = 0;//控制隊員顯示位置，如果隊員2空出，則把隊員3顯示位置補在隊員2的位置
        for (int i = 0; i < 3; i++)//最多3個隊員
        {
            if (Player.TeamDic[PlayerTeamID].Member[i] != 0)//如果該隊伍位置有怪物
            {
                if (ForceCondition.CharaStatusDic["PlayerMonster"][i].Alive)//如果該隊員活著
                {
                    CurMember = Player.MonsterDic[(Player.TeamDic[PlayerTeamID].Member[i])];
                    //設置怪物的各種屬性資料(名稱、等級、經驗、種族、體型、生命、殘暴、侵略、抵抗)
                    expSprite[pos].width = (int)(10 + ((float)CurMember.CurExp / CurMember.NeedExp) * 305);//顯示正確的經驗條長度
                    lvLabel[pos].text = CurMember.Lv.ToString();
                    lifeLabel[pos].text = CurMember.Health.ToString();
                    brutalLabel[pos].text = CurMember.Brutal.ToString();
                    aggressiveLabel[pos].text = CurMember.Aggressive.ToString();
                    resistanceLabel[pos].text = CurMember.Resistance.ToString();
                }
                pos++;
            }
        }
    }

    IEnumerator ReCheckEndExperienceShow()//如果跑間驗條動畫時間超出2秒，直接顯示最後的結果，跳過動畫
    {
        yield return new WaitForSeconds(2f);
        EndExperienceShow();
    }


    public static void GoTrainScene()
    {
        Application.LoadLevel("TrainScene");
    }
    /// <summary>
    /// 判斷獲勝還是戰敗
    /// </summary>
    void CalculateResult() //判斷哪邊獲勝
    {
        if (ForceStatusDic["PlayerMonster"].CurHealthPercent > ForceStatusDic["NPCMonster"].CurHealthPercent) Result = 1;
        else if (ForceStatusDic["PlayerMonster"].CurHealthPercent < ForceStatusDic["NPCMonster"].CurHealthPercent) Result = 2;
        else { Result = 3; }
    }
    /// <summary>
    /// 計算獲得的金錢
    /// </summary>
    void CalculateGoldReward()//取得獲得金錢
    {
        if (Result == 1)
        {
            GainGold = BaseGold + HitGold;
        }
        else
        {
            GainGold = HitGold;
        }
        RankGold = (int)(GainGold * RankExtraPercent);
        FinalGold = GainGold + RankGold;
    }
    /// <summary>
    /// 計算獲得的經驗
    /// </summary>
    void CalculateExpReward()//取得獲得經驗值
    {
        //判斷怪物死亡就取得怪物經驗
        for (int i = 0; i < ForceCondition.CharaStatusDic["NPCMonster"].Length; i++)
        {

            if (!ForceCondition.CharaStatusDic["NPCMonster"][i].Alive)
            {
                //KillGold += ForceCondition.CharaStatusDic["NPCMonster"][i].Gold;
                KillExp += ForceCondition.CharaStatusDic["NPCMonster"][i].Exp;
            }
        }
        if (Result == 1)
        {
            GainExp = BaseExp + KillExp;
        }
        else
        {
            GainExp = KillExp;
        }
        RankExp = (int)(GainExp * RankExtraPercent);
        FinalExp = GainExp + RankExp;
        //計算活著的隊員數，並取得每個隊員獲得的經驗量
        int aliveMember = 0;
        for (int i = 0; i < ForceCondition.CharaStatusDic["PlayerMonster"].Length; i++)
        {
            if (ForceCondition.CharaStatusDic["PlayerMonster"][i].Alive)
                aliveMember++;
        }
        if (aliveMember > 0)
            PerFinalExp = (int)(FinalExp / aliveMember);
    }
    /// <summary>
    /// 計算評價加權獎勵比例
    /// </summary>
    void CalculateRankPlusReward()//計算評價加權獎勵比例
    {
        switch (Rank)//依照評價取得評價獎勵
        {
            case 1:
                RankExtraPercent = 0.8f;
                break;
            case 2:
                RankExtraPercent = 0.5f;
                break;
            case 3:
                RankExtraPercent = 0.4f;
                break;
            case 4:
                RankExtraPercent = 0.3f;
                break;
            case 5:
                RankExtraPercent = 0.2f;
                break;
            default:
                RankExp = GainExp;
                RankGold = GainGold;
                Debug.Log("無法辨識評價等級");
                break;
        }
    }
    void CalculateReward()//計算獎勵
    {
        CalculateResult();//判斷哪邊獲勝
        CalculateRank();//計算評價
        CalculateRankPlusReward();//計算評價加權獎勵比例
        CalculateGoldReward();//取得獲得金錢
        CalculateExpReward();//取得獲得經驗值
        BattleResultReturn();//戰鬥結果回傳
    }
    /// <summary>
    /// 戰鬥結果回傳
    /// </summary>
    void BattleResultReturn()//戰鬥結果回傳
    {
        try
        {
            /////////////怪物增加經驗並回傳///////////////
            for (int i = 0; i < 3; i++)//最多3個隊員
            {
                if (Player.TeamDic[PlayerTeamID].Member[i] != 0)//如果該隊伍位置有怪物
                {
                    CurMember = Player.MonsterDic[(Player.TeamDic[PlayerTeamID].Member[i])];
                    if (ForceCondition.CharaStatusDic["PlayerMonster"][i].Alive)//如果該隊員活著
                    {
                        CurMember.GetExp(PerFinalExp);
                    }
                }
            }
            /////////////玩家增加金幣並回傳///////////////
            Player.OwnResource.AddResource("Gold", FinalGold);
            /////////////玩家增加資源並回傳///////////////
            List<int> dicBuffer = new List<int>(KillLootDic.Keys);
            foreach (int key in dicBuffer)
            {
                Player.OwnResource.AddEnhanceMaterial(key, KillLootDic[key]);
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning("回傳戰鬥結果時發生錯誤");
            Debug.LogWarning(ex);
        }
    }
    void CalculateRank()//計算評價
    {
        float elementHitRate = ForceStatusDic["PlayerMonster"].ElementHitRate;
        RankScore = ForceStatusDic["PlayerMonster"].Score;
        float curHealthPercent = ForceStatusDic["PlayerMonster"].CurHealthPercent;
        int maxCharaNum = ForceStatusDic["PlayerMonster"].MaxCharaNum;
        int deathCharaNum = ForceStatusDic["PlayerMonster"].DeathCharaNum;
        //caculate elementHit score
        if (elementHitRate < 20)
        {
            //doNothing
        }
        else if (elementHitRate >= 20 && elementHitRate < 40)
        {
            RankScore++;
        }
        else
        { RankScore += 2; }

        //caculate health score
        if (curHealthPercent < 35)
        {
            //donothing
        }
        else if (curHealthPercent < 80 && curHealthPercent >= 35)
        {
            RankScore++;
        }
        else
        { RankScore += 2; }
        //caculate time score
        if (SpentTime <= 15)
        {
            RankScore += 3;
        }
        else if (SpentTime > 15 && SpentTime <= 25)
        {
            RankScore += 2;
        }
        else
        {
            RankScore++;
        }
        //caculate dealth score
        RankScore += 3 * (maxCharaNum - deathCharaNum);

        //依照分數計算出評價
        if (RankScore >= 15)
        {
            Rank = 1;
        }
        else if (RankScore >= 13 && RankScore < 15)
        {
            Rank = 2;
        }
        else if (RankScore >= 8 && RankScore < 13)
        {
            Rank = 3;
        }
        else if (RankScore >= 5 && RankScore < 8)
        {
            Rank = 4;
        }
        else
        {
            Rank = 5;
        }
    }
    void CalculateAchievement()
    {
        AchievementGettor = new QuestAchievementGettor(new CompleteGettor());
        AchievementGettor.CheckAchieveCondition();
        AchievementGettor.SetAchievementType(new ExploreGettor());
        AchievementGettor.CheckAchieveCondition();
        AchievementGettor.SetAchievementType(new RankSGettor());
        AchievementGettor.CheckAchieveCondition();
        AchievementGettor.SetAchievementType(new HuttingGettor());
        AchievementGettor.CheckAchieveCondition();
    }
    void UnlockCheck()
    {
        QuestUnlockGettor = new UnlockQuestGettor();
        QuestUnlockGettor.UnlockQuest();
    }
    /// <summary>
    /// 結果計算
    /// </summary>
    void ResultCaculate()//結果計算
    {
        CalculatePerMemberExperience();//計算取得經驗
        CalculateReward();//計算獎勵
        CalculateAchievement();//計算取得成就
        UnlockCheck();//解鎖關卡檢查
    }

}
