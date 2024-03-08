using UnityEngine;

public class BlueOrc : MonoBehaviour
{
    private StatController StatController;
    
    private void Start()
    {
        StatController = GetComponent<StatController>();
        StatController.buffEvent += AddBlueBuff;
    }

    private void AddBlueBuff(GameObject shooter)
    {
        BlueBuff blueBuff = new BlueBuff();
        shooter.GetComponent<BuffController>()?.AddBuff(blueBuff);
    }
}