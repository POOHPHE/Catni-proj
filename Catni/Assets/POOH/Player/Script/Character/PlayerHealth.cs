using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public float maxHP;
    public float maxShield;
    public float hpRegenRate;
    public float shieldRegenRate;
    public float shieldCooldownTime;

    private float _myHP;
    private float _myShield;
    private float _shielCooldown;
    private Slider _hpBar, _shieldBar;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.GetContact(0));
    }


}
