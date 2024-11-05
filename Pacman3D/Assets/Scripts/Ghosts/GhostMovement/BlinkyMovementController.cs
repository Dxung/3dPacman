using UnityEngine;

public class BlinkyMovementController : GhostMovementController
{
    [SerializeField] private bool Rage = false;

    //Blinky duoi theo nguoi choi
    protected override void Chase()
    {
        //rage khi xuong 20% pellet con lai. Pellet Counter se gui tin hieu
        if (Rage)
        {
            SetSuitableRageSpeed();
        }
        else
        {
            SetGhostSpeed(_ghostSpeed);
        }

        //chuyen muc tieu di chuyen sang cho player
        _agent.SetDestination(_gameCommunicationSystem.WhereIsPlayer().position);
    }

    private void SetSuitableRageSpeed()
    {
        //neu toc do rage qua 90% toc do normal cua player => lay 90%
        // neu khong, lay 1.2 lan toc do cua ghost
        if (_ghostSpeed * 1.2f > _gameCommunicationSystem.HowFastIsPlayer() * 0.9f)
        {
            SetGhostSpeed(_gameCommunicationSystem.HowFastIsPlayer() * 0.9f);
        }
        else
        {
            SetGhostSpeed(_ghostSpeed * 1.3f);
        }
    }



    //pellet counter se goi khi con 20% pellet
    public void BlinkyRageWhenLowPellet()
    {
        Rage = true;
    }

}
