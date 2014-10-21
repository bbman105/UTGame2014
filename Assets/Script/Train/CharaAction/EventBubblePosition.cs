using UnityEngine;
using System.Collections;

public class EventBubblePosition : MonoBehaviour {
    float dir;
    float pos;


    public void AdjustEventBubblePosition(int faceDir)
    {
        dir = faceDir * 1;
        pos = faceDir * -1;
        transform.localScale = new Vector3(dir, 1, 1);
        transform.localPosition = new Vector3(pos, 1, 1);
    }
}
