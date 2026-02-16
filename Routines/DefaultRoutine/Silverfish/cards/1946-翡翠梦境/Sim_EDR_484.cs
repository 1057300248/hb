using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 中立 费用：7 攻击力：2 生命值：7
	//Scavenging Flytrap
	//食腐捕蝇草
	//After a minion dies,gain its Attack.
	//在一个随从死亡后，获得其攻击力。
	class Sim_EDR_484 : SimTemplate
	{
		public override void onMinionDiedTrigger(Playfield p, Minion m, Minion diedMinion)
        {
            if (m.silenced) return;
            
            // 当任何随从死亡时，获得其攻击力
            p.minionGetBuffed(m, diedMinion.Angr, 0);
        }
	}
}
