using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots 
{ 
/// <summary>
/// 英雄之魂（Fallen Hero）卡牌的模拟实现。
/// </summary>
class Sim_AT_003 : SimTemplate
{
    /// <summary>
    /// 当光环效果开始时调用的方法。
    /// </summary>
    /// <param name="p">当前游戏局面。</param>
    /// <param name="own">触发光环的随从。</param>
    public override void onAuraStarts(Playfield p, Minion own)
    {
        // 增加英雄技能的额外伤害
        if (own.own)
            p.ownHeroPowerExtraDamage++; // 己方英雄技能额外伤害+1
        else
            p.enemyHeroPowerExtraDamage++; // 敌方英雄技能额外伤害+1
    }

    /// <summary>
    /// 当光环效果结束时调用的方法。
    /// </summary>
    /// <param name="p">当前游戏局面。</param>
    /// <param name="own">触发光环的随从。</param>
    public override void onAuraEnds(Playfield p, Minion own)
    {
        // 减少英雄技能的额外伤害
        if (own.own)
            p.ownHeroPowerExtraDamage--; // 己方英雄技能额外伤害-1
        else
            p.enemyHeroPowerExtraDamage--; // 敌方英雄技能额外伤害-1
    }
}
}