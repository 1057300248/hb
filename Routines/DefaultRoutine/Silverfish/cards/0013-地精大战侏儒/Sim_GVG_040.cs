using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 沙鳞灵魂行者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为4点，攻击力2，生命值5
    // 卡牌效果：每当有其他友方鱼人死亡，便抽一张牌。<b>过载：</b>（1）
    class Sim_GVG_040 : SimTemplate //* 沙鳞灵魂行者 Siltfin Spiritwalker
    // Whenever another friendly Murloc dies, draw a card. <b><b>Overload</b>:</b> (1)
    // 每当有其他友方鱼人死亡，便抽一张牌。<b>过载：</b>（1） 
    {
        // 重写战吼效果方法，这是沙鳞灵魂行者卡牌效果的核心实现
        public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
        {
            // 如果是己方沙鳞灵魂行者，则增加过载计数
            if (own.own) p.ueberladung++;
        }

        // 重写随从死亡时的触发方法，当有随从死亡时调用
        public override void onMinionDiedTrigger(Playfield p, Minion m, Minion diedMinion)
        {
            // 获取本回合死亡的鱼人随从数量
            // 根据随从m的归属，获取己方或敌方死亡的鱼人数量
            int diedMinions = (m.own) ? p.tempTrigger.ownMurlocDied : p.tempTrigger.enemyMurlocDied;

            // 如果没有鱼人死亡，直接返回
            if (diedMinions == 0) return;

            // 计算剩余的死亡鱼人数量（避免重复处理）
            int residual = (p.pID == m.pID) ? diedMinions - m.extraParam2 : diedMinions;

            // 更新随从的处理状态，标记为已处理
            m.pID = p.pID;
            m.extraParam2 = diedMinions;

            // 根据死亡鱼人数量抽相应数量的牌
            for (int i = 0; i < residual; i++)
            {
                // 为沙鳞灵魂行者的拥有者抽一张牌
                // 参数说明：- CardDB.cardIDEnum.None表示随机抽牌，- m.own表示为哪一方抽牌
                p.drawACard(CardDB.cardIDEnum.None, m.own);
            }
        }
    }
}