using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_EDR_840 : SimTemplate
    {
        // 预加载所有魔种变体
        private List<CardDB.cardIDEnum> dreadseeds = new List<CardDB.cardIDEnum>()
        {
            CardDB.cardIDEnum.EDR_840t,  // 基础魔种
            CardDB.cardIDEnum.EDR_840t1, // 变体1
            CardDB.cardIDEnum.EDR_840t2  // 变体2
        };

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 1. 抽牌效果
            p.drawACard(CardDB.cardIDEnum.None, ownplay);

        }
    }
}