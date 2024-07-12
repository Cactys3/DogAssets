using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFightManager : MonoBehaviour
{
    [SerializeField] private int PlayerHP;
    [SerializeField] private int BossHP;
    [SerializeField] private int BasicEnemyDamage;
    [SerializeField] private int BossThrustDamage;
    [SerializeField] private int BossWaterfowlDamage;
    [SerializeField] private int BossNukeDamage;
    [SerializeField] private int BossWeaponDamage;
    [SerializeField] private int PlayerThrustDamage;
    [SerializeField] private int PlayerSpinDamage;
    [SerializeField] public int PlayerThrustStaminaCost;
    [SerializeField] public int PlayerSpinStaminaCost;
    [SerializeField] public float PlayerStaminaRegen;
    [SerializeField] private BossHUDManager HUD;
    [SerializeField] private float PlayerStamina;
    private bool StaminaRegening;

    private void Start()
    {

        StaminaRegening = true;
        PlayerStamina = 100;
        PlayerStaminaRegen = 20f;
        PlayerHP = 100;
        BossHP = 100;
        PlayerThrustDamage = 20;
        PlayerSpinDamage = 10;
        PlayerThrustStaminaCost = 10;
        PlayerSpinStaminaCost = 25;
        BossThrustDamage = 10;
        BossWaterfowlDamage = 10;
        BossWeaponDamage = 10;
        BossNukeDamage = 100;
        BasicEnemyDamage = 10;
    }
    private void Update()
    {
        if (BossHP <= 0)
        {
            Win();
        }
        if (PlayerHP <= 0)
        {
            Lose();
        }
        if (PlayerStamina < 100 && StaminaRegening)
        {
            PlayerStamina += PlayerStaminaRegen * Time.deltaTime;
            HUD.UpdatePlayerStamina(PlayerStamina);
        }
    }
    public bool HasStamina(int cost)
    {
        return (PlayerStamina > cost);
    }
    public void PlayerHit(string name)
    {
        int CurrentHP = PlayerHP;
        switch(name)
        {
            case "BossThrust":
                PlayerHP -= BossThrustDamage;
                break;
            case "BossWaterfowl":
                PlayerHP -= BossWaterfowlDamage;
                break;
            case "BossNuke":
                PlayerHP -= BossNukeDamage;
                break;
            case "BossWeapon":
                PlayerHP -= BossWeaponDamage;
                break;
            case "Enemy":
                PlayerHP -= BasicEnemyDamage;
                break;
        }
        if (CurrentHP != PlayerHP)
        {
            HUD.UpdatePlayerHP(PlayerHP);
        }
    }
    public void BossHit(string name)
    {
        int CurrentHP = BossHP;
        switch (name)
        {
            case "PlayerThrust":
                BossHP -= PlayerThrustDamage;
                FindObjectOfType<BossPlayerMovement>().DisableWeapons();
                break;
            case "PlayerSlash":
                BossHP -= PlayerSpinDamage;
                FindObjectOfType<BossPlayerMovement>().DisableWeapons();
                break;
        }
        if (CurrentHP != BossHP)
        {
            HUD.UpdateBossHP(BossHP);
        }
    }
    public void Win()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void UseStamina(string name)
    {
        StopCoroutine("StaminaTimer");
        StartCoroutine("StaminaTimer");
        switch(name)
        {
            case BossPlayerMovement.ThrustAnimName:
                PlayerStamina -= PlayerThrustStaminaCost;
                HUD.UpdatePlayerStamina(PlayerStamina);
                break;
            case BossPlayerMovement.SpinAnimName:
                PlayerStamina -= PlayerSpinStaminaCost;
                HUD.UpdatePlayerStamina(PlayerStamina);
                break;
        }
    }
    IEnumerator StaminaTimer()
    {
        StaminaRegening = false;
        yield return new WaitForSeconds(1);
        StaminaRegening = true;
    }
}
