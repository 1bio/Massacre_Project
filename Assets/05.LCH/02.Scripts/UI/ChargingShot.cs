﻿using TMPro;
using UnityEngine;

public class ChargingShot : MonoBehaviour
{
    private SkillData chargingShot;
    public TextMeshProUGUI[] chargingShotTexts; 

    #region Initialized Methods
    public SkillData GetChargingShotData() // 차징샷 데이터
    {
        return DataManager.instance.playerData.skillData[0];
    }

    private void Awake()
    {
        chargingShot = GetChargingShotData();
        UpdateUI();
    }
    #endregion


    #region Main Methods
    public void ChargingShot_LevelUp() // 버튼 이벤트
    {
        DataManager.instance.SkillLevelUp("차징샷", 1);
        UIManager.instance.SelectWindow(false);

        // 스킬 잠금 해제
        if (chargingShot.level > 0)
        {
            chargingShot.isUnlock = false;
        }

        UpdateUI(); // 차징샷 UI

        Destroy(gameObject); // UI 프리팹 제거
    }

    // 텍스트 업데이트
    private void UpdateUI()
    {
        chargingShotTexts[0].text = chargingShot.skillName; // 스킬 이름
        chargingShotTexts[1].text = $"레벨 {chargingShot.level}"; // 레벨
        chargingShotTexts[2].text = $"쿨타임: {Mathf.Floor(chargingShot.coolDown)}초"; // 쿨타임
        chargingShotTexts[3].text = chargingShot.description; // 설명
        chargingShotTexts[4].text = $"공격력 {Mathf.Floor((chargingShot.multipleDamage - 1) * 100)}% 증가"; // 공격력 증가율 
        chargingShotTexts[5].text = $"쿨타임 {Mathf.Floor((chargingShot.multipleCoolDown - 1) * 100)}% 감소"; // 쿨타임 감소율
    }
    #endregion
}