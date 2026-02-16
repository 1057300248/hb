using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 榴弹投手卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力3，生命值3
    // 卡牌效果：<b>战吼：</b>随机对一个敌方随从造成4点伤害。
    class Sim_GVG_099 : SimTemplate //* 榴弹投手 Bomb Lobber
    // <b>Battlecry:</b> Deal 4 damage to a random enemy minion.
    // <b>战吼：</b>随机对一个敌方随从造成4点伤害。 
    {
        // 重写战吼效果方法，这是榴弹投手卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 根据榴弹投手的归属确定要攻击的敌方随从列表
            List<Minion> enemyMinions = (own.own) ? p.enemyMinions : p.ownMinions;

            // 计算实际伤害值（考虑法术伤害加成）
            int damage = (own.own) ? p.getSpellDamageDamage(4) : p.getEnemySpellDamageDamage(4);

            // 如果有敌方随从存在
            if (enemyMinions.Count >= 1)
            {
                // 创建随机数生成器
                Random rand = new Random();

                // 随机选择一个敌方随从
                Minion targetMinion = enemyMinions[rand.Next(enemyMinions.Count)];

                // 对选中的敌方随从造成伤害
                p.minionGetDamageOrHeal(targetMinion, damage);
            }
        }
    }
}