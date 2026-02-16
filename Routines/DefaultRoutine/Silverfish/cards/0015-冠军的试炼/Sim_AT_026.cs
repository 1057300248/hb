using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 愤怒卫士（Wrathguard）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_026 : SimTemplate
    {
        /// <summary>
        /// 当随从受到伤害时触发的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">受到伤害的随从。</param>
        /// <param name="anzOwnMinionsGotDmg">己方随从受到伤害的数量。</param>
        /// <param name="anzEnemyMinionsGotDmg">敌方随从受到伤害的数量。</param>
        /// <param name="anzOwnHeroGotDmg">己方英雄受到伤害的数量。</param>
        /// <param name="anzEnemyHeroGotDmg">敌方英雄受到伤害的数量。</param>
        public override void onMinionGotDmgTrigger(Playfield p, Minion m, int anzOwnMinionsGotDmg, int anzEnemyMinionsGotDmg, int anzOwnHeroGotDmg, int anzEnemyHeroGotDmg)
        {
            // 检查随从是否受到伤害
            if (m.anzGotDmg > 0)
            {
                // 重置伤害计数器
                m.anzGotDmg = 0;

                // 对己方英雄造成等量伤害
                p.minionGetDamageOrHeal(m.own ? p.ownHero : p.enemyHero, m.GotDmgValue);
            }
        }
    }
}