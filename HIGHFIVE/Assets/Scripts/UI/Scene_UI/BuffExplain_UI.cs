using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuffExplain_UI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject _info;
    [SerializeField] TMP_Text _buffName;
    [SerializeField] TMP_Text _buffInfo;
    private List<BaseBuff> _activeBuff;

    private void Start()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;
        _activeBuff = myCharacter.GetComponent<BuffController>().onBuffList;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        _info.SetActive(true);

        GameObject targetObject = eventData.pointerEnter;

        foreach (BaseBuff buff in _activeBuff)
        {
            if (targetObject.name == buff.ToString())
            {
                buff.RenewalInfo();
                _buffName.text = buff.buffData.buffName;
                _buffInfo.text = buff.buffData.info;
            }
        }
        
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        _info.SetActive(false);
    }

}
