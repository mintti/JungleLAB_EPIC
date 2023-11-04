using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static TH.Core.TMP.TMPUtil;

public class UIBossInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI testInfoTMP;

    public void UpdateInfo(BossManager boss)
    {
        var pattern = boss.GetCurrentPattern();
        testInfoTMP.text = $"HP:{boss._currentHp}/{boss.maxHp}\n" +
                           $"행동: [{pattern.type.ToString()}] {pattern.value} ".ToTMP();
    }

    private void Update() {
        UpdateInfo(GameManager.Boss);
    }
}
