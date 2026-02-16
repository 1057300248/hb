using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 圣骑士 费用：2
	//Mark of Ursol
	//乌索尔印记
	//[x]Choose a minion. If it'san enemy, set its statsto 1/1. If it's friendly, setits stats to 3/3 instead.
	//选择一个随从。如果是敌方随从，将其属性值变为1/1；如果是友方随从，改为将其属性值变为3/3。
	class Sim_EDR_252 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 根据目标阵营设置属性
            if (target.own == ownplay)
            {
                // 友方随从设置为3/3
                p.minionGetBuffed(target, 3, 3);
            }
            else
            {
                // 敌方随从设置为1/1
                p.minionGetBuffed(target, 1, 1);
            }
        }

		public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),    // 目标必须是随从
            };
        }
    }
}
