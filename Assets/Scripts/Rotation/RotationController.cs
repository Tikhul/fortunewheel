using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class RotationController : FortuneWheelElement
{
    private Sequence rotationSequence; // Общая последовательность

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rotationSequence = DOTween.Sequence();
            CollectSequence();
        }
    }

    private void CollectSequence()
// Заполнение очереди анимаций
    {
        rotationSequence.Append(StartWheel());
        rotationSequence.Append(MiddleWheel());
        rotationSequence.Append(FinalWheel());
    }
    private Tweener StartWheel()
 //    От старта до максимальной скорости
    {
        float acceleration = Game.Model.RotationModel.MaxSpeed / Game.Model.RotationModel.TimeToMax; // Расчет ускорения
        float totalRotation = (acceleration * Game.Model.RotationModel.TimeToMax * Game.Model.RotationModel.TimeToMax) / 2; // Расчет градусов поворота

        Tweener startTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -totalRotation), Game.Model.RotationModel.TimeToMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.InSine);
        return startTween;
    }
    private Tweener MiddleWheel()
 // Вращение на максимальной скорости
    {
        float totalRotation = Game.Model.RotationModel.MaxSpeed * Game.Model.RotationModel.TimeAtMax; // Сколько градусов должен пройти

        Tweener middleTween = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -totalRotation), Game.Model.RotationModel.TimeAtMax, RotateMode.FastBeyond360)
            .SetRelative(true)
            .SetEase(Ease.Linear);
        return middleTween;
    }

    private Sequence FinalWheel()
    // Замедление до нужного сектора
    {
        Game.Controller.WheelController.LaunchWinnerChoice(); // Выбор победителя
        Sequence finalSequence = DOTween.Sequence();
        finalSequence.Append(FinalFullRotations());
        finalSequence.Append(FinalExtraRotations());
        finalSequence.SetEase(Ease.OutSine);
        return finalSequence;
    }

    private Tweener FinalFullRotations()
    {
        float deceleration = Game.Model.RotationModel.MaxSpeed / Game.Model.RotationModel.TimeAfterMax; // Расчет замедления
        float allRotation = (float)Math.Floor((deceleration * Game.Model.RotationModel.TimeAfterMax * Game.Model.RotationModel.TimeAfterMax) / 2); // Расчет общих градусов поворота
        float totalRotation = allRotation - (allRotation % 360); // Расчеты градусов поворота на 360

        Tweener finalFull = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -totalRotation), Game.Model.RotationModel.TimeAfterMax, RotateMode.FastBeyond360)
            .SetRelative(true);
        return finalFull;
    }

    private Tweener FinalExtraRotations()
    {
        float totalRotation = RotationToWinner();
        Debug.Log("FinalExtraRotations" + totalRotation);
        Tweener finalExtra = Game.Model.WheelModel.SectorsParent.transform.DORotate(
            new Vector3(0, 0, -totalRotation), Game.Model.RotationModel.TimeAfterMax, RotateMode.FastBeyond360)
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
