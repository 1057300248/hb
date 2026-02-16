using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 百兽之王卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力2，生命值6
    // 卡牌效果：<b>嘲讽，战吼：</b>你每控制一个其他野兽，便获得+1攻击力。
    class Sim_GVG_046 : SimTemplate //* 百兽之王 King of Beasts
    // <b>Taunt</b>. <b>Battlecry:</b> Gain +1 Attack for each other Beast you have.
    // <b>嘲讽，战吼：</b>你每控制一个其他野兽，便获得+1攻击力。 
    {
        // 重写战吼效果方法，这是百兽之王卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 初始化攻击力加成计数器
            int bonusattack = 0;

            // 根据随从的归属确定要检查的随从列表（己方或敌方）
            List<Minion> temp = (own.own) ? p.ownMinions : p.enemyMinions;

            // 遍历所有随从，统计其他野兽的数量
            foreach (Minion m in temp)
            {
                // 跳过百兽之王自身
                if (m.entitiyID == own.entitiyID) continue;

                // 检查随从是否为野兽种族
                if ((TAG_RACE)m.handcard.card.race == TAG_RACE.PET) bonusattack++;
            }

            // 给百兽之王增加相应的攻击力
            p.minionGetBuffed(own, bonusattack, 0);
        }
    }
}