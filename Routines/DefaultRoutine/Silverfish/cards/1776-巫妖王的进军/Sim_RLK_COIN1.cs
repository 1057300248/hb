using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 中立 费用：0
	//The Coin
	//幸运币
	//Gain 1 Mana Crystal this turn only.
	//在本回合中，获得一个法力水晶。
	class Sim_RLK_COIN1 : SimTemplate
	{
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 为当前玩家增加1点临时法力值
            if (ownplay)
            {
                p.mana++;       // 增加己方当前可用法力
            }
        }

    }
}
