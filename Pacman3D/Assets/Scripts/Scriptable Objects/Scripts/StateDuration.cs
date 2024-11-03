using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "State Duration", menuName = "My Game/StateDuration")]
public class StateDuration : ScriptableObject
{
    [Header("Both Ghost and Player")]
    [SerializeField] private int _frightenedOrPowerUpDuration;

    [Header("For Ghost Only")]
    //scatter1 - chase1- scatter2- chase2 - scatter3- chase3 - scatter 4
    [SerializeField] private int[] _scatterModeDuration;
    [SerializeField] private int[] _chaseModeDuration;

    [Header("For Player Only")]
    [SerializeField] private float _consumeTime;

    public int GhostScatterDurationAtMode(int modeNumber)
    {
        return _scatterModeDuration[modeNumber - 1];
    }

    public int GhostChaseDurationAtMode(int modeNumber)
    {
        return _chaseModeDuration[modeNumber - 1];
    }

    public int FrightenOrPowerUpDuration()
    {
        return _frightenedOrPowerUpDuration;
    }

    public float SlowdownDurationWhenConsuming()
    {
        return _consumeTime;
    }

}
