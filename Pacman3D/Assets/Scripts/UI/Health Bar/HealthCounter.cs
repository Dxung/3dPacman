using UnityEngine;

public class HealthCounter : MonoBehaviour
{
    private int _playerHealth = 3;
    [SerializeField] private GameObject[] _healthIcons;

    private void Start()
    {
        UpdatePlayerHealth();
    }

    public void UpdatePlayerHealth()
    {
        if (_playerHealth == 0)
        {
            foreach (GameObject healthobject in _healthIcons)
            {
                healthobject.SetActive(false);
            }
        }
        else
        {
            /*--- turn on/off health icons that equals to number of player's health ---*/
            for (int i = 0; i < _playerHealth; i++)
            {
                _healthIcons[i].SetActive(true);
            }
            for (int i = _playerHealth; i < _healthIcons.Length; i++)
            {
                _healthIcons[i].SetActive(false);
            }
        }
    }

    public bool CheckIfPlayerDead()
    {
        _playerHealth -= 1;
        if (_playerHealth == 0)
        {
            return true;
        }

        else
        {
            return false;
        }
    }


}
