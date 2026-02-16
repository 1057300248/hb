using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 巨型蟾蜍（Huge Toad）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_046 : SimTemplate
    {
        /// <summary>
        /// 当亡语效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发亡语的随从。</param>
        public override void onDeathrattle(Playfield p, Minion m)
        {
            Minion target = null;

            // 确定目标
            if (m.own)
            {
                target = p.getEnemyCharTargetForRandomSingleDamage(1);
            }
            else
            {
                target = p.searchRandomMinion(p.ownMinions, searchmode.searchLowestHP);
                if (target == null)
                    target = p.ownHero;
            }

            // 对目标造成1点伤害
            p.minionGetDamageOrHeal(target, 1);
        }
    }
}