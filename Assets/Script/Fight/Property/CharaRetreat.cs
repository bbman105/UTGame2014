using UnityEngine;
using System.Collections;

public partial class Status : MonoBehaviour
{
    GameObject RetreatIconObj;//撤退ICON物件
    public int RetreatCondition { get; set; }//撤退狀態 0=沒撤退 1=撤退中 2=已撤退 
    Vector3 RetreatPosistion;
    Vector3 FightPosistion;
    StatusEffect CureEffect;//撤退治癒效果
    ExcuteEffect RetretEffectExcuter;//撤退治癒器
    public void RetreatStartSet()
    {
        RetreatPosistion = new Vector3(-6f, 3, 0);//設定退場位置
        FightPosistion = new Vector3(-4.5f, 3, 0);//設定退場位置
        CureEffect = new StatusEffect(2000011, true);//設定治癒狀態
        RetreatIconObj = myTransform.FindChild("Retreat").gameObject;
    }
    public void SetRetreatPos()//設定為退場位置
    {
        myTransform.position = RetreatPosistion;//設定為退場位置
    }
    public void SetFightPos()//設定為出場位置
    {
        myTransform.position = FightPosistion;//設定為出場位置
    }
    public void Fight()//出戰
    {
        if (RetreatCondition==1)
        {
            rigidStatus.RemoveEnterRigid();//移除鋼體狀態
            SetNormalCollider();//設定怪獸會受到牆壁碰撞影響
            SetReverseMovDirection(false);//設定怪物往前跑
            swingEffect.SetReverseMovAnimation(false);//向前跑動畫
            SpeciesModeler.SetReverseFace(false);//設定腳色面向前方
            SpeciesModeler.SetSelectedColor(true);//怪物顏色恢復正常
            RetreatIconObj.SetActive(false);//隱藏撤退ICON
            RetreatCondition = 0;
        }
        else if(RetreatCondition==2)
        {
            SetEnterCollider();//設定腳色圖層為可穿透牆壁，但離開牆壁後會恢復可碰撞牆壁的圖層
            rigidStatus.SetEnterRigid();//給予鋼體狀態
            SetFightPos();//設定為出場位置
            canMov = true;
            canForceMov = true;
            twinkleEffect.enabled = false;
            swingEffect.enabled = true;
            shockEffect.enabled = false;
            SetReverseMovDirection(false);//設定怪物往前跑
            swingEffect.SetReverseMovAnimation(false);//向前跑動畫
            SpeciesModeler.SetReverseFace(false);//設定腳色面向前方
            SpeciesModeler.SetSelectedColor(true);//怪物顏色恢復正常
            RetreatIconObj.SetActive(false);//隱藏撤退ICON
            RemoveRetreatHealEffect();//移除撤退治癒效果
            RetreatCondition = 0;
        }
    }
    void SetRetreatCollider()//設定腳色圖層為可穿透牆壁
    {
        myGameObject.layer = LayerMask.NameToLayer("P1CharaRetreat");
        Collider2D[] colliders = this.GetComponents<Collider2D>();
        for(int i=0;i<colliders.Length;i++)
        {
            colliders[i].enabled = false;
            colliders[i].enabled = true;
        }        
    }
    void SetEnterCollider()//設定腳色圖層為可穿透牆壁，但離開牆壁後會恢復可碰撞牆壁的圖層
    {
        myGameObject.layer = LayerMask.NameToLayer("P1CharaEnter");//設定腳色圖層為可穿透牆壁，但離開牆壁後會恢復可碰撞牆壁的圖層
    }
    void SetNormalCollider()//設定怪獸會受到牆壁碰撞影響
    {
        myGameObject.layer = LayerMask.NameToLayer("P1Chara");//設定圖層為可碰撞牆壁
    }
    void AddRetreatHealEffect()//撤退的怪獸獲得治癒效果
    {
        RetretEffectExcuter = myGameObject.AddComponent<ExcuteEffect>();
        RetretEffectExcuter.SetEffect(this, this, CureEffect);
    }
    void RemoveRetreatHealEffect()//出戰怪獸移除撤退治癒效果
    {
        RetretEffectExcuter.EndEffect();
    }
    void OnRetreat()//撤退判斷
    {
        if (RetreatCondition == 1)
        {
            if (myTransform.localPosition.x < -4.2f)
            {
                IsRetreated();//已經退場
            }
        }
    }
    public void IsRetreated()//已經退場
    {
        canMov = false;
        canForceMov = false;
        twinkleEffect.enabled = false;
        swingEffect.enabled = false;
        shockEffect.enabled = false;
        RetreatIconObj.SetActive(false);//隱藏撤退ICON
        myRigidbody2D.velocity = Vector2.zero;
        SetEnterCollider();//設定腳色圖層為可穿透牆壁，但離開牆壁後會恢復可碰撞牆壁的圖層
        SetRetreatPos();//設定為退場位置
        ClearAllEffect();//清除狀態
        AddRetreatHealEffect();//撤退的怪獸獲得治癒效果
        RetreatCondition = 2;
    }
    public void StartRetreat()//開始撤退
    {
        SetReverseMovDirection(true);//設定怪物往後跑
        swingEffect.SetReverseMovAnimation(true);//向後跑動畫
        SpeciesModeler.SetReverseFace(true);//設定腳色面向後方
        SpeciesModeler.SetSelectedColor(false);//怪物顏色壓灰
        RetreatIconObj.SetActive(true);//顯示撤退ICON
        SetRetreatCollider();//設定腳色圖層為可穿透牆壁
        rigidStatus.SetEnterRigid();//給予鋼體狀態
        RetreatCondition = 1;
    }
}
