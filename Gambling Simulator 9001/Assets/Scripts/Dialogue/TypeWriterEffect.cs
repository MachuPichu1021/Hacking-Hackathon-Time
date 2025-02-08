using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriterEffect : MonoBehaviour
{
    [SerializeField] private float writingSpeed = 40;

    public bool IsRunning { get; private set; }

    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char> {'.', '!', '?'}, 0.6f),
        new Punctuation(new HashSet<char> {',', ':', ';'}, 0.3f)
    };

    private Coroutine typingCoroutine;

    public void Run(string text, TMP_Text textLabel)
    {
        typingCoroutine = StartCoroutine(TypeText(text, textLabel));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeText(string text, TMP_Text textLabel)
    {
        IsRunning = true;

        textLabel.text = "";

        float t = 0;
        int charIndex = 0;

        while (charIndex < text.Length)
        {
            int lastCharIndex = charIndex;

            t += Time.deltaTime * writingSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, text.Length);

            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= text.Length - 1;
                textLabel.text = text.Substring(0, i + 1);

                if (!isLast && IsPunctuation(text[i], out float waitTime) && !IsPunctuation(text[i + 1], out _))
                    yield return new WaitForSeconds(waitTime);
            }


            yield return null;
        }

        IsRunning = false;
    }

    private bool IsPunctuation(char c, out float waitTime)
    {
        foreach(Punctuation punctuation in punctuations)
        {
            if (punctuation.Punctuations.Contains(c))
            {
                waitTime = punctuation.WaitTime;
                return true;
            }
        }

        waitTime = 0;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> Punctuations;
        public readonly float WaitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            Punctuations = punctuations;
            WaitTime = waitTime;
        }
    }
}
