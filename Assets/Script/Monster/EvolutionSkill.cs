using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EvolutionSkill
{
    public Dictionary<int,int> EvoSkill { get; set; }
    public EvolutionSkill()
    {
        EvoSkill = new Dictionary<int, int>();
    }
}
