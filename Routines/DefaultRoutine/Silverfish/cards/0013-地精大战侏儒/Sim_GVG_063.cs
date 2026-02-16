using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 伯瓦尔·弗塔根卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个圣骑士职业随从卡牌，费用为5点，攻击力1，生命值7
    // 卡牌效果：如果这张牌在你的手牌中，每当一个友方随从死亡，便获得+1攻击力。
    class Sim_GVG_063 : SimTemplate
    {
        // 随从 圣骑士 费用：5 攻击力：1 生命值：7
        // Bolvar Fordragon
        // 伯瓦尔·弗塔根
        // Whenever a friendly minion dies while this is in your hand, gain +1 Attack.
        // 如果这张牌在你的手牌中，每当一个友方随从死亡，便获得+1攻击力。

        // 重写随从死亡时的触发方法，当有友方随从死亡时调用
        public override void onMinionDiedTrigger(Playfield p, Minion m, Minion diedMinion)
        {
            // 检查伯瓦尔·弗塔根是否在手牌中且与死亡随从属于同一方
            if (m.own == diedMinion.own)
            {
                // 检查伯瓦尔·弗塔根是否在手牌中（通过检查是否在场上）
                bool isInHand = true;
                foreach (Minion minion in (m.own ? p.ownMinions : p.enemyMinions))
                {
                    if (minion.entitiyID == m.entitiyID)
                    {
                        isInHand = false;
                        break;
                    }
                }

                // 如果在手牌中，则增加攻击力
                if (isInHand)
                {
                    p.minionGetBuffed(m, 1, 0);
                }
            }
        }
    }
}