using UnityEngine;
using System.Collections;

public class EnhanceMaterial
{

    public int ItemID { get; set; }
    public Sprite ItemSprite { get; set; }
    public string ItemSpriteName { get; set; }
    public string ItemName { get; set; }


    /// <summary>
    /// 強化怪獸素材建構式，傳入物品ID、物品名稱、物品spriteID
    /// </summary>
    public EnhanceMaterial(int _itemID, string _itemName, int _itemSpriteID)
    {
        try
        {
            ItemID = _itemID;
            ItemSprite = Resources.Load<Sprite>(string.Format("Sprite/Item/Item{0}", _itemSpriteID));
            ItemSpriteName = string.Format("item{0}", _itemSpriteID);
            ItemName = _itemName;
        }
        catch
        {
            Debug.LogError("建構怪獸強化物品時發生錯誤");
        }
    }
}
