using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSyncer : MonoBehaviour
{
    [PunRPC]
    public void SetLayer(int layer)
    {
        gameObject.layer = layer;
    }

    [PunRPC]
    public void SetHpBarColor()
    {
        Image fillImage = null;
        foreach (Image component in gameObject.GetComponentsInChildren<Image>())
        {
            if (component.name == "Fill")
            {
                fillImage = component;
            }
        }
        if (fillImage != null)
        {
            if (gameObject.layer == (int)Define.Camp.Red)
            {
                fillImage.color = Define.GreenColor;
            }
            else
            {
                fillImage.color = Define.BlueColor;
            }
        }
    }
}
