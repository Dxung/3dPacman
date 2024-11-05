using UnityEngine;

public class ClydeMovementController : GhostMovementController
{
    //clyde thay doi trang thai Chase <-> Scatter khi o trong/ngoai khoang cach switch so voi player ===> xet trong clydestate
    protected override void Chase()
    {
        //dat lai toc do cua Clyde ve ban dau
        SetGhostSpeed(_ghostSpeed);

        //chuyen muc tieu di chuyen sang cho player
        _agent.SetDestination(_gameCommunicationSystem.WhereIsPlayer().position);
    }
}
