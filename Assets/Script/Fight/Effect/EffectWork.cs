using UnityEngine;
using System.Collections;

public class EffectWork : MonoBehaviour
{
    public bool IsUsing;
    private bool onInstantiate;
    public void NotUsing()
    {
        IsUsing = false;

        if (!onInstantiate)
        {
            IsUsing = false;
        }
        else
        {
            IsUsing = true;
            onInstantiate = false;
        }
    }
    public void InstantiateUsing()
    {
        onInstantiate = true;
    }
}
