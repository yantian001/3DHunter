using UnityEngine;


public class Creator : MonoBehaviour
{

    TargetPosition[] targets;
    // Use this for initialization
    int exsitCount = 0;
    void Start()
    {

    }

    public void OnEnable()
    {
        targets = FindObjectsOfType<TargetPosition>();
        CreateTarget();
    }

    public void Awake()
    {
       // LeanTween.addListener((int)Events.ENEMYDIE, OnEnemyDie);
        LeanTween.addListener((int)Events.ENEMYAWAY, OnEnemyDie);
      
    }

    public void OnDestroy()
    {
        //LeanTween.removeListener((int)Events.ENEMYDIE, OnEnemyDie);
        LeanTween.removeListener((int)Events.ENEMYAWAY, OnEnemyDie);
    }

    private void OnEnemyDie(LTEvent obj)
    {
        exsitCount--;
        CheckExist();
    }

    void CheckExist()
    {
        if (exsitCount <= 0)
        {
            LeanTween.dispatchEvent((int)Events.ENEMYCLEARED);
        }
    }

    void CreateTarget()
    {
        if (GameValue.s_currentObjective.targetObjects == null)
        {
            Debug.LogError("Miss target object");
        }

        for (int i = 0; i < GameValue.s_currentObjective.targetSpwanCount; i++)
        {
            TargetPosition tp = null;
            int posId = GameValue.s_currentObjective.targetPositionId[Random.Range(0, GameValue.s_currentObjective.targetPositionId.Length)];
            for (int j = 0; j < targets.Length; j++)
            {
                if (targets[j].id == posId)
                {
                    tp = targets[j];
                }
            }
            Vector3 spawnPoint = CommonUtils.DetectGround(tp.transform.position + new Vector3(Random.Range(-(int)(tp.transform.localScale.x / 2.0f), (int)(tp.transform.localScale.x / 2.0f)), 0, Random.Range((int)(-tp.transform.localScale.z / 2.0f), (int)(tp.transform.localScale.z / 2.0f))));
            GameObject.Instantiate(GameValue.s_currentObjective.targetObjects, spawnPoint, new Quaternion(0, Random.Range(0, 360f), 0, 0));
            exsitCount += 1;
        }
    }

}
