using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 圣光炸弹卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为6点
    // 卡牌效果：对所有随从造成等同于其攻击力的伤害。
    class Sim_GVG_008 : SimTemplate //* 圣光炸弹 Lightbomb
                                    // Deal damage to each minion equal to its Attack.
                                    // 对所有随从造成等同于其攻击力的伤害。 
    {
        // 重写卡牌打出时的效果方法，这是圣光炸弹卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 遍历己方场上的所有随从
            foreach (Minion m in p.ownMinions)
            {
                // 对每个己方随从造成等同于其当前攻击力的伤害
                // 参数说明：
                // - m: 目标随从对象
                // - m.Angr: 伤害值，等于随从的当前攻击力
                // - true: 第三个参数为true，表示这是一个伤害操作（而不是治疗）
                p.minionGetDamageOrHeal(m, m.Angr, true);
            }

            // 遍历敌方场上的所有随从
            foreach (Minion m in p.enemyMinions)
            {
                // 对每个敌方随从造成等同于其当前攻击力的伤害
                // 参数说明：
                // - m: 目标随从对象
                // - m.Angr: 伤害值，等于随从的当前攻击力
                // - true: 第三个参数为true，表示这是一个伤害操作（而不是治疗）
                p.minionGetDamageOrHeal(m, m.Angr, true);
            }
        }
    }
}