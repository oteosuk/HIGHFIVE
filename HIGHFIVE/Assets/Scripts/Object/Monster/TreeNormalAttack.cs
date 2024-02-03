using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNormalAttack : MonoBehaviour
{
    private Tree tree;
    private void Start()
    {
        tree = transform.parent.GetComponent<Tree>();
    }
    public void OnNormalAttack()
    {
        if (tree.targetObject != null) 
        { 
            tree.targetObject.GetComponent<Stat>()?.TakeDamage(tree.stat.Attack);
        }
    }
}
