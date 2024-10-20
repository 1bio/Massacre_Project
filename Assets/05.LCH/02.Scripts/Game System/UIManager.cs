using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("스킬 선택창 UI 필드")]
    public GameObject selectWindow;
    public GameObject[] selectPosition = new GameObject[2];

    [Header("스킬 프리팹")]
    public List<GameObject> skillPrefabs;

    [Header("인벤토리 UI")]
    public GameObject inventoryWindow;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        selectWindow.SetActive(true);

        GetRandomSkill();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            selectWindow.SetActive(true);

            GetRandomSkill();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryWindow.SetActive(true);
        }
    }


    #region Toggle Window
    // 스킬 선택 UI 토글
    public void SelectWindow(bool openWindow)
    {
        selectWindow.SetActive(openWindow);
    }   
    
    // 인벤토리 UI 토글
    public void InventoryWindow(bool openWindow)
    {
        inventoryWindow.SetActive(openWindow);
    }
    #endregion

    // 랜덤 스킬 생성
    public void GetRandomSkill()
    {
        int[] randomValues = RandomNumberGenerator.GenerateRandomIndex(skillPrefabs.Count, selectPosition.Length); 

        for (int i = 0; i < randomValues.Length; i++)
        {
            int randomValue = randomValues[i];

            Instantiate(skillPrefabs[randomValue], selectPosition[i].transform);


        }
    }
}
