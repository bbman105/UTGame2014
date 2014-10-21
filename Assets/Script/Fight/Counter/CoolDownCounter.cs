using UnityEngine;
using System.Collections;

public abstract class CoolDownCounter
{

    float cdTime;
    public float CdTime
    {
        get { return cdTime; }
        set { cdTime = value; }
    }

    bool onCD;
    public bool OnCD
    {
        get { return onCD; }
        set { onCD = value; }
    }

    float cdTimer;
    public float CdTimer
    {
        get { return cdTimer; }
        set { cdTimer = value; }
    }
    public abstract void StartCD();
    public abstract void OnCDFunction();
    protected abstract void CDDone();
}
