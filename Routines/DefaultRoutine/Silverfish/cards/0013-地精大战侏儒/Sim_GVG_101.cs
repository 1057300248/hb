using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 血色净化者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力2，生命值3
    // 卡牌效果：<b>战吼：</b>对所有具有<b>亡语</b>的随从造成2点伤害。
    class Sim_GVG_101 : SimTemplate //* 血色净化者 Scarlet Purifier
    // <b>Battlecry:</b> Deal 2 damage to all minions with <b>Deathrattle</b>.
    // <b>战吼：</b>对所有具有<b>亡语</b>的随从造成2点伤害。 
    {
        // 重写战吼效果方法，这是血色净化者卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 遍历己方所有随从
            foreach (Minion m in p.ownMinions)
            {
                // 检查随从是否具有亡语效果且未被沉默
                if (m.handcard.card.deathrattle && !m.silenced)
                {
                    // 对符合条件的随从造成2点伤害
                    p.minionGetDamageOrHeal(m, 2);
                }
            }

            // 遍历敌方所有随从
            foreach (Minion m in p.enemyMinions)
            {
                // 检查随从是否具有亡语效果且未被沉默
                if (m.handcard.card.deathrattle && !m.silenced)
                {
                    // 对符合条件的随从造成2点伤害
                    p.minionGetDamageOrHeal(m, 2);
                }
            }
        }
    }
}