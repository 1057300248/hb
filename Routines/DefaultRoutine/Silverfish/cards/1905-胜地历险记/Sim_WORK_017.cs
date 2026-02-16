using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 潜行者 费用：2
	//Silvermoon Brochure
	//银月城宣传单
	//Give a minion <b>Immune</b> this turn and +2/+2. <i>(Flips each turn.)</i>
	//使一个随从获得在本回合中<b>免疫</b>和+2/+2。<i>（每回合翻面。）</i>
	class Sim_WORK_017 : SimTemplate
	{
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 给目标随从 +2/+2
            p.minionGetBuffed(target, 2, 2);

            // 使目标随从在本回合中获得免疫
            target.immune = true;
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[]
            {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET)    // 目标必须是随从
            };
        }
    }
}
