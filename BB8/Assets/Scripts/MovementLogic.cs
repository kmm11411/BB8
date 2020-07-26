using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLogic : MonoBehaviour
{
    public float m_speed = 5f;
    private float m_horizontalMovement;
    private float m_verticalMovement;

    Rigidbody m_rigidBody;
    public float m_headRadius = .325f;
    public Transform m_ParentTransform;

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
        //Vector3.
        Debug.Log(Vector3.SignedAngle(m_ParentTransform.position, transform.position + new Vector3(0, m_headRadius, 0),Vector3.up));
        var angle = Vector3.Dot(m_ParentTransform.transform.right, m_rigidBody.velocity.normalized) * (Mathf.Rad2Deg * Time.fixedDeltaTime * 12f); ;
        var q = Quaternion.AngleAxis(angle, Vector3.up);
        m_ParentTransform.position = transform.position + new Vector3(0, m_headRadius, 0);
        m_ParentTransform.rotation *= q;
        //m_ParentTransform.Rotate(direction);
    }


    /*
     * var angle = Vector3.Dot(Head.transform.right, rbody.velocity.normalized) * (Mathf.Rad2Deg * Time.fixedDeltaTime * 12f);
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        Head.rotation *= q;
    */
}
