using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 麦迪文的残影卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为4点
    // 卡牌效果：复制你的所有随从，并将其置入你的手牌。
    class Sim_GVG_005 : SimTemplate //* 麦迪文的残影 Echo of Medivh
    // Put a copy of each friendly minion into your hand.
    // 复制你的所有随从，并将其置入你的手牌。 
    {
        // 重写卡牌打出时的效果方法，这是麦迪文的残影卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 根据是否是己方打出此卡牌，确定要复制的随从列表
            // 如果是己方打出（ownplay为true），则复制己方场上的所有随从（p.ownMinions）
            // 如果是敌方打出（ownplay为false），则复制敌方场上的所有随从（p.enemyMinions）
            List<Minion> temp = (ownplay) ? p.ownMinions : p.enemyMinions;

            // 遍历选定的随从列表中的每一个随从
            foreach (Minion m in temp)
            {
                // 为每个随从创建一个副本并添加到手牌中
                // 参数说明：
                // - m.handcard.card.nameEN: 获取当前随从的英文卡牌名称，用于确定要抽取的具体卡牌
                // - ownplay: 指示将卡牌添加到哪一方的手牌中（true为己方，false为敌方）
                // - true: 第三个参数为true，表示这是一个特殊效果抽卡（如复制、发现等），会触发相应的卡牌处理逻辑
                p.drawACard(m.handcard.card.nameEN, ownplay, true);
            }
        }
    }
}