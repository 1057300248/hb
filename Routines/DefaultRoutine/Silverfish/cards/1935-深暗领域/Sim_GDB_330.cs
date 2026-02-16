using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 中立 费用：3 攻击力：5 生命值：1
    //Ur'zul Rager
    //乌祖尔暴怒者
    //<b>Lifesteal</b> <b><b>Spellburst</b>:</b> Attack a random enemy minion.
    //<b>吸血</b><b><b>法术迸发</b>：</b>随机攻击一个敌方随从。
    class Sim_GDB_330 : SimTemplate
    {
        /// <summary>
        /// 法术迸发效果实现
        /// </summary>
        public override void OnSpellburst(Playfield p, Minion m, Handmanager.Handcard hc)
        {
            if (m.own)
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
