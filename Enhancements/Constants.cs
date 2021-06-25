namespace Enhancements
{
    public static class Constants
    {
        internal const string AUROSDEV = "https://donate.auros.dev";
        internal const string LATESTRELEASE = "https://github.com/Auros/Enhancements/releases/latest";

        internal const string CHANGELOG = @"
        <size=150%><color=#888888>VERSION 3.0.7</color></size>
        -    Updated to Beat Saber (1.16.2)
        -    Fixed a bug where timers would not properly after some time if they weren't dismissed in game.

        <size=150%><color=#888888>VERSION 3.0.5</color></size>
        -    Updated to Beat Saber (1.16.0)

        <size=150%><color=#888888>VERSION 3.0.4</color></size>
        -    Fixed a critical bug for the new Beat Saber Update (1.15.0)

        <size=150%><color=#888888>VERSION 3.0.3</color></size>
        -    Fixed a bug which played the level failed audio at normal volume if music volume is turned down.

        <size=150%><color=#888888>VERSION 3.0.2</color></size>
        Some changes for the new 1.13.4 Beat Saber update.
        -    Changed the menu preview system slightly.
        -    Removed menu background setting! It's now a base game feature.

        <size=150%><color=#888888>VERSION 3.0.1</color></size>
        Some bug fixes for the new 1.13.2 Beat Saber update.
        -    Added Version Reference In Config
        -    Fixed bug with Reminders not requeuing in the menu if Notify In Game is disabled.
        -    Made the navigation icons look normal.
        -    Fixed curving with the UI.

        <size=150%><color=#888888>VERSION 3.0.0</color></size>
        An entirely new overhaul to the mod! Now with easier to use UI, more features, and better performance.
        -    New: Completely new UI menu
        -    New: Timers
        -    Timers: Create reminders in game
        -    Breaktime: Improved Performance
        -    Breaktime: New asset loading system
        -    Breaktime: New profile system
        -    Breaktime: Can now randomize the effects
        -    Breaktime: Can now select the animation type.
        -    Mini Tweaks: Can now disable 360 Mode for maps without 360 events
        -    Clock: More Fonts
        -    Clock: Opacity Option
        -    Clock: Improved Performance
        -    Volume: Improved Performance
";
    }
}
