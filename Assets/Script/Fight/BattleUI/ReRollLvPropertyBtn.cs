using UnityEngine;
using System.Collections;

public class ReRollLvPropertyBtn : MonoBehaviour
{
    public PlayerMonster playerMonster;
    public bool CanReroll;
    public int RollBtnID { get; set; }
    UISprite sprites;
    UILabel labels;
    UIButton button;
    public void StartSet()
    {
        button = this.GetComponent<UIButton>();
        sprites = transform.FindChild("reRoll").GetComponent<UISprite>();
        labels = transform.FindChild("reRollText").GetComponent<UILabel>();
        CanReroll = false;
    }
    public void EnableButton(bool _enable)
    {
        if (_enable)
        {
            sprites.color = Color.white;
            labels.color = Color.white;
        }
        else
        {
            sprites.color = Color.gray;
            labels.color = Color.gray;
        }
        button.enabled = _enable;
    }
    void OnClick()
    {
        if (CanReroll)
        {
            playerMonster.ReRollLvProperty();//重骰屬性
            ForceCondition.ReRollChangeLabelValue(RollBtnID, playerMonster);//重骰更新UI數值
        }
    }
}
