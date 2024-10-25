using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private void LateUpdate()
    {
        CameraFollowPlayer();
    }

    private void CameraFollowPlayer()
    {
        Vector3 newPosition = _player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, _player.eulerAngles.y, 0f);
    }
}
