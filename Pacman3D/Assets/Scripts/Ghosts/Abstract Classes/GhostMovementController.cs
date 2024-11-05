using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.AI;

public abstract class GhostMovementController : MonoBehaviour
{
    [Header("Ghost Data")]
    [SerializeField] private GhostData _ghostData;
    [SerializeField] protected float _ghostSpeed;
    [SerializeField] private bool _ghostStop;
    [SerializeField] private float _speedPercentageFrightened = 0.5f;
    [SerializeField] private float _speedPercentageEaten = 1.5f;

    [Header("Navigation Path Finding")]
    protected NavMeshAgent _agent;

    [Header("Ref")]
    [SerializeField] protected GameCommunicationSystem _gameCommunicationSystem;

    [Header("Ghost State Controller")]
    private GhostStateController _ghostStateController;    

    [Header("Scatter Path Finding")]
    [SerializeField] private int _currentScatterPointGoalNum;


    private void Start()
    {
        //Get Ref
        _ghostStateController = this.gameObject.GetComponentInChildren<GhostStateController>();
        _agent = this.GetComponent<NavMeshAgent>();
        _gameCommunicationSystem = GameCommunicationSystem.Instance;

        //Respawn
        RespawnGhost();

    }

    private void Update()
    {
        UpdateGhostMovement();
    }

    private void LateUpdate()
    {
        LookAtMovingDirection();
    }



                                ///*--- Reset ---*///
    
    public void RespawnGhost()
    {
        // Restart State
        _ghostStateController.RestartGhostStateWhenRespawn();

        //Restart Movement
        RestartGhostMovementWhenRespawn();
    }

    private void RestartGhostMovementWhenRespawn()
    {
        //chuyen dich ghost ve vi tri spawn
        this.gameObject.transform.position = _ghostData.GetSpawnPosition();

        //reset scatter point ve ban dau
        ResetScatterPoint();

        //khoi tao speed cho ghost
        SetGhostSpeed(_ghostSpeed);

        //bat lai tim duong AI
        this.gameObject.GetComponent<NavMeshAgent>().enabled = true;

        //ghost co the move
        _ghostStop = false;
    }


                                ///*--- Abstact Func ---*///

    protected abstract void Chase();

                                ///*--- Ghost Moving Settings ---*///
    
    private void UpdateGhostMovement()
    {
        if (!_ghostStop)
        {
            if (_ghostStateController.CheckCurrentState(GhostState.chase))
            {
                Chase();
            }
            if (_ghostStateController.CheckCurrentState(GhostState.scatter))
            {
                Scatter();
            }
            if (_ghostStateController.CheckCurrentState(GhostState.frightened))
            {
                Frightened();
            }
            if (_ghostStateController.CheckCurrentState(GhostState.eaten))
            {
                Eaten();
            }
        }
    }

    private void LookAtMovingDirection()
    {
        if (_agent.velocity.sqrMagnitude > Mathf.Epsilon)
        {
            this.gameObject.transform.rotation = Quaternion.LookRotation(_agent.velocity.normalized);
        }
    }

    public void GhostStopMoving()
    {
        _ghostStop = true;
        _agent.SetDestination(this.gameObject.transform.position);
        SetGhostSpeed(0);
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
    }

    private void Scatter()
    {
        //doi toc do cua agent thanh toc do binh thuong
        SetGhostSpeed(_ghostSpeed);

        //dat vi tri den diem scattergoal
        _agent.SetDestination(GetCurrentScatterGoal(_currentScatterPointGoalNum));
    }

    private void Frightened()
    {
        //doi toc do con 1/2
        SetGhostSpeed(_ghostSpeed* _speedPercentageFrightened);

        //di chuyen den spawn point
        _agent.SetDestination(_ghostData.GetSpawnPosition());
    }

    private void Eaten()
    {
        //doi toc do len 1.5 lan
        SetGhostSpeed(_ghostSpeed * _speedPercentageEaten);

        //di chuyen den spawn point
        _agent.SetDestination(_ghostData.GetSpawnPosition());
    }

                                ///*--- Getter & Setter ---*///
    

    protected void SetGhostSpeed(float speed)
    {
        _agent.speed = speed;
    }

    private Vector3 GetCurrentScatterGoal (int scatterPointNum)
    {
        return _ghostData.GetScatterPointAtNumber(_currentScatterPointGoalNum);
    }


                                ///*--- Goal Switch For Scatter State ---*///
    // Ham nay duoc dung o cac "Scatter Goal gameObject" de chuyen huong goal tiep theo
    public void ChangeScatterGoal()
    {
        if (_currentScatterPointGoalNum == _ghostData.HowManyScatterPoints())
        {
            ResetScatterPoint();
        }
        else
        {
            _currentScatterPointGoalNum++;
        }
    }

    //1 -> 2 -> 3 -> 4 -> ...
    private void ResetScatterPoint()
    {
        _currentScatterPointGoalNum = 1;
    }

    public bool IsItScattering()
    {
        return _ghostStateController.CheckCurrentState(GhostState.scatter);
    }

    public bool IsThisCurrentGoalPoint(Vector3 pos)
    {
        return GetCurrentScatterGoal(_currentScatterPointGoalNum) == pos;
    }

    public bool IsThisTheGhostIThink(GhostName ghostname)
    {
        return ghostname.ToString() == _ghostData.WhichGhostIsThis().ToString();
    }
}
