using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GhostData", menuName = "My Game/GhostData")]
public class GhostData : ScriptableObject
{
    [Header("Ghost's Name")]
    [SerializeField] private GhostName _ghostName;

    [Header("Ghost's Spawn Position")]
    [SerializeField] private Vector3 _GhostSpawnPosition;

    [Header("Ghost's Scatter Position")]
    [SerializeField] private List<Vector3> _ghostScatterPaths;

    [Header("Ghost's Color")]
    [SerializeField] private Color _ghostNormalColor;
    [SerializeField] private Color _ghostfrightendedColor;
    [SerializeField] private Color _ghostflickeringColor;

                                ///*--- Getter & Setter ---*///

    /*--- Name ---*/
    public GhostName WhichGhostIsThis()
    {
        return _ghostName;
    }

    /*--- Spawn ---*/
    public Vector3 GetSpawnPosition()
    {
        return _GhostSpawnPosition;
    }

    public void SetGhostSpawnPoint(Vector3 ghostSpawnPos)
    {
        _GhostSpawnPosition = ghostSpawnPos;
    }

    /*--- Scatter Point ---*/
    public Vector3 GetScatterPointAtNumber(int number)
    {
        return _ghostScatterPaths[number-1];
    }

    public int HowManyScatterPoints()
    {
        return _ghostScatterPaths.Count;
    }

    public void ClearAllScatterPaths()
    {
        _ghostScatterPaths.Clear();
    }

    public void AddScatterPointToPath(Vector3 scatterPointPos)
    {
        _ghostScatterPaths.Add(scatterPointPos);
    }

    /*--- Color ---*/
    public Color GetNormalGhostColor()
    {
        return _ghostNormalColor;
    }

    public Color GetFrightenedGhostColor()
    {
        return _ghostfrightendedColor;
    }

    public Color GetGhostFlickeringColor()
    {
        return _ghostflickeringColor;
    }


}
