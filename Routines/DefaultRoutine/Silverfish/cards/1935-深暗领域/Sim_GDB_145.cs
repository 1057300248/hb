using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 中立 费用：7 攻击力：7 生命值：7
	//Kil'jaeden
	//基尔加丹
	//[x]<b>Battlecry:</b> Replace your deckwith an endless portal ofDemons. Each turn, they gainan additional +2/+2.
	//<b>战吼：</b>将你的牌库替换成一座无尽的恶魔传送门。每个回合，这些恶魔都会额外获得+2/+2。
	class Sim_GDB_145 : SimTemplate
	{
		public virtual void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 增强牌库中的所有随从
            foreach (CardDB.Card card in p.ownDeck)
            {
                if (card.type == CardDB.cardtype.MOB) // 检查是否为随从卡牌
                {
                    card.Attack += 2; // 增加攻击力
                    card.Health += 2; // 增加生命值
                }
            }
			// 牌库替换为无限恶魔传送门
			p.ownDeckSize = 30;
			return;
        }
		
	}
}
