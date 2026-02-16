using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 阿努比萨斯哨兵（Anubisath Sentinel）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_061 : SimTemplate
    {
        /// <summary>
        /// 当亡语效果触发时调用的方法。
        /// </summary>
        /// <param name="p">当前游戏局面。</param>
        /// <param name="m">触发亡语的随从。</param>
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 获取友方随从列表
            List<Minion> friendlyMinions = m.own ? p.ownMinions : p.enemyMinions;

            // 过滤掉阿努比萨斯哨兵本身
            List<Minion> validTargets = new List<Minion>();
            foreach (Minion minion in friendlyMinions)
            {
                if (minion.entitiyID != m.entitiyID)
                {
                    validTargets.Add(minion);
                }
            }

            // 如果存在有效的目标，则随机选择一个并给予+3/+3
            if (validTargets.Count > 0)
            {
                Random random = new Random();
                Minion target = validTargets[random.Next(validTargets.Count)];
                p.minionGetBuffed(target, 3, 3);
            }
        }
    }
}