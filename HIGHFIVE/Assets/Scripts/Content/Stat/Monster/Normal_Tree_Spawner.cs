using UnityEngine;

public class Normal_Tree_Spawneer : MonsterSpawner
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        CheckFull();
        if (!isFull)
        {
            _curTime -= Time.deltaTime;
            if (_curTime <= 0)
            {
                ReSpawn("Monster/Normal/Tree");
            }
        }
    }
}
