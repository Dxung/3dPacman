using UnityEngine;

public class GhostRevive : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagEnum.Ghosts.ToString()))
        {
            Debug.Log("dung tag");
            GhostStateController ghostStateController = other.transform.gameObject.GetComponentInChildren<GhostStateController>();

            Debug.Log(ghostStateController);
            if (ghostStateController.CheckCurrentState(GhostState.eaten))
            {
                Debug.Log("da vao");
                ghostStateController.ResumeGhostStatus();
            }
        }
    }
}
