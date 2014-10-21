using UnityEngine;
using System.Collections;

public class DetailView : MonoBehaviour
{
    private bool[] viewDetail;
    public int curSkillCard;
    public void StartSet()
    {
        viewDetail = new bool[3] { false, false, false };
    }
    void OnClick()
    {
        if (!viewDetail[curSkillCard])
        {
            viewDetail[curSkillCard] = true;
            //EditMemberData.skillDetail[curSkillCard].SetActive(true);
            //EditMemberData.skillSummary[curSkillCard].SetActive(false);
        }
        else
        {
            viewDetail[curSkillCard] = false;
            //EditMemberData.skillDetail[curSkillCard].SetActive(false);
            //EditMemberData.skillSummary[curSkillCard].SetActive(true);
        }
    }

}
