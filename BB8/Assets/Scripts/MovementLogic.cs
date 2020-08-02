using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MovementLogic : MonoBehaviour
{
    public float m_speed = 5f;
    private float m_horizontalMovement;
    private float m_verticalMovement;
    private bool m_hologramActive = false;
    private bool m_blowTorchActive = false;
    private bool m_electricArkActive = false;
    public GameObject m_hologram;
    public VisualEffect m_blowTorchEffect;
    public VisualEffect m_electricArkEffect;

    Rigidbody m_rigidBody;
    public float m_headRadius = .325f;
    public Transform m_ParentTransform;

    public Animator m_armAnimator;
    public bool m_armActive = false;

    public Animator m_headAnimator;
    public float blendTiltParam = 0;
    private float m_maxSpeed = 0;
    private Vector3 m_maxForce;

    private float m_horizontalInput = 0;
    private float m_verticalInput = 0;
    private Vector3 m_movementInput;

    GameObject m_camera;

    void Start()
    {
        m_camera = Camera.main.gameObject;
        m_rigidBody = GetComponent<Rigidbody>();
        m_blowTorchEffect.Stop();
        m_electricArkActive.Stop();
    }

    void Update()
    {
        m_maxForce = new Vector3(m_speed, 0, m_speed);
        m_maxSpeed = m_maxForce.magnitude;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            m_hologramActive = !m_hologramActive;
        } else if (Input.GetKeyDown(KeyCode.V)) {
            //SwitchBlowTorch();
            m_armAnimator.SetTrigger("RaiseArm");
            m_armActive = true;
        }

        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");

        m_movementInput = new Vector3(m_horizontalInput, 0, m_verticalInput);

        var forward = m_camera.transform.forward;
        var right = m_camera.transform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        var desiredMoveDirection = forward * m_verticalInput + right * m_horizontalInput;

        m_horizontalMovement = desiredMoveDirection.x * m_speed;
        m_verticalMovement = desiredMoveDirection.z * m_speed;

        //transform.rotation = Quaternion.LookRotation(desiredMoveDirection);

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

        m_headAnimator.SetFloat("HeadTilt", blendTiltParam);
        m_ParentTransform.position = transform.position + new Vector3(0, m_headRadius, 0);

    }

    void FixedUpdate()
    {
        

    }

    void SwitchBlowTorch() {
        m_blowTorchActive = !m_blowTorchActive;

        if(m_blowTorchActive) {
            m_blowTorchEffect.Play();
        } else {
            m_blowTorchEffect.Stop();
        }
    }

}
