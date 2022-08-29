using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillProjectile : MonoBehaviour
{
    
    enum SkillType
    {
        Arrow,
        Damage,
        Stunned
    };
    [SerializeField] SkillType _skillType;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
