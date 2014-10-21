using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetNpcMonsterSpellRank : RandomSpellGetor,IViewDebugLog
{
    private List<SpellAI> SpellAIList;
    private int TotalWeight { get; set; }
    private List<int> WeightCaseList;
    private int CurTotalWeight { get; set; }
    private List<int> ActiveSpellList { get; set; }
    private AI ai;

    public void SetNpcMonsterSpellRank(AI _ai)
    {
        ai = _ai;
        this.SpellAIList = ai.SpellAIList;
        WeightCaseList = new List<int>();
        ActiveSpellList = new List<int>();
        SetTotalWeight();
    }
    private void SetTotalWeight()
    {
        WeightCaseList.Clear();
        TotalWeight = 0;
        SelectSpellRank = 0;
        for (int i = 0; i < SpellAIList.Count; i++)
        {
            if (!SpellAIList[i].OnCD && SpellAIList[i].Active)
            {
                WeightCaseList.Add(SpellAIList[i].Weight);
                TotalWeight += SpellAIList[i].Weight;
                ActiveSpellList.Add(i);
            } 
        }
        TotalWeight += 1;
    }


    //依權重亂數抽出Npc怪物的第幾排序技能
    public override int GetNumber()
    {
        RandomNum = Random.Range(1, TotalWeight);
        CurTotalWeight = TotalWeight;
        CurTotalWeight = RandomNum;
        int curRank = 0;
        if (WeightCaseList.Count!=0)
        {
            while ((CurTotalWeight > WeightCaseList[curRank]))
            {
                CurTotalWeight -= WeightCaseList[curRank];
                curRank++;
                if (CurTotalWeight <= WeightCaseList[curRank])
                {
                    SelectSpellRank = curRank;
                }
            }
            ///////因為有技能在CD中，所以選出來的技能排序要做調整，也就是說選出排序為1時，實際上是挑，可施放技能中排序為1的技能
            SelectSpellRank = ActiveSpellList[SelectSpellRank];
            //ViewDebugLog();//查看Log
        }
        else
        {
            Debug.LogWarning(string.Format("AI編號{0}無可施放的技能", ai.AiID));
        }

        return SelectSpellRank;
    }

    public void ViewDebugLog()
    {
        Debug.Log(SpellAIList[SelectSpellRank].SpellID);
    }
}
