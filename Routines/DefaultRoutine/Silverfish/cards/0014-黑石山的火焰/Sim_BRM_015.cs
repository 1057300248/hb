using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 复仇打击卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张战士职业法术卡牌，费用为2点
    // 卡牌效果：对所有随从造成1点伤害。如果你的生命值小于或等于12点，则改为造成3点伤害。
    class Sim_BRM_015 : SimTemplate //* 复仇打击 Revenge
    // Deal $1 damage to all minions. If you have 12 or less Health, deal $3 damage instead.
    // 对所有随从造成$1点伤害。如果你的生命值小于或等于12点，则改为造成$3点伤害。 
    {
        // 重写卡牌使用效果方法，这是复仇打击卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 初始化伤害值为1
            int damage = 1;

            // 检查使用方英雄的生命值
            int heroHealth = (ownplay) ? p.ownHero.Hp : p.enemyHero.Hp;

            // 如果英雄生命值小于或等于12，则伤害值改为3
            if (heroHealth <= 12)
            {
                damage = 3;
            }

            // 计算最终伤害值，考虑法术伤害加成
            damage = (ownplay) ? p.getSpellDamageDamage(damage) : p.getEnemySpellDamageDamage(damage);

            // 对所有随从造成伤害
            p.allMinionsGetDamage(damage);
        }
    }
}