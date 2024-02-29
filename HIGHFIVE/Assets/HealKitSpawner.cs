using System.Collections;
using TMPro;
using UnityEngine;

public class HealKitSpawner : MonoBehaviour
{
    public GameObject healKitPrefab;
    private readonly float _respawnTime = 20f;

    [SerializeField] private TMP_Text _healkitCoolTime;


    public void healRespawn(GameObject gameObj)
    {
        gameObj.SetActive(false);
        StartCoroutine(HealKitSpawnAfterDelay(gameObj));
    }

    IEnumerator HealKitSpawnAfterDelay(GameObject gameObj)
    {
        float remainingTime = _respawnTime;
        while (remainingTime > 0)
        {
            _healkitCoolTime.text = remainingTime.ToString("00");
            yield return new WaitForSeconds(1f);
            remainingTime -= 1f;
        }

        _healkitCoolTime.text = "00";
        gameObj.SetActive(true);
    }
}