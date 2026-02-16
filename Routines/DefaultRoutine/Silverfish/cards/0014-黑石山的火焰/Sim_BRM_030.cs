using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 奈法利安卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业随从卡牌，费用为9点，攻击力8，生命值8
    // 卡牌效果：<b>战吼：</b>随机将两张（你对手职业的）法术牌置入你的手牌。
    class Sim_BRM_030 : SimTemplate //* 奈法利安 Nefarian
    // <b>Battlecry:</b> Add two random spells from your opponent's class to your hand.
    // <b>战吼：</b>随机将两张（你对手职业的）法术牌置入你的手牌。
    {
        // 重写战吼效果方法，这是奈法利安卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 随机添加两张卡牌到手牌
            p.drawACard(CardDB.cardIDEnum.None, m.own, true);
            p.drawACard(CardDB.cardIDEnum.None, m.own, true);
        }
    }
}