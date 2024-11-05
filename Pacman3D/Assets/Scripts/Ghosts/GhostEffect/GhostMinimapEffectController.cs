using UnityEngine;

public class GhostMinimapEffectController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _ghostNormalSprite;
    [SerializeField] private Sprite _ghostFrightenedSprite;
    [SerializeField] private Sprite _ghostFlickeringSprite;
    [SerializeField] private Sprite _ghostIsEatenSprite;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void GhostMinimapIsNormal()
    {
        _spriteRenderer.sprite = _ghostNormalSprite;
    }

    public void GhostMinimapIsFrightened()
    {
        _spriteRenderer.sprite = _ghostFrightenedSprite;
    }

    public void GhostMinimapIsFlickering()
    {
        _spriteRenderer.sprite = _ghostFlickeringSprite;
    }

    public void GhostMinimapIsEaten()
    {
        _spriteRenderer.sprite = _ghostIsEatenSprite;
    }

}
