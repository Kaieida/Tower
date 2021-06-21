using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AttackingCo());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator AttackingCo()
    {
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attacking();
            }
            yield return null;
        }
    }

    private void Attacking()
    {
        Debug.Log("Attacking");
    }
}
