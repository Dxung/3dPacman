using UnityEngine;

public class PlayerColliderController : MonoBehaviour
{
    [Header("Player State")]
    PlayerStateController _playerStateController;

    [Header("UI Elements")]
    [SerializeField] PelletCounter _pelletCounter;
    [SerializeField] ScoreBar _scoreCounter;

    public void ConsumePowerPellet()
    {
        _pelletCounter.ConsumePowerPellet();
        _scoreCounter.AddPointFromPowerPellet();
    }


    /*
     PlaceHolder For Ghost Collide Processing
     */
}
