using UnityEngine;
using System.Collections;

public class ShowRewardText : IShowText
{
    Transform targetBubble;
    Transform targetRewardText;
    Animator[] targetAnimator;
    SpriteRenderer happySpriteRenderer;//快樂的spriteRenderer
    static Sprite happyUpSpriteResource;//快樂度增加圖片位置
    static Sprite happyDownSpriteResource;//快樂度減少圖片位置
    //TextMesh[] targetTextMesh;
    Transform targetMonster;
    PlayerMonster monsterStatus;
    string feedBackText;
    int feedBackPoint;
    int feedBackType;
    //string personality;
    float textDirX;
    /// <summary>
    /// 初始設定獨立事件回饋文字
    /// </summary>
    public static void StartSetShowRewardText()//初始設定獨立事件回饋文字
    {
        happyUpSpriteResource = Resources.Load<Sprite>("Sprite/ResourceItem/happyup");
        happyDownSpriteResource = Resources.Load<Sprite>("Sprite/ResourceItem/happydown");
    }

    public ShowRewardText(Transform _targetBubble)//此方法傳入的target是eventBubble
    {
        targetBubble = _targetBubble;
        //抓取點取的文字母物件
        targetRewardText = targetBubble.parent.FindChild("RewardText");
        happySpriteRenderer = targetBubble.parent.FindChild("RewardText").FindChild("rewardText1").FindChild("eventIcon").GetComponent<SpriteRenderer>();
        targetAnimator = targetRewardText.GetComponentsInChildren<Animator>();
        //targetTextMesh = targetRewardText.GetComponentsInChildren<TextMesh>();
        //抓取點取的文字母物件
        //抓取點取的怪物物件
        targetMonster = targetBubble.parent;
        //抓取PlayerMonster Component
        monsterStatus = targetMonster.GetComponent<PlayerMonster>();
    }

    public void ShowText(int choseCase, int _feedBackType)
    {
        //personality = "";
        feedBackPoint = TrainScene.EventFeedBackDic[string.Format("{0}-{1}", monsterStatus.Personality, TrainScene.CurEventID)].ansFeedback[choseCase][_feedBackType];
        switch (_feedBackType)
        {
            case 0://增加快樂度
                monsterStatus.AddHappy(feedBackPoint);
                break;
            case 1://增加金幣
                Player.OwnResource.AddResource(0, feedBackPoint);
                break;
            case 2://增加能量
                Player.OwnResource.AddResource(1, feedBackPoint);
                break;
            default :
                Debug.Log("獨立事件回饋要增加的屬性類型無法辨識");
                break;
        }
        /*
        if (TrainScene.EventFeedBackDic[string.Format("{0}-{1}", monsterStatus.Personality, TrainScene.CurEventID)].ansFeedback[choseCase][_feedBackType] > 0)
        {
            personality += " + ";
        }
        feedBackText = personality + feedBackPoint.ToString();
        */
        monsterStatus.GetEvolutionPoint(_feedBackType, feedBackPoint);
        //選取對應到的種類展示文字
        if (targetAnimator[_feedBackType].GetCurrentAnimatorStateInfo(0).IsName("stop"))
        {
            //設定文字
            //targetTextMesh[_feedBackType].text = feedBackText;
            if(_feedBackType==0)//如果是增加或減少快樂度，判斷快樂度圖像
            {
                if(feedBackPoint>0)
                {
                    happySpriteRenderer.sprite = happyUpSpriteResource;
                }
                else
                {
                    happySpriteRenderer.sprite = happyDownSpriteResource;
                }
            }
            //設定文字動畫
            targetAnimator[_feedBackType].SetTrigger(string.Format("reward{0}", _feedBackType));
        }
    }

    public void UpdateTextDirection()
    {
        textDirX = targetMonster.localScale.x;
        targetRewardText.localScale = new Vector3(textDirX, 1, 1);
    }

    public void ShowText()
    {
        throw new System.NotImplementedException();
    }

    public void ShowText(int choseCase)
    {
        throw new System.NotImplementedException();
    }



}
