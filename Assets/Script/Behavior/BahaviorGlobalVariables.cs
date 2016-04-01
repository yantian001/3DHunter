using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;

public class BahaviorGlobalVariables : MonoBehaviour
{

    public float crouchTimeMax = 8f;

    public float oneShootTime = 1.6f;

    public float attackRate = 50f;

    public int enemyBulletCount = 1;

    public float enemySpread = 1f;

    public float enemyAttack = 5f;
    // Use this for initialization
    void Start()
    {
        GlobalVariables.Instance.SetVariableValue("PlayerFired", false);

        float seed = 1f;
        if (GameValue.level < 4)
        {
            seed = .6f;
        }
        else if (GameValue.level < 11 && GameValue.level > 3)
        {
            seed = .7f;
        }
        else if (GameValue.level < 21 && GameValue.level > 10)
        {
            seed = 1f;
        }
        else if (GameValue.level < 31 && GameValue.level > 20)
        {
            seed = 1.5f;
        }
        else if (GameValue.level < 41 && GameValue.level > 30)
        {
            seed = 1.8f;
        }

        SetVariableValue("CounchTime", crouchTimeMax - 0.06f * GameValue.level);
        SetVariableValue("EnemyThinkTime", (oneShootTime - 0.01f * (float)GameValue.level) * (2f - seed));
        SetVariableValue("AttackRate", attackRate + GameValue.level * seed);
        SetVariableValue("EnemyBulletCount", enemyBulletCount + GameValue.level / 10);
        SetVariableValue("AttackSpread", (enemySpread - (0.01f * GameValue.level) * seed));
        SetVariableValue("Attack", (enemyAttack + (0.01f * GameValue.level) * seed));
    }

   public static void SetVariableValue(string strName, object val)
    {
        GlobalVariables.Instance.SetVariableValue(strName, val);
    }

}
