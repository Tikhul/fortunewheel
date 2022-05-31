using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationCalculations : FortuneWheelElement
{
    public float Acceleration()
// ������ ��������� �� ���� �� ������������ ��������
    {
        return Game.Model.RotationModel.MaxSpeed / Game.Model.RotationModel.TimeToMax;
    }
    
    public float RotationToMax()
// ������ �������� �������� �� ���� �� ������������ ��������
    {
        return (Acceleration() * Game.Model.RotationModel.TimeToMax * Game.Model.RotationModel.TimeToMax) / 2;
    }
    
    public float RotationAtMax()
 // ������ �������� �������� �� ������������ ��������
    {
        return Game.Model.RotationModel.MaxSpeed * Game.Model.RotationModel.TimeAtMax;
    }

    public float Deceleration()
 // ������ ���������� �� ������������ �������� �� ����
    {
        return Game.Model.RotationModel.MaxSpeed / Game.Model.RotationModel.TimeAfterMax;
    }
    
    public float TotalRotationToStop()
 // ������ ����� �������� �������� �� ���������
    {
        return (float)Math.Floor((Deceleration() * Game.Model.RotationModel.TimeAfterMax * Game.Model.RotationModel.TimeAfterMax) / 2);
    }
    // ������ �������� �������� �� 360 ����� ����������

    public float FinalFullRotations()
    {
        return TotalRotationToStop() - (TotalRotationToStop() % 360);
    }

    
    public float FinalFullTime()
 // ������ ������� �������� �� 360 ����� ����������
    {
        return (float)(Game.Model.RotationModel.TimeAfterMax - Math.Sqrt(2 * TotalRotationToStop() / Deceleration()));
    }

    public float FinalExtraTime()
// ������ ������� ��������, ����������� �� ����������
    {
        return Game.Model.RotationModel.TimeAfterMax - FinalFullTime();
    }
    public float RotationToWinner()
// ������ ��������, ����������� �� ����������
    {
        List<double> degrees = Game.Model.WheelModel.SectorsInfo[Game.Model.WheelModel.WinnerId];
        float maxDegree = (float)(degrees[0] - 180);
        float minDegree = (float)(degrees[1] - 180);
        return UnityEngine.Random.Range(minDegree, maxDegree);
    }
}
