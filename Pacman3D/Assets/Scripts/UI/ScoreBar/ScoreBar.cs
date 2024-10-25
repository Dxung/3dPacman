using System;
using TMPro;
using UnityEngine;

public class ScoreBar : MonoBehaviour
{
    private int _score = 0;
    private int _highScore = 0;
    private int _ghostKilled = 0;

    [SerializeField]private TextMeshProUGUI _scoreText;

    

    private void Start()
    {
        _scoreText = this.gameObject.GetComponent<TextMeshProUGUI>();
        UpdateScore();
        
    }


    public void AddPointFromSmallPellet()
    {
        _score += 10;
        UpdateScore();
    }

    public void AddPointFromPowerPellet()
    {
        ResetGhostKilledCounter();
        _score += 50;
        UpdateScore();
    }

    public void AddPointFromGhost()
    {
        _score += 200 * Convert.ToInt32(Mathf.Pow(2, _ghostKilled));
        _ghostKilled += 1;
        UpdateScore();
    }

    public int GetHighScore()
    {
        return _highScore;
    }



    /*--- private methods ---*/
    private void UpdateScore()
    {
        if (_score > _highScore)
        {
            _highScore = _score;
        }
        DisplayScore();
    }

    private void DisplayScore()
    {
        _scoreText.text = "Score: " + _score.ToString();
    }

    private void ResetGhostKilledCounter()
    {
        _ghostKilled = 0;
    }



}
