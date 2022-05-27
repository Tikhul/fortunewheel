using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheelModel : MonoBehaviour
{
    [SerializeField] private WheelModel _wheelModel;

    public WheelModel WheelModel
    {
        get => _wheelModel;
        set => _wheelModel = value;
    }
}
