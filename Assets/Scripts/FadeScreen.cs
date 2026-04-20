using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private bool fadeOnStart = true;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private Color fadeColor = Color.black;
    [SerializeField] private AnimationCurve fadeCurve;
    [SerializeField] private string colorPropertyName = "_Color";

    //private Material mat;

    private Renderer rend;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = false;
        //mat = rend.material;
        //print("mat name = " + mat.name);
        //print(mat.GetColor(colorPropertyName));


        if (fadeOnStart)
        {
            // float old = fadeDuration;
            // fadeDuration = .01f;
            FadeIn();
            //fadeDuration = old;
        }
    }

    // =========================
    // ACCESSORS (GETTERS/SETTERS)
    // =========================

    public void SetFadeDuration(float duration)
    {
        fadeDuration = Mathf.Max(0f, duration); // prevent negatives
    }

    public float GetFadeDuration()
    {
        return fadeDuration;
    }

    public void SetFadeColor(Color color)
    {
        fadeColor = color;
    }

    public Color GetFadeColor()
    {
        return fadeColor;
    }

    // Optional: useful for UnityEvent (can’t pass Color easily)
    public void SetFadeColorFromRenderer()
    {
        fadeColor = rend.material.GetColor(colorPropertyName);
    }

    // =========================

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StopAllCoroutines(); // prevents overlapping fades
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        rend.enabled = true;

        float timer = 0f;

        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;

            newColor.a = Mathf.Lerp(
                alphaIn,
                alphaOut,
                fadeCurve.Evaluate(timer / fadeDuration)
            );

            rend.material.SetColor(colorPropertyName, newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color finalColor = fadeColor;
        finalColor.a = alphaOut;
        rend.material.SetColor(colorPropertyName, finalColor);

        if (alphaOut == 0)
            rend.enabled = false;
    }
}
