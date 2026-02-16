using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 重型攻城战车卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个战士职业随从卡牌，费用为5点，攻击力5，生命值5
    // 卡牌效果：每当你获得护甲值，使本随从获得+1攻击力。
    class Sim_GVG_086 : SimTemplate
    {
        // 随从 战士 费用：5 攻击力：5 生命值：5
        // Siege Engine
        // 重型攻城战车
        // Whenever you gain Armor, give this minion +1 Attack.
        // 每当你获得护甲值，使本随从获得+1攻击力。

        // 重写获得护甲时的触发方法，当英雄获得护甲时调用
        public override void onHeroGetArmor(Playfield p, Minion triggerMinion, bool ownHero, int armor)
        {
            // 只检查己方随从（因为是"你"获得护甲）
            if (ownHero)
            {
                // 遍历己方所有随从
                foreach (Minion minion in p.ownMinions)
                {
                    // 使用卡牌ID来检查是否为重型攻城战车
                    if (minion.handcard.card.cardIDenum == CardDB.cardIDEnum.GVG_086)
                    {
                        // 给重型攻城战车增加+1攻击力
                        p.minionGetBuffed(minion, 1, 0);
                    }
                }
            }
        }
    }
}