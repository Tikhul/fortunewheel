using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationController : FortuneWheelElement
{
    private Sequence rotationSequence; // ����� ������������������

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
        float acceleration = Game.Model.RotationModel.MaxSpeed / Game.Model.RotationModel.TimeToMax; // ������ ���������
        float totalRotation = (acceleration * Game.Model.RotationModel.TimeToMax * Game.Model.RotationModel.TimeToMax) / 2; // ������ �������� ��������

        Tweener startTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -totalRotation), Game.Model.RotationModel.TimeToMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.InSine);
        return startTween;
    }
    private Tweener MiddleWheel()
 // �������� �� ������������ ��������
    {
        float totalRotation = Game.Model.RotationModel.MaxSpeed * Game.Model.RotationModel.TimeAtMax; // ������� �������� ������ ������

        Tweener middleTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -totalRotation), Game.Model.RotationModel.TimeAtMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.Linear);
        return middleTween;
    }

    private Tweener FinalWheel()
    // ���������� �� ������� �������
    {
        Game.Controller.WheelController.LaunchWinnerChoice(); // ����� ����������
        float deceleration = Game.Model.RotationModel.MaxSpeed / Game.Model.RotationModel.TimeAfterMax; // ������ ����������
        float fullRotation = (float)Math.Floor((deceleration * Game.Model.RotationModel.TimeAfterMax * Game.Model.RotationModel.TimeAfterMax) / 2 / 360); // ������ �������� �������� �� 360
        float totalRotation = fullRotation + RotationToWinner(); // ������� ������� �� ����������

        Tweener finalTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -totalRotation), Game.Model.RotationModel.TimeAfterMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.OutSine);
        return finalTween;
    }

    private float RotationToWinner()
    {
        List<double> degrees = Game.Model.WheelModel.SectorsInfo[Game.Model.WheelModel.WinnerId];
        float minDegree = (float)(degrees[0] - (degrees[0] / 2 * 0.9));
        float maxDegree = (float)(degrees[1] + (degrees[1] / 2 * 0.9));
        return UnityEngine.Random.Range(minDegree, maxDegree);
    }
}
