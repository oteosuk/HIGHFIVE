public class Elite_BossOrc_Spawner : MonsterSpawner
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        ReSpawnProcess("Monster/Elite/Elite_BossOrc");
    }
}
