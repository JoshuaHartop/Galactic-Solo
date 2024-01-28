using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Holds data which determines how many times each upgrade has been
/// upgraded in total.
/// </summary>
public class PlayerUpgradeData : SaveData<PlayerUpgradeData>
{
    public int healthUpgrades;
    public int bulletVelocityUpgrades;
    public int bulletCountUpgrades;
}

[RequireComponent(typeof(OutOfBoundsListener))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private string _gameOverSceneName;

    [SerializeField]
    private Transform _bulletSpawnPoint;

    [SerializeField]
    private AudioClip _fireSound;

    [SerializeField]
    private AudioClip _damageSound;

    public float playerSpeed; // modifies player speed value
    private Rigidbody2D rb; 
    private Vector2 playerDirection;
    public GameObject bullet;
    private float lastAttackTime; // number to get last time the space was pressed

    [SerializeField]
    private int _HP;
    public int _BulletUpgrade = 1; // number for how many bullets to shoot
    private float attackCD = 1f; // float to describe attack cooldown (smaller = attack more often)

    private float _BulletVelocity = 5f;

    private Camera _mainCamera;

    private OutOfBoundsListener.BoundsDirection _outOfBoundsDirectionFlags;

    private PlayerUpgradeData _playerData;

    [SerializeField]
    private int perHealthUpgradeAmount = 3;

    [SerializeField]
    private int perBulletCountUpgradeAmount = 1;

    [SerializeField]
    private float perBulletVelocityUpgradeAmount = 2.5f;

    public int HP
    {
        get
        {
            return _HP;
        }

        set
        {
            _HP = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;

        rb = GetComponent<Rigidbody2D>();

        GetComponent<OutOfBoundsListener>().onOutOfBounds += OnOutOfBounds;

        _playerData = PlayerUpgradeData.Load();

        // Don't question it
        for (int i = 1; i < _playerData.healthUpgrades; i++)
        {
            _HP += perHealthUpgradeAmount;
        }

        for (int i = _BulletUpgrade; i < _playerData.bulletCountUpgrades; i++)
        {
            _BulletUpgrade += perBulletCountUpgradeAmount;
        }

        for (float i = _BulletVelocity; i < _playerData.bulletVelocityUpgrades; i++)
        {
            _BulletVelocity += perBulletVelocityUpgradeAmount;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float directionY = Input.GetAxisRaw("Vertical"); // gets the value if the player is pressing W or S
        float directionX = Input.GetAxisRaw("Horizontal"); // gets the value if the player is pressing A or D
        playerDirection = new Vector2(directionX, directionY).normalized; // sets the player to "look" and the direction of the button(s) being pressed

        if (Input.GetKey("space") && Time.time > lastAttackTime + attackCD)
        {
            SoundManager.Instance.PlaySound(_fireSound, 0.2f);

            spawnBullet();
            lastAttackTime = Time.time;
        }

        if (_HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = playerDirection * playerSpeed;

        // Prevent player from going out of bounds
        if (_outOfBoundsDirectionFlags.HasFlag(OutOfBoundsListener.BoundsDirection.North))
        {
            if (targetVelocity.y > 0f)
                targetVelocity.y = 0f;
        }
        else if (_outOfBoundsDirectionFlags.HasFlag(OutOfBoundsListener.BoundsDirection.South))
        {
            if (targetVelocity.y < 0f)
                targetVelocity.y = 0f;
        }

        if (_outOfBoundsDirectionFlags.HasFlag(OutOfBoundsListener.BoundsDirection.East))
        {
            if (targetVelocity.x > 0f)
                targetVelocity.x = 0f;
        }
        else if (_outOfBoundsDirectionFlags.HasFlag(OutOfBoundsListener.BoundsDirection.West))
        {
            if (targetVelocity.x < 0f)
                targetVelocity.x = 0f;
        }

        rb.velocity = targetVelocity; // moves the player in the direction of the button(s) being pressed
    }

    void spawnBullet()
    {

        for (int i = 0; i < _BulletUpgrade; i++)
        {
            Vector2 pos = new Vector2(_bulletSpawnPoint.position.x, _bulletSpawnPoint.transform.position.y - (_BulletUpgrade / 2f) + i * 1f + .5f);

            // pos += new Vector2(_bulletSpawnPoint.position.x, _bulletSpawnPoint.transform.position.y);

            // Keep the rotation set in the bullet prefab
            GameObject bulletObject = Instantiate(bullet, bullet.transform.position + (Vector3)pos, bullet.transform.rotation); // spawning the bullet prefab infront of the player with the same rotation
            Bullet bulletComponent = bulletObject.GetComponent<Bullet>();
            bulletComponent.BulletVelocity = _BulletVelocity;
        }

    }

    public void takeDamage(int damage)
    {
        SoundManager.Instance.PlaySound(_damageSound, 0.33f);

        _HP = _HP - damage;
    }

    private void OnOutOfBounds(OutOfBoundsListener.BoundsDirection direction)
    {
        _outOfBoundsDirectionFlags = direction;
    }

    private void OnDestroy()
    {
        SceneManager.LoadScene(_gameOverSceneName);
    }

}
