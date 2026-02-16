using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 罗宁（Rhonin）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_009 : SimTemplate
    {
        /// <summary>
        /// 当亡语效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发亡语的随从。</param>
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 将三张奥术飞弹置入触发亡语的玩家手牌
            for (int i = 0; i < 3; i++)
            {
                p.drawACard(CardDB.cardNameEN.arcanemissiles, m.own, true);
            }
        }
    }
}