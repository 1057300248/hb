
using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_TSC_631 : SimTemplate //* 鱼群聚集 Schooling
                                    //将三张1/1的食人鱼集群置入你的手牌。
                                    //Add three 1/1 PiranhaSwarmers to your hand.
    {
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            p.drawACard(CardDB.cardIDEnum.TSC_638, ownplay, true);
            p.drawACard(CardDB.cardIDEnum.TSC_638t, ownplay, true);
            p.drawACard(CardDB.cardIDEnum.TSC_638t2, ownplay, true);
        }
    }


}
