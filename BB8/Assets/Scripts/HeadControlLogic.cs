using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadControlLogic : MonoBehaviour
{
    public Transform m_body;

    // Start is called before the first frame update
    void Start()
    {

    }

    void LateUpdate()
    {
        transform.position = m_body.transform.position;
        //transform.position = new Vector3 (m_body.position.x, m_body.position.y + m_body.localScale.y / 2f, m_body.position.z);
    }
}
