using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheelModel : MonoBehaviour
{
    [SerializeField] private WheelModel _wheelModel;
    [SerializeField] private RotationModel _rotationModel;

    public WheelModel WheelModel
    {
        get => _wheelModel;
        set => _wheelModel = value;
    }
    public RotationModel RotationModel
    {
        get => _rotationModel;
        set => _rotationModel = value;
    }
}
