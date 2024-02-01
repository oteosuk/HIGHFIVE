using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revival : MonoBehaviour
{
    private Animator Animator;
    private void Start()
    {
        Animator = GetComponent<Animator>();
    }
    public void Reviva()
    {
        Animator.SetBool("isDie", false);
        Main.ResourceManager.Destroy(transform.parent.gameObject);
    }
}
