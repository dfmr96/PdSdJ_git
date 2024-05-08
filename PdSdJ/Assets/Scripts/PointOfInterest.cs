using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : MonoBehaviour, IInteractuable
{
    [SerializeField] private string text;
    [SerializeField] DescriptionPromptController _descriptionPromptController; 

    public void Interact()
    {
        _descriptionPromptController.gameObject.SetActive(true);
        StartCoroutine(_descriptionPromptController.CO_PromptText(text));
    }
}
