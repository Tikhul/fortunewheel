using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class StartButtonView : FortuneWheelElement
{
    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(LaunchWheel);
    }

    private void LaunchWheel()
    {
        GetComponent<Button>().enabled = false;
        Sequence rotationSequence = DOTween.Sequence();
        Game.Controller.RotationController.CollectSequence(rotationSequence);
        rotationSequence.OnComplete(ActivateButton);
    }

    public void ActivateButton()
    {
        GetComponent<Button>().enabled = true;
    }
}
