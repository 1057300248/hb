using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 黑暗教徒卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力3，生命值4
    // 卡牌效果：<b>亡语：</b>随机使一个友方随从获得+3生命值。
    class Sim_FP1_023 : SimTemplate //* 黑暗教徒 Dark Cultist
    // <b>Deathrattle:</b> Give a random friendly minion +3 Health.
    // <b>亡语：</b>随机使一个友方随从获得+3生命值。
    {
        // 重写亡语效果方法，这是黑暗教徒卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 创建随机数生成器
            Random rand = new Random();

            // 根据黑暗教徒的归属确定要处理的友方随从列表
            List<Minion> friendlyMinions = m.own ? p.ownMinions : p.enemyMinions;

            // 如果有友方随从存在（排除黑暗教徒自己）
            if (friendlyMinions.Count >= 1)
            {
                // 随机选择一个友方随从
                Minion target = friendlyMinions[rand.Next(friendlyMinions.Count)];

                // 给选中的随从增加+3生命值
                p.minionGetBuffed(target, 0, 3);
            }
        }
    }
}