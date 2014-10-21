using UnityEngine;
using System.Collections;

public class HealthUI : MonoBehaviour
{
    public GameObject[] healthBar = new GameObject[2];
    UIWidget[] widget;
    float sumCurHealthRight;
    float sumCurHealthLeft;
    float sumHealthRight;
    float sumHealthLeft;
    float barBaseLength;
    float barFullLength;
    void Start()
    {
        sumCurHealthRight = 0;
        sumCurHealthLeft = 0;
        sumHealthRight = 0;
        sumHealthLeft = 0;
        barBaseLength = 70;
        barFullLength = 200;
        widget = new UIWidget[2];
        widget[0] = healthBar[0].GetComponent<UIWidget>();
        widget[1] = healthBar[1].GetComponent<UIWidget>();
    }

    void Update()
    {
        //RightCharacter
        GameObject[] goLeft = GameObject.FindGameObjectsWithTag("PlayerMonster");
        GameObject[] goRight = GameObject.FindGameObjectsWithTag("NPCMonster");
        Status[] statusL = new Status[goLeft.Length];
        Status[] statusR = new Status[goRight.Length];
        sumCurHealthRight = 0;
        sumCurHealthLeft = 0;
        sumHealthRight = 0;
        sumHealthLeft = 0;


        for (int i = 0; i < goLeft.Length; i++)
        {
            statusL[i] = (Status)goLeft[i].GetComponent("Status");
            sumCurHealthLeft += statusL[i].CurHealth;
            sumHealthLeft += statusL[i].MaxHealth;

        }
        for (int i = 0; i < goRight.Length; i++)
        {
            statusR[i] = (Status)goRight[i].GetComponent("Status");
            sumCurHealthRight += statusR[i].CurHealth;
            sumHealthRight += statusR[i].MaxHealth;
        }


        widget[0].width = (int)(((sumCurHealthLeft / sumHealthLeft) * barFullLength) + barBaseLength);
        widget[1].width = (int)(((sumCurHealthRight / sumHealthRight) * barFullLength) + barBaseLength);
    }
}
