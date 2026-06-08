using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    public Text healthText;
    public Image healthBar;

    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponentInParent<PhotonView>();
        currentHealth = maxHealth;
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = (float)currentHealth / maxHealth;
        healthText.text = currentHealth.ToString();
    }

    [PunRPC]
    public void TakeDamageRPC(int damage)
    {
        currentHealth -= damage;

        if (photonView.IsMine)
        {
            UpdateHealthBar();
        }

        Debug.Log($"{gameObject.name} HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.SetActive(false);
    }
}