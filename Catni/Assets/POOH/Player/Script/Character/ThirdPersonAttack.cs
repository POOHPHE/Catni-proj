using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
using Cinemachine;
using UnityEngine.InputSystem;
public class ThirdPersonAttack : MonoBehaviour
{   
 
    public float aimSpeed = 20f;
    public int maxArrowStack = 4, arrowStack;
    public float arrowCooldownTime = 6f, skill1CooldownTime = 10f, skill2CooldownTime = 10f, skill3CooldownTime = 20f;
    public AudioSource skill3Sound;

    [SerializeField] private CinemachineVirtualCamera aimCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] Transform arrowPF, skill1PF, skill2PF;
    [SerializeField] Transform spawnSkillPos;
    [SerializeField] ParticleSystem skill3PF;
    [SerializeField] ParticleSystem slashVFX;
    [SerializeField] AudioSource slashSound;


    private ThirdPersonController _thirdPersonController;
    private StarterAssetsInputs _starterAssetsInputs;
    private Animator _animator;
    private float _skill1CooldownTimer = 0f, _skill2CooldownTimer = 0f, _skill3CooldownTimer = 0f;
    private List<float> _arrowCooldownTimer;

    readonly int m_HashStateTime = Animator.StringToHash("StateTime");
    private void Awake()
    {
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        _animator = GetComponent<Animator>();

        _arrowCooldownTimer = new List<float>();
    }
    void Start()
    {
        arrowStack = maxArrowStack;
    }

    void Update()
    {   
        //Skill cooldown
        if(_arrowCooldownTimer.Count > 0)
        {
            _arrowCooldownTimer[0] -= Time.deltaTime;
            if(_arrowCooldownTimer[0] <= 0f)
            {
                _arrowCooldownTimer.RemoveAt(0);
                arrowStack += 1;
            }
        }
        
        if(_skill1CooldownTimer > 0f)
        {
            _skill1CooldownTimer -= Time.deltaTime;
        }

        if (_skill2CooldownTimer > 0f)
        {
            _skill2CooldownTimer -= Time.deltaTime;
        }
        if (_skill3CooldownTimer > 0f)
        {
            _skill3CooldownTimer -= Time.deltaTime;
        }
        //animator value setting
        _animator.ResetTrigger("NormalAttack");
        _animator.ResetTrigger("Skill1");
        _animator.SetFloat(m_HashStateTime, Mathf.Repeat(_animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f));
        //mouse position
        Vector3 mouseWorldPosition = Vector3.zero;
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        {
            mouseWorldPosition = raycastHit.point;
        }

        if (_starterAssetsInputs.aim)
        {
            //Camera
            aimCamera.gameObject.SetActive(true);
            _thirdPersonController.SetSensitivity(aimSensitivity);
            _thirdPersonController.SetRotateOnMove(false);

            Vector3 worldAimTarget = mouseWorldPosition;
            worldAimTarget.y = transform.position.y;
            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * aimSpeed);

            if (_starterAssetsInputs.attack)
            {
                _starterAssetsInputs.attack = false;
                if(arrowStack > 0)
                {
                    arrowStack -= 1;
                    _arrowCooldownTimer.Add(arrowCooldownTime);

                    Vector3 aimDir = (mouseWorldPosition - spawnSkillPos.position).normalized;
                    Instantiate(arrowPF, spawnSkillPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                }
                
                
            }
            if (_starterAssetsInputs.skill1)
            {
                _starterAssetsInputs.skill1 = false;
                if(_skill1CooldownTimer <= 0f)
                {
                    _skill1CooldownTimer = skill1CooldownTime;

                    Vector3 aimDir = (mouseWorldPosition - spawnSkillPos.position).normalized;
                    Instantiate(skill1PF, spawnSkillPos.position, Quaternion.LookRotation(aimDir, Vector3.up));
                }
                
                
            }
            if (_starterAssetsInputs.skill2)
            {
                _starterAssetsInputs.skill2 = false;
                if (_skill2CooldownTimer <= 0f)
                {
                    _skill2CooldownTimer = skill2CooldownTime;

                    Vector3 aimDir = (mouseWorldPosition - spawnSkillPos.position).normalized;
                    Instantiate(skill2PF, spawnSkillPos.position, Quaternion.LookRotation(aimDir, Vector3.up));

                }

            }
            if (_starterAssetsInputs.skill3)
            {
                _starterAssetsInputs.skill3 = false;
            }
        }
        else
        {
            //Camera
            aimCamera.gameObject.SetActive(false);
            _thirdPersonController.SetSensitivity(normalSensitivity);
            _thirdPersonController.SetRotateOnMove(true);

            //Cannot attack when in air and while aim
            if (!_thirdPersonController.Grounded)
            {
                _starterAssetsInputs.attack = false;
                _starterAssetsInputs.skill1 = false;
                _starterAssetsInputs.skill2 = false;
                _starterAssetsInputs.skill3 = false;
            }
            if (_starterAssetsInputs.attack)
            {

                _starterAssetsInputs.attack = false;
                _thirdPersonController.CanMove = false;
                _animator.SetTrigger("NormalAttack");
                slashVFX.Play();
                slashSound.Play();
            }

            if (_starterAssetsInputs.skill3)
            {
                _starterAssetsInputs.skill3 = false;

                if(_skill3CooldownTimer <= 0f)
                {
                    _skill3CooldownTimer = skill3CooldownTime;
                    skill3PF.Play();
                    skill3Sound.Play();
                }
            }

            if (_starterAssetsInputs.skill2)
            {
                _starterAssetsInputs.skill2 = false;
            }

            if (_starterAssetsInputs.skill1)
            {
                _starterAssetsInputs.skill1 = false;
            }
        }
    }

    public void AttackBegin()
    {
        
    }

    public void AttackEnd()
    {
        _thirdPersonController.CanMove = true;
    }

    public void Shoot()
    {

    }


    
    
}
