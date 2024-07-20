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
    [SerializeField] private BossAIScript boss;
    [SerializeField] private BossPlayerMovement player;
    [SerializeField] private ChangeToScene SceneChanger;
    [SerializeField] private float PlayerStamina;
    [SerializeField] public int PhaseNum;
    [SerializeField] private GameObject PhaseTransitionExplosion;
    [SerializeField] private GameObject BounceCollider;
    private bool StaminaRegening;

    private bool won;

    private float Stopwatch;

    private void Start()
    {
        BounceCollider.SetActive(false);
        won = false;
        PhaseTransitionExplosion.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.P))
        {
            BossHP = 0;
        }


        Stopwatch = Time.timeSinceLevelLoad;
        if (PhaseNum == 1 && BossHP == 5 && !won)
        {
            Win();
            won = true;
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
                player.DisableWeapons();
                break;
            case "PlayerSlash":
                BossHP -= PlayerSpinDamage;
                player.DisableWeapons();
                break;
        }
        if (BossHP < 5 && PhaseNum == 1)
        {
            BossHP = 5; //can only go to 5hp for phase one
        }
        if (CurrentHP != BossHP)
        {
            HUD.UpdateBossHP(BossHP);
        }
    }
    public void Win()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 0f;
        boss.DisableEverything();
        player.DisableEverything();
        BounceCollider.SetActive(true);

        //TODO: set player and boss velocities and make explosion animation happen
        PhaseTransitionExplosion.SetActive(true);
        PhaseTransitionExplosion.transform.position = new Vector3((player.transform.position.x + boss.transform.position.x) / 2, (player.transform.position.y + boss.transform.position.y) / 2, 0);
        PhaseTransitionExplosion.GetComponent<Animator>().Play("Explosion");

        float ExplosionForce = 2000;

        Time.timeScale = 1f;
        player.body.AddForce((player.transform.position - boss.transform.position).normalized * ExplosionForce);
        Debug.Log((player.transform.position - boss.transform.position).normalized + " is " + (player.transform.position - boss.transform.position).normalized * ExplosionForce);
        boss.body.AddForce((boss.transform.position - player.transform.position).normalized * ExplosionForce);

        

        StartCoroutine("StartPhaseTwo");
    }
    IEnumerator StartPhaseTwo()
    {
        boss.SetupPhaseTwo();
        player.SetupPhase2();
        yield return new WaitUntil(() => (player.PlayerInPosition() && boss.BossInPosition()));
        //TODO: go to phase 2
        boss.PhaseTwoAnimation();
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => boss.AnimDone("animation"));

        PlayerHP = 100;
        BossHP = 10;
        PhaseNum = 2;
        won = false;
        boss.EnablePhaseTwo();
        player.EnableEverything();

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

    public float GetTime()
    {
        return Stopwatch;
    }
}
