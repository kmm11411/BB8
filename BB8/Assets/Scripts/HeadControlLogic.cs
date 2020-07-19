using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadControlLogic : MonoBehaviour
{
    Rigidbody m_rigidbody;
    public float m_torqueScale = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        var target = Vector3.up;
        var current = m_rigidbody.transform.forward;

        var x = Vector3.Cross(target, current);
        var theta = Mathf.Asin(x.magnitude);

        var w = x.normalized * (theta / Time.fixedDeltaTime * m_torqueScale);
        var q = m_rigidbody.transform.rotation * m_rigidbody.inertiaTensorRotation;

        w = Quaternion.Inverse(q) * w;

        var t = q * Vector3.Scale(m_rigidbody.inertiaTensor, w);

        m_rigidbody.AddTorque(t, ForceMode.Force);
    }
}
