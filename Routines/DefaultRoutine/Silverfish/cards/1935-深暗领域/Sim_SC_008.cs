using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    // 随从 猎人 费用：3 攻击力：4 生命值：2
    // Hydralisk
    // 刺蛇
    // [x]<b>Battlecry:</b> Deal 2 damage to a random enemy. 
    // Repeat for each other Zerg minion you control.
    // <b>战吼：</b>随机对一个敌人造成 2 点伤害。你每控制一个其他异虫随从，重复一次。

    class Sim_SC_008 : SimTemplate
    {
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            List<Minion> temp = own.own ? p.ownMinions : p.enemyMinions;
            int zergCount = 0;

            foreach (Minion m in temp)
            {
                if (m != own && m.zerg) // 排除自身，统计其他异虫
                {
                    zergCount++;
                }
            }

            for (int i = 0; i <= zergCount; i++) // 正确次数：1（基础）+ zergCount次
            {
                target = p.getEnemyCharTargetForRandomSingleDamage(2);
                p.minionGetDamageOrHeal(target, 2);
            }
        }
    }
}