using UnityEngine;
using System.Collections;

public class SpeciesModel: IMonsterSize
{
    private string species;
    private SpriteRenderer spriteRenderer;
    private Sprite speciesSprite;
    /// <summary>
    /// 設定Size
    /// </summary>
    private float size;
    float spritePosX;
    float spritePosY;
    float spriteScale;
    //反向
    float reverseFace = -1;
    BoxCollider2D boxCollider2D;
    CircleCollider2D circleCollider2D;
    float circleColliderCenterX;
    float circleColliderCenterY;
    float circleColliderRadius;
    float boxColliderCenterX;
    float boxColliderCenterY;
    float boxColliderSizeX;
    float boxColliderSizeY;

    public SpeciesModel(Transform _transform, SpriteRenderer _spriteRenderer,string _speciesKey,float _size)
    {
        spriteRenderer = _spriteRenderer;
        species = _speciesKey;
        size = _size;
        boxCollider2D = _transform.GetComponent<BoxCollider2D>();
        circleCollider2D = _transform.GetComponent<CircleCollider2D>();
    }
    public void SetReverseFace(bool _reverse)//設定怪物面向如果傳入true，怪獸面相背後，怪獸面向正面
    {
        if (_reverse)
        {
            if(spriteRenderer.transform.localScale.x>0)
            {
                spriteRenderer.transform.localScale = new Vector3(spriteRenderer.transform.localScale.x * reverseFace, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);   
            }
        }
        else
        {
            if (spriteRenderer.transform.localScale.x < 0)
            {
                spriteRenderer.transform.localScale = new Vector3(spriteRenderer.transform.localScale.x * reverseFace, spriteRenderer.transform.localScale.y, spriteRenderer.transform.localScale.z);
            }
        }         
    }

    public void SetSpeciesModel()
    {
        speciesSprite = Resources.Load<Sprite>(string.Format("Sprite/MonsterSprite/{0}", species));
        spriteRenderer.sprite = speciesSprite;
        SetSize(size);
    }
    public void SetSelectedColor(bool _selected) //設定怪物顏色，selected=true時為白色，false時為灰色
    {
        if(_selected)
        {
            spriteRenderer.color = Color.white;
        }
        else
        {
            spriteRenderer.color = Color.gray;
        }
    }
    public void SetSpriteSortingLayer(string _layer)
    {
        try
        {
            spriteRenderer.sortingLayerName = _layer;
        }
        catch
        {
            Debug.Log("指定不存在的sortingLayer");
        }
    }
    public void SetSize(float _size)
    {
        spritePosX = 0;
        spritePosY = -0.2f + (0.8f * ((_size - 0.5f) / 1.2f));
        spriteScale = _size;
        spriteRenderer.transform.localPosition = new Vector3(spritePosX, spritePosY, 0); //設定圖像位置
        spriteRenderer.transform.localScale = new Vector3(spriteScale, spriteScale, 1);//設定圖像大小

        circleColliderCenterX = 0;
        circleColliderCenterY = -0.4f + (0.4f * ((_size - 0.5f) / 1.2f));
        circleColliderRadius = 0.25f + (0.45f * ((_size - 0.5f) / 1.2f));
        boxColliderCenterX = 0;
        boxColliderCenterY = -0.25f + (0.55f * ((_size - 0.5f) / 1.2f));
        boxColliderSizeX = 0.5f + (0.9f * ((_size - 0.5f) / 1.2f));
        boxColliderSizeY = 0.3f + (0.7f * ((_size - 0.5f) / 1.2f));
        //設定circleCollider碰撞大小及位置
        circleCollider2D.center = new Vector2(circleColliderCenterX, circleColliderCenterY);
        circleCollider2D.radius = circleColliderRadius;
        //設定bpxCollider碰撞大小及位置
        boxCollider2D.center = new Vector2(boxColliderCenterX, boxColliderCenterY);
        boxCollider2D.size = new Vector2(boxColliderSizeX, boxColliderSizeY);
    }
}
