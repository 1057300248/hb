using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 恶魔猎手 费用：7 攻击力：4 生命值：10
    //Mythical Terror
    //神秘恐魔
    //[x]<b>Lifesteal</b>At the end of your turn,force all enemy minionsto attack this.
    //<b>吸血</b>。在你的回合结束时，攻击所有敌方随从。
    class Sim_TTN_866 : SimTemplate
    {
        public override void onTurnEndsTrigger(Playfield p, Minion m, bool turnEndOfOwner)
        {
            if (m.silenced == false && turnEndOfOwner == m.own)
            {
                List<Minion> temp = (m.own) ? p.enemyMinions : p.ownMinions;

                for (int i = 0; i < temp.Count; i++)
                {
                    Minion sm = temp[i];
                    p.minionAttacksMinion(m, sm);
                }
            }
        }
    }
}
