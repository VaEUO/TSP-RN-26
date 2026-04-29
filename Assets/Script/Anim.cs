using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Anim : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        //aniamtor=GameObject.FindGameObjectsWithTag("luchadora").GetComponent<Animator>//
    }

    public void PlayAnim()

    {
        animator.enabled = true;
        animator.Play("animación");
    }


    public void StopAnim()

    {
        animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
