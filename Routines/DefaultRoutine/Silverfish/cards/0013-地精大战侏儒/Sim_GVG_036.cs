using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 动力战锤卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业武器卡牌，费用为3点，攻击力1，耐久度3
    // 卡牌效果：<b>亡语：</b>随机使一个友方机械获得+2/+2。
    class Sim_GVG_036 : SimTemplate //* 动力战锤 Powermace
    // <b>Deathrattle:</b> Give a random friendly Mech +2/+2.
    // <b>亡语：</b>随机使一个友方机械获得+2/+2。 
    {
        // 在类级别定义动力战锤卡牌对象，用于装备武器
        CardDB.Card weapon = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.GVG_036);

        // 重写卡牌打出时的效果方法，这是动力战锤卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 装备动力战锤武器
            // 参数说明：- weapon：要装备的武器卡牌对象，- ownplay：指示为哪一方装备武器
            p.equipWeapon(weapon, ownplay);
        }

        // 重写亡语效果方法，当动力战锤被摧毁时触发
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 根据随从的归属确定要检查的随从列表（己方或敌方）
            List<Minion> temp = (m.own) ? p.ownMinions : p.enemyMinions;

            // 只有当场上存在随从时才执行效果
            if (temp.Count >= 1)
            {
                // 初始化最小属性值和目标随从
                int sum = 1000;
                Minion t = null;

                // 遍历所有随从，寻找攻击力和生命值之和最小的机械随从
                foreach (Minion mnn in temp)
                {
                    // 检查随从是否为机械种族
                    if ((TAG_RACE)mnn.handcard.card.race == TAG_RACE.MECHANICAL)
                    {
                        // 计算攻击力和生命值之和
                        int s = mnn.maxHp + mnn.Angr;

                        // 寻找属性值最小的机械随从
                        if (s < sum)
                        {
                            t = mnn;
                            sum = s;
                        }
                    }
                }

                // 如果找到符合条件的机械随从，则给予+2/+2增益
                if (t != null && sum < 999)
                {
                    // 给目标机械随从增加+2攻击力和+2生命值
                    p.minionGetBuffed(t, 2, 2);
                }
            }
        }
    }
}