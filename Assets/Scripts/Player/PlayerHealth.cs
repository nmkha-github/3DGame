using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public GameObject player;
    public GameObject gun;
    [Header("Health Bar")]
    private float health;
    private float lerpTimer;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public TextMeshProUGUI healthText;

    [Header("Damge Overlay")]
    public Image overlay;
    public float duration = 2f;
    public float fadeSpeed = 1.5f;

    [Header("Game Over")]
    public GameObject gameOverPanel;

    private float durationTimer;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();
        if (overlay.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if (durationTimer > duration)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha -= Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }
        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }
        healthText.text = health + "/" + maxHealth;
    }

    public void TakeDamage(float damage)
    {
        health = Mathf.Max(-1.0f, health - damage);
        lerpTimer = 0f;
        durationTimer = 0f;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 1);

        if (health == 0)
        {
            GameOver();
        }
    }

    public void RestoreHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }

    void GameOver()
    {
         Time.timeScale = 0.2f;
        Instantiate(Resources.Load("Prefabs/Explosion") as GameObject, transform.position + transform.forward, transform.rotation);
        player.GetComponent<PlayerMotor>().enabled = false;
        player.GetComponent<PlayerLook>().enabled = false;
        player.GetComponent<PlayerInputManager>().enabled = false;
        Destroy(GameObject.Find("Crosshair"));
        Destroy(gun);
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }
}
