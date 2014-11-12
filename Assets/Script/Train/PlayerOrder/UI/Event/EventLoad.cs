using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;

public partial class EventLoad : MonoBehaviour
{
    public static bool OnEvent { get; set; }
    static private UILabel charaName;
    static private UISprite charaSprite;
    static private UILabel dialogue;
    static private UILabel choice1;
    static private UILabel choice2;
    static private UILabel choice3;
    static private UISprite ans1Icon;
    static private UISprite ans2Icon;
    static private UISprite ans3Icon;
    static private UISprite dialogueIcon;


    public static void LoadValues()
    {
        PlayerMonster m = Player.MonsterDic[Player.curMonsterID];
        SetName(m.MonsterName);
        LoadMonsterSprite(charaSprite, m.Species, m.SpeciesLevel);
        LoadDialogue();
    }

    static void SetName(string _name)
    {
        charaName.text = _name;
    }
    public static void LoadMonsterSprite(UISprite _sprite, int _species, int _speciesLevel)
    {
        _sprite.atlas = Resources.Load<UIAtlas>(string.Format("Atlas/Character/{0}", _species));
        _sprite.spriteName = _speciesLevel.ToString();
        _sprite.MakePixelPerfect();
    }
    public void StartSetting()
    {
        OnEvent = false;
        charaName = transform.FindChild("name").GetComponent<UILabel>();
        charaSprite = transform.FindChild("character").FindChild("character").GetComponent<UISprite>();
        dialogue = transform.FindChild("dialogue").GetComponent<UILabel>();
        dialogueIcon = transform.FindChild("dialogueIcon").GetComponent<UISprite>();
        choice1 = transform.FindChild("choice1").FindChild("choice1").GetComponent<UILabel>();
        choice2 = transform.FindChild("choice2").FindChild("choice2").GetComponent<UILabel>();
        choice3 = transform.FindChild("choice3").FindChild("choice3").GetComponent<UILabel>();
        ans1Icon = transform.FindChild("Ans1Icon").GetComponent<UISprite>();
        ans2Icon = transform.FindChild("Ans2Icon").GetComponent<UISprite>();
        ans3Icon = transform.FindChild("Ans3Icon").GetComponent<UISprite>();
    }




}
