using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 小个子驱魔者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为3点，攻击力1，生命值2
    // 卡牌效果：<b>嘲讽</b>，<b>战吼：</b>每有一个具有<b>亡语</b>的敌方随从，便获得+1/+1。
    class Sim_GVG_097 : SimTemplate //* 小个子驱魔者 Lil' Exorcist
    // <b>Taunt</b><b>Battlecry:</b> Gain +1/+1 for each enemy <b>Deathrattle</b> minion.
    // <b>嘲讽</b>，<b>战吼：</b>每有一个具有<b>亡语</b>的敌方随从，便获得+1/+1。 
    {
        // 重写战吼效果方法，这是小个子驱魔者卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 根据小个子驱魔者的归属确定要检查的敌方随从列表
            List<Minion> enemyMinions = (own.own) ? p.enemyMinions : p.ownMinions;

            // 计算具有亡语效果的敌方随从数量
            int gain = 0;
            foreach (Minion m in enemyMinions)
            {
                // 检查随从是否具有亡语效果
                if (m.handcard.card.deathrattle)
                {
                    gain++;
                }
            }

            // 如果有具有亡语效果的敌方随从，则给小个子驱魔者增加相应数值的攻击力和生命值
            if (gain >= 1)
            {
                p.minionGetBuffed(own, gain, gain);
            }
        }
    }
}