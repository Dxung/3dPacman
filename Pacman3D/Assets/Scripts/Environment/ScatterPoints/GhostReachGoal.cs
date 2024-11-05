using UnityEngine;

public class GhostReachGoal : MonoBehaviour
{
    [SerializeField] private GhostName _triggerForWhichGhost;


    //tieu chi xet: 
    // La Ghost? => co phai Ghost cho scatter point nay khong => No co dang trong trang thai Scattering khong => Diem hien tai nay co phai la diem goal current khong ?
    private void OnTriggerEnter(Collider other)
    {
        //chi xet va cham voi cac Ghosts
        if (other.gameObject.CompareTag(TagEnum.Ghosts.ToString()))
        {
            //lay bo dieu khien movement cua ghost va cham
            GhostMovementController ghostMovementController = other.gameObject.GetComponent<GhostMovementController>();

            //Neu ghost cham vao la ghost tuong ung voi scatterpoint nay
            if (ghostMovementController.IsThisTheGhostIThink(_triggerForWhichGhost))
            {
                //Chi xet trong truong hop ghost dang scattering
                if (ghostMovementController.IsItScattering())
                {

                    //neu diem scatter point nay la diem currentgoal (khi den currentgoal thi se chuyen goal)
                    if (ghostMovementController.IsThisCurrentGoalPoint(this.transform.position))
                    {
                        ghostMovementController.ChangeScatterGoal();
                    }

                }
            }
        }
    }
}
