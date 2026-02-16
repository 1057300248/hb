using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//英雄 无效的 费用：7
	//Kerrigan, Queen of Blades
	//刀锋女王凯瑞甘
	//[x]<b>Battlecry:</b> Summon two2/5 Brood Queens. Deal3 damage to all enemies.
	//<b>战吼：</b>召唤两个2/5的虫巢女王。对所有敌人造成3点伤害。
	class Sim_SC_004 : SimTemplate
	{
		CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.SC_003);
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
			p.setNewHeroPower(CardDB.cardIDEnum.SC_004hp, ownplay); 
            if (ownplay) p.ownHero.armor += 5;
            else p.enemyHero.armor += 5;
			
			int pos = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;
            p.callKid(kid, pos, ownplay);
            p.callKid(kid, pos, ownplay);
			p.allCharsOfASideGetDamage(!ownplay, 3);
        }
	}
}
