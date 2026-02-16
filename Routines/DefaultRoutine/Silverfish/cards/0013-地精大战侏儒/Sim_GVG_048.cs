using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 金刚刃牙兽卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力3，生命值3
    // 卡牌效果：<b>战吼：</b>使你的其他机械获得+2攻击力。
    class Sim_GVG_048 : SimTemplate //* 金刚刃牙兽 Metaltooth Leaper
    // <b>Battlecry:</b> Give your other Mechs +2 Attack.
    // <b>战吼：</b>使你的其他机械获得+2攻击力。 
    {
        // 重写战吼效果方法，这是金刚刃牙兽卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 根据随从的归属确定要检查的随从列表（己方或敌方）
            List<Minion> temp = (own.own) ? p.ownMinions : p.enemyMinions;

            // 遍历所有随从，为其他机械随从增加攻击力
            foreach (Minion m in temp)
            {
                // 跳过金刚刃牙兽自身
                if (m.entitiyID == own.entitiyID) continue;

                // 检查随从是否为机械种族
                if ((TAG_RACE)m.handcard.card.race == TAG_RACE.MECHANICAL)
                {
                    // 给符合条件的机械随从增加+2攻击力
                    p.minionGetBuffed(m, 2, 0);
                }
            }
        }
    }
}