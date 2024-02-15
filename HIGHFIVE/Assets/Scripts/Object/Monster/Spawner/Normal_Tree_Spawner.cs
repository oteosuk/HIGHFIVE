public class Normal_Tree_Spawneer : MonsterSpawner
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        ReSpawnProcess("Monster/Normal/Normal_Tree");
    }
}
