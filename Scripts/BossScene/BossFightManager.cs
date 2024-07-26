using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossFightManager : MonoBehaviour
{
    [SerializeField] private int PlayerHP;
    [SerializeField] private float BossHP;
    private int BasicEnemyDamage;
    private int BossThrustDamage;
    private int BossWaterfowlDamage;
    private int BossNukeDamage;
    private int BossWeaponDamage;
    private int PlayerThrustDamage;
    private int PlayerSpinDamage;
    private int MapSlashDamage;
    private int PhantomDamage;
    private int MiniNukeDamage;
    public int PlayerThrustStaminaCost;
    public int PlayerSpinStaminaCost;
    public float PlayerStaminaRegen;
    [SerializeField] private BossHUDManager HUD;
    [SerializeField] private BossAIScript boss;
    [SerializeField] private BossPlayerMovement player;
    [SerializeField] private ChangeToScene SceneChanger;
    [SerializeField] private float PlayerStamina;
    [SerializeField] public int PhaseNum;
    [SerializeField] private GameObject PhaseTransitionExplosion;
    [SerializeField] private GameObject BounceCollider;
    [SerializeField] private MiniNukeSpawner MiniNukeSpawn;
    [SerializeField] private GameObject MapSlashObject;
    [SerializeField] private GameObject PhantomObject;
    [SerializeField] private BigNukeScript BigNuke;
    private bool StaminaRegening;
    private bool won;
    private bool DrainBossHP;


    private float Stopwatch;

    private bool PhaseTwoPlayedBefore;
    private void Start()
    {
        DrainBossHP = false;
        BounceCollider.SetActive(false);
        won = false;
        PhaseTransitionExplosion.SetActive(false);
        StaminaRegening = true;
        PlayerStamina = 100;
        PlayerStaminaRegen = 20f;
        PlayerHP = 100;
        BossHP = 100;
        PlayerThrustDamage = 5;
        PlayerSpinDamage = 10;
        PlayerThrustStaminaCost = 10;
        PlayerSpinStaminaCost = 25;
        BossThrustDamage = 65;
        BossWaterfowlDamage = 30;
        BossWeaponDamage = 20;
        BossNukeDamage = 100;
        BasicEnemyDamage = 20;
        MiniNukeDamage = 35;
        MapSlashDamage = 45;
        PhantomDamage = 45;
        PhaseTwoPlayedBefore = false;
    }
    private void Update()
    {
        if (Mathf.Abs(player.transform.position.x) > 10 || Mathf.Abs(player.transform.position.y) > 10)
        {
            player.transform.position = new Vector3(0, -2, 0);
        }

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
            if (PhaseNum == 1)
            {
                Lose();
            }
            if (PhaseNum == 2)
            {
                Lose(); //temporary solution to just reset level on phase 2
                StopAllCoroutines();
                StartCoroutine("PhaseTwo");
            }
        }
        if (PlayerStamina < 100 && StaminaRegening)
        {
            PlayerStamina += PlayerStaminaRegen * Time.deltaTime;
            HUD.UpdatePlayerStamina(PlayerStamina);
        }
        if (DrainBossHP)
        {
            if (BossHP - (170f * Time.deltaTime) < 0)
            {
                DrainBossHP = false;
                HUD.UpdateBossHP(0);
            }
            else
            {
                BossHP = BossHP - (170f * Time.deltaTime);
                HUD.UpdateBossHP(BossHP);
            }
        }
    }
    public bool HasStamina(int cost)
    {
        return (PlayerStamina > cost);
    }
    public void PlayerHit(Collider2D collision)
    {
        string name = collision.tag;
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
            case "MapSlash":
                PlayerHP -= MapSlashDamage;
                break;
            case "Phantom":
                PlayerHP -= PhantomDamage;
                break;
            case "MiniNuke":
                PlayerHP -= MiniNukeDamage;
                player.body.AddForce((player.transform.position - collision.transform.position).normalized * 700);
                break;
        }
        if (CurrentHP != PlayerHP)
        {
            if (PlayerHP < 0)
            {
                PlayerHP = 0;
            }
            HUD.UpdatePlayerHP(PlayerHP);
        }
    }
    public void BossHit(string name)
    {
        if (PhaseNum == 2)
        {
            return; //can't hit boss during phase 2!
        }
        float CurrentHP = BossHP;
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
            if (BossHP < 0)
            {
                BossHP = 0;
            }
            HUD.UpdateBossHP(BossHP);
        }
    }
    public void Win()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Time.timeScale = 0f;
        boss.DisableEverything(1);
        player.DisableEverything();
        player.SetDrag(0, 0);
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

        

        StartCoroutine("SetupPhaseTwo");
    }
    IEnumerator SetupPhaseTwo()
    {
        if (PhaseTwoPlayedBefore) //reset to phase 2 start
        {
            boss.DisableEverything(1);
            player.DisableEverything();
            player.transform.position = new Vector3(0, -2, 0);
            player.transform.rotation = Quaternion.identity; // what does this do ?
            boss.anim.Play("Blank");
            boss.transform.position = boss.PhaseTwoPosition;
            boss.transform.rotation = Quaternion.identity;
            BossHP = 5;
            HUD.UpdateBossHP(5);
            PlayerHP = 100;
            HUD.UpdatePlayerHP(100);
        }
        else //continue 
        {
            PhaseTwoPlayedBefore = true;
            boss.FadeOut();
            player.SetupPhase2();
            yield return new WaitUntil(() => (player.PlayerInPosition() && boss.BossInPosition()));
        }

        //start phase 2:
        boss.PhaseTwoAnimation();
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => boss.AnimDone("Transition"));

        PlayerHP = 100;
        HUD.UpdatePlayerHP(100);
        PhaseNum = 2;
        won = false;
        boss.EnablePhaseTwo();
        player.EnableEverything();
        StartCoroutine("PhaseTwo");
    }
    IEnumerator PhaseTwo()
    {
        yield return new WaitForSeconds(2);
        boss.PlayPoint();
        //TODO: mininukes first
        MiniNukeSpawn.Delay = 0.9f;
        MiniNukeSpawn.StartSpawning();
        yield return new WaitForSeconds(10f);
        MiniNukeSpawn.StopSpawning();
        yield return new WaitForSeconds(7f);
        BossHP = 30;
        HUD.UpdateBossHP(30);

        yield return new WaitForSeconds(2);
        boss.PlayPoint();
        //TODO: mapslash second
        foreach (MapSlashScript p in MapSlashObject.GetComponentsInChildren<MapSlashScript>())
        {
            p.Play();
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(5f);
        BossHP = 55;
        HUD.UpdateBossHP(55);


        yield return new WaitForSeconds(2);
        boss.PlayPoint();
        //TODO: phantoms third
        foreach (PhantomScript p in PhantomObject.GetComponentsInChildren<PhantomScript>())
        {
            p.Delay = 0.6f;
            p.Play();
        }
        BossHP = 70;
        HUD.UpdateBossHP(70);

        //TODO: another round of mininukes but faster spawnrate
        yield return new WaitForSeconds(2);
        boss.PlayPoint();
        MiniNukeSpawn.Delay = 0.6f;
        MiniNukeSpawn.StartSpawning();
        yield return new WaitForSeconds(15f);
        MiniNukeSpawn.StopSpawning();
        yield return new WaitForSeconds(7f);
        BossHP = 85;
        HUD.UpdateBossHP(85);

        //TODO: now mapslash and phantoms at the same time
        yield return new WaitForSeconds(1);
        boss.PlayPoint();
        foreach (PhantomScript p in PhantomObject.GetComponentsInChildren<PhantomScript>())
        {
            p.Delay = 0.6f;
            p.Play();
        }
        foreach (MapSlashScript p in MapSlashObject.GetComponentsInChildren<MapSlashScript>())
        {
            p.Play();
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(5f);

        
        BossHP = 100;
        HUD.UpdateBossHP(100);

        //TODO: change to bignuke scene
        boss.DisableEverything(2);

        //boss creates the bignuke visual
        boss.PlayBigNuke();

        yield return new WaitUntil(() => boss.DoneWithBigNukeBool);

        //set the actual bignuke visual active and remove it from boss's sprite
        BigNuke.transform.position = boss.transform.position + new Vector3(0.2502f, -0.628f, 0);
        BigNuke.gameObject.SetActive(true);
        
        boss.anim.Play(BossAIScript.Static2Name);

        //make the bignuke rush to the player and then stop time and player movement
        yield return new WaitUntil(() => BigNuke.AtPlayer());

        player.DisableEverything();
        player.body.angularVelocity = 0;
        player.transform.rotation = Quaternion.Euler(0, 0, -90 + Mathf.Atan2((boss.transform.position - player.transform.position).normalized.y, (boss.transform.position - player.transform.position).normalized.x) * Mathf.Rad2Deg);

        yield return new WaitUntil(() => BigNuke.Done());
        DrainBossHP = true;
        boss.FadeOut();

        yield return new WaitUntil(() => BossHP < 2);
        yield return new WaitForSeconds(6);

        SceneChanger.ChangeScene();
    }
    public void Lose()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void UseStamina(string name)
    {
        float CurrentStamina = PlayerStamina;
        StopCoroutine("StaminaTimer");
        StartCoroutine("StaminaTimer");
        switch(name)
        {
            case BossPlayerMovement.ThrustAnimName:
                PlayerStamina -= PlayerThrustStaminaCost;
                break;
            case BossPlayerMovement.SpinAnimName:
                PlayerStamina -= PlayerSpinStaminaCost;
                break;
        }
        if (PlayerStamina != CurrentStamina)
        {
            if (PlayerStamina < 0)
            {
                PlayerStamina = 0;
            }
            HUD.UpdatePlayerStamina(PlayerStamina);
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

    public float GetPlayerHP()
    {
        return PlayerHP;
    }
    public float GetPlayerStamina()
    {
        return PlayerStamina;
    }
    public float GetBossHP()
    {
        return BossHP;
    }
}
