using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//地标 萨满祭司 费用：3
	//Fairy Tale Forest
	//童话林地
	//[x]Draw a <b>Battlecry</b> minion.It costs (1) less.
	//抽一张<b>战吼</b>随从牌，其法力值消耗减少（1）点。
	class Sim_TOY_507 : SimTemplate
	{
        public override void useLocation(Playfield p, Minion triggerMinion, Minion target)
        {
            if (triggerMinion.own && (triggerMinion.handcard.card.CooldownTurn == 0 || triggerMinion.Ready == true))
            {
                p.drawACard(CardDB.cardIDEnum.DAL_052, true);
                var hc1 = p.owncards[p.owncards.Count - 1];
                hc1.manacost = Math.Max(0, hc1.manacost - 1);
            }
        }
    }
}
