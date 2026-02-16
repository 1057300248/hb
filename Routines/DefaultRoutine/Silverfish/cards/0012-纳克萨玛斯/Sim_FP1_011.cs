using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 结网蛛卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为1点，攻击力1，生命值1
    // 卡牌效果：<b>亡语：</b>随机将一张野兽牌置入你的手牌。
    class Sim_FP1_011 : SimTemplate //* 结网蛛 Webspinner
    // <b>Deathrattle:</b> Add a random Beast card to your hand.
    // <b>亡语：</b>随机将一张野兽牌置入你的手牌。 
    {
        // 重写亡语效果方法，这是结网蛛卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 为结网蛛的拥有者添加一张随机野兽牌到手牌
            // 参数说明：- CardDB.cardNameEN.rivercrocolisk：野兽牌名称（河蟹）
            // - m.own：指示为哪一方添加手牌
            // - true：表示这是特殊效果抽卡
            p.drawACard(CardDB.cardNameEN.rivercrocolisk, m.own, true);
        }
    }
}