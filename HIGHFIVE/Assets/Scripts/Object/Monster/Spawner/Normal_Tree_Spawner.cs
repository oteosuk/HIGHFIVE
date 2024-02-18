using System.Collections.Generic;
using UnityEngine;

public class Normal_Tree_Spawneer : MonsterSpawner
{
    protected override void Start()
    {
        array = new KeyValuePair<Transform, GameObject>[5];
        _respawnDelayTime = 15;
        base.Start();
    }

    protected override void Update()
    {
        ReSpawnProcess("Monster/Normal/Normal_Tree");
    }
}
