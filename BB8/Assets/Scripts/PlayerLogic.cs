using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public float m_speed = 5f;
    private float m_horizontalMovement;
    private float m_verticalMovement;

    [SerializeField]
    GameObject m_bb8Body;
    Rigidbody m_ridigBody;

    void Start()
    {
        m_ridigBody = m_bb8Body.GetComponent<Rigidbody>();
    }

    void Update() {
        m_horizontalMovement = Input.GetAxis("Horizontal");
        m_verticalMovement = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3 (m_horizontalMovement, 0.0f, m_verticalMovement);
        m_ridigBody.AddForce(movement * m_speed);
    }
}
