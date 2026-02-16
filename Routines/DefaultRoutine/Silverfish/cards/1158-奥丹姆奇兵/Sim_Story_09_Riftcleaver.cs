using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 术士 费用：6 攻击力：7 生命值：5
	//Riftcleaver
	//裂隙屠夫
	//<b>Battlecry:</b> Destroy a minion. Your hero takes damage equal to its Health.
	//<b>战吼：</b>消灭一个随从。你的英雄受到等同于该随从生命值的伤害。
	class Sim_Story_09_Riftcleaver : SimTemplate
	{
		
		public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),   // 目标必须是随从
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),    // 目标必须是敌方
            };
        }
	}
}
