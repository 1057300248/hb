using System;
using System.Collections.Generic;
using System.Text;
using log4net;
using Logger = Triton.Common.LogUtilities.Logger;

namespace HREngine.Bots
{
    //英雄技能 德鲁伊 费用：2
    //Blessing of the Golem
    //魔像的祝福
    //Summon a <b>@</b>/<b>@</b> Plant_Golem.
    //召唤一个<b>@</b>/<b>@</b>的植物魔像。
    class Sim_EDR_847p : SimTemplate
    {
		private static readonly ILog Log = Logger.GetLoggerInstanceForType();
        // 基础魔像的攻击力和生命值
        private int baseGolemAttack = 1;
        private int baseGolemHealth = 1;
        
        // 英雄技能效果
        public override void onCardPlay(Playfield p, bool ownplay, Minion target, int choice)
        {
            // 计算当前魔像的属性值
            // 每使用两个法术，魔像获得+1/+1
            int spellBonus = p.spellsplayedThisTurn / 2;  
			Log.ErrorFormat("已使用法术数量={0}", p.spellsplayedThisTurn);

            int golemAttack = baseGolemAttack + spellBonus;
            int golemHealth = baseGolemHealth + spellBonus;
            
            // 召唤植物魔像
            p.callKid(CardDB.Instance.getCardDataFromID(CardDB.cardIDEnum.EDR_847pt2), p.ownMinions.Count, ownplay);
            
            // 获取刚召唤的魔像
            Minion golem = ownplay ? p.ownMinions[p.ownMinions.Count - 1] : p.enemyMinions[p.enemyMinions.Count - 1];
            
            // 设置魔像的属性
            p.minionGetBuffed(golem, golemAttack - golem.Angr, golemHealth - golem.Hp);
            
            
        }
    }
}