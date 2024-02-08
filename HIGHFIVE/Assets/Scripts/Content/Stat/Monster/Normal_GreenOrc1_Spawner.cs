using UnityEngine;

public class Normal_GreenOrc1_Spawner : MonsterSpawner
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
                ReSpawn("Monster/GreenOrc1");
            }
        }
    }
}
