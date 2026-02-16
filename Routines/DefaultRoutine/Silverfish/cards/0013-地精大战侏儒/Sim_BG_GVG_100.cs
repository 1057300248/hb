using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 术士 费用：5 攻击力：4 生命值：4
    //Floating Watcher
    //漂浮观察者
    //Whenever your hero takes damage on your turn, gain +2/+2.
    //每当你的英雄在你的回合受到伤害，便获得+2/+2。
    class Sim_BG_GVG_100 : SimTemplate
    {
        public override void onMinionGotDmgTrigger(Playfield p, Minion m, int anzOwnMinionsGotDmg, int anzEnemyMinionsGotDmg, int anzOwnHeroGotDmg, int anzEnemyHeroGotDmg)
        {
            // 检查是否己方英雄受到伤害，且在己方回合
            if (anzOwnHeroGotDmg > 0 && p.isOwnTurn)
            {
                // 漂浮观察者获得+2/+2
                p.minionGetBuffed(m, 2, 2);
            }
        }
    }
}