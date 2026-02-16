using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 黑铁潜藏者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张潜行者职业随从卡牌，费用为5点，攻击力4，生命值3
    // 卡牌效果：<b>战吼：</b>对所有未受伤的敌方随从造成2点伤害。
    class Sim_BRM_008 : SimTemplate //* 黑铁潜藏者 Dark Iron Skulker
    // <b>Battlecry:</b> Deal 2 damage to all undamaged enemy minions.
    // <b>战吼：</b>对所有未受伤的敌方随从造成2点伤害。 
    {
        // 重写战吼效果方法，这是黑铁潜藏者卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 根据黑铁潜藏者的归属确定要处理的敌方随从列表
            List<Minion> enemyMinions = (m.own) ? p.enemyMinions : p.ownMinions;

            // 遍历所有敌方随从
            foreach (Minion enemyMinion in enemyMinions)
            {
                // 检查随从是否未受伤（健康状态）
                if (!enemyMinion.wounded)
                {
                    // 对未受伤的敌方随从造成2点伤害
                    p.minionGetDamageOrHeal(enemyMinion, 2);
                }
            }
        }
    }
}