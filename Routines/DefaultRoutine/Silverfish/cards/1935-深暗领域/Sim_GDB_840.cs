using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HREngine.Bots
{
	//随从 猎人 费用：2 攻击力：0 生命值：2
	//Extraterrestrial Egg
	//异星虫卵
	//[x]<b>Deathrattle:</b> Summon a3/5 Beast that attacks thelowest Health enemy.
	//<b>亡语：</b>召唤一只3/5的野兽并使其攻击生命值最低的敌人。
	class Sim_GDB_840 : SimTemplate
	{
		CardDB.Card c = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GDB_840t);//3/5的野兽
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 召唤3/5的野兽到死亡随从左侧位置
            int pos = m.zonepos - 1;
            p.callKid(c, pos, m.own);

            // 获取新召唤的野兽
            Minion summoned = null;
            List<Minion> ownerMinions = m.own ? p.ownMinions : p.enemyMinions;
            if (pos >= 0 && pos < ownerMinions.Count)
            {
                summoned = ownerMinions[pos];
            }
            else
            {
                summoned = ownerMinions.LastOrDefault();
            }
            if (summoned == null) return;

            // 根据所属方确定敌人列表
            List<Minion> enemies = m.own ? p.enemyMinions : p.ownMinions;
            Minion target = p.searchRandomMinion(enemies, searchmode.searchLowestHP);

            // 如果没有找到随从目标，则攻击敌方英雄
            if (target == null)
            {
                target = m.own ? p.enemyHero : p.ownHero;
            }

            // 强制攻击目标（忽略冲锋限制）
            if (target != null)
            {
                p.minionAttacksMinion(summoned, target);
            }
        }
    }
}
