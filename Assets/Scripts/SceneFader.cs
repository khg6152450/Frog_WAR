using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

    void Start()
    {
        //StartCoroutine(FadeIn());
        Debug.Log("동작이 되긴하니?2");
    }

    public void FadeTo(string scene)
    {
        Debug.Log("동작이 되긴하니?3");
        StartCoroutine(FadeOut(scene));
        Debug.Log("동작이 되긴하니?4");
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            // skip to the next frame
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;
        Debug.Log("동작이 되긴하니?5");
        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a);
            // skip to the next frame
            yield return 0;
        }
        Debug.Log("동작이 되긴하니?6");
        // load Scene
        SceneManager.LoadScene(scene);
        Debug.Log("동작이 되긴하니?7");

    }

    
}
