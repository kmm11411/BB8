using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BB8ParentLogic : MonoBehaviour
{
    public Transform m_BodyTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = m_BodyTransform.position;
    }
}
