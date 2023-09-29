using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    [SerializeField] private Image _fadeImage;

    public static FadeManager instance;

    private void Awake()
    {
        if (instance != null) {
            Destroy(this);
        } else
        {
            instance = this;
        }
        FadeIn(0f);
    }

    private void Start()
    {
        FadeOut(2f);
    }

    public void FadeIn(float duration)
    {
        _fadeImage.DOFade(1f, duration);
    }

    public void FadeOut(float duration)
    {
        _fadeImage.DOFade(0f, duration);
    }

}
