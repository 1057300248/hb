using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 砰砰机器人卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为1点，攻击力1，生命值1
    // 卡牌效果：<b>亡语：</b>随机对一个敌人造成1-4点伤害。
    class Sim_GVG_110t : SimTemplate //* 砰砰机器人 Boom Bot
    // <b>Deathrattle:</b> Deal 1-4 damage to a random enemy.
    // <b>亡语：</b>随机对一个敌人造成1-4点伤害。 
    {
        // 重写亡语效果方法，这是砰砰机器人卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 创建所有可能目标的列表（包括敌方随从和敌方英雄）
            List<Minion> possibleTargets = new List<Minion>();

            // 添加敌方随从到目标列表
            if (m.own)
            {
                possibleTargets.AddRange(p.enemyMinions);
                // 添加敌方英雄到目标列表
                possibleTargets.Add(p.enemyHero);
            }
            else
            {
                possibleTargets.AddRange(p.ownMinions);
                // 添加己方英雄到目标列表
                possibleTargets.Add(p.ownHero);
            }

            // 创建随机数生成器
            Random rand = new Random();

            // 随机生成1-4点伤害
            int damage = rand.Next(1, 5);

            // 如果有可攻击的目标
            if (possibleTargets.Count > 0)
            {
                // 随机选择一个目标
                Minion target = possibleTargets[rand.Next(possibleTargets.Count)];

                // 对选中的目标造成随机伤害
                p.minionGetDamageOrHeal(target, damage);
            }
        }
    }
}