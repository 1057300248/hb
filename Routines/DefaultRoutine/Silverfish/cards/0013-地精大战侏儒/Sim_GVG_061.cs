using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 作战动员卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业法术卡牌，费用为3点
    // 卡牌效果：召唤三个1/1的白银之手新兵，装备一把1/4的武器。
    class Sim_GVG_061 : SimTemplate //* 作战动员 Muster for Battle
    // Summon three 1/1 Silver Hand Recruits. Equip a 1/4 Weapon.
    // 召唤三个1/1的白银之手新兵，装备一把1/4的武器。 
    {
        // 在类级别定义白银之手新兵和武器卡牌对象
        CardDB.Card kid = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CS2_101t); // 白银之手新兵
        CardDB.Card w = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.CS2_091);   // 圣剑

        // 重写卡牌打出时的效果方法，这是作战动员卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 确定召唤位置：己方随从数量或敌方随从数量
            int pos = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;

            // 召唤三个白银之手新兵
            // 第一个新兵使用特殊参数false（可能用于标记首次召唤）
            p.callKid(kid, pos, ownplay, false);

            // 召唤另外两个新兵
            for (int i = 0; i < 2; i++)
            {
                p.callKid(kid, pos, ownplay);
            }

            // 装备1/4的武器
            p.equipWeapon(w, ownplay);
        }
    }
}