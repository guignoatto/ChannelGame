using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : MonoBehaviour
{
    [SerializeField] private int _quantity360;
    [SerializeField] private GameObject _360ProjectilePrefab;
    [Header(" ")]
    [SerializeField] private int _quantityScythe;
    [SerializeField] private GameObject _scytheProjectilePrefab;
    [Header(" ")]
    [SerializeField] private int _quantityCyber;
    [SerializeField] private GameObject _swordCyberProjectilePrefab;
    [Header(" ")]
    [SerializeField] private int _quantityMachineGun;
    [SerializeField] private GameObject _machineGunProjectilePrefab;
    [Header(" ")]
    [SerializeField] private int _quantitySword;
    [SerializeField] private GameObject _swordProjectilePrefab;
    [Header(" ")]
    [SerializeField] private int _quantityLightning;
    [SerializeField] private GameObject _lightningProjectilePrefab;

    private List<ProjectileBase> _360ProjectilePool;
    private List<ProjectileBase> _scytheProjectilePool;
    private List<ProjectileBase> _swordCyberProjectilePool;
    private List<ProjectileBase> _machineGunProjectilePool;
    private List<ProjectileBase> _swordProjectilePool;
    private List<ProjectileBase> _lightningProjectilePool;

    public void Initialize()
    {
        _360ProjectilePool = InstantiatePrefabs(_360ProjectilePrefab, _quantity360);
        _scytheProjectilePool = InstantiatePrefabs(_scytheProjectilePrefab, _quantityScythe);
        _swordCyberProjectilePool = InstantiatePrefabs(_swordCyberProjectilePrefab, _quantityCyber);
        _swordProjectilePool = InstantiatePrefabs(_swordProjectilePrefab, _quantitySword);
        _machineGunProjectilePool = InstantiatePrefabs(_machineGunProjectilePrefab, _quantityMachineGun);
        _lightningProjectilePool = InstantiatePrefabs(_lightningProjectilePrefab, _quantityLightning);
    }

    public ProjectileBase GetProjectile(ISkillType skillType)
    {
        ProjectileBase projectile = null;
        var list = GetListWithType(skillType);

        if (list.Count < 1)
        {
            list.Add(Instantiate(GetPrefab(skillType), transform).GetComponent<ProjectileBase>());
        }
        projectile = list[0];
        list.RemoveAt(0);
        projectile.gameObject.SetActive(true);
        return projectile;
    }

    private List<ProjectileBase> InstantiatePrefabs(GameObject prefab, int qt)
    {
        List<ProjectileBase> projectilePool = new List<ProjectileBase>();
        for (int i = 0; i < qt; i++)
        {
            var newProjectile = Instantiate(prefab, transform).GetComponent<ProjectileBase>();
            projectilePool.Add(newProjectile);
            newProjectile.projectileDestroyed += ReturnProjectileToPool;
            newProjectile.gameObject.SetActive(false);
        }

        return projectilePool;
    }

    private void ReturnProjectileToPool(ISkillType skillType, ProjectileBase projectile)
    {
        var list = GetListWithType(skillType);
        projectile.gameObject.SetActive(false);
        projectile.transform.parent = transform;
        list.Add(projectile);
    }

    private List<ProjectileBase> GetListWithType(ISkillType skillType)
    {
        switch (skillType)
        {
            case ISkillType.SKILL360:
                return _360ProjectilePool;
            case ISkillType.SWORD:
                return _swordProjectilePool;
            case ISkillType.ORBITAL:
                return _scytheProjectilePool;
            case ISkillType.LIGHTSABER:
                return _swordCyberProjectilePool;
            case ISkillType.MACHINEGUN:
                return _machineGunProjectilePool;
            case ISkillType.LIGHTNING_SKILL:
                return _lightningProjectilePool;
            default:
                return null;
        }
    }
    
    private GameObject GetPrefab(ISkillType skillType)
    {
        switch (skillType)
        {
            case ISkillType.SKILL360:
                return _360ProjectilePrefab;
            case ISkillType.SWORD:
                return _swordProjectilePrefab;
            case ISkillType.ORBITAL:
                return _scytheProjectilePrefab;
            case ISkillType.LIGHTSABER:
                return _swordCyberProjectilePrefab;
            case ISkillType.MACHINEGUN:
                return _machineGunProjectilePrefab;
            case ISkillType.LIGHTNING_SKILL:
                return _lightningProjectilePrefab;
            default:
                return null;
        }
    }

    public List<ProjectileBase> Projectile360Pool
    {
        get { return _360ProjectilePool; }
        set { _360ProjectilePool = value; }
    }

    public List<ProjectileBase> ScytheProjectilePool
    {
        get { return _scytheProjectilePool; }
        set { _scytheProjectilePool = value; }
    }

    public List<ProjectileBase> SwordCyberProjectilePool
    {
        get { return _swordCyberProjectilePool; }
        set { _swordCyberProjectilePool = value; }
    }

    public List<ProjectileBase> SwordProjectilePool
    {
        get { return _swordProjectilePool; }
        set { _swordProjectilePool = value; }
    }

    public List<ProjectileBase> MachineGunProjectilePool
    {
        get { return _machineGunProjectilePool; }
        set { _machineGunProjectilePool = value; }
    }
    
    public List<ProjectileBase> LightningProjectilePool
    {
        get { return _lightningProjectilePool; }
        set { _lightningProjectilePool = value; }
    }
}