using System.Collections.Generic;
using UnityEngine;

public class Epic_BlueOrc_Spawner : MonsterSpawner
{
    protected override void Start()
    {
        array = new KeyValuePair<Transform, GameObject>[1];
        _respawnDelayTime = 60;
        base.Start();
    }

    protected override void Update()
    {
        ReSpawnProcess("Monster/Epic/Epic_BlueOrc");
    }
}
