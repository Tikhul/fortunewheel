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

    private float time1 = 0;
    private float time2 = 0;
    private Tweener FinalFullRotations()
// �������� (������) �������� �� ���������
    {
        float deceleration = Game.Model.RotationModel.MaxSpeed / Game.Model.RotationModel.TimeAfterMax; // ������ ����������
        float allRotation = (float)Math.Floor((deceleration * Game.Model.RotationModel.TimeAfterMax * Game.Model.RotationModel.TimeAfterMax) / 2); // ������ ����� �������� ��������
        float totalRotation = allRotation - (allRotation % 360); // ������� �������� �������� �� 360
        time1 = (float)(Game.Model.RotationModel.TimeAfterMax - Math.Sqrt(2 * allRotation / deceleration));

        Tweener finalFull = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -totalRotation), time1, RotateMode.FastBeyond360)
            .SetRelative(true);
        return finalFull;
    }

    private Tweener FinalExtraRotations()
// ������������ �� ����������
    {
        float totalRotation = RotationToWinner();
        time2 = Game.Model.RotationModel.TimeAfterMax - time1;

        Tweener finalExtra = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -totalRotation), time2, RotateMode.FastBeyond360)
            .SetRelative(false);
        return finalExtra;
    }
    private float RotationToWinner()
    {
        List<double> degrees = Game.Model.WheelModel.SectorsInfo[Game.Model.WheelModel.WinnerId];
        float maxDegree = (float)(degrees[0] - 180);
        float minDegree = (float)(degrees[1] - 180);
        return UnityEngine.Random.Range(minDegree, maxDegree);
    }
}
