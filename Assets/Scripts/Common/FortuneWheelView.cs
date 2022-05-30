using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheelView : MonoBehaviour
{
    [SerializeField] private SectorView _sectorView;

    public SectorView SectorView
    {
        get => _sectorView;
        set => _sectorView = value;
    }
}
