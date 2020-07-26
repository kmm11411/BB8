using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OLDMovementLogic : MonoBehaviour
{
    public float m_speed = 5f;
    public float m_torqueScale = 0.3f;
    private float m_horizontalMovement;
    private float m_verticalMovement;

    public Rigidbody m_HeadRigidBody;
    Rigidbody m_ridigBody;

    void Start()
    {
        m_ridigBody = GetComponent<Rigidbody>();
    }

    void Update() {
        m_horizontalMovement = Input.GetAxis("Horizontal") * m_speed;
        m_verticalMovement = Input.GetAxis("Vertical") * m_speed;
    }

    void FixedUpdate()
    {
        m_HeadRigidBody.position = transform.position;
        m_HeadRigidBody.AddTorque(m_ridigBody.angularVelocity);
        HeadUp(m_HeadRigidBody, m_torqueScale);

        var angle = Vector3.Dot(m_HeadRigidBody.transform.right, m_ridigBody.velocity.normalized) * (Mathf.Rad2Deg * Time.deltaTime * 12f);
        var quar = Quaternion.AngleAxis(angle, Vector3.forward);
        m_HeadRigidBody.rotation *= quar;

        Vector3 movement = new Vector3 (m_horizontalMovement, 0.0f, m_verticalMovement);
        m_ridigBody.AddForce(movement);
    }

    void LateUpdate() {
        m_HeadRigidBody.transform.position = transform.position;
    }

    void HeadUp(Rigidbody body, float scale)
    {
        var target = Vector3.up;
        var current = body.transform.forward;
        // Axis of rotation
        var x = Vector3.Cross(current, target);
        var theta = Mathf.Asin(x.magnitude);
        // Change in angular velocity
        var w = x.normalized * (theta / Time.fixedDeltaTime * scale);
        // Current rotation in world space
        var q = body.rotation * body.inertiaTensorRotation;
        // Transform to local space
        w = Quaternion.Inverse(q) * w;
        // Calculate torque and convert back to world space
        var T = q * Vector3.Scale(body.inertiaTensor, w);
        body.AddTorque(T, ForceMode.Force);
    }
}
