using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Camera movement class, hard coded no longer needed due to cinemachine revolution
public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float f;
    public Vector2 maxPosition;
    public Vector2 minPosition;

    public Animator anim;

    public VectorValue camMin;
    public VectorValue camMax;
    void Start()
    {
        maxPosition = camMax.positionValue;
        minPosition = camMin.positionValue;
        anim = GetComponent<Animator>();
    }


    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position, targetPosition, f);
        }
    }

    public void BeginScreenKick()
    {
        anim.SetBool("kickActive", true);
        StartCoroutine(KickCo());
    }

    public IEnumerator KickCo()
    {
        yield return null;
        anim.SetBool("kickActive", false);
    }
}