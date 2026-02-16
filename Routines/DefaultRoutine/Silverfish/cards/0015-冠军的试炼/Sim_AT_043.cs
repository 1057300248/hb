using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 星界沟通（Astral Communion）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_043 : SimTemplate
    {
        /// <summary>
        /// 当卡牌被使用时触发的事件处理方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="ownplay">是否为当前玩家使用此卡牌。</param>
        /// <param name="target">目标随从（如果需要）。</param>
        /// <param name="choice">选择的选项（如果有）。</param>
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 弃掉当前玩家的所有手牌
            p.discardCards(p.owncards.Count, ownplay);

            // 增加10个法力水晶（最多不超过10个）
            if (ownplay)
            {
                p.mana = Math.Min(10, p.mana + 10);
                p.ownMaxMana = Math.Min(10, p.ownMaxMana + 10);
            }
            else
            {
                p.mana = Math.Min(10, p.mana + 10);
                p.enemyMaxMana = Math.Min(10, p.enemyMaxMana + 10);
            }
        }
    }
}