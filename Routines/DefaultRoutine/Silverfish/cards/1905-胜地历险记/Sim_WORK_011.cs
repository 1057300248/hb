using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_WORK_011 : SimTemplate
    {
        public override void onTurnEndsTrigger(Playfield p, Minion m, bool turnEndOfOwner)
        {
            // 当回合结束的玩家是该随从的所有者时触发
            if (turnEndOfOwner == m.own)
            {
                // 根据随从所属方选择目标列表
                List<Minion> targetList = m.own ? p.ownMinions : p.enemyMinions;
                int pos = m.zonepos;

                // 左侧相邻随从
                if (pos > 0 && pos - 1 < targetList.Count)
                {
                    Minion left = targetList[pos - 1];
                    if (left != null)
                    {
                        p.minionGetBuffed(left, 1, 1); // +1/+1
                    }
                }

                // 右侧相邻随从
                if (pos < targetList.Count - 1)
                {
                    Minion right = targetList[pos + 1];
                    if (right != null)
                    {
                        p.minionGetBuffed(right, 1, 1); // +1/+1
                    }
                }
            }
        }
    }
}