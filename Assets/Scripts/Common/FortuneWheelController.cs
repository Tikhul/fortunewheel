using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheelController : MonoBehaviour
{
    [SerializeField] private WheelController _wheelController;
    public WheelController WheelController
    {
        get => _wheelController;
        set => _wheelController = value;
    }
}
