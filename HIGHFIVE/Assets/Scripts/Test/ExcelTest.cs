using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ExcelTest : MonoBehaviour
{
    [SerializeField] //인스펙터창에서 수동으로 연결위해
    private HIGHFIVE_Data characterData;

    void Start()
    {
        GameObject.Find("Name").GetComponent<TMP_Text>().text = characterData.Characters[0].job;
    }

    void Update()
    {
        
    }
}
