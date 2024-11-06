using UnityEngine;

public class GhostRevive : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagEnum.Ghosts.ToString()))
        {
            GhostStateController ghostStateController = other.transform.gameObject.GetComponentInChildren<GhostStateController>();

            if (ghostStateController.CheckCurrentState(GhostState.eaten))
            {
                ghostStateController.ResumeGhostStatus();
            }
        }
    }
}
