using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 小鬼骑士（Tiny Knight of Evil）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_021 : SimTemplate
    {
        /// <summary>
        /// 当弃牌效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="hc">被弃掉的手牌。</param>
        /// <param name="own">触发效果的随从。</param>
        /// <param name="num">弃牌数量。</param>
        /// <param name="checkBonus">是否检查奖励。</param>
        /// <returns>是否触发效果。</returns>
        public override bool onCardDicscard(Playfield p, Handmanager.Handcard hc, Minion own, int num, bool checkBonus)
        {
            // 检查触发随从是否存在
            if (own == null) return false;

            // 如果是检查奖励阶段，则不触发效果
            if (checkBonus) return false;

            // 为触发随从增加攻击力和生命值
            p.minionGetBuffed(own, num, num);
            return false;
        }
    }
}