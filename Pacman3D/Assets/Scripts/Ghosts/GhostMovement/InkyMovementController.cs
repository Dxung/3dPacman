using UnityEngine;
using UnityEngine.AI;

public class InkyMovementController : GhostMovementController
{
    protected override void Chase()
    {
        //Chuyen ve toc do binh thuong
        SetGhostSpeed(_ghostSpeed);

        //Inky se ngam muc tieu den 1 diem nam tren duong thang noi Blinky va Player
        Vector3 targetposition = this.transform.position + (_gameCommunicationSystem.WhereIsPlayer().position - _gameCommunicationSystem.WhereIsBlinky().position)*1.5f;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(targetposition, out hit,3.0f, NavMesh.AllAreas))
        {
            _agent.SetDestination(targetposition);
        }
        else
        {
            Debug.Log("Inky khong xac dinh duoc diem den kha thi");
        }
    }
}

