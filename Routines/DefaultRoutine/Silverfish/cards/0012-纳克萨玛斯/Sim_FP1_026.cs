using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 阿努巴尔伏击者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为4点，攻击力5，生命值1
    // 卡牌效果：<b>亡语：</b>随机将一个友方随从移回你的手牌。
    class Sim_FP1_026 : SimTemplate //* 阿努巴尔伏击者 Anub'ar Ambusher
    // <b>Deathrattle:</b> Return a random friendly minion to your hand.
    // <b>亡语：</b>随机将一个友方随从移回你的手牌。 
    {
        // 重写亡语效果方法，这是阿努巴尔伏击者卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 创建随机数生成器
            Random rand = new Random();

            // 根据阿努巴尔伏击者的归属确定要处理的友方随从列表
            List<Minion> friendlyMinions = m.own ? p.ownMinions : p.enemyMinions;

            // 如果有友方随从存在
            if (friendlyMinions.Count >= 1)
            {
                // 随机选择一个友方随从
                Minion target = friendlyMinions[rand.Next(friendlyMinions.Count)];

                // 将选中的随从移回手牌
                // 参数说明：- 目标随从，- 随从归属，- 0表示立即返回手牌
                p.minionReturnToHand(target, m.own, 0);
            }
        }
    }
}