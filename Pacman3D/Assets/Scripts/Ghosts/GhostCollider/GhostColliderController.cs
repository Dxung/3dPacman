using UnityEngine;

public class GhostColliderController : MonoBehaviour
{
    [SerializeField] private GameCommunicationSystem _gameCommunicationSystem;

    private void Start()
    {
        _gameCommunicationSystem = GameCommunicationSystem.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Neu va cham vao player
        if (other.CompareTag(TagEnum.Player.ToString()))
        {

            PlayerStateController playerStateController = other.gameObject.GetComponentInChildren<PlayerStateController>();
            GhostStateController ghostStateController = this.transform.parent.GetComponentInChildren<GhostStateController>();

            /*--- Neu trong truong hop player dang khong co suc manh gi*/
            if(playerStateController.CheckCurrentState(PlayerState.normal) || playerStateController.CheckCurrentState(PlayerState.consume))
            {
                if (!ghostStateController.CheckCurrentState(GhostState.eaten))//de phong pacman an ghost o giay cuoi, no van dang lao ve diem hoi sinh
                {
                    _gameCommunicationSystem.GhostCaughtPlayer();
                }
                

            /*--- Neu trong truong hop player dang co suc manh ---*/
            }else if (playerStateController.CheckCurrentState(PlayerState.powerUp) && ghostStateController.CheckCurrentState(GhostState.frightened))
            {
                _gameCommunicationSystem.PlayerCaughtGhost();
                ghostStateController.TurnToEatenState();

            }


        }
    }
}
