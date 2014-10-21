using UnityEngine;
using System.Collections;

public class ViewSkill : MonoBehaviour
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
            EditMemberData.Skill[i].SetActive(true);
        }
        */
        SetGray();
        ViewData.SetWhite();
        EditMemberData.DataUI.SetActive(false);
        EditMemberData.SkillUI.SetActive(true);
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
