using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationController : FortuneWheelElement
{
    private Sequence rotationSequence; // ����� ������������������
    [SerializeField] private RotationCalculations _calculations;

    public RotationCalculations Calculations
    {
        get => _calculations;
        set => _calculations = value;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotationSequence = DOTween.Sequence();
            CollectSequence();
        }
    }

    private void CollectSequence()
// ���������� ������� ��������
    {
        rotationSequence.Append(StartWheel());
        rotationSequence.Append(MiddleWheel());
        rotationSequence.Append(FinalWheel());
    }
    private Tweener StartWheel()
 //    �� ������ �� ������������ ��������
    {
        Tweener startTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -Calculations.RotationToMax()), Game.Model.RotationModel.TimeToMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.InSine);
        return startTween;
    }
    private Tweener MiddleWheel()
 // �������� �� ������������ ��������
    {
        Tweener middleTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -Calculations.RotationAtMax()), Game.Model.RotationModel.TimeAtMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.Linear);
        return middleTween;
    }

    private Sequence FinalWheel()
    // ���������� �� ������� �������
    {
        Game.Controller.WheelController.LaunchWinnerChoice(); // ����� ����������
        Sequence finalSequence = DOTween.Sequence();
        finalSequence.Append(FinalFullRotations());
        finalSequence.Append(FinalExtraRotations());
        finalSequence.SetEase(Ease.OutSine);

        return finalSequence;
    }

    private Tweener FinalFullRotations()
// �������� (������) �������� �� ���������
    {
        Tweener finalFull = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -Calculations.FinalFullRotations()), Calculations.FinalFullTime(), RotateMode.FastBeyond360)
            .SetRelative(true);
        return finalFull;
    }

    private Tweener FinalExtraRotations()
// ������������ �� ����������
    {
        Tweener finalExtra = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -Calculations.RotationToWinner()), Calculations.FinalExtraTime(), RotateMode.FastBeyond360)
            .SetRelative(false);
        return finalExtra;
    }
    
}
