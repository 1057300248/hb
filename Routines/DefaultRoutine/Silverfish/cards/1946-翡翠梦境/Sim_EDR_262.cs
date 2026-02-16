using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 猎人 费用：3
	//Spirit Bond
	//灵魂联结
	//Deal $3 damageto a minion.If that kills it, summon a3/2 Wolf with <b>Rush</b>.
	//对一个随从造成$3点伤害。如果消灭该随从，召唤一只3/2并具有<b>突袭</b>的狼。
	class Sim_EDR_262 : SimTemplate
	{
		// 预加载狼的卡牌数据
        CardDB.Card wolf = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EDR_850pe);
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
			bool targetWasAlive = target.Hp > 0; // 记录目标是否存活

            // 造成$3点伤害
            int dmg = (ownplay) ? p.getSpellDamageDamage(3) : p.getEnemySpellDamageDamage(3);
            p.minionGetDamageOrHeal(target, dmg);

			// 判断消灭条件
            if (targetWasAlive && target.Hp <= 0)
            {
                // 获取召唤位置
                int pos = ownplay ? p.ownMinions.Count : p.enemyMinions.Count;

                // 召唤狼
                p.callKid(wolf, pos, ownplay);

            }
        }
		
		public override PlayReq[] GetPlayReqs()
        {
            return new PlayReq[] {
                new PlayReq(CardDB.ErrorType2.REQ_TARGET_TO_PLAY),  // 需要选择一个目标
				new PlayReq(CardDB.ErrorType2.REQ_MINION_TARGET),    // 目标必须是随从
                new PlayReq(CardDB.ErrorType2.REQ_ENEMY_TARGET),    // 目标必须是敌方
            };
        }
	}
}
