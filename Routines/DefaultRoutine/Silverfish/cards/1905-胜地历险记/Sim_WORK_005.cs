using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//法术 牧师 费用：4
	//Sharp Shipment
	//快刀快递
	//Give your weapon +2/+2.
	//使你的武器获得+2/+2。
	class Sim_WORK_005 : SimTemplate
	{
		public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
			if (ownplay && p.ownWeapon.Durability > 0)
            {
                p.ownWeapon.Angr += 2; // 设置武器攻击力为3
                p.ownWeapon.Durability += 2; // 设置武器耐久度为3
                p.ownHero.Angr = p.ownWeapon.Angr; // 更新英雄攻击力
                p.ownHero.updateReadyness(); // 更新英雄的准备状态
            }
		}
		
	}
}
