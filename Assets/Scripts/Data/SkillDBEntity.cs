[System.Serializable]

public class SkillDBEntity
{
    public string job;
    public string name;
    public string info;
    public float range;
    public int damage;//최종 데미지 => 데미지 + (atk * damageRatio)
    public float animTime;
    public float damageRatio;
    public float coolTime;
    public float castingTime;
    public int condition;//상태이상
}
