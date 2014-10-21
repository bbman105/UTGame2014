using UnityEngine;
using System.Collections;

public class SpellGetor
{
    RandomSpellGetor GetRandomNumType;
    public void SetSpellGetor(RandomSpellGetor _type)
    {
        this.GetRandomNumType = _type;
    }

    public int GetSpellRank()//照機率取得第幾項技能
    {
        int SelectedSpellRank = GetRandomNumType.GetNumber();
        return SelectedSpellRank;
    }
}
