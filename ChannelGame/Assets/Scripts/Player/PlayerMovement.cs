using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Action<ISkillType> GetWeaponTypeEvent;
    public Action PlayerStop;
    public Action PlayerWalk;
    [Range(0, 10)] 
    [SerializeField] private float _speed;

    private Rigidbody2D _rbd;
    private Vector2 _moveDirection;
    private bool _sendStop = true;
    private bool _sendWalk = true;
    
    private void Start()
    {
        _rbd = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _moveDirection.x = Input.GetAxis("Horizontal");
        _moveDirection.y = Input.GetAxis("Vertical");
        _rbd.velocity = _moveDirection * _speed;

        if (_rbd.velocity == Vector2.zero && _sendStop)
        {
            PlayerStop?.Invoke();
            _sendStop = false;
            _sendWalk = true;
        }

        if (_rbd.velocity.magnitude > 0 && _sendWalk)
        {
            PlayerWalk?.Invoke();
            _sendStop = true;
            _sendWalk = false;
        }
    }

    private void FixedUpdate()
    {
    }
}
