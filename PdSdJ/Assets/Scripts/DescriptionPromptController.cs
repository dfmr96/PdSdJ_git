using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DescriptionPromptController : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private void Awake()
    {
        text.SetText(string.Empty);
    }

    public IEnumerator CO_PromptText(string newText)
    {
        Time.timeScale = 0;
        text.SetText(newText);
        yield return new WaitUntil(() => Input.GetButtonDown("Submit"));
        text.SetText(string.Empty);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}
