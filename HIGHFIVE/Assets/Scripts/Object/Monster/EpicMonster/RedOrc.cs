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
        //BlueBuff blueBuff = new BlueBuff();
        //shooter.GetComponent<BuffController>()?.AddBuff(blueBuff);
    }
}
