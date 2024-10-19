using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("스킬 선택 UI 필드")]
    public GameObject selectWindow;
    public GameObject[] selectPosition = new GameObject[2];

    [Header("스킬 프리팹")]
    public List<GameObject> skillPrefabs;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            selectWindow.SetActive(true);

            GetRandomSkill();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            SceneController.instance.LoadScene("Level");
        }
    }

    // 스킬 선택 UI 생성
    public void SelectWindow(bool openWindow)
    {
        selectWindow.SetActive(openWindow);
    }

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
