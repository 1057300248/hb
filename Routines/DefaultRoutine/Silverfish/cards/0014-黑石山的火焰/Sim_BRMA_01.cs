using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 烈焰之心卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张中立职业法术卡牌，费用为3点
    // 卡牌效果：抽两张牌。获得4点护甲值。
    class Sim_BRMA_01 : SimTemplate //* 烈焰之心 Flameheart
    // Draw 2 cards. Gain 4 Armor.
    // 抽两张牌。获得4点护甲值。
    {
        // 重写卡牌使用效果方法，这是烈焰之心卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 抽两张牌
            p.drawACard(CardDB.cardIDEnum.None, ownplay); // 抽第一张牌
            p.drawACard(CardDB.cardIDEnum.None, ownplay); // 抽第二张牌

            // 给英雄增加4点护甲值
            // 参数说明：- 目标英雄（根据使用方选择己方或敌方英雄），- 增加的护甲值
            Minion hero = ownplay ? p.ownHero : p.enemyHero;
            p.minionGetArmor(hero, 4);
        }
    }
}