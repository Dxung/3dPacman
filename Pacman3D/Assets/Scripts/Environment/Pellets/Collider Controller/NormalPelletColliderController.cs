using Unity.VisualScripting;
using UnityEngine;

public class NormalPelletColliderController : MonoBehaviour
{
    [Header("Pellet Particle System")]
    protected ParticleSystem _pelletParticleSystem;
    protected float _timeToCompletePelletEffect;

    [Header("References when particle effect happens")]
    protected Collider _pelletTrigger;
    protected MeshRenderer _pelletMesh;
    protected Light _pelletLight;
    protected SpriteRenderer _pelletMinimapIcon;
    [SerializeField] protected TagEnum _playerTag;

    [Header("Game Controller System")]
    protected GameControllerSystem _gameControllerSystem;



    /***--- Init ---***/
    protected void Awake()
    {
        _pelletParticleSystem = this.transform.parent.GetComponentInChildren<ParticleSystem>();
        _pelletMesh = this.transform.parent.GetComponent<MeshRenderer>();
        _pelletTrigger = this.GetComponent<Collider>();
        _pelletLight = this.transform.parent.GetComponentInChildren<Light>();
        _pelletMinimapIcon = this.transform.parent.GetComponentInChildren<SpriteRenderer>();
        _gameControllerSystem = GameControllerSystem.Instance;

        setUpTotalEffectTime();
}

    protected void setUpTotalEffectTime()
    {
        /*--- main.duration: Khoang thoi gian ma he thong Particle System tao ra cac Particle ---*/
        /*--- startLifetime: Khoang thoi gian Particle ton tai sau khi duoc tao ra ---*/

        /*--- _timeToCompletePelletEffect : Tong thoi gian tu luc cac Particle duoc tao ra cho den luc chung bien mat hoan toan(hoan thanh hieu ung) ---*/
        _timeToCompletePelletEffect = _pelletParticleSystem.main.duration + _pelletParticleSystem.main.startLifetime.constant;
    }


        /*--- When Collide ---*/
        protected virtual void OnTriggerEnter(Collider playerHit)
        {
            if (playerHit.gameObject.CompareTag(_playerTag.ToString())){

            //Send Event to GameControllerSystem
            _gameControllerSystem.NormalPelletConsumed();

            //Change Status of Pellet
            ChangePelletStatus();
                    
            }
        }


        protected void ChangePelletStatus()
        {

        //Turn off Pellet Appearance
        _pelletMesh.enabled = false;
        _pelletLight.enabled = false;
        _pelletTrigger.enabled = false;
        _pelletMinimapIcon.enabled = false;

        //Play Collide Effect
        _pelletParticleSystem.Play();

        //Destroy this Pellet when finish effect
        Destroy(this.transform.parent.gameObject, _timeToCompletePelletEffect);
        }



}
