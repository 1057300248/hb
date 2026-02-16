using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //法术 圣骑士 费用：3
    //Libram of Clarity
    //明澈圣契
    //Draw 2 minions. If this costs (0), give them +2/+1.
    //抽两张随从牌。如果本牌的法力值消耗为（0）点，使抽到的随从牌获得+2/+1。
    class Sim_GDB_137 : SimTemplate
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 存储当前手牌数量，用于追踪新抽到的牌
            int currentHandCount = 0;
            if (ownplay) currentHandCount = p.owncards.Count;
            
            // 抽两张随从牌 - 我们假设这会抽随从牌，根据实际模拟器行为可能需要调整
            p.drawACard(CardDB.cardIDEnum.None, ownplay, true);
            p.drawACard(CardDB.cardIDEnum.None, ownplay, true);
            
            // 检查这张卡牌是否费用为0
            // 我们可以通过检查玩家的法力水晶变化来推断卡牌实际费用
            // 由于libram效果，明澈圣契的费用可能被减到0
            if (p.libram >= 3 && ownplay)  // 假设libram计数器>=3时，明澈圣契费用为0
            {
                // 如果费用为0且是自己打出且成功抽到了牌
                if (ownplay && p.owncards.Count > currentHandCount)
                {
                    // 给新抽到的牌+2/+1
                    int newCards = p.owncards.Count - currentHandCount;
                    for (int i = 0; i < Math.Min(newCards, 2); i++)
                    {
                        int cardIndex = p.owncards.Count - 1 - i;
                        if (cardIndex >= 0 && cardIndex < p.owncards.Count)
                        {
                            // 增强手牌中的随从
                            p.owncards[cardIndex].addattack += 2;
                            p.owncards[cardIndex].addHp += 1;
                        }
                    }
                }
            }
        }
    }
}