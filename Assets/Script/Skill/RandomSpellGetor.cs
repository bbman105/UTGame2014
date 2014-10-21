using UnityEngine;
using System.Collections;

public abstract class RandomSpellGetor {

    public abstract int GetNumber();//取得機率數字方法
    protected int RandomNum { get; set; }
    protected int SelectSpellRank { get; set; }
}
