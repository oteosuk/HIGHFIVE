public class Normal_GreenOrc2_Spawner : MonsterSpawner
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        ReSpawnProcess("Monster/Normal/Normal_GreenOrc2");
    }
}