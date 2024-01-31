using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : UIBase
{
    private enum GameObjects
    {
        HPBar
    }

    private Stat _stat;

    private void Start()
    {
        Bind<GameObject>(typeof(GameObjects));
        _stat = transform.parent.GetComponent<Stat>();
    }

    private void Update()
    {
        Transform parent = transform.parent;
        transform.position = new Vector2(parent.position.x, parent.position.y);
        float ratio = _stat.CurHp / (float)_stat.MaxHp;
        SetHpRatio(ratio);
    }

    public void SetHpRatio(float ratio)
    {
        Get<GameObject>((int)GameObjects.HPBar).GetComponent<Slider>().value = ratio;
    }
}
