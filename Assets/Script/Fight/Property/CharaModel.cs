using UnityEngine;
using System.Collections;

public partial class Status : MonoBehaviour
{
    public SpeciesModel SpeciesModeler;
    public SpriteRenderer CharaSpriteRenderer;//怪獸圖像
    private static int SortingRank;//腳色圖層排序階級
    public void StartSetModeler()
    {
        CharaSpriteRenderer = myTransform.FindChild("Chara").FindChild("sprite").GetComponent<SpriteRenderer>();//抓取怪獸圖像位置
        UpdateCharaSorting();//更新腳色圖曾階級
        SpeciesModeler = new SpeciesModel(myTransform, CharaSpriteRenderer, SpeciesKey, Size);
    }
    public void UpdateCharaSorting()//更新腳色塗層階級
    {
        CharaSpriteRenderer.sortingOrder = SortingRank;//設定圖層階級為sortingRank，sortingRank會在腳色受到傷害時更新
    }
}
