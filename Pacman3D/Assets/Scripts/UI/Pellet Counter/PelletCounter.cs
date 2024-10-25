using UnityEngine;
using TMPro;

public class PelletCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _smallPelletNumberText;
    [SerializeField] private TextMeshProUGUI _powerPelletNumberText;

    private int _currentSmallPelletNumber = 0;
    private int _maxSmallPelletNumber = 0;
    private int _currentPowerPelletNumber = 0;
    private int _maxPowerPelletNumber = 0;


    /*--- Add pellets when init ---*/
    public void AddOneSmallPelletToCounter()
    {
        _currentSmallPelletNumber = _maxSmallPelletNumber += 1;
        UpdateSmallPelletCounterText();
    }
    public void AddOnePowerPelletToCounter()
    {
        _currentPowerPelletNumber = _maxPowerPelletNumber += 1;
        UpdatePowerPelletCounterText();
    }


    /*--- update pellet number when consume pellets ---*/
    public void ConsumeSmallPellet()
    {
        _currentSmallPelletNumber -= 1;
        UpdateSmallPelletCounterText();
    }

    public void ConsumePowerPellet()
    {
        _currentPowerPelletNumber -= 1;
        UpdatePowerPelletCounterText();
    }


    /*--- update counting text ---*/
    private void UpdateSmallPelletCounterText()
    {
        _smallPelletNumberText.text = _currentSmallPelletNumber.ToString() + "/" + _maxSmallPelletNumber.ToString();

    }

    private void UpdatePowerPelletCounterText()
    {
        _powerPelletNumberText.text = _currentPowerPelletNumber.ToString() + "/" + _maxPowerPelletNumber.ToString();

    }
}
