using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class PinkyMovementController : GhostMovementController
{
    [SerializeField] private float _distanceInFrontOfPlayer = 3f;
    [SerializeField] private float _navmeshCheckRadius = 3.6f;

    protected override void Chase()
    {
        // Lay vi tri truoc mat player, cach mot doan "_distanceInFrontOfPlayer"
        Vector3 targetFrontPosition = _gameCommunicationSystem.WhereIsPlayer().position + _gameCommunicationSystem.WhereIsPlayer().forward * _distanceInFrontOfPlayer;


        NavMeshHit hit;

        //Neu vi tri truoc mat player nay bi trung (ra ngoai map/ trung obstacle) thi tim diem kha thi gan nhat
        if (NavMesh.SamplePosition(targetFrontPosition, out hit, _navmeshCheckRadius, NavMesh.AllAreas))
        {
            // hit.position la vi tri hop le
            _agent.SetDestination(hit.position);
        }
        else
        {
            Debug.Log("cannot find pinky suitable target. Please check again distance");
        }

    }

}
