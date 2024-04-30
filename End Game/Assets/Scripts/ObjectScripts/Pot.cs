using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//destructable objects
public class Pot : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyPot()
    {
        _animator.SetBool("isDestroyed", true);
        StartCoroutine(Breakroutine());
    }

    IEnumerator Breakroutine()
    {
        yield return new WaitForSeconds(.3f);
        this.gameObject.SetActive(false);
    }
}
