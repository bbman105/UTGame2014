using UnityEngine;
using System.Collections;

public class ViewSkill : MonoBehaviour
{
    Transform myTransform;
    public static UISprite[] sprites;
    public void StartSet()
    {
        myTransform = transform;
        sprites = new UISprite[2];
        sprites[0] = myTransform.FindChild("skillIcon").GetComponent<UISprite>();
        sprites[1] = myTransform.FindChild("btn").GetComponent<UISprite>();
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
