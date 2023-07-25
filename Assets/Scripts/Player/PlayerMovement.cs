using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Action<ISkillType> GetWeaponTypeEvent;
    public Action PlayerStop;
    public Action PlayerWalk;
    public EnemyDetectionField enemyDetectionField;
    
    [Range(0, 10)] 
    [SerializeField] private float _speed;

    private Rigidbody2D _rbd;
    private Vector2 _moveDirection;
    private bool _sendStop = true;
    private bool _sendWalk = true;

    private Vector2 minMapBounds; 
    private Vector2 maxMapBounds; 

    private void Start()
    {

        MapGenerator mapGenerator = FindObjectOfType<MapGenerator>();
        float mapWidth = mapGenerator.mapWidth * mapGenerator.chunkWidth;
        float mapHeight = mapGenerator.mapHeight * mapGenerator.chunkHeight;
        minMapBounds = new Vector2(-mapWidth / 2f, -mapHeight / 2f);
        maxMapBounds = new Vector2(mapWidth / 2f, mapHeight / 2f);

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

        // Clamp the player's position to within the map bounds
        Vector2 clampedPosition = new Vector2(Mathf.Clamp(transform.position.x, minMapBounds.x, maxMapBounds.x), Mathf.Clamp(transform.position.y, minMapBounds.y, maxMapBounds.y));
        transform.position = (Vector3)clampedPosition;
    }
}
