using UnityEngine;

public class MinimapCameraController : MonoBehaviour
{
    [SerializeField] private GameCommunicationSystem _gameCommunicationSystem;

    private void Start()
    {
        _gameCommunicationSystem = GameCommunicationSystem.Instance;
    }

    private void LateUpdate()
    {
        CameraFollowPlayer();
    }

    private void CameraFollowPlayer()
    {
        Vector3 newPosition = _gameCommunicationSystem.WhereIsPlayer().position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        transform.rotation = Quaternion.Euler(90f, _gameCommunicationSystem.WhereIsPlayer().eulerAngles.y, 0f);
    }
}
