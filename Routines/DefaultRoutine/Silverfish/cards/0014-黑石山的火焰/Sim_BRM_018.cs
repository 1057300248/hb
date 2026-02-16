using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 龙王配偶卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业随从卡牌，费用为5点，攻击力5，生命值5
    // 卡牌效果：<b>战吼：</b>你的下一张龙牌的法力值消耗减少（2）点。
    class Sim_BRM_018 : SimTemplate //* 龙王配偶 Dragon Consort
    // <b>Battlecry:</b> The next Dragon you play costs (2) less.
    // <b>战吼：</b>你的下一张龙牌的法力值消耗减少（2）点。 
    {
        // 重写战吼效果方法，这是龙王配偶卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 检查龙王配偶的归属
            if (m.own)
            {
                // 增加己方龙王配偶计数器
                // 这个计数器会用于减少下一张龙牌的费用
                p.龙族减费++;
            }
        }
    }
}