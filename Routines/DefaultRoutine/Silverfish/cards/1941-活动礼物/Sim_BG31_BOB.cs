using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 中立 费用：6 攻击力：4 生命值：5
	//Bob the Bartender
	//调酒师鲍勃
	//[x]<b>Battlecry:</b> Choosean action from theBattlegrounds!
	//<b>战吼：</b>选择一项酒馆战棋绝技！
	class Sim_BG31_BOB : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            if (choice == 1)
			{
				List<Minion> temp = (ownplay) ? p.enemyMinions : p.ownMinions;
                foreach (Minion t in temp)
                {
                    p.minionGetFrozen(t);
                }
			}
			if (choice == 2)
			{
				if (target != null && !target.own) // 确保目标是敌方随从
                {
                    // 将目标随从移回玩家的手牌
                    p.minionReturnToHand(target, ownplay, target.handcard.getManaCost(p));
                }
			}
            if (choice == 3)
            {
                
            }
            if (choice == 4)
            {
               
            }
            
        }
        
		
	}
}
