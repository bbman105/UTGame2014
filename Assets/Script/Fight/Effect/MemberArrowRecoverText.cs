using UnityEngine;
using System.Collections;

public class MemberArrowRecoverText : MonoBehaviour {

    SpriteRenderer[] NumSprites;
    void Awake()
    {
        NumSprites = transform.FindChild("Number").GetComponentsInChildren<SpriteRenderer>();
    }

    public void HideSprites()
    {
        for (int i = 0; i < NumSprites.Length; i++)
        {
            NumSprites[i].enabled = false;
        }
    }
    public void ShowText(int _column)
    {
        for (int i = 0; i < NumSprites.Length; i++)
        {
            if (i < _column)
                NumSprites[i].enabled = true;
            else
                NumSprites[i].enabled = false;
        }
    }
}
