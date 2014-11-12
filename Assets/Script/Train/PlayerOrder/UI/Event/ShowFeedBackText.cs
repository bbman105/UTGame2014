using UnityEngine;
using System.Collections;

public class ShowFeedBackText:IShowText
{
    Transform targetBubble;
    Transform targetText;
    Transform text;
    Animator textAnimator;
    TextMesh textTextMesh;
    Transform targetMonster;
    PlayerMonster monsterStatus;
    string feedBackText;
    float textDirX;

    public ShowFeedBackText(Transform _targetBubble)//此方法傳入的target是eventBubble
    {
        targetBubble = _targetBubble;
        //抓取點取的怪物物件
        targetMonster = targetBubble.parent;
        //抓取PlayerMonster Component
        monsterStatus = targetMonster.GetComponent<PlayerMonster>();
        //抓取點取的文字母物件
        targetText = targetBubble.parent.FindChild("EventText");
        textAnimator = targetText.FindChild("EventText").GetComponent<Animator>();
        //抓取顯示的文字子物件
        text = targetText.FindChild("EventText").FindChild("eventText");
        textTextMesh = text.GetComponent<TextMesh>();
    }
    public void ShowText(int choseCase)
    {

        if (textAnimator.GetCurrentAnimatorStateInfo(0).IsName("stop"))
        {
            //根據所選得選項及怪物個性對應到顯示的文字
            feedBackText = LoadingScene.EventFeedBackDic[string.Format("{0}-{1}", monsterStatus.Personality, LoadingScene.CurEventID)].ansDialogue[choseCase];
            //設定文字
            textTextMesh.text = WrapWord.GetWarpWord(feedBackText, 20);
            //設定文字動畫
            textAnimator.SetTrigger(LoadingScene.EventFeedBackDic[string.Format("{0}-{1}", monsterStatus.Personality, LoadingScene.CurEventID)].ansTextAni[choseCase]);
            //校正文字方向
            UpdateTextDirection();
        }
    }

    public void UpdateTextDirection()
    {
        textDirX = targetMonster.localScale.x;
        targetText.localScale = new Vector3(textDirX, 1, 1);
    }


    public void ShowText()
    {
        throw new System.NotImplementedException();
    }

    public void ShowText(int choseCase, int _feedBackType)
    {
        throw new System.NotImplementedException();
    }
}
