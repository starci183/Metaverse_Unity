using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainController : SingletonPersistent<MainController>
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private Rigidbody _rigidbody;

    [SerializeField]
    private MeshCollider _messCollider;

    [SerializeField]
    private float _movementSpeed;

    [SerializeField]
    private float _xSensitivity;

    [SerializeField]
    private float _jumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("CityMap");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");
        
        if (horizontalInput != 0 || verticalInput != 0)
        {
            var velocityVector = transform.TransformDirection(new Vector3(horizontalInput, 0f, verticalInput).normalized);
            var velocity = _movementSpeed * velocityVector;
            velocity.y = _rigidbody.velocity.y;

            _rigidbody.velocity = velocity;

            _animator.SetFloat("SpeedParam", 1f);
        } else
        {
            _rigidbody.velocity = Vector3.zero;
            _animator.SetFloat("SpeedParam", 0f);
        }

        if (Input.GetMouseButton(0))
        {
            var xMouseInput = Input.GetAxis("Mouse X");
            transform.Rotate(Vector3.up, xMouseInput * Time.deltaTime * _xSensitivity);
        }

        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            _animator.SetTrigger("JumpTriggerParam");
        }
        Debug.Log(IsGrounded());
    }

    private readonly float extraHeight = .01f;
    private bool IsGrounded()
    {
        return Physics.Raycast(_messCollider.bounds.center, Vector3.down, _messCollider.bounds.extents.y + extraHeight);
    }
}
