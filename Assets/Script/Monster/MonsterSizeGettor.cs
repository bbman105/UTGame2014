using UnityEngine;
using System.Collections;

public class MonsterSizeGettor
{

    PlayerMonster pm;
    float spritePosY;//怪獸圖像的Y軸
    float colliderRadius;//碰撞範圍大小
    float eventColliderPosY;//碰撞物件的Y軸
    public MonsterSizeGettor(PlayerMonster _pm)
    {
        this.pm = _pm;
        spritePosY = 0;//怪獸圖像的Y軸
        colliderRadius = 0;//碰撞範圍大小
        eventColliderPosY = 0;//碰撞物件的Y軸
    }


    public void SetSize(float _size)
    {
        spritePosY = 1 * ((pm.Size - 0.5f) / 1.2f);
        pm.MonsterSprite.transform.localPosition = new Vector3(0, spritePosY, 0); //設定圖像位置
        pm.MonsterSprite.transform.localScale = new Vector3(_size, _size, 1);//設定圖像大小
        colliderRadius = 0.8f + (0.6f * ((pm.Size - 0.5f) / 1.2f));
        eventColliderPosY = 0.8f + (0.7f * ((pm.Size - 0.5f) / 1.2f));
        pm.EventBubble.transform.localPosition = new Vector3(0, eventColliderPosY, 0);//設定eventBubble碰撞位置
        pm.EventCollider.radius = colliderRadius;//設定eventBubble碰撞範圍
    }
}
