using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndOfDayManager : MonoBehaviour
{
    [SerializeField] private TMP_Text dayText;

    private void Start()
    {
        GameManager.instance.EndDay();
        dayText.text = $"Day {GameManager.instance.Day}";
        StartCoroutine(FadeIn(2));
    }

    private IEnumerator FadeIn(float duration)
    {
        Color startColor = new Color(1, 1, 1, 0);
        Color endColor = Color.white;
        float timeElapsed = 0;

        dayText.color = startColor;
        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            dayText.color = Color.Lerp(startColor, endColor, t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        dayText.color = endColor;
        yield return new WaitForSeconds(0.75f);

        if (GameManager.instance.Day != 5)
            GameManager.instance.LoadScene(1);
        else
            GameManager.instance.LoadScene(3);
    }
}
