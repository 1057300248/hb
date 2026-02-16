using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Triton.Common.LogUtilities;

namespace HREngine.Bots
{
    public partial class Behavior丨通用丨不设惩罚 : Behavior
    {
        private static readonly ILog ilog_0 = Logger.GetLoggerInstanceForType();

        private int bonus_enemy = 4;
        private int bonus_mine = 4;
        private int hpboarder = 15;       // 危险血线
        private int aggroboarder = 15;    // 抢脸血线

        public override string BehaviorName() { return "丨通用丨不设惩罚"; }
        PenalityManager penman = PenalityManager.Instance;

        public  bool shouldKeepCard(CardDB.Card card, Playfield p)
        {
            switch (card.nameCN.ToString())
            {
                case "墓地尊主塔兰吉":
                    // 判断手牌里是否已有邦桑迪
                    bool hasBonsandi = p.owncards.Any(hc => hc.card.nameCN == CardDB.cardNameCN.邦桑迪);
                    if (!hasBonsandi)
                        return true;   // 没有邦桑迪 → 保留塔兰吉
                    else
                        return false;  // 已有邦桑迪 → 不保留塔兰吉
                default:
                    return false;      // 其他卡按默认策略
            }
        }

        public override int getComboPenality(CardDB.Card card, Minion target, Playfield p, Handmanager.Handcard nowHandcard)
        {
             // 添加空值检查，确保 p.ownHero 和 p.enemyHero 不为 null
             if (p.ownHero == null || p.enemyHero == null)
             {
                ilog_0.Info("ownHero 或 enemyHero 未初始化，跳过相关逻辑。");
                return 0; // 直接返回安全值，避免报错
             }

            if (target != null && target.untouchable)
                return 100000;

            int penalty = 0;

            switch (card.nameCN.ToString())
            {
                case "树篱迷宫": penalty = -47; break;
                case "远足步道": penalty = -49; break;
                case "尤格萨隆的监狱": penalty = -52; break;
                case "惊险悬崖": penalty = -68; break;
                case "鹦鹉乐园": penalty = -59; break;
                case "大地之末号": penalty = -59; break;
                case "潮汐之地": penalty = -50; break;
                case "小玩物小屋": penalty = -48; break;
				case "恐怖再起": //出龟途DK任务
                    penalty = -1000;
                    break;
                case "放出巨虫": //出龟途DH任务
                    penalty = -1000;
                    break;
                case "治愈荒野": //出龟途德任务
                    penalty = -1000;
                    break;
                case "食物链": //出龟途猎任务
                    penalty = -1000;
                    break;
                case "禁忌序列": //出龟途法任务
                    penalty = -1000;
                    break;
                case "潜入葛拉卡": //出龟途骑任务
                    penalty = -1000;
                    break;
                case "寻求平衡": //出龟途牧任务
                    penalty = -1000;
                    break;
                case "暗中设伏": //出龟途贼任务
                    penalty = -1000;
                    break;
                case "群山之灵": //出龟途萨任务
                    penalty = -1000;
                    break;
                case "逃离邪能地窟": //出龟途术任务
                    penalty = -1000;
                    break;
				case "走进失落之城": //出龟途战任务
                    penalty = -1000;
                    break;
                case "月度魔范员工":
                    if (!target.own) penalty += 1000;
                    break;
                case "摇滚堕落者":
                    if (!target.own) penalty += 1000;
                    break;

                case "时空扭曲":
                {
                    int ownMinions = p.ownMinions.Count;

                    if (p.nextTurnWin())  // 下回合能斩杀 → 强烈鼓励
                    {
                       penalty = -100;
                       break;
                    }

                    if (ownMinions >= 3)
                    {
                       penalty = -50;   // 推荐使用
                       break;
                    }

                    penalty = 50;  // 其他情况 → 不推荐，但不会卡死
                    break;
                }

                case "赞达拉的惨象":
                    int lowHpEnemies = p.enemyMinions.Count(m => m.Hp <= 2 && !m.divineshild && !m.immune);
                    if (p.enemyMinions.Count >= 2)  // 至少两个敌方随从才使用
                    {
                        if (lowHpEnemies >= 3) penalty = -120;
                        else if (lowHpEnemies == 2) penalty = -80;
                        else if (lowHpEnemies == 1) penalty = -40;
                        else penalty = 0;

                        if (p.ownHero.Hp <= 10) penalty -= 20;
                    }
                    else
                        penalty = 1000; // 不使用
                    break;

                case "基尔加丹":
                {
                    int remainingDeck = p.owncards.Count + p.ownMinions.Count; // 或者手牌数量 + 已打出卡牌数等估算
                    int estimatedDeckCount = Math.Max(0, 30 - remainingDeck); // 假设30张牌开始
                    if (estimatedDeckCount <= 5)
                       penalty = -50; // 剩余牌少时优先使用
                    else
                       penalty = 1000; // 剩余牌多时不使用
                    break;
                }

                case "高阶教徒赫雷恩":
                {
      
                    int boardSpace = 7 - p.ownMinions.Count; // 场上空位

                    // 统计手牌里亡语随从数量
                    int deathrattleInHand = p.owncards.Count(hc => hc.card.deathrattle && hc.card.type == CardDB.cardtype.MOB);

                    // 统计场上已打出的亡语随从数量
                    int deathrattleOnBoard = p.ownMinions.Count(m => m.handcard.card.deathrattle && m.handcard.card.type == CardDB.cardtype.MOB);

                    // 假设总共牌库里有4张亡语
                    int totalDeathrattle = 4;

                    // 剩余牌库中亡语数量
                    int deathrattleInDeck = totalDeathrattle - deathrattleInHand - deathrattleOnBoard;

                    // 仅当牌库还有亡语且场上空位够时才优先使用
                    if (deathrattleInDeck > 0 && boardSpace >= 2)
                        penalty = -100; // 优先使用
                    else
                        penalty = 100;  // 不推荐使用

                    break;
                }

                case "邦桑迪":
                case "行程保安":
                    penalty = -50; // 尽早使用
                    break;

                case "邪爆":
                {
                    int corpseCount = p.getCorpseCount();               // 我方可用尸体数量
                    int enemyMinionCount = p.enemyMinions.Count;        // 敌方随从数量
                    int ownMinionCount = p.ownMinions.Count;           // 我方随从数量
                    int ownCanTrade = p.ownMinions.Count(m => m.Angr >= 1 && m.Hp >= 1); // 可用随从换掉敌方随从

                    bool dangerMinionExists = false;

                    // 判断敌方随从是否威胁我方英雄血量
                    foreach (Minion m in p.enemyMinions)
                    {
                       if (m.Angr >= p.ownHero.Hp) // 威胁条件可调整
                       {
                          dangerMinionExists = true;
                          break;
                       }
                    }

                    if (enemyMinionCount >= 2)
                    {
                       // 对面随从多
                       if (ownMinionCount > 0)
                       {
                          // 我方随从可以交易消灭敌方随从
                          int canTradeKill = Math.Min(enemyMinionCount, ownCanTrade);
                          if (canTradeKill >= 1)
                             penalty = 1000; // 用随从处理，不打邪爆
                          else
                          {
                             int canKillWithCorpse = Math.Min(enemyMinionCount, corpseCount);
                             penalty = canKillWithCorpse >= 1 ? -50 : 1000; // 尸体可解就用邪爆，否则不打
                          }
                       }
                       else
                       {
                          // 我方没有随从，用尸体决定是否打邪爆
                          int canKillWithCorpse = Math.Min(enemyMinionCount, corpseCount);
                          penalty = canKillWithCorpse >= 1 ? -50 : 1000;
                       }
                    }
                    else if (enemyMinionCount == 1)
                    {
                       // 对面只有1个随从
                       if (dangerMinionExists && ownMinionCount == 0 && corpseCount >= 1)
                          penalty = -50; // 敌方随从威胁血量，且我方没随从且有尸体 → 用邪爆
                       else
                          penalty = 1000; // 否则不打
                    }
                    else
                    {
                       // 场上没随从（包括我方和敌方）
                       penalty = 1000;
                    }

                    break;
                }

				case "拾箭龙鹰":
                {
                    int handCount = (p.owncards != null) ? p.owncards.Count : 0;

                    // 中文名转枚举（先取一次，避免未定义）
                    CardDB.cardNameCN enumName = CardDB.Instance.cardNameCNstringToEnum("拾箭龙鹰");

                    // 手牌中拾箭龙鹰数量
                    int thisCardCount = 0;
                    if (p.owncards != null)
                    {
                        foreach (Handmanager.Handcard hc in p.owncards)
                        {
                            if (hc.card.nameCN == enumName)
                            {
                                thisCardCount++;
                            }
                        }
                    }

                    // 情况 1：手里只有 1 张牌，且就是拾箭龙鹰
                    if (handCount == 1 && thisCardCount == 1)
                    {
                        penalty = -80;
                    }
                    // 情况 2：手里只剩 2 张牌，且都是拾箭龙鹰
                    else if (handCount == 2 && thisCardCount == 2)
                    {
                        penalty = -49;
                    }
                    else
                    {
                        penalty = 20;
                    }

                    break;
                }

                case "卡纳莎的故事":
                {
                    int enemyMinionCount = p.enemyMinions != null ? p.enemyMinions.Count : 0;

                    if (enemyMinionCount == 0)
                    {
                        // 对面空场 → 优先级高一点
                        penalty = -30;
                    }
                    else
                    {
                        // 对面有随从 → 优先级低一些
                        penalty = -8;  // 数值可根据整体策略调整
                    }
                    break;
                }

                case "城市首脑埃舒":
                {
                    int ownMinionCount = p.ownMinions.Count; // 我方场上随从数量

                    if (ownMinionCount >= 2)
                    {
                        // 优先使用这张牌，可以设置一个较高的出牌权重
                        penalty = -1000;  // 数值越低，AI越倾向于出牌
                    }
                    else
                    {
                        // 如果随从不足2，不强制出牌，可以用默认权重
                        penalty = -10;
                    }
                    break;
                }

                case "漆彩帆布龙":
                {
                    int ownMinionCount = p.ownMinions.Count; // 我方随从数量

                    if (ownMinionCount >= 2)
                    {
                       // 优先使用这张牌
                       penalty = -100;  // 数值可根据需要调整
                    }
                    else
                    {
                       // 随从不足2，使用默认优先级
                       penalty = 20;
                    }
                    break;
                }

                case "狡诈的郊狼":
                {
                    int currentCost = card.cost;
                    int reducedCost = Math.Max(0, currentCost);

                    // 基础惩罚值：费用越低，惩罚越小 → 优先使用
                    penalty = 0; // 默认不急着出
                    if (reducedCost <= 3)
                    {
                       penalty = -10 * (5 - reducedCost); // 费用3 → -20，费用2 → -30
                    }

                    // 如果场上还有我方随从可以攻击，则增加惩罚值 → 压后使用
                    if (p.ownMinions.Exists(m => !m.frozen && m.Angr > 0))
                    {
                       penalty += 20; // 增加惩罚值，表示优先让其他随从先攻击
                    }

                    break;
                }

                case "游侠将军希尔瓦娜斯":
                {
                    penalty = -20; // 默认中等优先
                    if (p.enemyMinions.Count >= 2)
                    {
                        penalty = -100; // 敌方随从超过2 → 优先使用
                    }
                    break;
                }

                case "游侠队长奥蕾莉亚":
                case "游侠新兵温蕾萨":
                {
                    if (p.enemyMinions.Count < 2)
                    {
                        penalty = -50; // 敌方随从少于2 → 高优先级
                    }
                    else
                    {
                        penalty = -20; // 否则按正常逻辑
                    }
                    break;
                }
               
                case "梦想策划师杰弗里斯":
                {
                     int enemyMinionCount = p.enemyMinions.Count;

                     // 基本逻辑：敌方随从数量 >= 3 时，优先使用
                     if (enemyMinionCount >= 3)
                          penalty = -80; // 优先使用

                     // 费用充足时增加优先级
                     if (p.mana >= 3&& penalty == 0)
                          penalty -= 6; // 费用充足，降低惩罚

                     break;
                }

                case "永恒雏龙":
                {
                     int enemyMinionCount = p.enemyMinions.Count;
                     int totalEnemyAttack = p.enemyMinions.Sum(m => m.Angr);

                     // 基本逻辑：敌方随从 1-2 个且攻击力超过5时优先使用
                     if (enemyMinionCount >= 1 && enemyMinionCount <= 2 && totalEnemyAttack > 5)
                          penalty = -80; // 优先使用

                     // 费用充足时增加优先级
                     if (p.mana >= 3&& penalty == 0)
                          penalty -= 5; // 费用充足，降低惩罚

                     break;
                }

                case "海关执法者":
                {
                     int enemyMinionCount = p.enemyMinions.Count;
                     int totalEnemyAttack = p.enemyMinions.Sum(m => m.Angr);

                     // 基本逻辑：敌方空场或随从攻击力不超过4时优先使用
                     if (enemyMinionCount == 0 || totalEnemyAttack <= 4)
                          penalty = -80; // 优先使用

                     // 费用充足时增加优先级
                     if (p.mana >= 3&& penalty == 0)
                          penalty -= 5; // 费用充足，降低惩罚

                     break;
                }             
              

                case "星辰坠落":
                case "暴风雪":
                {
                     int enemyMinionCount = p.enemyMinions != null ? p.enemyMinions.Count : 0;
                     if (enemyMinionCount == 0)
                     {
                          penalty = 1000; // 没有敌方随从不打
                          break;
                     }

                     // 检查敌方随从是否全在2血或更低
                     bool allLowHp = p.enemyMinions
                          .Where(m => m != null)
                          .All(m => m.Hp <= 2);

                     if (allLowHp)
                     {
                          // 敌方随从都能被2伤害清掉 → 高优先级
                          penalty = -100;
                     }
                     else
                     {
                          // 默认优先级，可根据情况调整
                          penalty = -20;
                     }
                     break;
                }

                case "灵魂联结":
                {
                    // 检查敌方随从是否有血量小于等于3的
                    bool hasLowHpEnemy = p.enemyMinions.Any(m => m.Hp <= 3);

                    if (hasLowHpEnemy)
                    {
                        // 提高使用优先级
                        penalty = -50;  // 数值可根据整体策略调整
                    }

                    break;
                }


                case "顶级恐龙学":
                    penalty = -900;
                    break;

                case "穆克拉":
                    penalty = -14;
                    break;


                case "抛接嬉戏":
                    penalty = -14;
                    break;

                case "可靠的鱼竿":
                    penalty = -15;
                    break;

                case "拼布好朋友":
                    penalty = -15;
                    break;

                case "鸭妈妈":
                {
                    int boardFreeSpace = 7 - p.ownMinions.Count; // 场上最多7个位置
                    if (boardFreeSpace >= 3)
                    {
                        penalty = -16; // 可以下
                    }
                    else
                    {
                        penalty = 50; // 不推荐下，设置一个高惩罚值
                    }
                    break;
                }

  
                case "主人的召唤":
                {
                    if (p.owncards.Count < 3) // 手牌少于3张
                    {
                        penalty = -20; // 提高优先级，负值越低优先级越高
                    }
                    else
                    {
                        penalty = 0; // 正常优先级
                    }
                    break;
                }


                case "牧人之杖":
                    penalty = -16;
                    break;

                case "可靠的老马":
                    penalty = -16;
                    break;

                case "远古迅猛龙":
                    penalty = -14;
                    break;

                case "惬意的沃金":
                {
                    if (p.ownMinions.Count == 0 || p.enemyMinions.Count == 0)
                    {
                        penalty = 300; // 没有合法目标
                        break;
                    }

                    Minion enemyMax = null;
                    Minion ownMin = null;

                    // 找敌方最大属性随从（攻击+生命）
                    int maxEnemyValue = int.MinValue;
                    foreach (var m in p.enemyMinions)
                    {
                        if (m != null)
                        {
                            int val = m.Angr + m.Hp;
                            if (val > maxEnemyValue)
                            {
                                maxEnemyValue = val;
                                enemyMax = m;
                            }
                        }
                    }

                    // 找我方最小属性随从
                    int minOwnValue = int.MaxValue;
                    foreach (var m in p.ownMinions)
                    {
                        if (m != null)
                        {
                            int val = m.Angr + m.Hp;
                            if (val < minOwnValue)
                            {
                                minOwnValue = val;
                                ownMin = m;
                            }
                        }
                    }

                    // 判断是否交换
                    if (enemyMax != null && ownMin != null)
                    {
                        int enemyTotal = enemyMax.Angr + enemyMax.Hp;
                        int ownTotal = ownMin.Angr + ownMin.Hp;

                        if (enemyTotal > ownTotal)
                        {
                            penalty = -100; // 优先出
                        }
                        else
                        {
                            penalty = 300; // 不出
                        }
                    }
                    else
                    {
                        penalty = 300; // 没有合法目标
                    }

                    break;
                }


                case "渺小的振翅蝶":
                    penalty = -14;
                    break;

                case "奇利亚斯豪华版3000型":
                    penalty = -50;
                    break;

                case "阿玛拉的故事":
                {
                    // 只有当自己英雄血量低于 15 才使用
                    if (p.ownHero.Hp < 15)
                    {
                        // 优先使用 → 惩罚值越低，越倾向打出
                        penalty = -100;
                    }
                    else
                    {
                        // 血量高于安全值 → 不用
                        penalty = 300;
                    }
                    break;
                }
               

                case "末日使者安布拉":
                {
                      int boardFreeSpace = 7 - p.ownMinions.Count;

                      // 统计关键亡语死亡数
                      int keyDeathrattleDiedCount = 0;

                      foreach (Minion m in p.ownMinions)
                      {
                           if (m != null && m.Hp <= 0 && !m.silenced && m.handcard != null)
                           {
                               if (m.handcard.card.nameCN == CardDB.cardNameCN.索利托斯循环新生)
                               {
                                    keyDeathrattleDiedCount++;
                               }
                           }
                      }

                      if (keyDeathrattleDiedCount > 0)
                      {
                           // 关键亡语死亡越多，惩罚越低 → 越优先使用
                           penalty = -40 - keyDeathrattleDiedCount * 10;

                           // 防止过度降低
                           if (penalty < -100) penalty = -100;
                      }
                      else
                      {
                           // 没有关键亡语死亡 → 不打
                           penalty = 300;
                      }

                      break;
                }

                case "黏团焦油":
                    penalty = -14;
                    break;

                case "鲜血魔术师":
                {
                    int corpseCount2 = p.getCorpseCount();  // 与邪爆使用同方法，确保不报错

                    if (corpseCount2 >= 1)
                       penalty = -11;  // 有尸体 → 优先使用（战吼能触发）
                    else
                       penalty = 150;  // 没尸体 → 尽量不要使用（避免浪费战吼）

                    break;
                }
                
                default:
                    penalty = 0;
                    break;
            }
   
            return penalty;
        }

        public override float getPlayfieldValue(Playfield p)
        {
            if (p.value > -200000) return p.value;

            float retval = 0;
            retval += getGeneralVal(p);
            retval += getHpValue(p, hpboarder, aggroboarder);

            int count = p.playactions.Count;
            bool useAb = false;

            for (int i = 0; i < count; i++)
            {
                Action a = p.playactions[i];

                switch (a.actionType)
                {
                    case actionEnum.trade:
                    case actionEnum.useLocation:
                    case actionEnum.useTitanAbility:
                    case actionEnum.forge:
                        retval -= 20;
                        continue;
                    case actionEnum.attackWithHero:
                        continue;
                    case actionEnum.useHeroPower:
                        useAb = true;
                        retval -= 8; // 降低英雄技能权重
                        break;
                }
            }

            retval += enemyTurnPen(p);
            retval -= p.lostDamage;
            retval += getSecretPenality(p);
            retval -= p.enemyWeapon.Angr * 3 + p.enemyWeapon.Durability * 3;

            return retval;
        }

        public override int getEnemyMinionValue(Minion m, Playfield p)
        {
            bool dieNextTurn = false;
            foreach (Minion mm in p.enemyMinions)
                if (mm.handcard.card.nameCN == CardDB.cardNameCN.末日预言者)
                    dieNextTurn = true;

            foreach (CardDB.cardIDEnum s in p.ownSecretsIDList)
                if ((s == CardDB.cardIDEnum.EX1_610 || s == CardDB.cardIDEnum.VAN_EX1_610) && m.Hp <= 2)
                    dieNextTurn = true;

            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart)
                dieNextTurn = true;

            if (dieNextTurn) return -1;
            if (m.Hp <= 0) return 0;

            int retval = 4;
            if (m.Angr > 0 || p.enemyHeroStartClass == TAG_CLASS.PRIEST) retval += m.Hp * bonus_enemy;
            retval += m.spellpower * bonus_enemy * 3 / 2;

            if (!m.frozen && !m.cantAttack)
            {
                retval += m.Angr * bonus_enemy;
                if (m.windfury) retval += m.Angr * bonus_enemy / 2;
            }

            if (m.silenced) return retval;

            if (m.taunt) retval += 2;
            if (m.divineshild) retval += m.Angr * 2;
            if (m.divineshild && m.taunt) retval += 5;
            if (m.stealth) retval += 2;
            if (m.lifesteal) retval += m.Angr * bonus_enemy;
            if (m.poisonous)
            {
                retval += 4;
                if (p.ownMinions.Count < p.enemyMinions.Count) retval += 10;
            }

            if (p.ownHero.Hp <= 15)
            {
                retval += (16 - p.ownHero.Hp) * 3;
                if (p.ownHero.Hp <= 6) retval *= 2;
            }

            return retval;
        }

        public override int getMyMinionValue(Minion m, Playfield p)
        {
            bool dieNextTurn = false;
            foreach (Minion mm in p.enemyMinions)
                if (mm.handcard.card.nameCN == CardDB.cardNameCN.末日预言者)
                    dieNextTurn = true;

            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart)
                dieNextTurn = true;

            if (dieNextTurn) return -1;
            if (m.Hp <= 0) return 0;

            int retval = 5;
            retval += m.Hp * bonus_mine;
            retval += m.Angr * bonus_mine;

            if (m.Hp <= 1 && !m.divineshild) retval -= (m.Angr - 1) * (bonus_mine - 1);
            if (m.Angr > m.Hp + 4) retval -= (m.Angr - m.Hp) * (bonus_mine - 1);
            if ((!m.playedThisTurn || m.rush == 1 || m.charge == 1) && m.windfury) retval += m.Angr;
            if (m.divineshild) retval += m.Angr * 3;
            if (m.stealth) retval += m.Angr / 2 + 1;
            if (m.lifesteal) retval += m.Angr / 2 + 1;
            if (m.divineshild && m.taunt) retval += 4;
            if (m.dormant > 0) retval -= bonus_mine * m.dormant;

            return retval;
        }

        public override int getSirFinleyPriority(List<Handmanager.Handcard> discoverCards)
        {
            return -1;
        }

        public override int getSirFinleyPriority(CardDB.Card card)
        {
            return SirFinleyPriorityList[card.nameEN];
        }

        private Dictionary<CardDB.cardNameEN, int> SirFinleyPriorityList = new Dictionary<CardDB.cardNameEN, int>
        {
            { CardDB.cardNameEN.lesserheal, 0 },
            { CardDB.cardNameEN.shapeshift, 6 },
            { CardDB.cardNameEN.fireblast, 7 },
            { CardDB.cardNameEN.totemiccall, 1 },
            { CardDB.cardNameEN.lifetap, 9 },
            { CardDB.cardNameEN.daggermastery, 5 },
            { CardDB.cardNameEN.reinforce, 4 },
            { CardDB.cardNameEN.armorup, 2 },
            { CardDB.cardNameEN.steadyshot, 8 }
        };

        public override int getHpValue(Playfield p, int hpboarder, int aggroboarder)
        {
            int offset_enemy = 0;
            int retval = 0;

            if (p.ownHero.Hp + p.ownHero.armor > hpboarder)
                retval += (5 + p.ownHero.Hp + p.ownHero.armor - hpboarder);
            else
                retval -= 5 * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor) * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor);

            if (p.ownHero.Hp + p.ownHero.armor < 10 && p.ownHero.Hp + p.ownHero.armor > 0)
                retval -= 200 / (p.ownHero.Hp + p.ownHero.armor);

            if (p.enemyHero.Hp + p.enemyHero.armor + offset_enemy >= aggroboarder)
                retval += 2 * (aggroboarder - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            else
                retval += 4 * (aggroboarder + 1 - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);

            if (p.anzEnemyTaunt == 0 && p.calTotalAngr() + p.calDirectDmg(p.mana, false) >= p.enemyHero.Hp + p.enemyHero.armor)
                retval += 2000;

            return retval;
        }

        public override int getUseLocationPenality(Minion m, Minion target, Playfield p)
        {
            int penalty = 0;
            switch (m.handcard.card.nameCN.ToString())
            {
                case "树篱迷宫": penalty = -47; break;
                case "远足步道": penalty = -49; break;
                case "尤格萨隆的监狱": penalty = -52; break;
                case "惊险悬崖": penalty = -68; break;
                case "鹦鹉乐园": penalty = -59; break;
                case "大地之末号": penalty = -59; break;
                case "潮汐之地": penalty = -50; break;
                case "小玩物小屋": penalty = -48; break;
                case "维希度斯的窟穴": penalty = -52; break;
                default: penalty = 0; break;
            }
            return penalty;
        }

        public override int getDiscoverVal(CardDB.Card card, Playfield p)
        {
            int baseVal = 0;
            Hsreplay hs = Hsreplay.Instance;

            var cardStats = Hsreplay.AllCardStats.FirstOrDefault(c => c.DbfId == card.dbfId);
            if (cardStats != null)
            {
                Helpfunctions.Instance.logg("getDiscoverVal - 使用Hsreplay数据比对" + card.nameCN + " => " + cardStats.WinrateWhenDrawn);
                ilog_0.Info("getDiscoverVal - 使用Hsreplay数据比对" + card.nameCN + " => " + cardStats.WinrateWhenDrawn);

                baseVal = (int)cardStats.WinrateWhenDrawn;
            }
            // --- 强制远古迅猛龙只选“活性孢子”选项 ---
            CardDB.cardNameCN raptorCN = CardDB.Instance.cardNameCNstringToEnum("远古迅猛龙");
            bool hasRaptorInHand = p.owncards.Any(hc => hc.card.nameCN == raptorCN); 

            if (hasRaptorInHand)
            {
                 CardDB.cardNameCN sporeCN = CardDB.Instance.cardNameCNstringToEnum("活性孢子");

                 // 如果选择的是“活性孢子”，给它很高的优先级
                 if (card.nameCN == sporeCN)
                 {
                      baseVal += 1000;  // 强烈优先选择活性孢子
                 }
                 else
                 {
                      baseVal -= 1000;  // 强烈不选择其他进化目标
                 }
                 return baseVal; // 返回选项的优先级
            }

            // 灵魂唤醒者逻辑，检查牌库和手牌
            CardDB.cardNameCN soulAwakenerCN = CardDB.Instance.cardNameCNstringToEnum("灵魂唤醒者");
            bool hasSoulAwakener = p.owncards.Any(hc => hc.card.nameCN == soulAwakenerCN) 
                   || p.ownDeck.Any(hc => hc.nameCN == soulAwakenerCN);

            if (hasSoulAwakener && card.type == CardDB.cardtype.MOB && card.deathrattle)
            {
                   baseVal -= 500;
                   Helpfunctions.Instance.logg("套牌中有灵魂唤醒者，降低发现带亡语随从的价值: " + card.nameCN);
                   ilog_0.Info("套牌中有灵魂唤醒者，降低发现带亡语随从的价值: " + card.nameCN);
            }
            return baseVal;
        }
    }
}

