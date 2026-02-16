using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 中立 费用：1 攻击力：1 生命值：1
	//Astral Vigilant
	//星域戒卫
	//<b>Battlecry:</b> Get a copy of the last Draenei you played.
	//<b>战吼：</b>获取你使用的上一个德莱尼的一张复制。
	class Sim_GDB_461 : SimTemplate
	{
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
		{
			
               // 判断是否为当前玩家
               bool isOwn = own.own;
               CardDB.cardIDEnum lastDraeneiId = isOwn ? p.ownLastDraeneiCardId : p.enemyLastDraeneiCardId;

               // 检查是否存在有效的德莱尼卡牌ID
               if (lastDraeneiId != CardDB.cardIDEnum.None)
               {
                    // 将该卡牌的复制加入玩家手牌
                    p.drawACard(lastDraeneiId, isOwn, true);
               }
        }
	}
}
