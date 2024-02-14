using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SkillExplan_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject _explain;
    public void OnPointerEnter(PointerEventData eventData)
    {
        _explain.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _explain.SetActive(false);
    }

}
