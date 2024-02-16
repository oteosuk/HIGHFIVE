public class Epic_RedOrc_Spawner : MonsterSpawner
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        ReSpawnProcess("Monster/Epic/Epic_RedOrc");
    }
}
