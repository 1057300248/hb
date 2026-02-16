using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    class Sim_CS2_038 : SimTemplate //* 先祖之魂 Ancestral Spirit
    {
        //Give a minion "<b>Deathrattle:</b> Resummon this minion."
        //使一个随从获得“<b>亡语</b>：再次召唤该随从。”
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            if (ownplay && target != null)
            {
                target.ancestralspirit++;
            }
        }

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),     // 需要指定目标
                new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),     // 目标必须是随从
                new PlayReq(CardDB.ErrorType2.REQ_FRIENDLY_TARGET),   // 目标必须是友方随从               
            };
        }
    }
}