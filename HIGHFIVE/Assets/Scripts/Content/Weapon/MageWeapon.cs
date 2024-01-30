using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageWeapon : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        int mask = (1 << (int)Define.Layer.Monster) | (1 << (Main.GameManager.SelectedCamp == Define.Camp.Red ? (int)Define.Layer.Blue : (int)Define.Layer.Red));
        if (collision.CompareTag("Monster"))
        {
            Debug.Log("Damage");
            //Main.GameManager.SpawnedCharacter.stat.Attack 나중에 교체
            collision.gameObject.GetComponent<DamageReceiver>()?.TakeDamage(15);
            //풀링
            Main.ResourceManager.Destroy(gameObject);
        }
        
    }
}
