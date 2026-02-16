using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 中立 费用：8 攻击力：6 生命值：9
    //Foe Reaper 4000
    //死神4000型
    //Also damages the minions next to whomever it attacks.
    //同时对其攻击目标相邻的随从造成伤害。
    class Sim_BG_GVG_113 : SimTemplate
    {
        public override void onMinionAttack(Playfield p, Minion attacker, Minion target)
        {
            // 检查是否是死神4000型在攻击
            if (attacker.name == CardDB.cardNameEN.foereaper4000)
            {
                // 获取攻击目标相邻的随从并造成相同伤害
                List<Minion> adjacentMinions = new List<Minion>();

                // 根据攻击者是敌方还是友方，确定目标阵营并查找相邻随从
                if (attacker.own) // 如果是己方随从攻击
                {
                    // 攻击敌方目标时，查找敌方场上目标相邻的随从
                    foreach (Minion enemyMinion in p.enemyMinions)
                    {
                        // 检查是否是攻击目标的相邻随从
                        int targetIndex = p.enemyMinions.IndexOf(target);
                        int currentIndex = p.enemyMinions.IndexOf(enemyMinion);

                        if (Math.Abs(targetIndex - currentIndex) == 1 && enemyMinion.Hp > 0)
                        {
                            adjacentMinions.Add(enemyMinion);
                        }
                    }
                }
                else // 如果是敌方随从攻击
                {
                    // 攻击己方目标时，查找己方场上目标相邻的随从
                    foreach (Minion ownMinion in p.ownMinions)
                    {
                        // 检查是否是攻击目标的相邻随从
                        int targetIndex = p.ownMinions.IndexOf(target);
                        int currentIndex = p.ownMinions.IndexOf(ownMinion);

                        if (Math.Abs(targetIndex - currentIndex) == 1 && ownMinion.Hp > 0)
                        {
                            adjacentMinions.Add(ownMinion);
                        }
                    }
                }

                // 对相邻随从造成伤害
                foreach (Minion adjacentMinion in adjacentMinions)
                {
                    if (adjacentMinion != null && adjacentMinion.Hp > 0)
                    {
                        adjacentMinion.Hp -= attacker.Angr;
                        if (adjacentMinion.Hp <= 0)
                        {
                            adjacentMinion.Hp = 0;
                            adjacentMinion.wounded = true;
                        }
                    }
                }
            }
        }
    }
}