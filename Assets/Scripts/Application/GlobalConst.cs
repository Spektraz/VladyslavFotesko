using UnityEngine;

namespace Assets.Scripts.Application
{
    public class GlobalConst
    {
        [Header("Save")]     
        public static readonly string CardDaily = "CardDaily";
        public static readonly string CardTime = "CardTime";
        public static readonly string PresentDaily = "PresentDaily";
        public static readonly string CoinsNow = "CoinsNow";
        public static readonly string CountDaily = "CountDaily";
        public static readonly string SkinFirst = "SkinFirst";
        public static readonly string SkinSecond = "SkinSecond";
        public static readonly string SkinThird = "SkinThird";
        public static readonly string SkinFourth= "SkinFourth";
        public static readonly string SkinFifth = "SkinFifth";
        public static readonly string SkinSixth = "SkinSixth";
        public static readonly string SkinSeventh = "SkinSeventh";
        public static readonly string SkinEighth = "SkinEighth";
        public static readonly string SkinNineth = "SkinNineth";
        public static readonly string SkinTenth = "SkinTenth";
        public static readonly string SkinEleventh = "SkinEleventh";
        public static readonly string SkinTwelveth = "SkinTwelveth";
        public static readonly string SkinNowIndex = "SkinIndexNow";
        public static readonly string CountLvl = "CountLvl";
        public static readonly string NamePlayer = "NamePlayer";
        [Header("Scene")]
        public static readonly string MenuGameLvl = "MainMenu";
        public static readonly string MainGameLvl = "MainGame";
        public static readonly string FortuneGameLvl = "FortuneMenu";
        public static readonly string SkinGameLvl = "SkinMenu";
        public static readonly string CurrentLvl = "CurrentLvl";
        [Header("Card Present")]
        public static readonly int LowMoney = 25;
        public static readonly int MiddleMoney = 50;
        public static readonly int BigMoney = 100;
        [Header("Days")]
        public static readonly int MaxDaysPresent = 7;
        [Header("Hit Bonus")]
        public static readonly int HitCoins = 5;
        [Header("Result Game")]
        public static readonly string WinResult = "WIN";
        public static readonly string LooseResult = "LOOSE";
        [Header("Animator")]
        public static readonly string AnimatorOpenCard = "Open";
        public static readonly string AnimatorFlyCards = "Fly";
        [Header("Animator")]
        public static readonly string NameDraw = "Body";
    }

#region Enums

    public enum TypeSkinItems
    {
        Unset = 0,
        BrushRed = 1,
        BrushGreen = 2,
        BrushDarkBlue = 3,
        BrushPurple = 4,
        BrushYellow = 5,
        BrushBlue = 6,
        BrushLightPurple = 7,
        BigBrushRed = 8,
        BigBrushGreen = 9,
        BigBrushDarkBlue = 10,
        BigBrushPurple = 11,
        BigBrushYellow = 12,
    }
    public enum TypeButtonMainMenu
    {
        Unset = 0,
        Card = 1,
        Daily = 2,
        Skin = 3,
        Game = 4,
    }
    public enum BonusType
    {
        Coins,
        Skin
    }
    #endregion
}
