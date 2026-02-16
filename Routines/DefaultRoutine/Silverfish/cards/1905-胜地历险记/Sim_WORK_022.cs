using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 战士 费用：3
	//Punch Card
	//打卡
	//Give your hero +3_Attack and "Also damages adjacent minions" this turn.
	//在本回合中，使你的英雄获得+3攻击力和“同时对相邻随从造成伤害”。
	class Sim_WORK_022 : SimTemplate
	{
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 给英雄增加3点攻击力
            if (ownplay)
            {
                p.minionGetTempBuff(p.ownHero, 3, 0); // 临时为英雄增加+3攻击力
                // 标记本回合英雄可以对相邻随从造成伤害
                p.ownHeroHasCleave = true;
            }
            else
            {
                p.minionGetTempBuff(p.enemyHero, 3, 0); // 临时为敌方英雄增加+3攻击力
                // 标记本回合敌方英雄可以对相邻随从造成伤害
                p.enemyHeroHasCleave = true;
            }
        }
    }
}
