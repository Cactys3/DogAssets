using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class GenerativeNumbers : MonoBehaviour
{
    [Header("Number Sprite Stuff")]
    [SerializeField] private Sprite Zero;
    [SerializeField] private Sprite One;
    [SerializeField] private Sprite Two;
    [SerializeField] private Sprite Three;
    [SerializeField] private Sprite Four;
    [SerializeField] private Sprite Five;
    [SerializeField] private Sprite Six;
    [SerializeField] private Sprite Seven;
    [SerializeField] private Sprite Eight;
    [SerializeField] private Sprite Nine;
    [SerializeField] private Sprite Blank;
    [SerializeField] private Sprite Max;
    [SerializeField] private GameObject NumberInstance;
    [SerializeField] private long Number;
    [SerializeField] private float Offset;
    [SerializeField] private long MaxNumber;
    [SerializeField] private float Scale;
    private long OldNumber;
    private GameObject ParentObject;
    private List<GameObject> ListOfNumbers;
    private void Awake()
    {
        if (Scale == 0)
        {
            Scale = 1;
        }
        OldNumber = -999;
        ListOfNumbers = new List<GameObject>();
        ParentObject = this.gameObject;
    }
    public void SetNumber(long num)
    {
        Debug.Log("Number is now: " + num);
        Number = num;
        UpdateNumbers();
    }
    public void SetScale(float num)
    {
        Scale = num;
        UpdateNumbers();
    }
    public float GetScale(float num)
    {
        return Scale;
    }
    public void SetSpacing(float num)
    {
        Offset = num;
        UpdateNumbers();
    }
    public float GetSpacing(float num)
    {
        return Offset;
    }
    public void IncrementNumber(int num)
    {
        Number += num;
    }
    public void IncrementNumber()
    {
        Number += 1;
    }
    void Update()
    {
        if (Number != OldNumber)
        {
            UpdateNumbers();
        }
    }
    private void UpdateNumbers()
    {
        foreach (GameObject G in ListOfNumbers)
        {
            Destroy(G);
        }

        long J = 0;
        long I = Number.ToString().Count();
        while (J < I)
        {
            GameObject NewInstance = Instantiate(NumberInstance);

            NewInstance.transform.parent = ParentObject.transform;
            NewInstance.transform.localScale = new Vector3(Scale, Scale, 1);
            NewInstance.transform.position = this.transform.position - new Vector3(Offset * J, 0, 0);
            Debug.Log(this.transform.position.x + " minus " + Offset * J + " should equal: " + NewInstance.transform.position.x);

            NewInstance.GetComponent<SpriteRenderer>().sprite = GetNumberSprite((Number / (long)(Mathf.Pow(10, J))) % 10);
            J += 1;
            ListOfNumbers.Add(NewInstance);
        }
        OldNumber = Number;
        int index = this.gameObject.transform.childCount - 1;
    /**    while (index >= 0)
        {
            this.gameObject.transform.GetChild(index).transform.localScale = new Vector3(Scale, Scale, 1);
            index--;
        } */
    }
    private Sprite GetNumberSprite(long num)
    {
        switch (num)
        {
            case 0:
                return Zero;
            case 1:
                return One;
            case 2:

                return Two;
            case 3:

                return Three;
            case 4:

                return Four;
            case 5:

                return Five;
            case 6:

                return Six;
            case 7:

                return Seven;
            case 8:

                return Eight;
            case 9:

                return Nine;
            default:
                return Blank;
        }
    }

}
