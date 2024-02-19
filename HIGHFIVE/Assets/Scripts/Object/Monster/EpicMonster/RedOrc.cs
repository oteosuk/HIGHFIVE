using UnityEngine;

public class RedOrc : MonoBehaviour
{
    private StatController StatController;

    private void Start()
    {
        StatController = GetComponent<StatController>();
        StatController.buffEvent += AddRedBuff;
    }

    private void AddRedBuff(GameObject shooter)
    {
        RedBuff redBuff = new RedBuff();
        shooter.GetComponent<BuffController>()?.AddBuff(redBuff);
    }
}