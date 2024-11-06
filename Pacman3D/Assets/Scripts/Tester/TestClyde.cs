using UnityEngine;

public class TestClyde : MonoBehaviour
{
    public GameObject player;
    public float radius = 10f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(player.transform.position, radius);
    }
}
