using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 金刚刃牙兽卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个猎人法师职业随从卡牌，费用为3点，攻击力6，生命值6
    // 卡牌效果：<b>战吼：</b>使你的其他机械获得+4攻击力。
    class Sim_TB_BaconUps_066 : SimTemplate //* 金刚刃牙兽 Metaltooth Leaper
    // <b>Battlecry:</b> Give your other Mechs +4 Attack.
    // <b>战吼：</b>使你的其他机械获得+4攻击力。
    {
        // 重写战吼效果方法，这是金刚刃牙兽卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 根据金刚刃牙兽的归属确定要处理的随从列表
            List<Minion> minionsToBuff = (own.own) ? p.ownMinions : p.enemyMinions;

            // 遍历所有随从，给其他机械随从增加攻击力
            foreach (Minion m in minionsToBuff)
            {
                // 排除金刚刃牙兽自己，并检查是否为机械种族
                if (m.entitiyID != own.entitiyID && (TAG_RACE)m.handcard.card.race == TAG_RACE.MECHANICAL)
                {
                    // 给符合条件的机械随从增加+4攻击力
                    p.minionGetBuffed(m, 4, 0);
                }
            }
        }
    }
}