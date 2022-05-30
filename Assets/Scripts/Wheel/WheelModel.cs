using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelModel : FortuneWheelElement
{
    private Dictionary<int, List<double>> _sectorsInfo = new Dictionary<int, List<double>>();
    private int _probabilitySum;
    [SerializeField] private Canvas _wheelCanvas;
    [SerializeField] private List<SectorSO> _sectors;
    [SerializeField] private Image _sectorPrefab;

    public Dictionary<int, List<double>> SectorsInfo
    {
        get => _sectorsInfo;
        set => _sectorsInfo = value;
    }
    public int ProbabilitySum
    {
        get => _probabilitySum;
        set => _probabilitySum = value;
    }
    public Canvas WheelCanvas
    {
        get => _wheelCanvas;
        set => _wheelCanvas = value;
    }

    public List<SectorSO> Sectors
    {
        get
        {
            if (_sectors.Count < 3 || _sectors.Count > 15)
            {
                Debug.Log("Нужно от 3 до 15 секторов");
                return null;
            }
            else
            {
                return _sectors;
            }
        }
        set => _sectors = value;
    }

    public Image SectorPrefab
    {
        get => _sectorPrefab;
        set => _sectorPrefab = value;
    }
}
