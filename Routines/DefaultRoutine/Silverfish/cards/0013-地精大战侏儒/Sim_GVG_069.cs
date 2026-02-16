using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 老式治疗机器人卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力3，生命值3
    // 卡牌效果：<b>战吼：</b>为你的英雄恢复#8点生命值。
    class Sim_GVG_069 : SimTemplate //* 老式治疗机器人 Antique Healbot
    // <b>Battlecry:</b> Restore #8 Health to your hero.
    // <b>战吼：</b>为你的英雄恢复#8点生命值。 
    {
        // 重写战吼效果方法，这是老式治疗机器人卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 检查老式治疗机器人是否属于己方
            if (own.own)
            {
                // 计算己方的实际治疗量（考虑治疗加成）
                int heal = p.getMinionHeal(8);

                // 为己方英雄恢复生命值
                // 传递负的伤害值来实现治疗效果
                // 第三个参数true表示这是治疗操作
                p.minionGetDamageOrHeal(p.ownHero, -heal, true);
            }
            else
            {
                // 计算敌方的实际治疗量（考虑治疗加成）
                int heal = p.getEnemyMinionHeal(8);

                // 为敌方英雄恢复生命值
                p.minionGetDamageOrHeal(p.enemyHero, -heal, true);
            }
        }
    }
}