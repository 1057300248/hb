using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 克洛玛古斯卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张法师职业随从卡牌，费用为8点，攻击力6，生命值8
    // 卡牌效果：每当你抽一张牌时，将该牌的另一张复制置入你的手牌。
    class Sim_BRM_031 : SimTemplate //* 克洛玛古斯 Chromaggus
    // Whenever you draw a card, put another copy into your hand.
    // 每当你抽一张牌时，将该牌的另一张复制置入你的手牌。 
    {
        // 重写光环开始方法，当克洛玛古斯进入战场时触发
        public override void onAuraStarts(Playfield p, Minion own)
        {
            // 根据克洛玛古斯的归属增加相应的计数器
            if (own.own)
            {
                p.复制抽到的牌++; // 增加己方克洛玛古斯计数
            }
            else
            {
                p.敌方复制抽到的牌++; // 增加敌方克洛玛古斯计数
            }
        }

        // 重写光环结束方法，当克洛玛古斯离开战场时触发
        public override void onAuraEnds(Playfield p, Minion own)
        {
            // 根据克洛玛古斯的归属减少相应的计数器
            if (own.own)
            {
                p.复制抽到的牌--; // 减少己方克洛玛古斯计数
            }
            else
            {
                p.敌方复制抽到的牌--; // 减少敌方克洛玛古斯计数
            }
        }
    }
}