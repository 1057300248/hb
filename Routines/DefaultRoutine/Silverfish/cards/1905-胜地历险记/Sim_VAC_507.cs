using System;
using System.Collections.Generic;
using System.Text;

namespace HREngine.Bots
{
	//随从 圣骑士 费用：5 攻击力：2 生命值：6
	//Sunsapper Lynessa
	//阳光汲取者莱妮莎
	//[x]<b>Rogue Tourist</b>Your spells that cost (2)or less cast twice.
	//<b>潜行者游客</b>你的法力值消耗小于或等于（2）点的法术会施放两次。
	class Sim_VAC_507 : SimTemplate
	{
		// 当随从被召唤或场上状态变化时调用
		public override void onAuraStarts(Playfield p, Minion m)
		{
            p.ownSunsapperlynessa = true;
        }

        // 当光环效果结束或条件不满足时调用
        public override void onAuraEnds(Playfield p, Minion m)
        {
            p.ownSunsapperlynessa = false;
        }
		
	}
}
