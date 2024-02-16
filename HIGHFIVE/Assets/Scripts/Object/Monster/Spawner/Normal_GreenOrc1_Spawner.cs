public class Normal_GreenOrc1_Spawner : MonsterSpawner
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        ReSpawnProcess("Monster/Normal/Normal_GreenOrc1");
    }
}
