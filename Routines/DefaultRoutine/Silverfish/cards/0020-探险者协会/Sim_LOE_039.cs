using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// A3型机械金刚（Gorillabot A-3）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_039 : SimTemplate
    {
        /// <summary>
        /// 当战吼效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="own">触发战吼的随从。</param>
        /// <param name="target">战吼的目标（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 获取当前玩家的随从列表
            List<Minion> minions = own.own ? p.ownMinions : p.enemyMinions;

            // 检查是否有其他机械随从
            foreach (Minion m in minions)
            {
                // 排除自身
                if (m.entitiyID == own.entitiyID) continue;

                // 如果有其他机械随从，则发现一张机械牌
                if (m.handcard.card.race == CardDB.Race.MECHANICAL)
                {
                    p.drawACard(CardDB.cardNameEN.unknown, own.own, true);
                    break;
                }
            }
        }
    }
}