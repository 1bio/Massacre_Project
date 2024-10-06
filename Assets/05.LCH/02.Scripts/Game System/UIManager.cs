using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private CoolDownManager coolDownManager;

    public TextMeshProUGUI skill1;
    public TextMeshProUGUI skill2;

    private float skill1_Time;
    private float skill2_Time;

    private void Start()
    {
        coolDownManager = GetComponent<CoolDownManager>();
    }

    public void Update()
    {
        skill1_Time = Mathf.Ceil(coolDownManager.GetRemainingCooldown("Aiming"));
        skill2_Time = Mathf.Ceil(coolDownManager.GetRemainingCooldown("RapidShot"));

        skill1.text = skill1_Time.ToString();
        skill1.text = skill2_Time.ToString();
    }

}
