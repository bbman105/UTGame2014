using UnityEngine;
using System.Collections;

public class ChapLoad : MonoBehaviour
{

    GameObject chap;
    public bool Show { get; set; }
    public void EnableFunction()
    {
        if (Show)
        {
            chap.SetActive(true);
        }
        else
            chap.SetActive(false);
    }
    public void UnableFunction()
    {
        chap.SetActive(false);
    }


    // Use this for initialization
    public void StartSet()
    {
        Show = false;
        chap = transform.FindChild("Chap").gameObject;
        chap = transform.FindChild("Chap").gameObject;
    }


}
