using System.Collections.Generic;
using System;
using System.Linq;
using log4net;
using Logger = Triton.Common.LogUtilities.Logger;

namespace HREngine.Bots
{
    public partial class Behavior丨狂野丨十职业通用挂机 : Behavior
    {
        private static readonly ILog ilog_0 = Logger.GetLoggerInstanceForType();

        private int bonus_enemy = 4;
        private int bonus_mine = 4;
        // 危险血线
        private int hpboarder = 25;
        // 抢脸血线
        private int aggroboarder = 5;

        private static readonly ILog Log = Logger.GetLoggerInstanceForType();
        

        public override string BehaviorName() { return "丨狂野丨十职业通用挂机"; }
        PenalityManager penman = PenalityManager.Instance;



        // 在 getAttackWithHeroPenality 方法中加入打脸奖励逻辑和冻结状态检查
        public override int getAttackWithHeroPenality(Minion target, Playfield p)
        {
            // 检查目标是否有效：空目标、不可触及、典狱长或血量小于等于0的情况
            if (target == null || target.untouchable || target.handcard.card.nameCN == CardDB.cardNameCN.典狱长 || target.Hp <= 0) return 1000;

            // 检查己方英雄是否被冻结，如果被冻结则不能攻击
            if (p.ownHero.frozen) return 1000;

            // 根据自己血量动态计算打脸奖励值
            int enfaceReward = 0;
            if (Hrtprozis.Instance != null && target != null && target.isHero)
            {
                if ((p.ownHero.Hp + p.ownHero.armor) < 10)
                {
                    enfaceReward = 0;
                }
                else
                {
                    enfaceReward = -1500;
                }
            }

            // 返回基础攻击值减去英雄奖励值的结果
            return base.getAttackWithHeroPenality(target, p) - enfaceReward;
        }

        // 在 getAttackWithMininonPenality 方法中也加入同样的逻辑和冻结状态检查
        public override int getAttackWithMininonPenality(Minion m, Playfield p, Minion target)
        {
            // 检查目标是否有效（不存在、不可触及、是典狱长或生命值小于等于0）
            if (target == null || target.untouchable || target.handcard.card.nameCN == CardDB.cardNameCN.典狱长 || target.Hp <= 0) return 1000;

            // 检查攻击者是否能进行攻击（未被沉默且可攻击或不在手牌中）
            if (m != null && !m.silenced && (m.untouchable || m.handcard.card.CantAttack)) return 1000;

            // 检查随从是否被冻结，如果被冻结则不能攻击
            if (m != null && m.frozen) return 1000;

            // 检查随从是否不能攻击英雄
            if (m.cantAttackHeroes && target.isHero) return 1000;

            // 根据自己血量动态计算打脸奖励值
            int enfaceReward = 0;
            if (Hrtprozis.Instance != null && target != null && target.isHero)
            {
                if ((p.ownHero.Hp + p.ownHero.armor) < 10)
                {
                    enfaceReward = 0;
                }
                else
                {
                    enfaceReward = -1500;
                }
            }

            // 返回基础攻击值减去英雄攻击奖励值
            return base.getAttackWithMininonPenality(m, p, target) - enfaceReward;
        }

        /// <summary>
        /// 判断是否应该保留指定的卡牌
        /// </summary>
        /// <param name="card">要判断的卡牌</param>
        /// <param name="p">当前游戏场上的状态信息</param>
        /// <returns>如果应该保留该卡牌则返回true，否则返回false</returns>
        public bool shouldKeepCard(CardDB.Card card, Playfield p)
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

        /// <summary>
        /// 计算特定卡牌在当前游戏状态下的组合惩罚值，用于AI决策
        /// </summary>
        /// <param name="card">要评估的卡牌对象</param>
        /// <param name="target">卡牌的目标随从（如果有的话）</param>
        /// <param name="p">当前游戏局面信息</param>
        /// <param name="nowHandcard">当前手牌信息</param>
        /// <returns>惩罚值，数值越小表示越应该使用该卡牌</returns>
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
                    // 根据敌方低血量随从数量和己方血量情况计算惩罚值
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


                case "高阶教徒赫雷恩":
                    // 根据牌库中剩余亡语随从数量和场上空位决定使用优先级
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
                    // 根据尸体数量、敌方随从威胁程度和己方随从情况决定使用策略
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
                    // 根据手牌中该卡牌的数量决定使用优先级
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
                    // 根据敌方随从数量决定使用优先级
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
                    // 根据己方随从数量决定使用优先级
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
                    // 根据己方随从数量决定使用优先级
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
                    // 根据当前费用和场上攻击随从情况决定使用策略
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
                    // 根据敌方随从数量决定使用优先级
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
                    // 根据敌方随从数量决定使用优先级
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
                    // 根据敌方随从数量和费用情况决定使用优先级
                    {
                        int enemyMinionCount = p.enemyMinions.Count;

                        // 基本逻辑：敌方随从数量 >= 3 时，优先使用
                        if (enemyMinionCount >= 3)
                            penalty = -80; // 优先使用

                        // 费用充足时增加优先级
                        if (p.mana >= 3 && penalty == 0)
                            penalty -= 6; // 费用充足，降低惩罚

                        break;
                    }

                case "永恒雏龙":
                    // 根据敌方随从数量和总攻击力决定使用优先级
                    {
                        int enemyMinionCount = p.enemyMinions.Count;
                        int totalEnemyAttack = p.enemyMinions.Sum(m => m.Angr);

                        // 基本逻辑：敌方随从 1-2 个且攻击力超过5时优先使用
                        if (enemyMinionCount >= 1 && enemyMinionCount <= 2 && totalEnemyAttack > 5)
                            penalty = -80; // 优先使用

                        // 费用充足时增加优先级
                        if (p.mana >= 3 && penalty == 0)
                            penalty -= 5; // 费用充足，降低惩罚

                        break;
                    }

                case "海关执法者":
                    // 根据敌方随从数量和总攻击力决定使用优先级
                    {
                        int enemyMinionCount = p.enemyMinions.Count;
                        int totalEnemyAttack = p.enemyMinions.Sum(m => m.Angr);

                        // 基本逻辑：敌方空场或随从攻击力不超过4时优先使用
                        if (enemyMinionCount == 0 || totalEnemyAttack <= 4)
                            penalty = -80; // 优先使用

                        // 费用充足时增加优先级
                        if (p.mana >= 3 && penalty == 0)
                            penalty -= 5; // 费用充足，降低惩罚

                        break;
                    }


                case "星辰坠落":
                case "暴风雪":
                    // 根据敌方随从血量情况决定使用优先级
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
                    // 根据敌方低血量随从存在情况决定使用优先级
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
                    // 根据场上空位数量决定使用优先级
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
                    // 根据手牌数量决定使用优先级
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
                    // 根据敌我双方随从属性对比决定使用策略
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
                    // 根据己方英雄血量决定使用优先级
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
                    // 根据关键亡语随从死亡数量决定使用优先级
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
                case "基尔加丹":
                    penalty = -20;
                    break;
                    
                case "鲜血魔术师":
                    // 根据尸体数量决定使用优先级
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

        /// <summary>
        /// 获取游戏场地的价值评估
        /// </summary>
        /// <param name="p">游戏场地对象</param>
        /// <returns>评估后的场地价值浮点数</returns>
        public override float getPlayfieldValue(Playfield p)
        {
            if (p.value > -200000) return p.value;

            float retval = 0;
            retval += getGeneralVal(p);
            retval += getHpValue(p, hpboarder, aggroboarder);

            int count = p.playactions.Count;
            bool useAb = false;

            // 根据不同的动作类型调整价值评估
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
                        continue;
                    //在这里加出牌顺序
                    case actionEnum.playcard:

                        // 判断具体的卡牌，并根据出牌顺序调整评分  减分早下  加分晚下 分数别太极端 会出毛病
                        switch (a.card.card.nameCN)
                        {
                            case CardDB.cardNameCN.钢鬃卫兵:
                                retval -= i * 10;
                                break;
                        }
                        break;

                    default:
                        continue;
                }
            }

            // 应用敌方回合惩罚、损失伤害、秘密惩罚和武器惩罚
            retval += enemyTurnPen(p);
            retval -= p.lostDamage;
            retval += getSecretPenality(p);
            retval -= p.enemyWeapon.Angr * 3 + p.enemyWeapon.Durability * 3;

            return retval;
        }

        /// <summary>
        /// 计算敌方随从的价值评估
        /// </summary>
        /// <param name="m">要评估的敌方随从对象</param>
        /// <param name="p">当前游戏场面信息</param>
        /// <returns>返回随从的价值评分，如果随从将在下回合死亡则返回-1</returns>
        public override int getEnemyMinionValue(Minion m, Playfield p)
        {
            bool dieNextTurn = false;

            // 检查敌方是否有末日预言者，如果有则标记随从将在下回合死亡
            foreach (Minion mm in p.enemyMinions)
                if (mm.handcard.card.nameCN == CardDB.cardNameCN.末日预言者)
                    dieNextTurn = true;

            // 检查我方是否有爆炸陷阱且随从血量小于等于2，则标记随从将在下回合死亡
            foreach (CardDB.cardIDEnum s in p.ownSecretsIDList)
                if ((s == CardDB.cardIDEnum.EX1_610 || s == CardDB.cardIDEnum.VAN_EX1_610) && m.Hp <= 2)
                    dieNextTurn = true;

            // 检查随从是否有自动销毁效果
            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart)
                dieNextTurn = true;

            if (dieNextTurn) return -1;
            if (m.Hp <= 0) return 0;

            int retval = 4;
            if (m.Angr > 0 || p.enemyHeroStartClass == TAG_CLASS.PRIEST) retval += m.Hp * bonus_enemy;
            retval += m.spellpower * bonus_enemy * 3 / 2;

            // 计算攻击力价值（如果随从未被冻结且能攻击）
            if (!m.frozen && !m.cantAttack)
            {
                retval += m.Angr * bonus_enemy;
                if (m.windfury) retval += m.Angr * bonus_enemy / 2;
            }

            if (m.silenced) return retval;

            // 计算特殊属性加成价值
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

            // 根据我方英雄血量调整威胁值
            if (p.ownHero.Hp <= 15)
            {
                retval += (16 - p.ownHero.Hp) * 3;
                if (p.ownHero.Hp <= 6) retval *= 2;
            }

            return retval;
        }

        /// <summary>
        /// 计算并返回指定随从的价值评估分数
        /// </summary>
        /// <param name="m">需要评估价值的随从对象</param>
        /// <param name="p">当前游戏战场状态对象</param>
        /// <returns>随从的价值分数，如果随从将在下回合死亡则返回-1，血量为0则返回0，否则返回计算后的价值分数</returns>
        public override int getMyMinionValue(Minion m, Playfield p)
        {
            bool dieNextTurn = false;
            // 检查敌方是否有末日预言者，如果有则标记随从将在下回合死亡
            foreach (Minion mm in p.enemyMinions)
                if (mm.handcard.card.nameCN == CardDB.cardNameCN.末日预言者)
                    dieNextTurn = true;

            // 检查随从是否在各种时机被销毁，如果是则标记将在下回合死亡
            if (m.destroyOnEnemyTurnEnd || m.destroyOnEnemyTurnStart || m.destroyOnOwnTurnEnd || m.destroyOnOwnTurnStart)
                dieNextTurn = true;

            if (dieNextTurn) return -1;
            if (m.Hp <= 0) return 0;

            int retval = 5;
            // 基础价值计算：生命值和攻击力乘以奖励系数
            retval += m.Hp * bonus_mine;
            retval += m.Angr * bonus_mine;

            // 根据随从特殊属性调整价值
            if (m.Hp <= 1 && !m.divineshild) retval -= (m.Angr - 1) * (bonus_mine - 1);
            if (m.Angr > m.Hp + 4) retval -= (m.Angr - m.Hp) * (bonus_mine - 1);
            if ((!m.playedThisTurn || m.rush == 1 || m.charge == 1) && m.windfury) retval += m.Angr;
            if (m.divineshild) retval += m.Angr * 3;
            if (m.stealth) retval += m.Angr / 2 + 1;
            if (m.lifesteal) retval += m.Angr / 2 + 1;
            if (m.divineshild && m.taunt) retval += 4;
            // 处眠随从会降低价值
            if (m.dormant > 0) retval -= bonus_mine * m.dormant;

            switch (m.handcard.card.nameCN)
            {
                case CardDB.cardNameCN.钢鬃卫兵:
                    retval += 10 * bonus_mine;
                    break;
            }

            return retval;
        }

        /// <summary>
        /// 获取芬利的发现卡牌优先级
        /// </summary>
        /// <param name="discoverCards">发现选项中的卡牌列表</param>
        /// <returns>返回优先级数值，-1表示最低优先级或不选择</returns>
        public override int getSirFinleyPriority(List<Handmanager.Handcard> discoverCards)
        {
            return -1;
        }

        /// <summary>
        /// 获取芬利卡牌优先级
        /// </summary>
        /// <param name="card">要获取优先级的卡牌对象</param>
        /// <returns>返回指定卡牌在Sir Finley系统中的优先级数值</returns>
        public override int getSirFinleyPriority(CardDB.Card card)
        {
            return SirFinleyPriorityList[card.nameEN];
        }

        /// <summary>
        /// 芬利优先级列表，用于定义Sir Finley Mrrgglton英雄技能选择的优先级
        /// 字典键为卡牌名称枚举，值为对应的优先级数值（数值越小优先级越高）
        /// </summary>
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

        /// <summary>
        /// 计算基于当前战场状态的生命值评估值
        /// </summary>
        /// <param name="p">战场对象，包含双方英雄和游戏状态信息</param>
        /// <param name="hpboarder">友方英雄生命值边界阈值，用于判断生命值安全程度</param>
        /// <param name="aggroboarder">敌方英雄攻击边界阈值，用于判断攻击策略</param>
        /// <returns>计算得出的生命值评估分数，数值越高表示当前局面越有利</returns>
        public override int getHpValue(Playfield p, int hpboarder, int aggroboarder)
        {
            int offset_enemy = 0;
            int offset_mine = p.calEnemyTotalAngr() + Hrtprozis.Instance.enemyDirectDmg;
            int retval = 0;

            // 己方血量安全情况下的价值计算
            if (p.ownHero.Hp + p.ownHero.armor - offset_mine > hpboarder)
            {
                retval += (5 + p.ownHero.Hp + p.ownHero.armor - offset_mine - hpboarder) * 3 / 2;
            }
            // 己方血量危险但未死亡的情况
            else if (p.ownHero.Hp + p.ownHero.armor - offset_mine > 0)
            {
                retval -= 4 * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor + offset_mine) * (hpboarder + 1 - p.ownHero.Hp - p.ownHero.armor + offset_mine);
            }
            // 己方血量已经死亡的情况
            else
            {
                retval -= 3 * (hpboarder + 1) * (hpboarder + 1) + 100;
            }

            // 当己方血量在1-5之间时的额外惩罚
            if (p.ownHero.Hp + p.ownHero.armor - offset_mine < 6 && p.ownHero.Hp + p.ownHero.armor - offset_mine > 0)
            {
                retval -= 80 / (p.ownHero.Hp + p.ownHero.armor - offset_mine);
            }

            // 敌方血量安全情况下的价值计算
            if (p.enemyHero.Hp + p.enemyHero.armor + offset_enemy >= aggroboarder)
            {
                retval += 3 * (aggroboarder - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            }
            // 敌方血量危险，开始攻击脸面的情况
            else
            {
                retval += 4 * (aggroboarder + 1 - p.enemyHero.Hp - p.enemyHero.armor - offset_enemy);
            }

            // 判断是否能够完成斩杀的价值奖励
            if (p.anzEnemyTaunt == 0 && p.calTotalAngr() + p.calDirectDmg(p.mana, false) >= p.enemyHero.Hp + p.enemyHero.armor)
            {
                retval += 2000;
            }

            // 判断下回合能否斩杀本回合打脸的价值奖励
            if (p.calDirectDmg(p.ownMaxMana + 1, false, true) >= p.enemyHero.Hp + p.enemyHero.armor)
            {
                retval += 100;
            }
            
            return retval;
        }



        /// <summary>
        /// 获取使用地标惩罚值
        /// </summary>
        /// <param name="m">随从对象</param>
        /// <param name="target">目标随从</param>
        /// <param name="p">游戏场地</param>
        /// <returns>地标惩罚值，负数表示奖励，正数表示惩罚</returns>
        public override int getUseLocationPenality(Minion m, Minion target, Playfield p)
        {
            int penalty = 0;
            // 根据卡牌中文名称设置不同的地标惩罚值
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

        /// <summary>
        /// 获取卡牌发现时的价值评估
        /// </summary>
        /// <param name="card">要评估的卡牌</param>
        /// <param name="p">当前游戏场上的状态信息</param>
        /// <returns>返回该卡牌在发现时的价值评分</returns>
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
            // --- 强制远古迅猛龙只选"活性孢子"选项 ---
            CardDB.cardNameCN raptorCN = CardDB.Instance.cardNameCNstringToEnum("远古迅猛龙");
            bool hasRaptorInHand = p.owncards.Any(hc => hc.card.nameCN == raptorCN);

            if (hasRaptorInHand)
            {
                CardDB.cardNameCN sporeCN = CardDB.Instance.cardNameCNstringToEnum("活性孢子");

                // 如果选择的是"活性孢子"，给它很高的优先级
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

            // 检查是否拥有灵魂唤醒者并调整亡语随从的价值
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