using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShowTouchText : IShowText
{
    Transform targetMonster;
    Transform targetText;
    GameObject text;
    Animator textAnimator;
    TextMesh textTextMesh;
    PlayerMonster monsterStatus;
    string touchFeedBackText;
    List<int> feedBackTextIDList;
    float textDirX;
    int rnd;

    public ShowTouchText(Transform _targetMonster)
    {
        targetMonster = _targetMonster;
        //抓取點取的文字母物件        
        targetText = targetMonster.transform.FindChild("EventText").FindChild("EventText");
        textAnimator = targetText.GetComponent<Animator>();
        //抓取顯示的文字子物件
        text = targetText.FindChild("eventText").gameObject;
        textTextMesh = text.GetComponent<TextMesh>();
        monsterStatus = targetMonster.GetComponent<PlayerMonster>();
        feedBackTextIDList = new List<int>();
    }

    public void ShowText()
    {
        feedBackTextIDList.Clear();//清空回話清單
        if (textAnimator.GetCurrentAnimatorStateInfo(0).IsName("stop"))
        {

            //取得該個性擁有的TouchFeedbackID的Dictionary的key值
            Dictionary<int, LoadingScene.TouchFeedBack>.KeyCollection keyColl = LoadingScene.OwnPTouchFeedBack[monsterStatus.Personality].Keys;
            //取得key值並抓取符合的親密度TouchFeedback ID
            foreach (int key in keyColl)
            {
                if (monsterStatus.Happy <= LoadingScene.OwnPTouchFeedBack[monsterStatus.Personality][key].familiarMax
                        && monsterStatus.Happy >= LoadingScene.OwnPTouchFeedBack[monsterStatus.Personality][key].familiarMin)
                {
                    feedBackTextIDList.Add(LoadingScene.OwnPTouchFeedBack[monsterStatus.Personality][key].id);
                }
            }
            if (feedBackTextIDList.Count > 0)//feedBackTextIDList為0，代表沒有符合該個性快樂度區間的對話，則不執行
            {
                //從符合親密度TouchFeedback ID清單中，亂數抓取隨機回答
                UnityEngine.Random.seed = System.Guid.NewGuid().GetHashCode();
                rnd = UnityEngine.Random.Range(0, feedBackTextIDList.Count);
                touchFeedBackText = LoadingScene.OwnPTouchFeedBack[monsterStatus.Personality][feedBackTextIDList[rnd]].feedBackDialogue;
                //設定文字
                textTextMesh.text = WrapWord.GetWarpWord(touchFeedBackText, 20);
                //設定文字動畫
                textAnimator.SetTrigger(LoadingScene.OwnPTouchFeedBack[monsterStatus.Personality][feedBackTextIDList[rnd]].textAni);
                //校正文字方向
                UpdateTextDirection();
            }
        }
    }


    public void UpdateTextDirection()
    {
        /*
        textDirX = targetMonster.localScale.x;
        targetText.localScale = new Vector3(textDirX, 1, 1);
        */
    }


    public void ShowText(int choseCase)
    {
        throw new System.NotImplementedException();
    }

    public void ShowText(int choseCase, int _feedBackType)
    {
        throw new System.NotImplementedException();
    }
}
