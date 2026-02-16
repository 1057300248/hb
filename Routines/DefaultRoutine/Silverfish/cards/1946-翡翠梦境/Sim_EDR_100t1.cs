using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 中立 费用：0 种族：无效的 卡牌稀有度：无效的
	//Well Rested
	//休憩饱足
	//+2/+2 and <b>Elusive</b>.
	//+2/+2和<b>扰魔</b>。
	class Sim_EDR_100t1 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 给随从+2攻击力和+2生命值
            if (target != null)
            {
                // 给目标随从+2攻击力和+2生命值
                p.minionGetBuffed(target, 2, 2);
				List<Minion> temp = (ownplay) ? p.ownMinions : p.enemyMinions;
				foreach (Minion m in temp) m.cantBeTargetedBySpellsOrHeroPowers = true;
            }
        }
		
		public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),// 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),//目标必须是随从
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),// 目标必须是友方
            };
        }
	}
}
