using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float          cooldownTimer;
    //[SerializeField] private AnimationClip  attackAnim;
    [SerializeField] private Animator       animor;

    private void Start()
    {
        animor = GetComponent<Animator>();
        //anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("Attack");
            animor.SetTrigger("Attack");
            StartCoroutine(Resetanim());
        }

    }

    IEnumerator Resetanim()
    {
        yield return new WaitForSeconds(cooldownTimer);
        animor.ResetTrigger("Attack");
        Debug.Log("reset complete");
    }
}
