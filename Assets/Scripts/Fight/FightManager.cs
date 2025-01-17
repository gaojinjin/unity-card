using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//战斗枚举
public enum FightType
{
    None,
    Init,
    Player,//玩家回合
    Enemy,//敌人回合
    Win,
    Fail
}

//战斗管理器
public class FightManager : MonoBehaviour
{

    public static FightManager Instance;
    public FightUnit fightUnit;//战斗单元

    public int MaxHp;//最大血量
    public int CurHp;//当前血量
    public int MaxPowerCount;//最大能量（卡牌使用会消耗能量）
    public int CurPowerCount;//当前能量
    public int DefenseCount;//防御值

    public void Init()
    {
        MaxHp = 10;
        CurHp = 10;
        MaxPowerCount = 10;
        CurPowerCount = 10;
        DefenseCount = 10;
    }

    private void Awake()
    {
        Instance = this;
    }
    //切换战斗类型
    public void ChangeType(FightType type)
    {
        switch (type)
        {
            case FightType.None:
                break;
            case FightType.Init:
                fightUnit = new FightInit();
                break;
            case FightType.Player:
                fightUnit = new Fight_PlayerTurn();
                break;
            case FightType.Enemy:
                fightUnit = new Fight_EnemyTurn();
                break;
            case FightType.Win:
                fightUnit = new Fight_Win();
                break;
            case FightType.Fail:
                fightUnit = new Fight_Fail();
                break;
        }
        fightUnit.Init();// 初始化
    }
    private void Update()
    {
        if (fightUnit != null)
        {
            fightUnit.OnUpdate();
        }
    }

    //玩家受击
    public void GetPlayerHit(int hit)
    {
        //扣护盾
        if (DefenseCount > hit)
        {
            DefenseCount -= hit;
        } else {
            hit = hit - DefenseCount;
            DefenseCount = 0;
            CurHp -= hit;
            if (CurHp <= 0)
            {
                CurHp = 0;
                //切换到游戏失败状态
                ChangeType(FightType.Fail);
            }

        }
        // 更新界面
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateHp();
        UIManager.Instance.GetUI<FightUI>("FightUI").UpdateDefense();
    }


}
