using UnityEngine;
using System.Collections;

public class SizeReduceSpeedCalculator : Calculator
{
    int ReduceSpeed;
    public int GetReduceSize(float _size)
    {
        ReduceSpeed = 0;
        if (_size > 0.9f && _size < 1.3f)
        {
            ReduceSpeed = 2;
        }
        else if (_size >= 1.3f)
        {
            ReduceSpeed = 5;
        }
        return ReduceSpeed;
    }
}
