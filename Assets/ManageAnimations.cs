using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageAnimations : MonoBehaviour
{
    [SerializeField] private DuckController duck;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        duck.OnHit += Duck_OnHit;
        animator = GetComponent<Animator>();
    }

    private void Duck_OnHit(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Hit");
    }
}
