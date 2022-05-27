using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorView : FortuneWheelElement
{
    private double _zRotation;
    private double _fillAmount;
    public double ZRotation
    {
        get
        {
            _zRotation = Math.Round(_zRotation, 3);
            return _zRotation;
        }
        set => _zRotation = value;
    }

    public double FillAmount
    {
        get
        {
            _fillAmount = Math.Round(_fillAmount, 3);
            return _fillAmount;
        }
        set => _fillAmount = value;
    }

    public void SetRotation(double zRotation)
    {
        transform.Rotate(0, 0, (float)zRotation);
    }
}
