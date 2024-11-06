using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class InkyMovementController : GhostMovementController
{
    [SerializeField] private float _farFromPlayer = 2f;
    [SerializeField] private float _navmeshCheckRadius = 20f;

    protected override void Chase()
    {
        //Chuyen ve toc do binh thuong
        SetGhostSpeed(_ghostSpeed);

        //diem dich doi xung voi vi tri blinky qua 1 diem nam truoc mat pacman
        Vector3 inFrontOfPlayer = _gameCommunicationSystem.WhereIsPlayer().transform.position + _gameCommunicationSystem.WhereIsPlayer().transform.forward * _farFromPlayer;
        Vector3 vecto = inFrontOfPlayer - _gameCommunicationSystem.WhereIsBlinky().transform.position;
        Vector3 target = inFrontOfPlayer + vecto;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(target, out hit, _navmeshCheckRadius, NavMesh.AllAreas))
        {
            _agent.SetDestination(target);
            Debug.Log("inky tim duoc muc tieu");
        }
        else
        {
            Debug.Log("Inky khong xac dinh duoc diem den kha thi");
        }
    }

    //private void OnDrawGizmos()
    //{

    //    Vector3 inFrontOfPlayer = _gameCommunicationSystem.WhereIsPlayer().transform.position + _gameCommunicationSystem.WhereIsPlayer().transform.forward * _farFromPlayer;
    //    Vector3 vecto = inFrontOfPlayer - _gameCommunicationSystem.WhereIsBlinky().transform.position;
    //    Vector3 target = inFrontOfPlayer + vecto;

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(inFrontOfPlayer, _farFromPlayer);
    //    Gizmos.DrawLine(_gameCommunicationSystem.WhereIsPlayer().transform.position, inFrontOfPlayer);

    //    Gizmos.DrawLine(_gameCommunicationSystem.WhereIsBlinky().transform.position, target);
    //    Gizmos.DrawWireSphere(target, _navmeshCheckRadius);

    //    //check

    //}
}

