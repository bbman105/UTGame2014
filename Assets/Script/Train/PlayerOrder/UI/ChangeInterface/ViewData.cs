using UnityEngine;
using System.Collections;

public class ViewData : MonoBehaviour
{
    Transform myTransform;
    public static UISprite[] sprites;
    void Awake()
    {
        myTransform = transform;
        sprites = myTransform.GetComponentsInChildren<UISprite>();
    }
    void OnClick()
    {
        /*舊技能系統
        for (int i = 0; i < EditMemberData.Skill.Length; i++)
        {
            EditMemberData.Skill[i].SetActive(false);
        }
        */
        SetGray();
        ViewSkill.SetWhite();
        EditMemberData.DataUI.SetActive(true);
        EditMemberData.SkillUI.SetActive(false);
    }
    public static void SetGray()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = Color.gray;
        }
    }
    public static void SetWhite()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = Color.white;
        }
    }
}
