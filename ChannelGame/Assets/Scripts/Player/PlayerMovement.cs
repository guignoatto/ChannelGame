using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Action<ISkillType> GetWeaponTypeEvent;
    [Range(0, 10)] 
    [SerializeField] private float _speed;

    private Rigidbody2D _rbd;
    private Vector2 _moveDirection;
    
    private void Start()
    {
        _rbd = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.y = Input.GetAxis("Vertical");
    }

    private void FixedUpdate()
    {
        _rbd.MovePosition(_rbd.position + _moveDirection * _speed * Time.fixedDeltaTime);
    }
}
