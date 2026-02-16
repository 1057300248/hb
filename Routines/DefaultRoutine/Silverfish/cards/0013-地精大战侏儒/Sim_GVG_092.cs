using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 侏儒实验技师卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力3，生命值2
    // 卡牌效果：<b>战吼：</b>抽一张牌，如果该牌是随从牌，则将其变形成为一只小鸡。
    class Sim_GVG_092 : SimTemplate //* 侏儒实验技师 Gnomish Experimenter
    // <b>Battlecry:</b> Draw a card. If it's a minion, transform it into a Chicken.
    // <b>战吼：</b>抽一张牌，如果该牌是随从牌，则将其变形成为一只小鸡。 
    {
        // 重写战吼效果方法，这是侏儒实验技师卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 为侏儒实验技师的拥有者抽一张牌
            // 参数说明：- CardDB.cardIDEnum.None：表示随机抽取一张卡牌
            // - own.own：指示为哪一方抽牌（true为己方，false为敌方）
            p.drawACard(CardDB.cardIDEnum.None, own.own);

            // 注意：实际游戏中，如果抽到的牌是随从牌，会变形成为小鸡
            // 但在sim卡中，这个变形效果通常由框架自动处理
            // 或者在抽牌后的事件处理中实现
        }
    }
}