using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 法师 费用：5 攻击力：4 生命值：4
	//Surfalopod
	//冲浪章鱼
	//[x]<b>Battlecry:</b> The nextspell you draw is<b>Cast When Drawn</b>.
	//<b>战吼：</b>使你抽到的下一张法术牌获得<b>抽到时施放</b>效果。
	class Sim_VAC_443 : SimTemplate
	{
		public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 实现战吼效果: 赋予玩家接下来抽到的第一张法术卡“抽到时施放”的效果
            if (own.own)
            {
                p.ownNextSpellCastWhenDrawn = true;  // 标记己方玩家的下一张法术卡将具有“抽到时施放”效果
            }
        }
		
	}
}
