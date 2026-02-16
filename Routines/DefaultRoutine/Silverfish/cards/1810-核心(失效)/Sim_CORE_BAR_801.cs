using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 猎人 费用：1
	//Wound Prey
	//击伤猎物
	//Deal $1 damage. Summon a 1/1 Hyena with <b>Rush</b>.
	//造成$1点伤害。召唤一只1/1并具有<b>突袭</b>的土狼。
	class Sim_CORE_BAR_801 : SimTemplate
	{
		CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.BAR_035t);

		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            int dmg = (ownplay) ? p.getSpellDamageDamage(1) : p.getEnemySpellDamageDamage(1);
            p.minionGetDamageOrHeal(target, dmg);
            int pos = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;            
            p.callKid(kid, pos, ownplay, false);
		}

        public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),    // 目标必须是敌方
				new PlayReq(CardDB.ErrorType2.REQ_NUM_MINION_SLOTS, 1), //确保有位置召唤
            };
        }
		
	}
}
