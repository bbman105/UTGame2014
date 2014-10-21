using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class AI  {

    int aiID;
    public int AiID
    {
        get { return aiID; }
        set { aiID = value; }
    }
    protected Status status;
    public List<SpellAI> SpellAIList { get; set; }

    public AI(int _aiID,Status _status)
    {
        this.AiID = _aiID;
        this.status = _status;
    }
}
