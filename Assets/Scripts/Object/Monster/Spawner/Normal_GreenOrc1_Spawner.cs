using System.Collections.Generic;
using UnityEngine;

public class Normal_GreenOrc1_Spawner : MonsterSpawner
{
    protected override void Start()
    {
        array = new KeyValuePair<Transform, GameObject>[5];
        _respawnDelayTime = 10;
        base.Start();
    }

    protected override void Update()
    {
        ReSpawnProcess("Monster/Normal/Normal_GreenOrc1");
    }
}
