using UnityEngine;

public class ShieldManager : MonoBehaviour
{

    [SerializeField] private Renderer _playerRenderer;
    private Color _originalColor;
    [SerializeField] private Color _shieldColor;

    [SerializeField] private float _maxHoldTime;
    private float _currentHoldTime;

    public bool isShieldActive { get; private set; }

    private void Start()
    {
        _originalColor = _playerRenderer.material.color;
    }

    public void ButtonPressed()
    {
        isShieldActive = true;
        _playerRenderer.material.color = _shieldColor;
    }

    public void ButtonReleased()
    {
        isShieldActive = false;
        _currentHoldTime = 0;
        _playerRenderer.material.color = _originalColor;
    }

    private void Update()
    {
        if (!isShieldActive) return;

        _currentHoldTime += Time.deltaTime;

        if (_currentHoldTime >= _maxHoldTime)
        {
            ButtonReleased();
        }
    }
}
