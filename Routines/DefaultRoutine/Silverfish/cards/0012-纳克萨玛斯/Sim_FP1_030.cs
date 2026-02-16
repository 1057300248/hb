using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 洛欧塞布卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力5，生命值5
    // 卡牌效果：<b>战吼：</b>下个回合敌方法术的法力值消耗增加（5）点。
    class Sim_FP1_030 : SimTemplate //* 洛欧塞布 Loatheb
    // <b>Battlecry:</b> Enemy spells cost (5) more next turn.
    // <b>战吼：</b>下个回合敌方法术的法力值消耗增加（5）点。 
    {
        // 重写战吼效果方法，这是洛欧塞布卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 设置洛欧塞布效果标志位为true
            // 这会在敌方下个回合开始时触发法术费用增加效果
            p.法术费用增加 = true;
        }
    }
}