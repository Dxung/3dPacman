using UnityEngine;

public class GameControllerSystem : MonoBehaviour
{
    /*--- SingleTon Script => For Providing Ref for outside_scene_script (prefabs) & generalisation Ref for all Script ---*/

    public static GameControllerSystem Instance {  get; private set; }


    [Header("Player")]
    [SerializeField] private GameObject _player;
    [SerializeField] private PlayerStateController _playerStateController;


    [Header("UI")]
    [SerializeField] private ScoreBar _scoreBar;
    [SerializeField] private PelletCounter _pelletCounter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


   /*--- Event ---*/
   public void NormalPelletConsumed()
    {
        //Update UI
        UIWhenNormalPelletConsumed();

        //Change PlayerState to Consume
        _playerStateController.TurnToConsumeState();
    }

    /*--- Player ---*/

    public Transform WhereIsPlayer()
    {
        return _player.transform;
    }

    public PlayerStateController GetPlayerController()
    {
        return _playerStateController;
    } 




    /*--- UI ---*/
    private void UIWhenNormalPelletConsumed()
    {
        _pelletCounter.ConsumeSmallPellet();
        _scoreBar.AddPointFromSmallPellet();
    }


}
