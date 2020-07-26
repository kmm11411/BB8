using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementLogic : MonoBehaviour
{
    public float m_speed = 5f;
    private float m_horizontalMovement;
    private float m_verticalMovement;
    private bool m_hologramActive = false;
    public GameObject m_hologram;
    Rigidbody m_rigidBody;
    public float m_headRadius = .325f;
    public Transform m_ParentTransform;

    public Animator m_animator;
    public float blendTiltParam = 0;
    private float m_maxSpeed = 0;
    private Vector3 m_maxForce;

    void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        m_maxForce = new Vector3(m_speed, 0, m_speed);
        m_maxSpeed = m_maxForce.magnitude;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_hologramActive = !m_hologramActive;
        }
        m_horizontalMovement = Input.GetAxis("Horizontal") * m_speed;
        m_verticalMovement = Input.GetAxis("Vertical") * m_speed;
    }

    void FixedUpdate()
    {
        if (!m_hologramActive)
        {
            m_hologram.SetActive(m_hologramActive);

            Vector3 movement = new Vector3(m_horizontalMovement, 0.0f, m_verticalMovement);
            m_rigidBody.AddForce(movement);

            var angleY = Vector3.Dot(m_ParentTransform.transform.right, m_rigidBody.velocity.normalized) * (Mathf.Rad2Deg * Time.fixedDeltaTime * 12f);
            var headRotation = Quaternion.AngleAxis(angleY, Vector3.up);
            m_ParentTransform.rotation *= headRotation;
            if (m_rigidBody.velocity.magnitude > 2f)
            {
                blendTiltParam = Mathf.Lerp(blendTiltParam, 1, .03f);
            }
            else
            {
                blendTiltParam = Mathf.Lerp(blendTiltParam, 0, .05f);
            }

        }
        else if (m_rigidBody.velocity.magnitude <= .1f && blendTiltParam < .1f)
        {
            m_hologram.SetActive(m_hologramActive);
        }
        else
        {
            blendTiltParam = Mathf.Lerp(blendTiltParam, 0, .05f);
            m_rigidBody.velocity *= .8f;
        }

        m_animator.SetFloat("HeadTilt", blendTiltParam);
        m_ParentTransform.position = transform.position + new Vector3(0, m_headRadius, 0);

    }

}
