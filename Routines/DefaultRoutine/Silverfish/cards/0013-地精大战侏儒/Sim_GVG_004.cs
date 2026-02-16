using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 地精炎术师卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师随从卡牌，费用为4点，攻击力4，生命值4
    // 卡牌效果：<b>战吼：</b>如果你控制任何机械，则造成4点伤害，随机分配到所有敌人身上。
    class Sim_GVG_004 : SimTemplate //* 地精炎术师 Goblin Blastmage
    // <b>Battlecry:</b> If you have a Mech, deal 4 damage randomly split among all enemies.
    // <b>战吼：</b>如果你控制任何机械，则造成4点伤害，随机分配到所有敌人身上。 
    {
        // 重写战吼效果方法，这是地精炎术师卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 根据随从是否属于己方，确定要检查的随从列表
            // 如果是己方随从（own.own为true），则检查己方随从列表p.ownMinions
            // 如果是敌方随从（own.own为false），则检查敌方随从列表p.enemyMinions
            List<Minion> temp = (own.own) ? p.ownMinions : p.enemyMinions;

            // 遍历对应的随从列表，查找是否存在机械种族的随从
            foreach (Minion m in temp)
            {
                // 检查当前随从的种族是否为机械（MECHANICAL）
                // 将卡牌的race属性转换为TAG_RACE枚举类型进行比较
                if ((TAG_RACE)m.handcard.card.race == TAG_RACE.MECHANICAL)
                {
                    // 如果找到机械随从，触发战吼效果：
                    // 对所有敌方角色（包括敌方英雄和敌方随从）造成4点随机分配的伤害
                    // 参数说明：
                    // - !own.own: 取反操作，如果当前是己方随从，则对敌方造成伤害；如果是敌方随从，则对己方造成伤害
                    // - 4: 总伤害值为4点，会随机分配到所有目标上
                    p.allCharsOfASideGetRandomDamage(!own.own, 4);

                    // 找到第一个机械随从后立即跳出循环，避免重复触发效果
                    break;
                }
            }
        }
    }
}