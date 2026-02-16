using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 术士 费用：2 攻击力：3 生命值：2
	//Imployee of the Month
	//月度魔范员工
	//<b>Battlecry:</b> Give a friendly minion <b>Lifesteal</b>.
	//<b>战吼：</b>使一个友方随从获得<b>吸血</b>。
	class Sim_WORK_009 : SimTemplate
	{
		public override void getBattlecryEffect(Playfield p, Minion ownMinion, Minion target, int choice)
        {
            // 检查是否有友方目标随从
            if (target != null && target.own == ownMinion.own)
            {
                target.lifesteal = true;  // 给目标随从添加吸血效果
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),     // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),    // 目标必须是友方随从
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),      // 目标必须是随从
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE) // 如果有目标，则必须选择目标
            };
        }
		
	}
}
