using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 钢铁战蝎卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为6点，攻击力6，生命值5
    // 卡牌效果：<b>战吼：</b>将一张"地雷" 牌洗入你对手的牌库。当玩家抽到"地雷"时，便会受到10点伤害。
    class Sim_GVG_056 : SimTemplate //* 钢铁战蝎 Iron Juggernaut
    // <b>Battlecry:</b> Shuffle a Mine into your opponent's deck. When drawn, it explodes for 10 damage.
    // <b>战吼：</b>将一张"地雷" 牌洗入你对手的牌库。当玩家抽到"地雷"时，便会受到10点伤害。 
    {
        // 重写战吼效果方法，这是钢铁战蝎卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 检查钢铁战蝎是否属于己方
            if (own.own)
            {
                // 增加敌方牌库大小（模拟将地雷洗入牌库）
                p.enemyDeckSize++;
            }
            else
            {
                // 如果是敌方钢铁战蝎，则增加己方牌库大小
                p.ownDeckSize++;
            }
        }
    }
}