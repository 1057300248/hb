using System; // 引入.NET基础系统命名空间，提供基本类型和功能
using System.Collections.Generic; // 引入泛型集合类，用于处理列表、字典等集合类型
using System.Text; // 引入文本处理相关类，虽然在此文件中未直接使用

namespace HREngine.Bots // 定义HREngine.Bots命名空间，包含炉石传说AI机器人的相关类
{
    // 淤泥喷射者卡牌的模拟实现类，继承自SimTemplate基类
    // 这是一个法师职业随从卡牌，费用为5点，攻击力3，生命值5
    // 卡牌效果：<b>嘲讽，亡语：</b>召唤一个1/2并具有<b>嘲讽</b>的泥浆怪。
    class Sim_FP1_012 : SimTemplate //* 淤泥喷射者 Sludge Belcher
    // <b>TauntDeathrattle:</b> Summon a 1/2 Slime with <b>Taunt</b>.
    // <b>嘲讽，亡语：</b>召唤一个1/2并具有<b>嘲讽</b>的泥浆怪。 
    {
        // 定义要召唤的泥浆怪卡牌
        CardDB.Card slime = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.FP1_012t);

        // 重写亡语效果方法，这是淤泥喷射者卡牌效果的核心实现
        public override void onDeathrattle(Playfield p, Minion m)
        {
            // 在淤泥喷射者死亡的位置召唤一个1/2的泥浆怪
            // 参数说明：- 要召唤的卡牌，- 召唤位置（死亡随从的位置-1），- 是否为己方召唤
            p.callKid(slime, m.zonepos - 1, m.own);
        }
    }
}