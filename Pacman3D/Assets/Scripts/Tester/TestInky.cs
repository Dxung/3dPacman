using TMPro;
using UnityEngine;

public class TestInky : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject blinky;

    [SerializeField] private float farFromPlayer;
    [SerializeField] private float calculateRate;
    [SerializeField] private float CheckRadius;

    private void OnDrawGizmos()
    {
        Vector3 a = player.transform.position + player.transform.forward * farFromPlayer; // xac dinh vi tri 1 diem truoc mat player
        Vector3 vecto = a - blinky.transform.position; // xac dinhj vi tri cua vector tu blinky den a
        Vector3 target = a + vecto; //tu diem a, ve 1 vector co do dai va huong y het vector vecto o tren => diem ngon cua vector nay la diem target ==> diem nay se la diem doi xung voi b qua a

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(a, calculateRate);
        Gizmos.DrawLine(player.transform.position, a);

        Gizmos.DrawLine(blinky.transform.position, target);
        Gizmos.DrawWireSphere(target, CheckRadius);
    }
}
