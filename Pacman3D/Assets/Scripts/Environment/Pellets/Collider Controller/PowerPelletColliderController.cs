using UnityEngine;

public class PowerPelletColliderController : NormalPelletColliderController
{

    protected override void OnTriggerEnter(Collider playerHit)
    {
        if (playerHit.gameObject.CompareTag(_playerTag.ToString()))
        {

            //Send Event to GameControllerSystem
            _gameControllerSystem.PowerPelletConsumed();

            //Change Status of Pellet
            ChangePelletStatus();

        }
    }
}
