using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheelView : MonoBehaviour
{
    [SerializeField] private StartButtonView _startButtonView;

    public StartButtonView StartButtonView
    {
        get => _startButtonView;
        set => _startButtonView = value;
    }
}
