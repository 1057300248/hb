using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace HREngine.Bots
{
    //法术 萨满祭司 费用：1
    //Pop-Up Book
    //立体书
    //Deal $2 damage. Summon two 0/1 Frogs with <b>Taunt</b>.
    //造成$2点伤害。召唤两只0/1并具有<b>嘲讽</b>的青蛙。
    class Sim_TOY_508 : SimTemplate
    {
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.hexfrog);
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 对目标造成2点伤害
            p.minionGetDamageOrHeal(target, 2);

            int pos = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;
            p.callKid(kid, pos, ownplay, false);
            p.callKid(kid, pos, ownplay);
        }
        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),
            };
        }
    }
}
