using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 明光祭司卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力2，生命值4
    // 卡牌效果：如果在你的回合结束时，你控制一个<b>奥秘</b>，则为你的英雄恢复#4点生命值。
    class Sim_GVG_089 : SimTemplate //* 明光祭司 Illuminator
    // If you control a <b>Secret</b> at the end of your turn, restore #4 Health to your hero.
    // 如果在你的回合结束时，你控制一个<b>奥秘</b>，则为你的英雄恢复#4点生命值。 
    {
        // 重写回合结束触发方法，这是明光祭司卡牌效果的核心实现
        public override void onTurnEndsTrigger(Playfield p, Minion triggerEffectMinion, bool turnEndOfOwner)
        {
            // 检查明光祭司是否属于当前回合结束的一方
            if (turnEndOfOwner == triggerEffectMinion.own)
            {
                // 检查当前回合结束的一方是否控制至少一个奥秘
                if (((turnEndOfOwner) ? p.ownSecretsIDList.Count : p.enemySecretList.Count) >= 1)
                {
                    // 计算实际治疗量（考虑治疗加成）
                    int heal = (turnEndOfOwner) ? p.getMinionHeal(4) : p.getEnemyMinionHeal(4);

                    // 为当前回合结束一方的英雄恢复生命值
                    // 参数说明：- 目标英雄，- 负的伤害值（表示治疗），- true表示这是治疗操作
                    p.minionGetDamageOrHeal(((turnEndOfOwner) ? p.ownHero : p.enemyHero), -heal, true);
                }
            }
        }
    }
}