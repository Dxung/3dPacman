using UnityEngine;
using UnityEngine.UI;

public class PacmanFaceAnimationController : MonoBehaviour
{
    [SerializeField] private Sprite[] _imageListToSwitch;
    [SerializeField] private float _secondBetweenWait;

    private Image _imageToSwitch;
    private int _upcomingImageIndex;
    private float _currentTime;


    private void Start()
    {
        _upcomingImageIndex = 0;
        _imageToSwitch = this.gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        AnimUpdate();
    }


    private void AnimUpdate()
    {
        if (IsTimeToSwitch())                                                   //check if its time to turn to next image?
        {
            _imageToSwitch.sprite = _imageListToSwitch[_upcomingImageIndex];         //switch image to next image
            UpdateUpcomingImageIndex();                                              //calculate the next image index
            ResetTimer();                                                            //calculate time from beginning again

        }
        else
        {
            _currentTime += Time.unscaledDeltaTime * 1f;                        //if not, increase timer
        }
    }

    private void UpdateUpcomingImageIndex()
    {
        if(_upcomingImageIndex == _imageListToSwitch.Length - 1)
        {
            _upcomingImageIndex = 0;
        }
        else
        {
            _upcomingImageIndex++;
        }
    }

    private bool IsTimeToSwitch()
    {
        return _currentTime > _secondBetweenWait;
    }

    private void ResetTimer()
    {
        _currentTime = 0;
    }

}
