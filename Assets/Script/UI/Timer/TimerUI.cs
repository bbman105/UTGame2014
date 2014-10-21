using UnityEngine;
using System.Collections;

public class TimerUI : MonoBehaviour {

    public GameObject timerLabel;
    static int timer;
    static bool startTimer;
	// Use this for initialization
    public static void StartTimer()
    {
        timer = 0;
        startTimer = true;
    }
    void Update()
    {
        if(startTimer)
        {
            StartCoroutine("TimerFunction");
            startTimer = false;
        }
    }
    
    IEnumerator TimerFunction()
    {
        int minutes = 0;
        int seconds = 0;
        UILabel lbl = timerLabel.GetComponent<UILabel>();
        while (!ForceCondition.CallEnd)
        {
            yield return new WaitForSeconds(1);
            timer++;
            minutes = Mathf.FloorToInt(timer / 60f);
            seconds = Mathf.FloorToInt(timer - minutes * 60);            
            lbl.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
        }
        ForceCondition.SpentTime = timer;
    }


    /*
    void TimerFunction2()
    {
        timer++;
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        UILabel lbl = timerLabel.GetComponent<UILabel>();
        lbl.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }
	*/

	// Update is called once per frame

}
