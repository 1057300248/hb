using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 疯狂的科学家卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为2点，攻击力2，生命值2
    // 卡牌效果：亡语：从你的牌库中将一张奥秘牌置入战场。
    class Sim_FP1_004 : SimTemplate //* 疯狂的科学家 Mad Scientist
    // Deathrattle: Put a Secret from your deck into the battlefield.
    // 亡语：从你的牌库中将一张奥秘牌置入战场。
    {
        // 重写亡语效果方法，这是疯狂的科学家卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 检查疯狂的科学家是否属于己方
            if (m.own)
            {
                // 创建随机数生成器
                Random rand = new Random();

                // 收集牌库中所有的奥秘卡牌
                List<CardDB.cardIDEnum> secretCards = new List<CardDB.cardIDEnum>();

                // 遍历己方牌库
                foreach (CardDB.Card card in p.ownDeck)
                {
                    // 检查卡牌是否为奥秘类型
                    if (card.Secret)
                    {
                        secretCards.Add(card.cardIDenum);
                    }
                }

                // 如果牌库中有奥秘卡牌
                if (secretCards.Count > 0)
                {
                    // 随机选择一张奥秘卡牌
                    CardDB.cardIDEnum randomSecret = secretCards[rand.Next(secretCards.Count)];

                    // 将选中的奥秘加入战场
                    p.ownSecretsIDList.Add(randomSecret);
                }
            }
        }
    }
}