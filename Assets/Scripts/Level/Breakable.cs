using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    public bool isSecret;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("P_Attack"))
        {
            if(isSecret) { AudioManager.sound.TriggerSound("Secret"); }

            Destroy(gameObject);
        }
    }
}
