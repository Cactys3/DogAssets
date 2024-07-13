using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHUDManager : MonoBehaviour
{
    [Header("Player Stuff")]
    [SerializeField] private Sprite[] DashCDSprites;
    [SerializeField] private SpriteRenderer DashCDRenderer;
    [SerializeField] private BossPlayerMovement player;
    [SerializeField] private Transform PlayerHP;
    [SerializeField] private Transform PlayerStamina;
    [SerializeField] private float PlayerStaminaOffset;
    [Header("Boss Stuff")]
    [SerializeField] private BossAIScript boss;
    [SerializeField] private Transform BossHP;
    [SerializeField] private BossFightManager manager;

    private float BossMinValue;
    private float PlayerMinValue;
    private float StaminaMinValue;
    // Start is called before the first frame update
    void Start()
    {
        BossMinValue = -5.875f;
        PlayerMinValue = -2.814f;
        StaminaMinValue = -1.753f;
        PlayerStaminaOffset = 0.05f;
    }
    public void UpdatePlayerHP(float num)
    {
        //Debug.Log("update player hp" + new Vector3(PlayerMinValue + ((num / 100) * -PlayerMinValue), PlayerHP.position.y, PlayerHP.position.z));
        PlayerHP.position = new Vector3(PlayerMinValue + ((num / 100) * -PlayerMinValue), PlayerHP.position.y, PlayerHP.position.z);
    }
    public void UpdateBossHP(float num)
    {
        //Debug.Log("update boss hp" + BossMinValue + " " +  ((num / 100) + " " +  -BossMinValue));
        BossHP.position = new Vector3(BossMinValue + ((num / 100) * -BossMinValue), BossHP.position.y, BossHP.position.z);
    }
    public void UpdatePlayerStamina(float num)
    {
        //Debug.Log("Üpdated Statmina: " + (PlayerStamina.transform.position.x > StaminaMinValue) + " " + PlayerStamina.position.x + (num * -PlayerStaminaOffset));

            PlayerStamina.transform.position = new Vector3(StaminaMinValue + ((num / 100) * -StaminaMinValue), PlayerStamina.position.y, PlayerStamina.position.z);
        
    }
    //Player Stuff
    public void StartDashCD()
    {
        StartCoroutine("DashCD");
    }
    IEnumerator DashCD()
    {
        player.DashingOnCD = true;
        int i = 0;
        while (i < 8)
        {
            DashCDRenderer.sprite = DashCDSprites[i];
            yield return new WaitForSeconds(FindObjectOfType<BossPlayerMovement>().DashCDnum / 8f);
            i++;
        }
        DashCDRenderer.sprite = DashCDSprites[i];
        player.DashingOnCD = false;
    }
}
