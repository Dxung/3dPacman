using UnityEngine;

public class TestPinky : MonoBehaviour
{
    public float radius = 3f;                   // Bán kính của hình cầu


    public GameObject player;
    public Vector3 target;

    public float _distanceInFrontOfPlayer;


    void OnDrawGizmos()
    {
        target = player.transform.position + player.transform.forward * _distanceInFrontOfPlayer;
        Gizmos.color = Color.red;             // Màu sắc của hình cầu trong Scene View
        Gizmos.DrawWireSphere(target, radius);  // Vẽ hình cầu dạng wireframe
    }
}
