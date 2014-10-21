using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class Status : MonoBehaviour, IViewDebugLog
{
    private GameObject healthBarObj;//腳色頭上血條物件
    private Transform healthBarTransform;//腳色頭上寫條Transform
    private SpriteRenderer healthBar;//腳色頭上寫條SpriteRenderer
    private SpriteRenderer healthBarFrame;//血條框
    private SpriteRenderer healthBarBottom;//血條底色
    private float healthChangeValue;
    private byte healthChangeType;
    float redTencity;
    void UpdateSortingRank()//更新SortingRank
    {
        if (SortingRank < 32760)//sortingOrder最高似乎只到32767，所以超過32760就重新計算排序
            SortingRank += 5;
        else
            SortingRank = 0;
    }
    void UpdateHealthSorting()//設定血條與腳色圖層排序
    {
        healthBarBottom.sortingOrder = SortingRank + 1;
        healthBar.sortingOrder = SortingRank + 2;
        healthBarFrame.sortingOrder = SortingRank + 3;
    }
    public void StartSetHealthStatus()//初始設定
    {
        healthBarObj = myTransform.FindChild("HealthBar").gameObject;
        healthBarTransform = healthBarObj.transform.FindChild("healthBar").transform;
        healthBar = healthBarObj.transform.FindChild("healthBar").GetComponent<SpriteRenderer>();
        healthBarBottom = healthBarObj.transform.FindChild("healthBottom").GetComponent<SpriteRenderer>();
        healthBarFrame = healthBarObj.transform.FindChild("healthFrame").GetComponent<SpriteRenderer>();
        UpdateSortingRank();//更新SortingRank
        UpdateHealthSorting();//更新血條圖層排序
        switch (MainElement)
        {
            case 0:
                healthBar.color = Color.red;
                break;
            case 1:
                healthBar.color = Color.green;
                break;
            case 2:
                healthBar.color = new Color32(42, 144, 229, 255);
                break;
            default:
                healthBar.color = Color.white;
                Debug.Log("無法辨識的怪獸屬性");
                break;
        }

    }
    public void SetCurHealth(int _value)
    {
        this.healthChangeValue = _value;
        CurHealth = this.healthChangeValue;
        LoseHealth = MaxHealth - CurHealth;
        //生命<=0就設定為死亡
        if (CurHealth <= 0)
        {
            Alive = false;
        }
    }
    public void SetMaxHealth(int _value)
    {
        this.healthChangeValue = _value;
        MaxHealth = this.healthChangeValue;
    }
    //要改變腳色屬性需透過此function
    public void OnHealthChange(float _value, byte _type)//type類型0=加;1=乘;2=等於;3=減生命最大值得幾%
    {
        this.healthChangeValue = _value;
        this.healthChangeType = _type;
        switch (this.healthChangeType)
        {
            case 0:
                CurHealth += this.healthChangeValue;
                break;
            case 1:
                CurHealth *= this.healthChangeValue;
                break;
            case 2:
                CurHealth = this.healthChangeValue;
                break;
            case 3:
                CurHealth -= MaxHealth * _value;
                break;
            default:
                Debug.Log("血量改變運算子參數無法辨識");
                break;
        }
        //生命<=0就設定為死亡
        LoseHealth = MaxHealth - CurHealth;
        if (CurHealth > 0)
        {
            if (RetreatCondition == 0)//如果怪獸出戰中
            {
                //HealthChangeColor();血量影響色調，快死會越趨近紅色
            }
        }
        else
        {
            NPCMonsterSpawnLoot();//NPC怪物死亡噴寶物
            Alive = false;
            HideHealthBar();//隱藏血條
            ClearAllEffect();//清除狀態
            //ResetHealthColor();
            ForceDeathCheck();//判斷執行玩家或怪物死亡委託
            ForceCondition.OnSomebodyDeath();//執行怪獸死亡的委託事件
        }
        UpdateSortingRank();//更新SortingRank
        UpdateBarSprite();//更新腳色頭上血條
        UpdateCharaSorting();//更新腳色圖層階級
        ForceCondition.OnForceStatusChanged();
        //ViewDebugLog();//查看DebugLog
    }
    private void ForceDeathCheck()//判斷執行玩家或怪物死亡委託
    {
        if (myGameObject.tag == "PlayerMonster")
        {
            ForceCondition.OnPlayerDeath();//執行玩家怪獸死亡的委託事件
        }
        else if (myGameObject.tag == "NPCMonster")
        {
            ForceCondition.OnNPCDeath();//執NPC怪獸死亡的委託事件
        }
    }
    public void ViewDebugLog()
    {
        Debug.Log("改變值:" + this.healthChangeValue);
        Debug.Log("改變類型:" + this.healthChangeType);
        Debug.Log("改變後生命:" + this.CurHealth);
    }
    void HealthChangeColor()
    {
        redTencity = 0.3f + (0.7f * ((float)CurHealth / (float)MaxHealth));
        CharaSpriteRenderer.color = new Color(1, redTencity, redTencity);
    }
    void ResetHealthColor()
    {
        CharaSpriteRenderer.color = new Color(1, 1, 1);
    }
    void UpdateBarSprite()//設定腳色頭上血條
    {
        float tmpHealthBarLength = (8 * ((float)CurHealth / (float)MaxHealth));
        healthBarTransform.localScale = new Vector3(tmpHealthBarLength, 1, 1);//更新腳色ICON血條
        UpdateHealthSorting();//設定血條圖層排序
    }
    void HideHealthBar()//隱藏血條
    {
        healthBarObj.SetActive(false);
    }
    void NPCMonsterSpawnLoot()//NPC怪物死亡噴寶物
    {
        if (myGameObject.tag == "NPCMonster")
            ItemSpawner.SpawnItem(this, 1);//敵人死亡噴金幣
    }
}
