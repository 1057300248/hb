using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 萨满祭司 费用：4 攻击力：3 生命值：4
	//Turbulus
	//湍流元素特布勒斯
	//<b>Hunter Tourist</b>. <b>Battlecry:</b> Give +1/+1 to all other <b>Battlecry</b> minions in your hand, deck, and battlefield.
	//<b>猎人游客</b><b>战吼：</b>使你手牌，牌库和战场上的所有其他<b>战吼</b>随从获得+1/+1。
	class Sim_WORK_013 : SimTemplate
	{
		public override void getBattlecryEffect(Playfield p, Minion ownMinion, Minion target, int choice)
        {
            // 处理战场上的随从
            foreach (Minion m in (ownMinion.own ? p.ownMinions : p.enemyMinions))
            {
                if (m.handcard.card.battlecry && m != ownMinion) // 检查是否为战吼随从，且不是自身
                {
                    p.minionGetBuffed(m, 1, 1); // 使战场上的战吼随从获得+1/+1
                }
            }

            // 处理手牌中的随从
            foreach (var hc in (ownMinion.own ?  p.owncards : p.enemyHand))
            {
                if (hc.card.battlecry) // 检查是否为战吼随从
                {
                    hc.addattack += 1;
                    hc.addHp += 1; // 为手牌中的战吼随从增加+1/+1
                }
            }
        }
		
	}
}
