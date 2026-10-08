# UnityTestTask

Unity 2022.3.62f3, Android.

## What I Implemented

- **Combo system.** Clearing the same shape (row, column or 3×3 box) again continues the combo and gives a bonus equal to the combo counter (starts at 3, +1 per repeat). Each move without a clear decreases the counter, and at 0 the combo breaks. A different shape starts a new combo. The combo widget shows the shape to repeat and the next bonus. The combo state is saved between sessions.
- **Combo VFX.** The bonus flies into the score, combo particles replace regular ones, and the camera shakes. While a combo is active, the board glows and ambient particles float above it. Combo particle size and shake strength grow with the bonus, while board glow and ambient particle rate grow with the combo level. Ambient particles burst on combo start and each bonus.
- **Analytics.** Events for placing/returning figures, combo bonus, booster activation and Second Chance usage, logged to the console by a debug provider.
- **Fixes.** Enlarged the figure touch area to fill its tray slot (figures were hard to grab on a phone).

## Architectural Decisions

- **Reused existing patterns:** plain C# system + `ScoreMediator` + view (like the booster), effects as small classes in `EffectsManager`.
- **Gameplay doesn't know about analytics.** It only exposes C# events, and `AnalyticsTracker` subscribes to them. Events live in one `AnalyticsEvent` catalog, and providers are selected in the inspector, following the project's style (like `CameraFitSprite`). Adding either needs no gameplay changes.
- **Effects listen to `ScoreMediator` instead of `Board`**, so they always run after the combo is calculated.

## Assumptions

- **Second Chance is the only power-up.** The booster activates automatically, so it's tracked as a bonus.
- If a move clears several shapes, any match with the combo shape counts as a repeat. A new combo picks one shape by priority Box → Row → Column.
- The combo bonus isn't multiplied by the booster.

## What I Would Improve

- **Unified save system:** replace the three save methods (JSON, PlayerPrefs string, PlayerPrefs ints) with one save service and a single data model.
- **Dependency injection:** wire systems through a DI container instead of inspector references and the `GameController` singleton.
- **Pooling:** reuse particles from object pools instead of instantiating them (the bonus fly text is already pooled).
- **ScriptableObject settings:** move combo rules and VFX tuning into ScriptableObjects so they can be tuned without code changes.