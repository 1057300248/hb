using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 中立 费用：0
	//Recruit a Minion
	//招募随从
	//Put an enemy minion into your hand. Your opponent gets 3 Coins.
	//将一个敌方随从置入你的手牌。你的对手获取3张幸运币。
	class Sim_BG31_BOBt2 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            if (target != null && !target.own) // 确保目标是敌方随从
                {
                    // 将目标随从移回玩家的手牌
                    p.minionReturnToHand(target, ownplay, target.handcard.getManaCost(p));
                }
        }
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),   // 目标必须是随从
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),    // 目标必须是敌方
            };
        }
		
	}
}
