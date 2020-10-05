using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubtitlesController : MonoBehaviour
{
    public static SubtitlesController Instance;

    public List<string> queue;
    private Text subtitleText;

    private bool activeNow;

    private void Awake() {
        queue = new List<string>();
        subtitleText = GetComponent<Text>();
        Instance = this;
    }

    private void Update()
    {
        
    }

    public void Show(string text)
    {
        queue.Add(text);

        if (!activeNow)
        {
            StartCoroutine(ShowTexts());
        }
    }

    private float GetShowingTime(int length)
    {
        return Math.Min(length / 10, 3f);
    }
    
    private IEnumerator ShowTexts()
    {
        activeNow = true;

        while (queue.Count > 0)
        {
            var subtitle = queue[0];
            queue.RemoveAt(0);
            subtitleText.text = subtitle;
            yield return new WaitForSeconds(GetShowingTime(subtitle.Length));
            subtitleText.text = "";

        }
        
        activeNow = false;

    }
}
