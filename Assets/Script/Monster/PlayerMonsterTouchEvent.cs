using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public partial class PlayerMonster : Monster
{
    IShowText showTouchText;
    public List<IShowText> ShowTextList { get; set; }
    float showRewardTime;


    void StartSetTouch()
    {
        showTouchText = new ShowTouchText(transform);
        ShowTextList.Add(showTouchText);
    }

    public void ShowTouchText()
    {
        showTouchText.ShowText();
    }

}
