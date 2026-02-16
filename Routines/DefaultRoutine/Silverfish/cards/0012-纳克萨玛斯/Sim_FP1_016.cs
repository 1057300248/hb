using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 哀嚎的灵魂卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力3，生命值5
    // 卡牌效果：<b>战吼：沉默</b>你的其他随从。
    class Sim_FP1_016 : SimTemplate //* 哀嚎的灵魂 Wailing Soul
    // <b>Battlecry: Silence</b> your other minions.
    // <b>战吼：沉默</b>你的其他随从。 
    {
        // 重写战吼效果方法，这是哀嚎的灵魂卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 只处理己方随从（哀嚎的灵魂只能沉默自己的其他随从）
            if (own.own)
            {
                // 遍历所有己方随从，沉默除了哀嚎的灵魂以外的其他随从
                foreach (Minion m in p.ownMinions)
                {
                    // 排除哀嚎的灵魂自己
                    if (m.entitiyID != own.entitiyID)
                    {
                        // 沉默目标随从
                        p.minionGetSilenced(m);
                    }
                }
            }
        }
    }
}