using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 中立 费用：0 种族：无效的 卡牌稀有度：无效的
	//Sweet Dreams
	//甜美梦境
	//+4/+5. Place this card on top of your deck.
	//+4/+5。将本牌置于你的牌库顶。
	class Sim_EDR_100t8 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 给随从+4攻击力和+5生命值
            if (target != null)
            {
                // 给目标随从+4攻击力和+5生命值
                p.minionGetBuffed(target, 4, 5);
				List<Minion> temp = (ownplay) ? p.ownMinions : p.enemyMinions;
				foreach (Minion m in temp) m.cantBeTargetedBySpellsOrHeroPowers = true;
            }
        }
		public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),
            };
        }
	}
}
