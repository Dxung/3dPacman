using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    private enum PlayerState
    {
        normal,
        consume,
        powerUp,
        dead
    }

    [Header("Timer for each state")]
    [SerializeField] private float _slowdownTimeWhenConsuming;
    [SerializeField] private float _powerUpTime;
    private float _currentTimer;

    [Header("Player State")]
    private PlayerState _currentPlayerState;


    private void Start()
    {
        ReSpawn();
    }

    private void Update()
    {
        UpdatePlayerState();
    }



    /*--->>> DEBUG <<<---*/
    //Debug.Log(_currentPlayerState);
    //Debug.Log(_currentTimer);
    //Debug.Log(_currentPlayerState);





    /*--- State ---*/

    private void UpdatePlayerState()
    {
        if (_currentPlayerState == PlayerState.consume)
        {
            UpdateConsumeModeTimer();
        }
        else if (_currentPlayerState == PlayerState.powerUp)
        {
            UpdatePowerUpModeTimer();
        }
    }

    public void TurnToConsumeState()
    {
        if ((_currentPlayerState!=PlayerState.powerUp) && (_currentPlayerState != (PlayerState.dead)))
        {
            ResetTimer();
            ChangeState(PlayerState.consume);
        }
    }

    public void TurnToPowerUpState()
    {
        if (_currentPlayerState != (PlayerState.dead))
        {
            ResetTimer();
            ChangeState(PlayerState.powerUp);
        }
    }

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


    /*--- Timer ---*/
    private void UpdateConsumeModeTimer()
    {
        if (IsTimeOut(_slowdownTimeWhenConsuming))
        {
            ChangeState(PlayerState.normal);
            ResetTimer();
        }
        else
        {
            _currentTimer += Time.deltaTime;
        }
    }

    private void UpdatePowerUpModeTimer()
    {
        if (IsTimeOut(_powerUpTime))
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




    public bool IsitThatState(string check)
    {
        if(check == "normal")
        {
            if(_currentPlayerState == PlayerState.normal)
            {
                return true;
            }
            else return false;
        }
        if(check == "consume")
        {
            if (_currentPlayerState == PlayerState.consume)
            {
                return true;
            }
            else return false;
        }
        if(check == "powerUp")
        {
            if (_currentPlayerState == PlayerState.powerUp)
            {
                return true;
            }
            else return false;
        }
        if (check == "dead")
        {
            if (_currentPlayerState == PlayerState.dead)
            {
                return true;
            }
            else return false;
        }
        else return false;
    }
}
