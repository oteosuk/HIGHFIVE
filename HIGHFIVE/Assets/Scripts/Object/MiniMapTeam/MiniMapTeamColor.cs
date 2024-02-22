using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapTeamColor : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    void Start()
    {
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        TeamColor();
    }

    private void TeamColor()
    {
        Character myCharacter = Main.GameManager.SpawnedCharacter;

        if (myCharacter.gameObject == gameObject)
        {
            _spriteRenderer.color = Color.green;
        }
        else if ((int)Define.Layer.Red == gameObject.layer)
        {
            _spriteRenderer.color = Color.red;
        }
        else if((int)Define.Layer.Blue == gameObject.layer)
        {
            _spriteRenderer.color = Color.blue;
        }
    }
}