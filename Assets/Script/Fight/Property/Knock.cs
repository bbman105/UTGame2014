using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Status : MonoBehaviour
{

    Status targetStatus;//目標狀態
    GameObject targetObj;//目標物件
    const int minBeKnockBackStr = 150;//最小被擊飛力道
    const int maxBeKnockBackStr = 500;//最大被擊飛力道
    private float beKnockUpStr;
    public float BeKnockUpStr//被擊高力道
    {
        get { return beKnockUpStr; }
        set { beKnockUpStr = value; }
    }
    private float beKnockBackStr;//腳色被擊飛力道
    public float BeKnockBackStr
    {
        get { return beKnockBackStr; }
        set
        {
            if (value < minBeKnockBackStr)
            {
                beKnockBackStr = minBeKnockBackStr;
            }
            else if (beKnockBackStr > maxBeKnockBackStr)
            {
                beKnockBackStr = maxBeKnockBackStr;
            }
            else { beKnockBackStr = value; }
        }
    }


    //重擊傷害加值改變
    private int criticalDMGPlus;
    public int CriticalDMGPlus
    {
        get { return criticalDMGPlus; }
        set { criticalDMGPlus = value; }
    }
    //重擊傷害倍率改變
    private float criticalDMGTimes;
    public float CriticalDMGTimes
    {
        get { return criticalDMGTimes; }
        set { criticalDMGTimes = value; }
    }
    //受到重擊傷害加值改變
    private int beCriticalDMGPlus;
    public int BeCriticalDMGPlus
    {
        get { return beCriticalDMGPlus; }
        set { beCriticalDMGPlus = value; }
    }
    //受到重擊傷害倍率改變
    private float beCriticalDMGTimes;
    public float BeCriticalDMGTimes
    {
        get { return beCriticalDMGTimes; }
        set { beCriticalDMGTimes = value; }
    }
    //擊退力道加值改變
    private int knockStrengthPlus;
    public int KnockStrengthPlus
    {
        get { return knockStrengthPlus; }
        set { knockStrengthPlus = value; }
    }
    //被擊飛力道加值
    int beKnockStrengthPlus;
    public int BeKnockStrengthPlus
    {
        get { return beKnockStrengthPlus; }
        set { beKnockStrengthPlus = value; }
    }
    public const float BaseKnockNailTime = 0.5f;
    //擊飛定身時間
    private float knockNailTime;
    public float KnockNailTime
    {
        get { return knockNailTime; }
        set { knockNailTime = value; }
    }
    //逼牆傷害加值改變
    private int knockWallDMGPlus;
    public int KnockWallDMGPlus
    {
        get { return knockWallDMGPlus; }
        set { knockWallDMGPlus = value; }
    }
    //逼牆傷害倍率改變
    private float knockWallDMGTimes;
    public float KnockWallDMGTimes
    {
        get { return knockWallDMGTimes; }
        set { knockWallDMGTimes = value; }
    }
    //被逼牆傷害加值改變
    private int beKnockWallDMGPlus;
    public int BeKnockWallDMGPlus
    {
        get { return beKnockWallDMGPlus; }
        set { beKnockWallDMGPlus = value; }
    }
    //被逼牆傷害倍率改變
    private float beKnockWallDMGTimes;
    public float BeKnockWallDMGTimes
    {
        get { return beKnockWallDMGTimes; }
        set { beKnockWallDMGTimes = value; }
    }
    //被逼牆最終傷害加值改變
    private int beKnockWallResultDMGPlus;
    public int BeKnockWallResultDMGPlus
    {
        get { return beKnockWallResultDMGPlus; }
        set { beKnockWallResultDMGPlus = value; }
    }
    //被逼牆最終傷害倍率改變
    private float beKnockWallResultDMGTimes;
    public float BeKnockWallResultDMGTimes
    {
        get { return beKnockWallResultDMGTimes; }
        set { beKnockWallResultDMGTimes = value; }
    }
    public const float BaseKnockWallStunTime = 0.5f;
    //逼牆暈眩加值改變
    private float knockWallStunPlus;
    public float KnockWallStunPlus
    {
        get { return knockWallStunPlus; }
        set { knockWallStunPlus = value; }
    }
    //被逼牆暈眩加值改變
    private float beKnockWallStunPlus;
    public float BeKnockWallStunPlus
    {
        get { return beKnockWallStunPlus; }
        set { beKnockWallStunPlus = value; }
    }
    //被逼牆最終暈眩加值改變
    private float beKnockWallResultStun;
    public float BeKnockWallResultStun
    {
        get { return beKnockWallResultStun; }
        set { beKnockWallResultStun = value; }
    }

    //擊飛定身時間加值改變
    private float knockNailTimePlus;
    public float KnockNailTimePlus
    {
        get { return knockNailTimePlus; }
        set { knockNailTimePlus = value; }
    }
    //被擊飛定身時間加值改變
    private float beKnockNailTimePlus;
    public float BeKnockNailTimePlus
    {
        get { return beKnockNailTimePlus; }
        set { beKnockNailTimePlus = value; }
    }
    //雙方屬性相剋結果
    int elementResult;
    public int ElementResult
    {
        get { return elementResult; }
        set { elementResult = value; }
    }
    //是否技能屬性被剋
    bool onElementHit;
    public bool OnElementHit
    {
        get { return onElementHit; }
        set { onElementHit = value; }
    }
    //優勢體型
    int dominantSize;

    public int SelectSpellRank { get; set; }//自身使用第幾個技能
    public Skill SelectSpell;
    RandomSpellGetor playerMonsterRandomGetor;//PlayerMonster技能亂數類型
    SpellGetor playerSpellGetor;//技能亂數取得器
    RandomSpellGetor npcMonsterRandomGetor;//NpcMonster技能亂數類型
    SpellGetor npcSpellGetor;//技能亂數取得器
    ElementHitSpawner elementHitPlayer;//元素爆擊播放器

    public Dictionary<int, bool> OnExempt; //此怪物對場上每隻怪獸的碰撞豁免
    static float ExemptTime = 0.7f;//碰撞豁免CD時間
    private Dictionary<int, float> ExemptTimer { get; set; }//碰撞豁免計時器
    private List<int> OnExemptBuffer;
    public bool CanKnock { get; set; }//可以撞人
    public bool CanSpell { get; set; }//可施法
    public bool TmpCanSpell { get; set; }//暫存可施法，如果前置效果結束時判斷為可施法，那後置效果時期不會因為擊退暈眩效果導致無法施放後置效果
    public bool CanBeKnock { get; set; }//可被擊飛
    public bool CanKnockWall { get; set; }//可撞牆
    public bool TmpCanBeKnock { get; set; }//暫存可被擊飛，如果前置效果結束時判斷為可被擊飛

    //特效顯示
    EffectSpawner knockWallEffect;
    //撞牆傷害
    Damage KnockWallDamage;
    ExcuteDamage KnockWallDamageExcuter;


    private int knockDamage;
    public int KnockDamage//撞擊顯示傷害
    {
        get { return knockDamage; }
        set { knockDamage = value; }
    }
    //0=lava 1=emerald 2=gaia 3=sky
    public int SkillElement { get; set; }
    UIAtlas arrowAtlas;

    public void KnockSet()//初始化撞擊參數
    {
        OnExempt = new Dictionary<int, bool>();//設定碰撞豁免字典
        ExemptTimer = new Dictionary<int, float>();//設定碰撞豁免計時器
        CanBeKnock = true;//設定為可被擊飛
        CanSpell = true;//設定為可施法
        CanKnock = true;//設為可以施法
        TmpCanSpell = true;
        BeKnockBackStr = 0;
        BeKnockUpStr = 0;
        //是否技能被剋true為被剋制
        ElementResult = 0;
        OnElementHit = false;
        //優勢體型 0為相等體型;1為大1級;2為大2級
        dominantSize = 0;
        //擊飛力道加值
        BeKnockStrengthPlus = 0;
        KnockStrengthPlus = 0;

        playerMonsterRandomGetor = new GetPlayerMonsterSpellRank();//選擇使用玩者怪物亂數挑選
        playerSpellGetor = new SpellGetor();
        npcMonsterRandomGetor = new GetNpcMonsterSpellRank();//選擇使用玩者怪物亂數挑選
        npcSpellGetor = new SpellGetor();
        knockWallEffect = new EffectSpawner(17);//撞牆特效
        //撞牆扣血
        KnockWallDamage = new Damage(2000001, true);
        KnockWallDamageExcuter = new ExcuteDamage();
        //元素爆擊播放器
        elementHitPlayer = new ElementHitSpawner();
    }



    void OnCollisionStay2D(Collision2D _target)//碰撞到目標
    {
        targetObj = _target.gameObject;
        //抓取目標腳色status
        targetStatus = (Status)_target.gameObject.GetComponent("Status");
        switch (targetObj.tag)
        {
            case "NPCMonster":
                if (Alive)        // 如果碰撞目標為NPCMonster且自己還活著
                {
                    if (!OnExempt.ContainsKey(targetStatus.TmpMonsterID))
                    {
                        OnExempt.Add(targetStatus.TmpMonsterID, false);
                        ExemptTimer.Add(targetStatus.TmpMonsterID, 0);
                    }

                    if (!OnExempt[targetStatus.TmpMonsterID])//判斷對目標是否還處於豁免碰撞中
                    {
                        SkillPhase();//讀取雙方技能，並執行技能
                        DominantSizePhase();//計算雙方有無體型優勢階段
                        KnockBackPhase(); //計算彼此撞飛力道，並執行擊飛
                        StunPhase();//設定雙方碰撞後暈眩
                        KnockEffect();//設定雙方基本擊中特效
                        KnockExemptPhase();//限制"可被撞"及"可撞人"進入CD，此設定是防止瞬間碰撞多次
                        //BattleLogPlayer.ShowArrowLog(this, targetStatus);
                    }
                }
                break;
            case "LWall":
                if (myGameObject.tag == "PlayerMonster" && Alive)//如果腳色為玩家且沒死亡，且可撞牆，呼叫撞牆特效    
                {
                    if (CanKnockWall)
                    {
                        KnockWallSet();//撞牆受到傷害
                        KnockWallEffectSet(_target);//撞牆特效
                    }
                }
                else if (myGameObject.tag == "NPCMonster" && Alive)
                {
                    KnockWallEffectSet(_target);//撞牆特效
                    //取得撞牆力道
                    BeKnockBackStr = knockCalculator.GetKnockWallBackStrength();
                    //給予自身力道
                    rigidbody2D.AddForce(Vector2.right * BeKnockBackStr * -ForceDir);
                }
                else if (!Alive)//如果死亡了，呼叫撞到牆壁彈飛function      
                {
                    KnockWallEffectSet(_target);//撞牆特效
                    OnDyingClliding();
                }
                break;
            case "RWall":
                if (myGameObject.tag == "NPCMonster" && Alive)//如果腳色為NPC且沒死亡，且可撞牆，呼叫撞牆特效   
                {
                    if (CanKnockWall)
                    {
                        KnockWallSet();//撞牆受到傷害
                        KnockWallEffectSet(_target);//撞牆特效
                    }
                }
                else if (myGameObject.tag == "PlayerMonster" && Alive)
                {
                    KnockWallEffectSet(_target);//撞牆特效
                    //取得撞牆力道
                    BeKnockBackStr = knockCalculator.GetKnockWallBackStrength();
                    //給予自身力道
                    rigidbody2D.AddForce(Vector2.right * BeKnockBackStr * -ForceDir);
                }
                else if (!Alive)//如果死亡了，呼叫撞到牆壁彈飛function      
                {
                    KnockWallEffectSet(_target);//撞牆特效
                    OnDyingClliding();
                }
                break;
        }
    }
    private void KnockWallSet()//撞牆受到傷害暈眩，並執行撞牆效果
    {
        //撞牆傷害
        KnockWallDamageExcuter.SetKnockWallDamage(this, KnockWallDamage);
        KnockWallDamageExcuter.Excute(19);
        //撞牆暈眩
        stunStatus.SetKnockStun(BeKnockWallResultStun);
        StartKnockWallTriggerEffect();//執行被逼牆觸發傷害效果
        StartKnockWallTriggerDamage();//執行被逼牆觸發傷害效果
        StartKnockWallTriggerCure();//執行被逼牆觸發傷害效果
    }
    private void KnockWallEffectSet(Collision2D _target)//撞牆特效
    {
        //場景震動
        GameObject scene = GameObject.FindGameObjectWithTag("FightScene");
        ShockEffect[] shockEffects = scene.GetComponentsInChildren<ShockEffect>();
        for (int i = 0; i < shockEffects.Length; i++)
        {
            shockEffects[i].Shock(4);
        }
        //撞牆特效
        knockWallEffect.SpawnPointEffect(_target);
        //撞牆後，不會再次觸發撞牆
        CanKnockWall = false;
    }
    public void NpcSkillSet()//怪物技能初運算，先跑過一次，防止碰撞時瞬間LAG
    {
        Spell nullSpell = new PlayerMonsterSpell();
        ((NpcAI)Ai).CheckAISpell(this);//檢查目標NPC通過施法條件檢定的技能
        //取得目標使用技能
        ((GetNpcMonsterSpellRank)npcMonsterRandomGetor).SetNpcMonsterSpellRank(Ai);
        npcSpellGetor.SetSpellGetor(npcMonsterRandomGetor);
        SelectSpellRank = npcSpellGetor.GetSpellRank();//按照機率取得使用第幾個技能
        SpellCDCounter[SelectSpellRank].StartCD();//施放的施法AI進入CD
        ///////////////////////////////////////////////////////////////
        nullSpell.ExcutePreCure(this, this);//執行前置治癒
        nullSpell.ExcutePreEffect(this, this);//執行前置效果
        ////////////////////////////////////////////////////////////
        nullSpell.ExcuteSpellDamage(this, this);//執行施法
        /////////////////////////////////////////////////////////////////
        nullSpell.ExcuteReCure(this, this);//執行後置治癒
        nullSpell.ExcuteReEffect(this, this);//執行後置效果
        //設定自身碰撞豁免時間
        OnExempt[0] = true;
        ExemptTimer[0] = ExemptTime;
        //呼叫閃爍特效
        twinkleEffect.Twinkle(5);
        //呼叫震動特效
        shockEffect.Shock(5);
    }



    void SkillPhase()//讀取雙方技能，並執行技能
    {
        //一、前置設置///////////////////////////////////////////////
        if (CanSpell)//如果自身可以撞人就可施放技能
        {
            //讀取自身使用技能
            playerSpellGetor.SetSpellGetor(playerMonsterRandomGetor);
            SelectSpellRank = playerSpellGetor.GetSpellRank();//按照機率取得使用第幾個技能
        }
        if (targetStatus.CanSpell)//如果目標可以撞人，就可施放技能
        {
            ((NpcAI)targetStatus.Ai).CheckAISpell(this);//檢查目標NPC通過施法條件檢定的技能
            //取得目標使用技能
            ((GetNpcMonsterSpellRank)npcMonsterRandomGetor).SetNpcMonsterSpellRank(targetStatus.Ai);
            npcSpellGetor.SetSpellGetor(npcMonsterRandomGetor);
            targetStatus.SelectSpellRank = npcSpellGetor.GetSpellRank();//按照機率取得使用第幾個技能
            targetStatus.SpellCDCounter[targetStatus.SelectSpellRank].StartCD();//施放的施法AI進入CD
        }
        //二、前置效果執行期///////////////////////////////////////////////////
        //執行碰撞前觸發自身及目標效果
        if (CanSpell)//如果自身可以施法就可施放技能效果
        {
            SpellList[SelectSpellRank].ExcutePreCure(this, targetStatus);//執行前置治癒
            SpellList[SelectSpellRank].ExcutePreEffect(this, targetStatus);//執行前置效果
        }
        if (targetStatus.CanSpell)//如果目標可以施法，就可施放技能效果
        {
            targetStatus.SpellList[targetStatus.SelectSpellRank].ExcutePreCure(targetStatus, this);//執行前置治癒
            targetStatus.SpellList[targetStatus.SelectSpellRank].ExcutePreEffect(targetStatus, this);//執行前置效果
        }
        StartPreKnockTrigger();//設定撞擊前觸發效果
        TmpCanSpell = CanSpell;
        targetStatus.TmpCanSpell = targetStatus.CanSpell;
        TmpCanBeKnock = CanBeKnock;
        targetStatus.TmpCanBeKnock = targetStatus.CanBeKnock;
        //三、技能傷害執行期///////////////////////////////////////////////////
        ElementHitPhase();//計算雙方有無元素打擊階段
        if (CanSpell)//如果自身可以施法就可施放技能傷害
        {
            SpellList[SelectSpellRank].ExcuteSpellDamage(this, targetStatus);//執行施法
            //設定目標撞牆傷害加值
            targetStatus.BeKnockWallResultDMGPlus = (targetStatus.BeKnockWallDMGPlus + KnockWallDMGPlus);
            targetStatus.BeKnockWallResultDMGTimes = 1 + targetStatus.BeKnockWallDMGTimes + KnockWallDMGTimes;
            //設定目標撞牆暈眩加值
            targetStatus.BeKnockWallResultStun = BaseKnockWallStunTime + (targetStatus.BeKnockWallStunPlus + KnockWallStunPlus);
        }
        if (targetStatus.CanSpell)//如果目標可以施法，就可施放技能傷害
        {
            targetStatus.SpellList[targetStatus.SelectSpellRank].ExcuteSpellDamage(targetStatus, this);//執行施法
            //設定自身撞牆傷害加值
            BeKnockWallResultDMGPlus = (BeKnockWallDMGPlus + targetStatus.KnockWallDMGPlus);
            BeKnockWallResultDMGTimes = 1 + BeKnockWallDMGTimes + targetStatus.KnockWallDMGTimes;
            //設定自身撞牆暈眩加值
            BeKnockWallResultStun = BaseKnockWallStunTime + (BeKnockWallStunPlus + targetStatus.KnockWallStunPlus);
        }
        StartKnockTriggerDamage();//設定撞擊觸發傷害效果
        //四、後置效果執行期/////////////////////////////////////////////////// 
        if (TmpCanSpell)//如果自身可以施法就可施放技能效果
        {
            SpellList[SelectSpellRank].ExcuteReCure(this, targetStatus);//執行後置治癒
            SpellList[SelectSpellRank].ExcuteReEffect(this, targetStatus);//執行後置效果
        }
        if (targetStatus.TmpCanSpell)//如果目標可以施法，就可施放技能傷害
        {
            targetStatus.SpellList[targetStatus.SelectSpellRank].ExcuteReCure(targetStatus, this);//執行後置治癒
            targetStatus.SpellList[targetStatus.SelectSpellRank].ExcuteReEffect(targetStatus, this);//執行後置效果
        }
        //執行碰撞後觸發自身及目標效果
        StartReKnockTrigger();//設定撞擊後觸發效果
    }

    private void ElementHitPhase()//計算雙方有無元素打擊&是否播放技能特效階段
    {

        ElementResult = elementCalculator.GetElementHit(MainElement, targetStatus.MainElement);
        switch (ElementResult)
        {
            case 0://平手
                OnElementHit = false;
                targetStatus.OnElementHit = false;
                EffectSpawner targetAreaES = new EffectSpawner(17);
                targetAreaES.SpawnAreaEffect(this, targetStatus);
                ForceCondition.ForceStatusDic["PlayerMonster"].DoNormalHit();//一般撞擊
                break;
            case 1://剋對方
                OnElementHit = true;
                targetStatus.OnElementHit = false;
                SpellList[SelectSpellRank].SpawnEffect(this, targetStatus);
                ForceCondition.ForceStatusDic["PlayerMonster"].DoElementHit();//元素爆擊
                elementHitPlayer.ComboHit();//設定顯示Combo數字
                ItemSpawner.SpawnItem(targetStatus,0);//目標噴金幣
                break;
            case 2://被剋
                OnElementHit = false;
                targetStatus.OnElementHit = true;
                ForceCondition.ForceStatusDic["PlayerMonster"].DoBeElementHit();//被元素報及
                elementHitPlayer.InterruptCombo();//設定中斷Combo
                targetStatus.SpellList[targetStatus.SelectSpellRank].SpawnEffect(targetStatus, this);
                break;
        }
    }
    private void DominantSizePhase()//計算雙方有無體型優勢階段
    {
        if (CanKnock)//如果自身可以撞人
            dominantSize = dominantSizeCalculator.GetDominantSizeCalculator(Size, targetStatus.Size);
        if (targetStatus.CanKnock)//如果目標可以撞人
            targetStatus.dominantSize = dominantSizeCalculator.GetDominantSizeCalculator(targetStatus.Size, Size);
    }
    private void KnockBackPhase()//計算彼此撞飛力道，並執行擊飛
    {

        //如果自身處在可以被碰撞的狀態且目標也可以撞人
        if (TmpCanBeKnock && targetStatus.CanKnock)
        {
            //設定自身被撞飛力道
            BeKnockBackStr = knockCalculator.GetKnockBackStrength(Aggressive, AggressiveTimes, AggressivePlus, BeKnockStrengthPlus,
                targetStatus.Aggressive, targetStatus.AggressiveTimes, targetStatus.AggressivePlus, targetStatus.KnockStrengthPlus,
                targetStatus.OnElementHit);
            BeKnockUpStr = knockCalculator.GetKnockUpStrength(dominantSize);
            //給予自身力道
            rigidbody2D.AddForce(Vector2.up * BeKnockUpStr);
            rigidbody2D.AddForce(Vector2.right * BeKnockBackStr * -ForceDir);
            if (!CanKnockWall)
            {
                CanKnockWall = true;//設為可撞牆
            }
        }
        //如果自身可以撞人且目標可以被撞
        if (targetStatus.TmpCanBeKnock && CanKnock)
        {
            //設定目標被撞飛力道
            targetStatus.BeKnockBackStr = knockCalculator.GetKnockBackStrength(targetStatus.Aggressive, targetStatus.AggressiveTimes, targetStatus.AggressivePlus, targetStatus.BeKnockStrengthPlus,
                Aggressive, AggressiveTimes, AggressivePlus, KnockStrengthPlus,
                OnElementHit);
            targetStatus.BeKnockUpStr = knockCalculator.GetKnockUpStrength(targetStatus.dominantSize);
            //給予目標力道
            targetObj.rigidbody2D.AddForce(Vector2.right * targetStatus.BeKnockBackStr * -targetStatus.ForceDir);
            targetObj.rigidbody2D.AddForce(Vector2.up * targetStatus.BeKnockUpStr);
            if (!targetStatus.CanKnockWall)
            {
                targetStatus.CanKnockWall = true;//設為可撞牆
            }
        }
    }
    private void KnockExemptPhase()//設定碰撞豁免防止瞬間碰撞多次
    {
        if (!OnExempt[targetStatus.TmpMonsterID])
        {
            //設定自身碰撞豁免時間
            OnExempt[targetStatus.TmpMonsterID] = true;
            ExemptTimer[targetStatus.TmpMonsterID] = ExemptTime;
        }
    }
    private void KnockExemptCDTimerFunction()//擊中後碰撞豁免CD倒數
    {
        OnExemptBuffer = new List<int>(OnExempt.Keys);
        foreach (int key in OnExemptBuffer)
        {
            if (ExemptTimer[key] > 0)
            {
                ExemptTimer[key] -= Time.deltaTime;
                if (ExemptTimer[key] <= 0)
                {
                    OnExempt[key] = false;
                }
            }
        }
    }

    private void KnockEffect()//設定雙方基本擊中特效
    {
        ///////////自身被撞特效///////////////
        if (targetStatus.CanKnock)
        {
            //呼叫閃爍特效
            twinkleEffect.Twinkle(6);
            //呼叫震動特效
            //shockEffect.Shock(4);//暫時不加入撞擊震動
        }
        if (CanKnock)
        {
            ///////////目標被撞特效///////////////
            //呼叫閃爍特效
            targetStatus.twinkleEffect.Twinkle(6);
            //呼叫震動特效
            //targetStatus.shockEffect.Shock(4);//暫時不加入撞擊震動
        }
    }


    private void StunPhase()//設定雙方碰撞後定身
    {
        //有出招才能定身對手
        if (CanKnock)
        {
            if (!targetStatus.IsRigid)//非鋼體狀態才會受到撞擊而定身
            {
                targetStatus.KnockNailTime = BaseKnockNailTime + targetStatus.BeKnockNailTimePlus + KnockNailTimePlus;
                //定身目標
                targetStatus.nailStatus.SetKnockNail(targetStatus.KnockNailTime);
            }
        }
        if (targetStatus.CanKnock)
        {
            if (!IsRigid)//非鋼體狀態才會受到撞擊而定身
            {
                KnockNailTime = BaseKnockNailTime + BeKnockNailTimePlus + targetStatus.KnockNailTimePlus;
                //自身定身
                nailStatus.SetKnockNail(KnockNailTime);
            }
        }
    }
}
