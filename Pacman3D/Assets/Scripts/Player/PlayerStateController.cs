using UnityEngine;

public class PlayerStateController : MonoBehaviour
{

    [Header("Timer for each state")]
    [SerializeField] private StateDuration _stateDuration;
    private float _currentTimer;

    [Header("Player State")]
    private PlayerState _currentPlayerState;


    private void Start()
    {
        ReSpawn();
    }

    private void Update()
    {
        UpdateStateTimer();
    }


    /*--- State ---*/

    //if "consume state" -> countdown
    //if "powerUp state" -> countdown
    //if "dead state" -> respawn -> normal state ==> no need
    //if "normal state" ==> no need

    private void UpdateStateTimer()
    {
        if (_currentPlayerState == PlayerState.consume)
        {
            ConsumeModeTimerCountDown();
        }
        else if (_currentPlayerState == PlayerState.powerUp)
        {
            PowerUpModeTimerCountDown();
        }
    }


    //Not dead + Not PowerUp =>Can turn to Consume State (Speed Down)
    public void TurnToConsumeState()
    {
        if ((_currentPlayerState!=PlayerState.powerUp) && (_currentPlayerState != (PlayerState.dead)))
        {
            ResetTimer();
            ChangeState(PlayerState.consume);
        }
    }


    //Not Dead => Can turn to Powerup State
    public void TurnToPowerUpState()
    {
        if (_currentPlayerState != (PlayerState.dead))
        {
            ResetTimer();
            ChangeState(PlayerState.powerUp);
        }
    }


    //Dead => Turn to Dead State
    public void Dead()
    {
        ChangeState(PlayerState.dead);
    }

    public void ReSpawn()
    {
        ChangeState(PlayerState.normal);
    }
    

    private void ChangeState(PlayerState state)
    {
        _currentPlayerState = state;
    }

    public PlayerState GetPlayerCurrentState()
    {
        return _currentPlayerState;
    }


    //If ConsumedMode Time Out => Turn to Normal State
    /*--- Timer ---*/
    private void ConsumeModeTimerCountDown()
    {
        if (IsTimeOut(_stateDuration.SlowdownDurationWhenConsuming()))
        {
            ChangeState(PlayerState.normal);
            ResetTimer();
        }
        else
        {
            _currentTimer += Time.deltaTime;
        }
    }


    //If PowerUpMode Time Out => Turn to Normal State
    private void PowerUpModeTimerCountDown()
    {
        if (IsTimeOut(_stateDuration.FrightenOrPowerUpDuration()))
        {
            ChangeState(PlayerState.normal);
            ResetTimer();
        }
        else
        {
            _currentTimer += Time.deltaTime;
        }
    }


    void ResetTimer()
    {
        _currentTimer = 0;
    }

    private bool IsTimeOut(float timeForThatState)
    {
        return _currentTimer > timeForThatState;
    }

}
