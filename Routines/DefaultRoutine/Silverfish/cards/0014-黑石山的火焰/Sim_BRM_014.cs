using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 熔火怒犬卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张猎人职业随从卡牌，费用为3点，攻击力4，生命值4
    // 卡牌效果：<b>战吼：</b>如果你没有其他手牌，则获得+3/+3。
    class Sim_BRM_014 : SimTemplate //* 熔火怒犬 Core Rager
    // <b>Battlecry:</b> If your hand is empty, gain +3/+3.
    // <b>战吼：</b>如果你没有其他手牌，则获得+3/+3。 
    {
        // 重写战吼效果方法，这是熔火怒犬卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion m, Minion target, int choice)
        {
            // 检查手牌数量
            int handCardsCount = (m.own) ? p.owncards.Count : p.enemyAnzCards;

            // 如果手牌为空（不包括这张正在使用的牌）
            if (handCardsCount <= 0)
            {
                // 给熔火怒犬增加+3攻击力和+3生命值
                p.minionGetBuffed(m, 3, 3);
            }
        }
    }
}