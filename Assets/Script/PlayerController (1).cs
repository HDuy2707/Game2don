using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI; // cần để dùng Slider

public class PlayerController : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private float _jumpForce = 10f;

    private Rigidbody2D _rb;

    public PlayerDisplayName playerDisplayName;

    // Thanh máu UI
    [SerializeField] private Slider healthBar; // gán slider thanh máu trong Inspector

    // Máu tối đa
    private const int MaxHealth = 100;

    // Networked property để lưu máu hiện tại
    [Networked, OnChangedRender(nameof(OnHealthChanged))]
    public int Health { get; set; } = MaxHealth;

    // Networked property để lưu tên người chơi
    [Networked, OnChangedRender(nameof(OnNameChanged))]
    public NetworkString<_16> PlayerName { get; private set; }

    [Networked, OnChangedRender(nameof(OnCoinChanged))]
    public int Coins { get; set; } = 0;

    public void OnHealthChanged()
    {
        UpdateHealthUI();
        Debug.Log($"Health updated: {Health}");
    }

    public void OnCoinChanged()
    {
        Debug.Log($"Coins collected: {Coins}");
        if (playerDisplayName != null)
        {
            playerDisplayName._coinsText.text = Coins.ToString();
        }
    }

    public void OnNameChanged()
    {
        Debug.Log($"Player name changed to: {PlayerName}");
        if (playerDisplayName != null)
        {
            playerDisplayName._nameText.text = PlayerName.ToString();
        }
    }

    public override void Spawned()
    {
        _rb = GetComponent<Rigidbody2D>();
        playerDisplayName = GetComponent<PlayerDisplayName>();

        PlayerName = PlayerPrefs.GetString("PlayerName", "Player");
        Coins = 0;
        RPC_InfoPlayerJoinedGame(PlayerName.ToString());

        Health = MaxHealth;

        UpdateHealthUI();

    }

    [Rpc(RpcSources.All, RpcTargets.All)]
    public void RPC_InfoPlayerJoinedGame(string name)
    {
        Debug.Log($"......Player Joined: {name}");
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority)
            return;

        // Lấy input di chuyển từ người chơi
        float moveInput = Input.GetAxis("Horizontal");
        Vector2 velocity = _rb.linearVelocity;
        velocity.x = moveInput * _moveSpeed;
        _rb.linearVelocity = velocity;

        // Xử lý nhảy
        if (Input.GetButtonDown("Jump") && Mathf.Abs(_rb.linearVelocity.y) < 0.01f)
        {
            _rb.AddForce(new Vector2(0f, _jumpForce), ForceMode2D.Impulse);
        }
    }

    public override void Render()
    {
        if (playerDisplayName != null)
        {
            playerDisplayName._nameText.text = PlayerName.ToString();
            playerDisplayName._coinsText.text = Coins.ToString();
        }

        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = MaxHealth;
            healthBar.value = Health;
        }
    }

    // Ví dụ hàm trừ máu (có thể gọi khi player bị sát thương)
    public void TakeDamage(int damage)
    {
        if (!Object.HasStateAuthority) return;

        Health -= damage;
        if (Health < 0) Health = 0;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Item"))
        {
            RPC_RequestPickItem();
        }
    }



    [Rpc(RpcSources.InputAuthority, RpcTargets.InputAuthority)]
    public void RPC_RequestPickItem()
    {
        RPC_ConfirmPickItem();

        RPC_DenyPickItem();
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.InputAuthority)]
    public void RPC_ConfirmPickItem()
    {
        Coins += 1;
    }

    [Rpc(RpcSources.InputAuthority, RpcTargets.InputAuthority)]
    public void RPC_DenyPickItem()
    {
        Debug.Log("Can not  pick up item.");
    }


}
