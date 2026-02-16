using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    //随从 中立 费用：7 攻击力：7 生命值：7
    //Velen, Leader of the Exiled
    //维伦，流亡者领袖
    //[x]<b>Taunt</b>. <b>Deathrattle:</b> Trigger the<b>Battlecries</b> and <b>Deathrattles</b>of all other Draenei youplayed this game.
    //<b>嘲讽</b>。<b>亡语：</b>触发你在本局对战中使用过的所有其他德莱尼的<b>战吼</b>和<b>亡语</b>。
    class Sim_GDB_131 : SimTemplate
    {
        CardDB.Card blaine = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GDB_131);

        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 通用卡牌ID定义
            CardDB.cardIDEnum targetID = CardDB.cardIDEnum.ICC_466; // 萨隆苦囚的卡牌ID

            if (m.own) // 处理己方情况
            {
                bool hasMinion = false;
                // 检查己方场上是否存在萨隆苦囚
                foreach (Minion mi in p.ownMinions)
                {
                    if (mi.handcard.card.cardIDenum == targetID)
                    {
                        hasMinion = true;
                        break;
                    }
                }
                // 检查己方墓地或场上是否有苦囚
                if (Probabilitymaker.Instance.ownGraveyard.ContainsKey(targetID) || hasMinion)
                {
                    p.callKid(blaine, m.zonepos - 1, true); // 为己方召唤
                }
            }
            else // 处理敌方情况
            {
                bool hasMinion = false;
                // 检查敌方场上是否存在萨隆苦囚
                foreach (Minion mi in p.enemyMinions)
                {
                    if (mi.handcard.card.cardIDenum == targetID)
                    {
                        hasMinion = true;
                        break;
                    }
                }
                // 检查敌方墓地或场上是否有苦囚
                if (Probabilitymaker.Instance.enemyGraveyard.ContainsKey(targetID) || hasMinion)
                {
                    p.callKid(blaine, m.zonepos - 1, false); // 为敌方召唤
                }
            }
        }
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            p.libram += 1;//德莱尼计数器

        }
    }
}