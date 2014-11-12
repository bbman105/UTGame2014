using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public partial class EventLoad
{

    static private int curEventID;


    private static void LoadDialogue()
    {
        curEventID = LoadingScene.CurEventID;
        dialogue.text = LoadingScene.EventDic[curEventID].dialogue;
        dialogueIcon.spriteName = LoadingScene.EventDic[curEventID].dialogueIcon;
        dialogueIcon.MakePixelPerfect();
        choice1.text = LoadingScene.EventDic[curEventID].ans1;
        choice2.text = LoadingScene.EventDic[curEventID].ans2;
        choice3.text = LoadingScene.EventDic[curEventID].ans3;
        ans1Icon.spriteName = LoadingScene.EventDic[curEventID].ans1Icon;
        ans1Icon.MakePixelPerfect();
        ans2Icon.spriteName = LoadingScene.EventDic[curEventID].ans2Icon;
        ans2Icon.MakePixelPerfect();
        ans3Icon.spriteName = LoadingScene.EventDic[curEventID].ans3Icon;

        ans3Icon.MakePixelPerfect();
        //typewrite.enabled = true;
        //typewrite.type = true;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
