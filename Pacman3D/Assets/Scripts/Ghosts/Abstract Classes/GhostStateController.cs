using UnityEngine;

public abstract class GhostStateController : MonoBehaviour
{
    [Header("Ghost State")]
    [SerializeField] private GhostState _currentGhostState;

    [Header("Mode Change Timer")]
    [SerializeField] private float _currentTimer = 0;
    [SerializeField] protected int _modeSwitchIteration = 1;

    [Header("previous status before frighten or eaten")]
    [SerializeField] private GhostState _lastStateBeforeChange;
    [SerializeField] private float _lastTimerBeforeChange;

    [Header("References")]
    [SerializeField] private StateDuration _durationForState;


    //luon bat dau voi scatter
    protected virtual void Awake()
    {
        RestartGhostStateWhenRespawn();
    }

    private void Update()
    {
        UpdateGhostState();
    }


                                            ///*--- Abstract Func ---*///

    protected abstract void UpdateGhostState();



                                            ///*--- Update All State*///

    //Neu het thoi gian scatter o mode hien tai => chuyen trang thai sang chase
    protected void UpdateScatterMode()
    {
        if (IsTimeOut(_durationForState.GhostScatterDurationAtMode(_modeSwitchIteration)))
        {
            ChangeGhostStateTo(GhostState.chase);
            ResetTimer();
        }
        else
        {
            TimeRun();
        }
    }

    //Neu het thoi gian chase o mode hien tai => chuyen sang trang thai scatter va thay len mode tiep theo
    protected void UpdateChaseMode()
    {       
        if (IsTimeOut(_durationForState.GhostChaseDurationAtMode(_modeSwitchIteration)))
        {
            ChangeGhostStateTo(GhostState.scatter);
            ResetTimer();
            _modeSwitchIteration++;
        }
        else
        {
            TimeRun();
        }
    }

    //Neu het thoi gian Frighten => Ghost chuyen ve trang thai va thoi gian trang thai con lai truoc khi bi Frighten
    protected void UpdateFrightenedMode()
    {
        if (IsTimeOut(_durationForState.FrightenOrPowerUpDuration()))
        {
            ResumeGhostStatus();

        }
        else
        {
            TimeRun();
        }
    }



                                            ///*--- State Switch ---*///

    //Used when change ghost state to FrightenedMode
    //cannot change while in eaten state
    public void TurnToFrightenedState()
    {
        if (!CheckCurrentState(GhostState.eaten))
        {
            PauseGhostStatus();
            ResetTimer();
            ChangeGhostStateTo(GhostState.frightened);
        }
    }

    //Used when change ghost state to EatenMode
    //Change State to Eaten
    public void TurnToEatenState()
    {
        if (CheckCurrentState(GhostState.frightened))
        {
            ChangeGhostStateTo(GhostState.eaten);
        }
    }


                                            ///*--- Pause Before Frighten & Resume After Frighten/Eaten *///
    private void PauseGhostStatus()
    {

        if (!CheckCurrentState(GhostState.frightened) && !CheckCurrentState(GhostState.eaten))
        {
            _lastTimerBeforeChange = _currentTimer;
            _lastStateBeforeChange = _currentGhostState;
        }
    }

    public void ResumeGhostStatus()
    {
        _currentTimer = _lastTimerBeforeChange;
        ChangeGhostStateTo(_lastStateBeforeChange);
    }


                                            ///*--- Getter and Setter ---*///

    public bool CheckCurrentState(GhostState ghostStateToCheck)
    {
        return _currentGhostState == ghostStateToCheck;
    }

    protected void ChangeGhostStateTo(GhostState ghostStateToChangeTo)
    {
        _currentGhostState = ghostStateToChangeTo;
    }

    public bool TimeFrightenedNearlyOver(float timeBeforeOver)
    {
        float secondsRunned = _durationForState.FrightenOrPowerUpDuration() - timeBeforeOver;
        return _currentTimer > secondsRunned;
    }



                                             ///*--- Timer ---*///
    private void ResetTimer()
    {
        _currentTimer = 0;
    }

    private bool IsTimeOut(int timeForThatMode)
    {
        return _currentTimer > timeForThatMode;
    }

    private void TimeRun()
    {
        _currentTimer += Time.deltaTime;
    }

                                            ///*--- Respawn ---*///
    public void RestartGhostStateWhenRespawn()
    {
        //_modeSwitchIteration = 1;
        //ResetTimer();
        ChangeGhostStateTo(GhostState.scatter);
    }
}
