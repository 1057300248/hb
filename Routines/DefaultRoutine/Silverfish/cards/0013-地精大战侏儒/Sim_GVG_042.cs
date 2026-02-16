using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 耐普图隆卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为7点，攻击力7，生命值7
    // 卡牌效果：<b>战吼：</b>随机将四张鱼人牌置入你的手牌，<b>过载：</b>（3）
    class Sim_GVG_042 : SimTemplate //* 耐普图隆 Neptulon
    // <b>Battlecry:</b> Add 4 random Murlocs to your hand. <b>Overload:</b> (3)
    // <b>战吼：</b>随机将四张鱼人牌置入你的手牌，<b>过载：</b>（3） 
    {
        // 重写战吼效果方法，这是耐普图隆卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 循环4次，每次添加一张随机鱼人牌到手牌
            for (int i = 0; i < 4; i++)
            {
                // 添加随机鱼人牌到手牌（使用CardDB.cardIDEnum.None表示随机抽牌）
                // 参数说明：- CardDB.cardIDEnum.None：表示随机抽取一张卡牌
                // - m.own：指示添加到哪一方的手牌（true为己方，false为敌方）
                // - true：表示这是特殊效果抽卡（随机获得鱼人牌）
                p.drawACard(CardDB.cardIDEnum.None, m.own, true);
            }

            // 如果是己方耐普图隆，则增加过载计数3点
            if (m.own) p.ueberladung += 3;
        }
    }
}