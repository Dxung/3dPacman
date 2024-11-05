using UnityEngine;

public class GhostRespawnControllerSystem : MonoBehaviour
{
    [SerializeField] private GhostData _blinkyData;
    [SerializeField] private GhostData _pinkyData;
    [SerializeField] private GhostData _inkyData;
    [SerializeField] private GhostData _clydeData;

    [SerializeField] private GameObject _blinkySpawnPoint;
    [SerializeField] private GameObject _pinkySpawnPoint;
    [SerializeField] private GameObject _inkySpawnPoint;
    [SerializeField] private GameObject _clydeSpawnPoint;

    private void Awake()
    {
        CreatePrefabs(_blinkySpawnPoint);
        CreatePrefabs(_pinkySpawnPoint);
        CreatePrefabs(_inkySpawnPoint);
        CreatePrefabs(_clydeSpawnPoint);

        CreateSpawnPointForGhostData(_blinkySpawnPoint, _blinkyData);
        CreateSpawnPointForGhostData(_pinkySpawnPoint, _pinkyData);
        CreateSpawnPointForGhostData(_inkySpawnPoint, _inkyData);
        CreateSpawnPointForGhostData(_clydeSpawnPoint, _clydeData);
    }

    //Create prefabs of all the "spawn Point gameobject" in array
    private void CreatePrefabs(GameObject spawnPointToCreate)
    {
        Instantiate(spawnPointToCreate);
    }

    //Add Transform of all gameobjects in the array to its appropriate "ghost data"
    private void CreateSpawnPointForGhostData(GameObject spawnPoint, GhostData ghostData)
    {
        ghostData.SetGhostSpawnPoint(spawnPoint.transform.position);
    }
}
