using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //法术 圣骑士 费用：4
    //Libram of Divinity
    //神性圣契
    //Give a minion +3/+3. If this costs (0), return this to your hand at the end of your turn.
    //使一个随从获得+3/+3。如果本牌的法力值消耗为（0）点，在你的回合结束时将本牌移回你的手牌。
    class Sim_GDB_138 : SimTemplate
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 基本效果：给随从+3/+3
            p.minionGetBuffed(target, 3, 3);
            
            // 在模拟中，我们可以通过添加一点额外的场面评估值来模拟卡牌返回手牌的价值
            // 检查卡牌费用是否为0（通过libram计数判断）
            if (p.libram >= 4 && ownplay)
            {
                // 当libram计数高且这张卡能免费使用时，提高场面评估值
                // 这样AI会倾向于在满足条件时使用这张卡
                p.evaluatePenality -= 10;
                
                // 注意：这不会真正将卡牌返回手牌，只是通过评估系统让AI认为这种情况更有价值
                // 在实际游戏中，卡牌会返回手牌，但在模拟器中我们只能给这种情况一个正面评价
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