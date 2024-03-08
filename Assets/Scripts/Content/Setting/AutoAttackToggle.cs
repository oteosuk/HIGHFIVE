using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoAttackToggle : MonoBehaviour
{
    [SerializeField] Toggle autoAttackToggle;
    private void Start()
    {
        autoAttackToggle.isOn = Main.GameManager.isAutoAttack;
        autoAttackToggle.onValueChanged.AddListener(ToggleAutoAttack);
    }

    private void ToggleAutoAttack(bool isOn)
    {
        Main.GameManager.isAutoAttack = isOn;
    }
}
