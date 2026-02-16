using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 尼鲁巴蛛网领主卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力1，生命值4
    // 卡牌效果：战吼随从 +2 费
    class Sim_FP1_017 : SimTemplate //* 尼鲁巴蛛网领主 Nerub'ar Weblord
    {
        // 重写光环开始方法，当尼鲁巴蛛网领主进入战场时调用
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 增加尼鲁巴蛛网领主计数器
            p.尼鲁巴蛛网领主++;
        }

        // 重写光环结束方法，当尼鲁巴蛛网领主离开战场时调用
        public override void onAuraEnds(Playfield p, Minion own)
        {
            // 减少尼鲁巴蛛网领主计数器
            p.尼鲁巴蛛网领主--;
        }
    }
}

//效果实现在carddb.cs的calculateManaCost方法，标记在playfield.cs