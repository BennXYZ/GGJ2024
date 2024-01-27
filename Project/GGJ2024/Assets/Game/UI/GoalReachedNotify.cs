using System.Collections;
using TMPro;
using UnityEngine;

public class GoalReachedNotify : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI baseText;

    [SerializeField]
    TextMeshProUGUI animatedText;

    [SerializeField]
    AnimationCurve sizeCurve;

    [SerializeField]
    AnimationCurve alphaCurve;

    [SerializeField]
    float animationDuration;

    bool show = false;

    float animationTime;

    private void Start()
    {
        baseText.enabled = false;
        animatedText.enabled = false;
    }

    public void StartAnimation()
    {
        if (!show)
        {
            show = true;

            baseText.enabled = true;
            animatedText.enabled = true;
            animationTime = 0;

            StartCoroutine(PlayAnimation());
        }
    }
    private IEnumerator PlayAnimation()
    {
        while (animationTime <= animationDuration)
        {
            animationTime += Time.deltaTime;

            animatedText.fontSize = sizeCurve.Evaluate(animationTime / animationDuration);
            Color color = animatedText.color;
            color.a = alphaCurve.Evaluate(animationTime / animationDuration);
            animatedText.color = color;
            yield return null;
        }

        animatedText.enabled = false;
    }
}
