using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
    /// <summary>
    /// 远古暗影（Ancient Shade）卡牌的模拟实现。
    /// </summary>
    class Sim_LOE_110 : SimTemplate
    {
    // 获取“远古诅咒”卡牌数据
    private readonly CardDB.Card ancientCurse = CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.LOE_110t);

    /// <summary>
    /// 当战吼效果触发时调用的方法。
    /// </summary>
    /// <param name="p">当前游戏局面。</param>
    /// <param name="own">触发战吼的随从。</param>
    /// <param name="target">战吼的目标（如果需要）。</param>
    /// <param name="choice">选择的选项（如果有）。</param>
    public override void getBattlecryEffect(Playfield p, Minion own, Minion target, int choice)
    {
        // 将“远古诅咒”洗入牌库
        if (own.own)
        {
            p.AddToDeck(ancientCurse);
        }
        else
        {
            p.AddToDeck(ancientCurse);
        }
    }
    }
}