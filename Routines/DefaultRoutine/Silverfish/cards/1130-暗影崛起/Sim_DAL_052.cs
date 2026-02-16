using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 萨满祭司 费用：5 攻击力：4 生命值：4
	//Muckmorpher
	//泥沼变形怪
	//[x]<b>Battlecry:</b> Transform intoa 4/4 copy of a differentminion in your deck.
	//<b>战吼：</b>变形成为你的牌库中一个其他随从的4/4复制。
	class Sim_DAL_052 : SimTemplate
	{


        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.WW_382);
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            p.libram += 1;//圣契指示物加1
            p.callKid(kid, own.zonepos, own.own);
        }	    
	}
}
