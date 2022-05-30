using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheelController : MonoBehaviour
{
    [SerializeField] private WheelController _wheelController;
    [SerializeField] private SectorController _sectorController;
    [SerializeField] private RotationController _rotationController;
    public WheelController WheelController
    {
        get => _wheelController;
        set => _wheelController = value;
    }

    public SectorController SectorController
    {
        get => _sectorController;
        set => _sectorController = value;
    }
    public RotationController RotationController
    {
        get => _rotationController;
        set => _rotationController = value;
    }
}
