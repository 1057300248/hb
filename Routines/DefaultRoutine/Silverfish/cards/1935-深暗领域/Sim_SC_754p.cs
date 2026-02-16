using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//英雄技能 无效的 费用：2
	//Twin Blades
	//双刃
	//[x]Give a friendly minion andyour hero +1 Attack thisturn and <b>Divine Shield</b>.
	//使一个友方随从和你的英雄获得在本回合中的+1攻击力，以及<b>圣盾</b>。
	class Sim_SC_754p : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            if (ownplay)
            {
                p.minionGetTempBuff(p.ownHero, 1, 0);
                target.divineshild = true;
            }
            else
            {
                p.minionGetTempBuff(p.enemyHero, 1, 0);
                target.divineshild = true;
            }
        }
		public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_IF_AVAILABLE),  // 目标必须是角色
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),  // 目标必须是友方角色
            };
        }
		
	}
}
