using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 圣骑士 费用：4
	//Vacation Planning
	//假期规划
	//Restore #4 Health. Summon 3 Silver Hand Recruits. Draw 2 cards.
	//恢复#4点生命值。召唤3个白银之手新兵。抽两张牌。
	class Sim_WORK_003 : SimTemplate
	{
		 public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 恢复4点生命值
            if (target != null)
            {
                p.minionGetDamageOrHeal(target, -4); // 负值表示治疗
            }

            // 召唤3个白银之手新兵（Silver Hand Recruit，卡牌ID: "CS2_101t"）
            CardDB.Card silverHandRecruit = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CS2_101t);
            p.callKid(silverHandRecruit, p.ownMinions.Count, ownplay);
            p.callKid(silverHandRecruit, p.ownMinions.Count, ownplay);
            p.callKid(silverHandRecruit, p.ownMinions.Count, ownplay);

            // 抽两张牌
            p.drawACard(CardDB.cardIDEnum.None, ownplay);
            p.drawACard(CardDB.cardIDEnum.None, ownplay);
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET), // 目标必须是友方单位
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE) // 如果有目标就需要选择
            };
        }
		
	}
}
