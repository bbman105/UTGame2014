using UnityEngine;
using System.Collections;

public class HappyBar : MonoBehaviour
{

    SpriteRenderer happySpriteRenderer;
    SpriteRenderer happyIconSpriteRenderer;
    Sprite[] HappyIconResource;
    Transform myTransform;
    Animator myAnimator;
    SpriteRenderer[] spritRendererArray;
    int LastHappy { get; set; }
    int CurHappy { get; set; }
    int MaxHappy { get; set; }
    int LevelHappy { get; set; }//快樂度等級
    int MonsterNum { get; set; }//怪獸數量

    //固定參數設定
    const float scaleMaxX = 34.4f;
    const float WaitForShowCurHappyTime = 0.8f;
    //const float ShowHappyTime = 2f;
    const int PerMonsterMaxHappy = 100;//一隻怪獸最高的快樂度
    const int SetionHappy = 20;//一層的快樂度
    Color HappyColor = new Color32(0, 255, 0, 255);
    Color JoyColor = new Color32(28, 223, 223, 255);
    Color LittleHappyColor = new Color32(255, 183, 28, 255);
    Color UnHappyColor = new Color32(255, 121, 0, 255);
    Color RageColor = new Color32(246, 61, 61, 255);
    void Awake()//起始設定快樂度介面
    {
        myTransform = transform;
        happySpriteRenderer = myTransform.FindChild("happy").GetComponent<SpriteRenderer>();
        happyIconSpriteRenderer = myTransform.FindChild("happyIcon").GetComponent<SpriteRenderer>();
        HappyIconResource = new Sprite[5];
        for (int i = 0; i < HappyIconResource.Length; i++)//將快樂度ICON圖位置加入到陣列
        {
            HappyIconResource[i] = Resources.Load<Sprite>(string.Format("Sprite/ResourceItem/happyIcon{0}", i));
        }
        spritRendererArray = myTransform.GetComponentsInChildren<SpriteRenderer>();
        myAnimator = myTransform.GetComponent<Animator>();
    }
    public void SetSortingOrder(int _floor)//設定貼圖層級
    {
        for (int i = 0; i < spritRendererArray.Length; i++)
        {
            spritRendererArray[i].sortingOrder += 50 * _floor;
        }
    }
    /// <summary>
    /// 傳入起始快樂度、達到的快樂度、怪獸數量，以展示增加快樂度
    /// </summary>
    public void DisplayAddHappy(int _lastHappy, int _curHappy, int _maxHappy,int _monsterNum)
    {
        LastHappy = _lastHappy;
        CurHappy = _curHappy;
        MaxHappy = _maxHappy;
        MonsterNum = _monsterNum;
        ShowHappy(LastHappy);//顯示快樂值貼圖
        myAnimator.SetTrigger("HappyChange");
        StartCoroutine(WaitForShowCurHappy());
    }
    /// <summary>
    /// 結束拖曳取消顯示快樂度
    /// </summary>
    public void DragEndHideHappy()//結束拖曳取消顯示快樂度
    {
        myAnimator.SetTrigger("ToDefault");
    }
    /// <summary>
    /// 拖曳房間時顯示快樂度，傳入快樂度、怪獸數量
    /// </summary>
    public void DragDisplayHappy(int _happy, int _maxHappy,int _monsterNum)//拖曳時顯示快樂度
    {
        CurHappy = _happy;
        MaxHappy = _maxHappy;
        MonsterNum = _monsterNum;
        ShowHappy(CurHappy);//顯示快樂值貼圖
        myAnimator.SetTrigger("DisplayHappy");
    }
    IEnumerator WaitForShowCurHappy()//等待後顯示正確快樂度，再等待後關閉快樂度介面
    {
        yield return new WaitForSeconds(WaitForShowCurHappyTime);
        ShowHappy(CurHappy);//顯示快樂值貼圖
        //yield return new WaitForSeconds(ShowHappyTime);
    }
    /// <summary>
    /// 顯示快樂值貼圖，傳入要顯示的快樂值
    /// </summary>
    void ShowHappy(int _curHappy)//顯示快樂值貼圖
    {
        //設定快樂值貼圖
        float scalX = scaleMaxX * ((float)_curHappy / (float)MaxHappy);
        happySpriteRenderer.transform.localScale = new Vector3(scalX, 1, 1);
        AdustHappySpriteColor(_curHappy);//設定快樂值對應到的貼圖顏色
    }
    /// <summary>
    /// 傳入快樂值，設定快樂值對應到的貼圖顏色
    /// </summary>
    void AdustHappySpriteColor(int _happy)//設定快樂值對應到的貼圖顏色
    {
        //設定快樂度等級
        for (int i = 1; i < 6; i++)//快樂度共5個階級
        {
            if (_happy <= (SetionHappy * i * MonsterNum))//如果快樂度小於此層快樂度區間，則設定快樂等級
            {
                LevelHappy = i;
                break;
            }
        }
        //依照快樂等級設定貼圖顏色及頭像
        switch (LevelHappy)
        {
            case 1://憤怒
                happyIconSpriteRenderer.sprite = HappyIconResource[0];
                happySpriteRenderer.color = RageColor;
                break;
            case 2://不滿
                happyIconSpriteRenderer.sprite = HappyIconResource[1];
                happySpriteRenderer.color = UnHappyColor;
                break;
            case 3://小確幸
                happyIconSpriteRenderer.sprite = HappyIconResource[2];
                happySpriteRenderer.color = LittleHappyColor;
                break;
            case 4://開心
                happyIconSpriteRenderer.sprite = HappyIconResource[3];
                happySpriteRenderer.color = JoyColor;
                break;
            case 5://幸褔
                happyIconSpriteRenderer.sprite = HappyIconResource[4];
                happySpriteRenderer.color = HappyColor;
                break;
        }
    }
}
