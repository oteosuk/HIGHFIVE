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
        //BlueBuff blueBuff = new BlueBuff();
        //shooter.GetComponent<BuffController>()?.AddBuff(blueBuff);
    }
}
