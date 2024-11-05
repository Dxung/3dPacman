using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GhostEffectController : MonoBehaviour
{
    [Header("Ghost State Controller")]
    private GhostStateController _ghostStateController;

    [Header("Ghost Movement Controller")]
    private GhostMovementController _ghostMovementController;

    [Header("Minimap Effect Controller")]
    private GhostMinimapEffectController _ghostMinimapEffectController;

    [Header("Ghosts' Material - For Changing to Frighten Color")]
    [SerializeField] private List<Material> _ghostMaterials;

    [Header("Ghosts's Part to disable when in eaten mode")]
    [SerializeField] private GameObject[] _ghostPartsToDisappear;

    [Header("Ref")]
    [SerializeField] private GhostData _ghostData;


    //cac bien bool dung de tranh khoi tao cac gia tri nhieu lan
    //chi can so sanh cac bien bool thay vi khoi tao lai => do lang phi tai nguyen
    [SerializeField] private bool _disappearParts = false;    //When Eaten
    [SerializeField] private bool _colorChanged; //When Firghtened

    [SerializeField] private float _timeToStartFlickering;
    [SerializeField] private float _timeBetweenFlickering;
    [SerializeField] private float _flickerTimer;
    [SerializeField] private bool _firstTimeFlickering = false;
    [SerializeField] private bool _flicked = false;

    private void Start()
    {
        //Get Ref
        _ghostStateController = this.transform.parent.gameObject.GetComponentInChildren<GhostStateController>();
        _ghostMovementController = this.transform.parent.gameObject.GetComponentInChildren<GhostMovementController>();
        _ghostMinimapEffectController = this.transform.parent.gameObject.GetComponentInChildren<GhostMinimapEffectController>();

        //Reset het color ve ban dau
        ResetToOriginColor();
    }

    private void Update()
    {
        UpdateGhostEffect();
    }

    private void UpdateGhostEffect()
    {
        //frightened -> chase/scatter
        //eaten -> revive -> chase/scatter
        //flickering last second frightened -> chase/scatter
        // => turn off all light and effect => return back all to normal
        if(_ghostStateController.CheckCurrentState(GhostState.chase) || _ghostStateController.CheckCurrentState(GhostState.scatter))
        {
            if (_colorChanged)
            {
                ResetToOriginColor();
            }

            if (_disappearParts)
            {
                ReAppearParts();
            }
            if (_flicked)
            {
                _flicked = false;
            }
            ResetFlickerTimer();
        }
        else if (_ghostStateController.CheckCurrentState(GhostState.frightened))
        {
            PlayFrightenedEffect();
        }else if (_ghostStateController.CheckCurrentState(GhostState.eaten))
        {
            PlayEatenEffect();
            ResetFlickerTimer();
        }
    }

    /*--- Frightened ---*/
    private void PlayFrightenedEffect()
    {
        //Neu chuan bi het thoi gian Frightened => flickering bat dau
        if (_ghostStateController.TimeFrightenedNearlyOver(_timeToStartFlickering))
        {
            CheckIfFirstTimeFlickering(); // lan dau chuyen tu frightened sang flickering ?
            CheckIfSwitchOrNot(); //kiem tra xem den luc doi mau flickering chua. Neu co: flicked => not flicked -> danh dau la chua doi mau

            if (!_colorChanged)// Neu chua doi mau, neu doi mau roi thi thoi
            {
                if (!_flicked)//Neu chua flickering thi chuyen sang flickering
                {
                    ChangeMaterialToFlickeringColor();
                    _ghostMinimapEffectController.GhostMinimapIsFlickering();
                    _colorChanged = true;
                }
                else //Neu flickering roi
                {
                    ChangeMaterialToFrightenedColor();
                    _ghostMinimapEffectController.GhostMinimapIsFrightened();
                    _colorChanged = true;
                }
            }

        }
        else       //Neu chua den thoi gian Flickering thi van giu nguyen mau Frightened nhu cu
        {
            if(!_colorChanged) //Neu chua doi mau
            {
                //Chuyen mau ghost thanh mau frightened
                ChangeMaterialToFrightenedColor();
                _ghostMinimapEffectController.GhostMinimapIsFrightened();
                //tag thanh da doi mau
                _colorChanged = true;
            }
        }
    }

    private void PlayEatenEffect()
    {
        if (!_disappearParts)
        {
            DisappearParts();
        }

        _ghostMinimapEffectController.GhostMinimapIsEaten();
    }

    private void ChangeMaterialToFlickeringColor()
    {
        foreach (Material ghostMaterial in _ghostMaterials)
        {
            ghostMaterial.SetColor("_EmissionColor", _ghostData.GetGhostFlickeringColor());
        }
    }

    private void ChangeMaterialToFrightenedColor()
    {
        foreach (Material ghostMaterial in _ghostMaterials)
        {
            ghostMaterial.SetColor("_EmissionColor", _ghostData.GetFrightenedGhostColor());
        }
    }

    private void CheckIfSwitchOrNot()
    {
        //Neu da den luc chuyen mau Flickering (xanh -> trang -> xanh -> trang)
        if (_flickerTimer > _timeBetweenFlickering)
        {
            ResetFlickerTimer();
            _flicked = !_flicked;
            _colorChanged = false;
        }
        else
        {
            _flickerTimer += Time.deltaTime;
        }
    }

    private void CheckIfFirstTimeFlickering()
    {
        if (!_firstTimeFlickering)
        {
            _colorChanged = false;
            _firstTimeFlickering= true;
        }
    }

    private void DisappearParts()
    {
        foreach(GameObject part in _ghostPartsToDisappear)
        {
            part.SetActive(false);
        }

        _disappearParts = true;
    }

    private void ReAppearParts()
    {
        foreach (GameObject part in _ghostPartsToDisappear)
        {
            part.SetActive(true);
        }

        _disappearParts = false;
    }

    private void ResetToOriginColor()
    {
        //material chuyen mau ve binh thuong
        foreach (Material ghostMaterial in _ghostMaterials)
        {
            ghostMaterial.SetColor("_EmissionColor", _ghostData.GetNormalGhostColor());
        }

        //minimap cua ghost chuyen ve binh thuong
        _ghostMinimapEffectController.GhostMinimapIsNormal();

        _colorChanged = false;
        _firstTimeFlickering = false;
    }

    private void ResetFlickerTimer()
    {
        _flickerTimer = 0;
    }



}
