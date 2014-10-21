using UnityEngine;
using System.Collections;

public class textMeshDepth : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        this.renderer.sortingLayerName = "UI";
        this.renderer.sortingOrder = 200;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
