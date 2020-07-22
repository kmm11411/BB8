using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLogic : MonoBehaviour
{
    public float m_speed = 5f;
    private float m_horizontalMovement;
    private float m_verticalMovement;

    Rigidbody m_rigidBody;

    //public Transform m_ParentTransform;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    void Update() {
        m_horizontalMovement = Input.GetAxis("Horizontal") * m_speed;
        m_verticalMovement = Input.GetAxis("Vertical") * m_speed;
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3 (m_horizontalMovement, 0.0f, m_verticalMovement);
        m_rigidBody.AddForce(movement);
        //m_ParentTransform.position = transform.position;
        //m_ParentTransform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
    }
}
