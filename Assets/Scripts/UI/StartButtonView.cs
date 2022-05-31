using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartButtonView : FortuneWheelElement
{
    private void Awake()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(LaunchWheel);
    }

    private void LaunchWheel()
    {
        Sequence rotationSequence = DOTween.Sequence();
        Game.Controller.RotationController.CollectSequence(rotationSequence);
    }
}
