using UnityEngine;

public class ScatterPointControllerSystem : MonoBehaviour
{
    [Header("Scatter Points Prefabs (put in Order)")]
    [SerializeField] private GameObject[] _blinkyScatterPoints;
    [SerializeField] private GameObject[] _pinkyScatterPoints;
    [SerializeField] private GameObject[] _inkyScatterPoints;
    [SerializeField] private GameObject[] _clydeScatterPoints;


    [Header("GhostData For Store ScatterPointPosition")]
    [SerializeField] private GhostData _blinkyGhostData;
    [SerializeField] private GhostData _pinkyGhostData;
    [SerializeField] private GhostData _inkyGhostData;
    [SerializeField] private GhostData _clydeGhostData;

    private void Awake()
    {

        InitGhostDataValue(_blinkyScatterPoints, _blinkyGhostData);
        InitGhostDataValue(_pinkyScatterPoints, _pinkyGhostData);
        InitGhostDataValue(_inkyScatterPoints, _inkyGhostData);
        InitGhostDataValue(_clydeScatterPoints, _clydeGhostData);
    }


    private void InitGhostDataValue(GameObject[] scatterPointArray, GhostData ghostData)
    {
        //Khoi tao doi tuong truoc thi moi lay Transform.position duoc
        CreatePrefabs(scatterPointArray);

        //Mot khi doi tuong da duoc tao, bat dau add vao ghostdata tuong ung
        ScatterPointPosToGhostData(scatterPointArray, ghostData);
    }

    //Tao cac object scatterPoint tren len ban do
    private void CreatePrefabs(GameObject[] scatterPointArrayToCreate)
    {
        foreach (GameObject scatterPointObject in scatterPointArrayToCreate)
        {
            Instantiate(scatterPointObject);
        }
    }

    //Dua vao toa do cac object scatterPoint da tao, truyen toa do chung theo thu tu vao arr
    private void ScatterPointPosToGhostData(GameObject[] scatterPointArray, GhostData ghostData)
    {
        //Clear truoc tien, do ghostData la Scriptable Object, du lieu se duoc luu trong quy mo Project, neu khong xoa moi khi tai lai scene thi se bi trung
        ghostData.ClearAllScatterPaths();

        //Them pos
        foreach(GameObject scatterPointObject in scatterPointArray){
            ghostData.AddScatterPointToPath(scatterPointObject.transform.position);
        }
    }

}
