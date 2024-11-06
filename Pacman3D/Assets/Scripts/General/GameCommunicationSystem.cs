using System.Collections;
using UnityEngine;

public class GameCommunicationSystem : MonoBehaviour
{
    /*--- SingleTon Script => For Providing Ref for outside_scene_script (prefabs) & generalisation Ref for all Script ---*/

    public static GameCommunicationSystem Instance {  get; private set; }


    [Header("Player")]
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerStateController _playerStateController;

    [Header("Ghosts")]
    [SerializeField] private GameObject _blinky;
    [SerializeField] private GameObject _pinky;
    [SerializeField] private GameObject _inky;
    [SerializeField] private GameObject _clyde;

    [Header("UI")]
    [SerializeField] private ScoreBar _scoreBar;
    [SerializeField] private PelletCounter _pelletCounter;
    [SerializeField] private HealthCounter _healthCounter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


   /*--- Event ---*/
   public void NormalPelletConsumed()
    {
        //Update UI
        UIWhenNormalPelletConsumed();

        //Change PlayerState to Consume
        _playerStateController.TurnToConsumeState();
    }

    public void PowerPelletConsumed()
    {
        //Update UI
        UIWhenPowerPelletConsumed();

        //Change PlayerState to PowerUp
        _playerStateController.TurnToPowerUpState();

        //Chang GhostState to Frightened
        _blinky.GetComponentInChildren<GhostStateController>().TurnToFrightenedState();
        _pinky.GetComponentInChildren<GhostStateController>().TurnToFrightenedState();
        _inky.GetComponentInChildren<GhostStateController>().TurnToFrightenedState();
        _clyde.GetComponentInChildren<GhostStateController>().TurnToFrightenedState();

    }

    public void GhostCaughtPlayer()
    {
        //chuyen trang thai player sang "dead"
        _playerStateController.Dead();

        //update UI
        if (_healthCounter.CheckIfPlayerDead()) //Neu player het mang
        {
            StopAllGhost();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 0f;
            Debug.Log("Player Dead");
        }
        else
        {
            _healthCounter.UpdatePlayerHealth();
            StartCoroutine(RespawnPlayerAndGhost());
        }
    }

    public void PlayerCaughtGhost()
    {
        //update UI
        _scoreBar.AddPointFromGhost();
    }

    /*--- Player ---*/

    public Transform WhereIsPlayer()
    {
        return _player.transform;
    }


    public float HowFastIsPlayer()
    {
        return _player.GetComponent<PlayerMovingController>().GetPlayerNormalSpeed();
    }

    /*--- Ghosts ---*/
    public Transform WhereIsBlinky()
    {
        return _blinky.transform;
    }

    /*--- UI ---*/
    private void UIWhenNormalPelletConsumed()
    {
        _pelletCounter.ConsumeSmallPellet();
        _scoreBar.AddPointFromSmallPellet();
    }

    private void UIWhenPowerPelletConsumed()
    {
        _pelletCounter.ConsumePowerPellet();
        _scoreBar.AddPointFromPowerPellet();
    }

    public void NormalPelletToCounter()
    {
        _pelletCounter.AddOneSmallPelletToCounter();
    }

    public void PowerPelletToCounter()
    {
        _pelletCounter.AddOnePowerPelletToCounter();
    }

    /*--- When Player Got Caught ---*/
    private void StopAllGhost()
    {
        _blinky.gameObject.GetComponentInChildren<GhostMovementController>().GhostStopMoving();
        _pinky.gameObject.GetComponentInChildren<GhostMovementController>().GhostStopMoving();
        _inky.gameObject.GetComponentInChildren<GhostMovementController>().GhostStopMoving();
        _clyde.gameObject.GetComponentInChildren<GhostMovementController>().GhostStopMoving();
    }

    IEnumerator RespawnPlayerAndGhost()
    {
        StopAllGhost();
        
        //cho 2 giay
        yield return new WaitForSeconds(2);

        //Respawn Pacman = reset movement + reset state
        _player.GetComponent<PlayerMovingController>().ReSpawnPacman();

        _blinky.gameObject.GetComponentInChildren<GhostMovementController>().RespawnGhost();
        _pinky.gameObject.GetComponentInChildren<GhostMovementController>().RespawnGhost();
        _inky.gameObject.GetComponentInChildren<GhostMovementController>().RespawnGhost();
        _clyde.gameObject.GetComponentInChildren<GhostMovementController>().RespawnGhost();

    }

}
