using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 舞动之剑卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力4，生命值1
    // 卡牌效果：<b>亡语：</b>你的对手抽一张牌。
    class Sim_FP1_029 : SimTemplate //* 舞动之剑 Dancing Swords
    // <b>Deathrattle:</b> Your opponent draws a card.
    // <b>亡语：</b>你的对手抽一张牌。 
    {
        // 重写亡语效果方法，这是舞动之剑卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 让对手抽一张牌
            // 参数说明：- CardDB.cardIDEnum.None：表示随机抽取一张卡牌
            // - !m.own：取反操作，如果舞动之剑是己方的，则让敌方抽牌；如果是敌方的，则让己方抽牌
            p.drawACard(CardDB.cardIDEnum.None, !m.own);
        }
    }
}