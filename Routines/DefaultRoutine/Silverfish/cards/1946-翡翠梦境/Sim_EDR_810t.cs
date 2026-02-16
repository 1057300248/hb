using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 巫妖王 费用：1 攻击力：0 生命值：2
    //Bloated Leech
    //饱胀水蛭
    //[x]At the end of your turn, yourhero steals @ Health fromthe lowest Health enemy.
    //在你的回合结束时，你的英雄会从生命值最低的敌人处偷取1点生命值。
    class Sim_EDR_810t : SimTemplate
    {
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            if (triggerEffectMinion.own == turnEndOfOwner)
            {
                Minion target = null;
                int sj = p.水蛭伤害 + 1;
                if (turnEndOfOwner)
                {
                    // 友方回合：攻击敌方最低HP目标（含英雄）
                    target = p.getEnemyCharTargetForRandomSingleDamage1(sj, false);
                }
                else
                {
                    // 敌方回合：攻击友方最低HP目标（含英雄）
                    Minion lowestMinion = p.searchRandomMinion(p.ownMinions, searchmode.searchLowestHP);
                    int heroHP = p.ownHero.Hp;
                    int minionHP = (lowestMinion != null) ? lowestMinion.Hp : int.MaxValue;

                    target = (heroHP < minionHP) ? p.ownHero : lowestMinion;

                    if (target == null)
                    {
                        target = p.ownHero;
                    }
                }

                if (target != null)
                {
                    target.Hp -= sj;
                }
            }
        }
    }
}
