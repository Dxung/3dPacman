using UnityEngine;

public class ClydeStateController : GhostStateController
{
    [SerializeField] private float _switchingStateRadius;
    [SerializeField] private GameCommunicationSystem _gameCommunicationSystem;

    [Header("Transform")]
    [SerializeField] private Vector3 _clydeTransformXZ = Vector3.zero;
    [SerializeField] private Vector3 _playerTransformXZ = Vector3.zero;


    protected override void Awake()
    {
        _gameCommunicationSystem = GameCommunicationSystem.Instance;
    }

    //ghost is far from player --> chase
    //ghost is close to player --> scatter
    protected override void UpdateGhostState()
    {
        UpdatePlayerPositionXZ();
        UpdateClydePositionXZ();

        if (CheckCurrentState(GhostState.chase) || CheckCurrentState(GhostState.scatter))
        {

            if (DistanceFromClydeToPlayer()> _switchingStateRadius)
            {
                ChangeGhostStateTo(GhostState.chase);
            }
            else
            {
                ChangeGhostStateTo(GhostState.scatter);
            }
        }

        if (CheckCurrentState(GhostState.frightened))
        {
            UpdateFrightenedMode();
        }
    }

    
    private void UpdatePlayerPositionXZ()
    {
        _playerTransformXZ.x = _gameCommunicationSystem.WhereIsPlayer().position.x;
        _playerTransformXZ.z = _gameCommunicationSystem.WhereIsPlayer().position.z;
    }

    private void UpdateClydePositionXZ()
    {
        _clydeTransformXZ.x = this.transform.position.x;
        _clydeTransformXZ.z = this.transform.position.z;

    }

    private float DistanceFromClydeToPlayer()
    {
        return Vector3.Distance(_clydeTransformXZ, _playerTransformXZ);
    }
}
