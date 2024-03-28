using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;

public class SelectorView : MonoBehaviour
{
    [SerializeField] private float speed;
    private RectTransform rectTransform;
    [SerializeField] GameObject selected;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        
        
        /*GameObject selectedGameObject = EventSystem.current.currentSelectedGameObject;
        
        selected = (selectedGameObject == null) ? selected : selectedGameObject;
        */
        
        selected = EventSystem.current.currentSelectedGameObject;
        
        if (selected == null) return;
        
        //transform.position = selected.transform.position;
        transform.position = Vector3.Lerp(transform.position, selected.transform.position, speed * Time.deltaTime);

        RectTransform otherRect = selected.GetComponent<RectTransform>();
        float horizontalLerp = Mathf.Lerp(rectTransform.rect.size.x, otherRect.rect.size.x, speed * Time.deltaTime);
        float verticalLerp = Mathf.Lerp(rectTransform.rect.size.y, otherRect.rect.size.y, speed * Time.deltaTime);
        
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, horizontalLerp);
        rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, verticalLerp);
        
    }

    public void SetSelectedGameObject(GameObject gameObject)
    {
        selected = gameObject;
        EventSystem.current.SetSelectedGameObject(selected);
    }
}