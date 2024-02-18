using System.Collections.Generic;
using UnityEngine;

public class Epic_RedOrc_Spawner : MonsterSpawner
{
    protected override void Start()
    {
        array = new KeyValuePair<Transform, GameObject>[1];
        _respawnDelayTime = 30;
        base.Start();
    }

    protected override void Update()
    {
        ReSpawnProcess("Monster/Epic/Epic_RedOrc");
    }
}
