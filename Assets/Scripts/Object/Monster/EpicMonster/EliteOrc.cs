using UnityEngine;

public class EliteOrc : MonoBehaviour
{
    private StatController StatController;

    private void Start()
    {
        StatController = GetComponent<StatController>();
        StatController.buffEvent += AddEliteBuff;
    }

    private void AddEliteBuff(GameObject shooter)
    {
        EliteBuff eliteBuff = new EliteBuff();
        shooter.GetComponent<BuffController>()?.AddBuff(eliteBuff);
    }
}