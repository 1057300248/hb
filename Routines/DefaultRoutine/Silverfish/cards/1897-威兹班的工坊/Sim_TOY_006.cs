using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 中立 费用：1 攻击力：1 生命值：1
	//Scarab Keychain
	//甲虫钥匙链
	//<b>Battlecry:</b> <b>Discover</b> a2-Cost card.
	//<b>战吼：</b><b>发现</b>一张法力值消耗为（2）的卡牌。
	class Sim_TOY_006 : SimTemplate
	{
		public override void getBattlecryEffect(Playfield p, Minion ownMinion, Minion target, int choice)
        {
            // 发现机制，发现一张2费卡牌
            p.drawACard(CardDB.cardIDEnum.None, ownMinion.own, true); // None 代表 "发现" 选择机制
        }
		
	}
}
