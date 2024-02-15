public class Epic_BlueOrc_Spawner : MonsterSpawner
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        ReSpawnProcess("Monster/Epic/Epic_BlueOrc");
    }
}
