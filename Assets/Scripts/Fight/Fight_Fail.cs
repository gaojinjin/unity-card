using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//失败
public class Fight_Fail : FightUnit
{
    public override void Init()
    {
        Debug.Log("失败了");
        FightManager.Instance.StopAllCoroutines();
        //显失败界面石到这里的小伙伴可以自已作
    }

}
