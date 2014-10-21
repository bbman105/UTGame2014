using UnityEngine;
using System.Collections;

public class ElementHitCalculator : Calculator
{


    int previousElement;
    int nextElement;
    int targetElement;
    int result;
    void Reset()
    {
        previousElement = 0;

    }
    public int GetElementHit(int _selfElement, int _targetElement)
    {
        targetElement = _targetElement;
        //抓取自身元素的上一個元素
        if (_selfElement - 1 < 0) { previousElement = 2; }
        else { previousElement = _selfElement - 1; }
        //抓取自身元素的下一個元素
        if (_selfElement + 1 > 2) { nextElement = 0; }
        else { nextElement = _selfElement + 1; }
        SetResult();
        return result;
    }
    private void SetResult()//判斷相剋結果
    {
        //判斷輸贏
        if (targetElement == previousElement)//輸
        {
            result = 2;
        }
        else if (targetElement == nextElement)//贏
        {
            result = 1;
        }
        else//平手
        {
            result = 0;
        }
    }
}
