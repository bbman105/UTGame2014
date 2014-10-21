using UnityEngine;
using System.Collections;

public class EnhanceNeedMaterial
{
    /// <summary>
    /// 需求物品ID
    /// </summary>
    public int ItemID { get; set; }
    /// <summary>
    /// 需求物品數量
    /// </summary>
    public int ItemNum { get; set; }
    /// <summary>
    /// 需求物品Sprite
    /// </summary>
    public Sprite ItemSprite { get; set; }
    /// <summary>
    /// 需求物品SpriteName
    /// </summary>
    public string ItemSpriteName { get; set; }
    /// <summary>
    /// 需求物品名稱
    /// </summary>
    public string ItemName { get; set; }
    /// <summary>
    /// 需求黃金
    /// </summary>
    public int NeedGold { get; set; }
    /// <summary>
    /// 強化需求素材建構式，傳入物品ID、數量、黃金
    /// </summary>
    public EnhanceNeedMaterial(int _itemID, int _itemNum, int _gold)
    {
        if (_itemID != 0)
        {
            try
            {
                ItemID = _itemID;
                ItemNum = _itemNum;
                ItemSprite = GameDictionary.EnhanceMaterialDic[ItemID].ItemSprite;
                ItemSpriteName = GameDictionary.EnhanceMaterialDic[ItemID].ItemSpriteName;
                ItemName = GameDictionary.EnhanceMaterialDic[ItemID].ItemName;
                NeedGold = _gold;
            }
            catch
            {
                Debug.LogError("從字典裡取得怪獸需求素材時發生錯誤");
            }

        }
    }
}
