using UnityEngine;
using System.Collections;

public class ShowDebug : MonoBehaviour
{

    float deltaTime = 0;
    float fps = 0;
    bool show = false;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        deltaTime += Time.deltaTime;
        deltaTime /= 2.0f;
        fps = (int)(1.0f / deltaTime);
        if (!show)
        {
            StartCoroutine("ShowFPS");
            show = true;
        } 
        
	}
    IEnumerator ShowFPS()
    {
        yield return new WaitForSeconds(0.5f);
        transform.GetComponent<UILabel>().text = "FPS:" + fps.ToString();
        show = false; ;
    }

}
