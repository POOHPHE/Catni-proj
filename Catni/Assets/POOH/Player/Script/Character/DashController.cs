using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class DashController : MonoBehaviour
{
    [Tooltip("Dashing duration in seconds.")]
    public float dashDuration = 0.15f;

    [Tooltip("Dashing impulse, e.g. an instant velocity change while dashing.")]
    public float dashImpulse = 10.0f;

    private ThirdPersonController _thirdPersonController;
    private StarterAssetsInputs _starterAssetsInputs;
    private Rigidbody _rd;
    private CharacterController _controller;
    private Animator _animator;

    private bool _isDashing;
    private float _dashingTime;
    private bool useInAir;
    private void Awake()
    {   
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _controller = GetComponent<CharacterController>();
        _rd = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();

        _isDashing = false;
        _dashingTime = 0.0f;
        useInAir = false;
        
    }
    void Update()
    {
        if (_thirdPersonController.Grounded)
        {
            useInAir = false;
        }
        if (_starterAssetsInputs.dash)
        {
            _starterAssetsInputs.dash = false;
            if (!_thirdPersonController.Grounded && !useInAir)
            {
                useInAir = true;
                Dash();
            }
            else if(_thirdPersonController.Grounded)
            {
                Dash();
            }
        }

        if (IsDashing())
        {
            _thirdPersonController.CanMove = false;
            Dashing();
        }
        
    }
    public void Dash()
        {
            if (IsDashing())
                return;
            _animator.SetBool("Dash", true);
            _isDashing = true;
            _dashingTime = 0.0f;
        }
    public bool IsDashing()
    {
        return _isDashing;
    }

    /// <summary>
    /// Starts a dash.
    /// </summary>

    

    /// <summary>
    /// Stops the character from dashing.
    /// </summary>

    public void StopDashing()
    {
        if (!IsDashing())
            return;
        _animator.SetBool("Dash", false);
        _isDashing = false;
        _dashingTime = 0.0f;
        _thirdPersonController.CanMove = true;
        // Cancel dash momentum, if not grounded, preserve gravity

        if (_thirdPersonController.Grounded)
            _rd.velocity = Vector3.zero;      
        else
            _rd.velocity = Vector3.Project(_rd.velocity, transform.up);
            useInAir = true;
        //_rd.velocity = Vector3.zero;
        //_controller.Move(Vector3.zero);
    }

    /// <summary>
    /// Handle Dashing state.
    /// </summary>

    protected virtual void Dashing()
    {
        // Bypass acceleration, deceleration and friction while dashing

        //_rd.Move(moveDirection * dashImpulse, dashImpulse);
        //_rd.AddForce(transform.forward * dashImpulse);
        _controller.Move(transform.forward * dashImpulse * Time.deltaTime);
        // cancel any vertical velocity while dashing on air (e.g. Cancel gravity)

        if (!_thirdPersonController.Grounded)
        {
            _rd.velocity = Vector3.ProjectOnPlane(_rd.velocity, transform.up);
            //_controller.Move();
        }
            

        // Update dash timer, if time completes, stops dashing

        _dashingTime += Time.deltaTime;

        if (_dashingTime > dashDuration)
            StopDashing();
    }
}
