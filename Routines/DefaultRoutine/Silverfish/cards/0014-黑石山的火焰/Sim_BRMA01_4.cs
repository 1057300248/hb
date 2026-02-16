using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 搞定他们!卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一张中立职业法术卡牌，费用为3点
    // 卡牌效果：召唤四个1/1并具有<b>嘲讽</b>的矮人。
    class Sim_BRMA01_4 : SimTemplate //* 搞定他们! Get 'em!
    // Summon four 1/1 Dwarves with <b>Taunt</b>.
    // 召唤四个1/1并具有<b>嘲讽</b>的矮人。
    {
        // 定义要召唤的矮人卡牌
        CardDB.Card dwarf = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.BRMA01_4t); // 假设BRMA01_4t是1/1嘲讽矮人的卡牌ID

        // 重写卡牌使用效果方法，这是搞定他们!卡牌效果的核心实现
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 召唤四个1/1并具有嘲讽的矮人
            for (int i = 0; i < 4; i++)
            {
                // 在己方战场末尾召唤矮人
                // 参数说明：- 要召唤的卡牌，- 召唤位置（在战场末尾），- 是否为己方召唤，- false表示不是衍生物
                int summonPosition = (ownplay) ? p.ownMinions.Count : p.enemyMinions.Count;
                p.callKid(dwarf, summonPosition, ownplay, false);
            }
        }
    }
}