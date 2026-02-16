using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 玛洛恩卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为7点，攻击力9，生命值7
    // 卡牌效果：<b>亡语：</b>将该随从洗入你的牌库。
    class Sim_GVG_035 : SimTemplate //* 玛洛恩 Malorne
    // <b>Deathrattle:</b> Shuffle this minion into your deck.
    // <b>亡语：</b>将该随从洗入你的牌库。 
    {
        // 重写亡语效果方法，这是玛洛恩卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 检查玛洛恩是否属于己方
            if (m.own)
            {
                // 增加己方牌库大小计数（模拟将玛洛恩洗入牌库）
                p.ownDeckSize++;

                // 可选：记录日志信息
                // Helpfunctions.Instance.logg("玛洛恩亡语：洗入己方牌库");
            }
            else
            {
                // 增加敌方牌库大小计数（模拟将玛洛恩洗入牌库）
                p.enemyDeckSize++;

                // 可选：记录日志信息
                // Helpfunctions.Instance.logg("玛洛恩亡语：洗入敌方牌库");
            }
        }
    }
}