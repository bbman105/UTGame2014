using UnityEngine;
using System.Collections;

public class DominantSizeCalculator : Calculator
{
    int selfSizeType;
    int targetSizeType;
    int result;
    public int GetDominantSizeCalculator(float _selfSize, float _targetSize)
    {
        selfSizeType = GetSizeType(_selfSize);
        targetSizeType = GetSizeType(_targetSize);
        result = selfSizeType - targetSizeType;
        return result;
    }
    private int GetSizeType(float _size)
    {
        int sizeType = 0;
        if (_size <=0.9 )
        {
            sizeType = 0;
        }
        else if (_size > 0.9f && _size < 1.3f)
        {
            sizeType = 1;
        }
        else
        {
            sizeType = 2;
        }
        return sizeType;
    }
}
