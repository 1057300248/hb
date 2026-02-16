using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_CORE_EX1_407 : SimTemplate //* Brawl
    {
        // Destroy all minions except one. (chosen randomly) 破坏所有怪兽，以期望修正

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 获取当前场上的所有随从列表
            List<Minion> allMinions = new List<Minion>();

            foreach (var minion in p.ownMinions)
                allMinions.Add(minion);

            foreach (var minion in p.enemyMinions)
                allMinions.Add(minion);

            // 如果场上没有随从，直接返回（虽然条件已经检查过）
            if (allMinions.Count < 1)
                return;

            // 随机选择一个随从保留
            Random random = new Random();
            int indexToKeep = random.Next(allMinions.Count);
            Minion chosen = allMinions[indexToKeep];

            // 删除所有其他随从
            foreach (var minion in allMinions)
            {
                if (minion != chosen)
                {
                    if (minion.own) // 如果是己方随从，使用分解逻辑
                        p.minionGetDestroyed(minion);
                    else // 如果是敌方随从，直接移除
                        p.minionGetDestroyed(minion);
                }
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_MINIMUM_TOTAL_MINIONS, 4),
            };
        }
    }
}