using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 硬币卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业法术卡牌，费用为0点
    // 卡牌效果：获得1点临时法力值。
    class Sim_FP1_COIN : SimTemplate
    {
        // 重写卡牌使用效果方法，这是硬币卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 为当前玩家增加1点临时法力值
            if (ownplay)
            {
                // 增加己方当前可用法力
                p.mana++;
            }
        }
    }
}