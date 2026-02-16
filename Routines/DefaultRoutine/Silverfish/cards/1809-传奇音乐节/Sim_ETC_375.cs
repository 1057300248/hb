using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 德鲁伊 费用：1 攻击力：1 生命值：1
	//Peaceful Piper
	//沉静的吹笛人
	//<b>Choose One -</b> Draw a Beast; or <b>Discover</b> one.
	//<b>抉择：</b>抽一张野兽牌；或者<b>发现</b>一张野兽牌。
	class Sim_ETC_375 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            if (choice == 1 || (p.ownFandralStaghelm > 0 && ownplay))
            {
                p.drawACard(CardDB.cardIDEnum.None, ownplay);
            }
            if (choice == 2 || (p.ownFandralStaghelm > 0 && ownplay))
            {
				
            }
        }
		
	}
}
