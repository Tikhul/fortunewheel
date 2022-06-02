using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SectorView : FortuneWheelElement
{
    private int sectorId;
    private bool isHighlighted = false;
    private float transparent = 0.3f;
    private float highlighted = 1.0f;
    private float fadeTime = 1.5f;
    private float textRadius = 1.6f;
    
    [SerializeField] private TMP_Text _idText;


    public TMP_Text IdText
    {
        get => _idText;
        set => _idText = value;
    }

    public void SetUpSector(Image newSector, float zRotation, int index, float rotationStep)
    {
        sectorId = Game.Model.WheelModel.Sectors[index].Id;
        
        SetTransform(newSector, zRotation);
        SetColor(newSector, index);
        SetText(rotationStep);
    }

    private void SetTransform(Image newSector, float zRotation)
    {
        newSector.transform.SetParent(Game.Model.WheelModel.SectorsParent.transform);
        newSector.transform.localScale = new Vector3(1, 1, 1);
        newSector.transform.localPosition = newSector.transform.position;
        newSector.transform.localRotation = Quaternion.Euler(0, 0, zRotation);
    }
    private void SetColor(Image newSector, int index)
    {
        newSector.sprite = Game.Model.WheelModel.Sectors[index].SourceImage;
        Color tmp = newSector.color;
        tmp.a = transparent;
        newSector.color = tmp;
        newSector.fillAmount = (float)Math.Round((double)1 / Game.Model.WheelModel.Sectors.Count, 3);
    }
    private void SetText(float rotationStep)
    {
        IdText.transform.localRotation = Quaternion.Euler(0, 0, -rotationStep * 0.5f);
        float angle = (float)ConvertDegreesToRadians(rotationStep);
        IdText.transform.Translate(VectorFromAngle(-angle));
        IdText.text = sectorId.ToString();
    }

    private double ConvertDegreesToRadians(double degrees)
    {
        return (Math.PI / 180) * degrees;
    }

    private Vector2 VectorFromAngle(float theta)
    {
        return new Vector2(Mathf.Cos(theta), Mathf.Sin(theta));
    }
    public void HighlightSector(int receivedId)
    {
        if (receivedId.Equals(sectorId))
        {
            GetComponent<Image>().DOFade(highlighted, fadeTime);
            isHighlighted = true;
        }
    }
    public void TurnOffSector()
    {
        if (isHighlighted)
        {
            GetComponent<Image>().DOFade(transparent, fadeTime);
            isHighlighted = true;
        }
    }
}
