using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 肉用僵尸卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为1点，攻击力1，生命值3
    // 卡牌效果：<b>亡语：</b>为敌方英雄恢复#5点生命值。
    class Sim_FP1_001 : SimTemplate //* 肉用僵尸 Zombie Chow
    // <b>Deathrattle:</b> Restore #5 Health to the enemy hero.
    // <b>亡语：</b>为敌方英雄恢复#5点生命值。 
    {
        // 重写亡语效果方法，这是肉用僵尸卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 计算实际治疗量（考虑治疗加成）
            int heal = (m.own) ? p.getMinionHeal(5) : p.getEnemyMinionHeal(5);

            // 为敌方英雄恢复生命值
            // 参数说明：- 目标英雄（如果肉用僵尸是己方的，则治疗敌方英雄；反之亦然）
            // - 负的伤害值（表示治疗）
            p.minionGetDamageOrHeal(m.own ? p.enemyHero : p.ownHero, -heal);
        }
    }
}