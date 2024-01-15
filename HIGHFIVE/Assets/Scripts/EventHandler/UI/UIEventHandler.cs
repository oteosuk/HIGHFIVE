using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIEventHandler : MonoBehaviour, IPointerClickHandler
{
    public Action<PointerEventData> ClickAction;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (ClickAction != null)
        {
            ClickAction.Invoke(eventData);
        }
    }
}
