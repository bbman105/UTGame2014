using UnityEngine;
using System.Collections;

public class SceneSet
{

    private GameObject FightScene;
    public Sprite[] SceneSprite;
    private SpriteRenderer[] map;//場景地圖sprite
    public SceneSet(int _map)
    {
        FightScene = GameObject.FindGameObjectWithTag("FightScene").gameObject;
        SceneSprite = new Sprite[4];
        map = new SpriteRenderer[4];

        for (int i = 0; i < map.Length; i++)
        {
            map[i] = FightScene.transform.FindChild(string.Format("{0}", i+1)).GetComponent<SpriteRenderer>();
            SceneSprite[i] = Resources.Load<Sprite>(string.Format("Sprite/FightScene/map{0}{1}", _map, i + 1));
            map[i].sprite = SceneSprite[i];
        }
    }
    public void SetScene(int _map)
    {
        for (int i = 0; i < map.Length; i++)
        {
            SceneSprite[i] = Resources.Load<Sprite>(string.Format("Sprite/FightScene/map{0}{1}", _map, i + 1));
            map[i].sprite = SceneSprite[i];
        }
    }
}
