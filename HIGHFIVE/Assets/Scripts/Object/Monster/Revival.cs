using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revival : MonoBehaviour
{
    private Vector2 _respawnTransform; 
    private Animator Animator;
    public MonsterAnimationData MonsterAnimationData { get; set; }
    private void Start()
    {
        Animator = GetComponent<Animator>();
        MonsterAnimationData = new MonsterAnimationData();
        _respawnTransform = transform.parent.GetComponent<Monster>()._spawnPoint;
    }
    public void Reviva()
    {
        //체력 100% 
        //isDie false
        //SpawnPoint로 귀환

        Animator.SetBool("isDie", false);
        StartCoroutine(ReSpawn());
    }

    IEnumerator ReSpawn()
    {
        //죽으면
        //콜라이더, 레이어, 테그 제거, hpBar제거
        yield return new WaitForSeconds(2.0f);

        Main.ResourceManager.Destroy(transform.parent.gameObject);
    }

    //IEnumerator RealReSpawn()
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    Debug.Log("sdds");
    //    GameObject mons = Main.ResourceManager.Instantiate($"Monster/Enemy", _respawnTransform);
    //    Debug.Log(mons);
    //}
}
