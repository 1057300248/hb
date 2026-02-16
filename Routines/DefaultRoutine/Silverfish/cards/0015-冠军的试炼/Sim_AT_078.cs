using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 精英对决（Enter the Coliseum）卡牌的模拟实现。
    /// </summary>
    class Sim_AT_078 : SimTemplate
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
            // 处理敌方随从
            List<Minion> enemyMinions = new List<Minion>(p.enemyMinions);
            if (enemyMinions.Count >= 2)
            {
                // 按攻击力降序排序
                enemyMinions.Sort((a, b) => b.Angr.CompareTo(a.Angr));

                // 保留攻击力最高的随从，消灭其余随从
                for (int i = 1; i < enemyMinions.Count; i++)
                {
                    p.minionGetDestroyed(enemyMinions[i]);
                }
            }

            // 处理己方随从
            List<Minion> ownMinions = new List<Minion>(p.ownMinions);
            if (ownMinions.Count >= 2)
            {
                // 按攻击力降序排序
                ownMinions.Sort((a, b) => b.Angr.CompareTo(a.Angr));

                // 保留攻击力最高的随从，消灭其余随从
                for (int i = 1; i < ownMinions.Count; i++)
                {
                    p.minionGetDestroyed(ownMinions[i]);
                }
            }
        }
    }
}