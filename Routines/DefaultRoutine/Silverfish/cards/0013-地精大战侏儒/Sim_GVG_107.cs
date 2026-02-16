using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 强化机器人卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为4点，攻击力3，生命值2
    // 卡牌效果：<b>战吼：</b>随机使你的其他随从分别获得<b>风怒</b>，<b>嘲讽</b>，或者<b>圣盾</b>效果中的一种。
    class Sim_GVG_107 : SimTemplate //* 强化机器人 Enhance-o Mechano
    // <b>Battlecry:</b> Give your other minions <b>Windfury</b>, <b>Taunt</b>, or <b>Divine Shield</b><i>(at random)</i>.
    // <b>战吼：</b>随机使你的其他随从分别获得<b>风怒</b>，<b>嘲讽</b>，或者<b>圣盾</b>效果中的一种。 
    {
        // 重写战吼效果方法，这是强化机器人卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 根据强化机器人的归属确定要处理的随从列表
            List<Minion> minionsToEnhance = (own.own) ? p.ownMinions : p.enemyMinions;

            // 创建随机数生成器
            Random rand = new Random();

            // 遍历所有需要增强的随从（排除强化机器人自己）
            foreach (Minion m in minionsToEnhance)
            {
                if (m.entitiyID == own.entitiyID) continue;

                // 随机选择一种增强效果（0=风怒，1=嘲讽，2=圣盾）
                int enhancementType = rand.Next(3);

                switch (enhancementType)
                {
                    case 0: // 风怒
                        if (!m.windfury)
                        {
                            m.windfury = true;
                        }
                        break;
                    case 1: // 嘲讽
                        if (!m.taunt)
                        {
                            m.taunt = true;
                            if (m.own) p.anzOwnTaunt++;
                            else p.anzEnemyTaunt++;
                        }
                        break;
                    case 2: // 圣盾
                        if (!m.divineshild)
                        {
                            m.divineshild = true;
                        }
                        break;
                }
            }
        }
    }
}