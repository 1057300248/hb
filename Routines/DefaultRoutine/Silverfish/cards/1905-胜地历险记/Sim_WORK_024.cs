using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 德鲁伊 费用：2
	//Handle with Bear
	//蛮熊搬家
	//[x]Get two 3/3 Bearswith <b>Taunt</b>. Each turnthey are in your hand,they gain +1/+1.
	//获取两张3/3并具有<b>嘲讽</b>的熊，其每在你手牌中一回合便获得+1/+1。
	class Sim_WORK_024 : SimTemplate
	{
		CardDB.Card bear = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.WORK_024t); // 假设熊的卡牌ID为WORK_024t

        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 抽取两张3/3并具有嘲讽的熊加入手牌
            p.drawACard(bear.cardIDenum, ownplay, true); // 抽取第一张熊牌
            p.drawACard(bear.cardIDenum, ownplay, true); // 抽取第二张熊牌
        }
		
	}
}
