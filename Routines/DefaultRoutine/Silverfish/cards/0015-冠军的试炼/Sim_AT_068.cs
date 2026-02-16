using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 加固（Bolster）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_068 : SimTemplate
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
            // 获取当前玩家的随从列表
            List<Minion> minions = ownplay ? p.ownMinions : p.enemyMinions;

            // 为所有具有嘲讽的随从增加+2/+2
            foreach (Minion m in minions)
            {
                if (m.taunt)
                {
                    p.minionGetBuffed(m, 2, 2);
                }
            }
        }
    }
}
