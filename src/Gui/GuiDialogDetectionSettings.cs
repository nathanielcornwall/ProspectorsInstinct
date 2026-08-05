using System;
using System.Collections.Generic;
using System.Linq;
using ProspectorsInstinct.Config;
using Vintagestory.API.Client;

namespace ProspectorsInstinct.Gui;

public sealed class GuiDialogDetectionSettings : GuiDialog
{
    private readonly ProspectorsInstinctConfig workingConfig;

    private readonly List<KeyValuePair<string, bool>>
        displayedOres;

    public override string ToggleKeyCombinationCode =>
        "prospectorsinstinct-detection-settings";

    public GuiDialogDetectionSettings(
        ICoreClientAPI capi,
        ProspectorsInstinctConfig workingConfig)
        : base(capi)
    {
        this.workingConfig =
            workingConfig
            ?? throw new ArgumentNullException(
                nameof(workingConfig));

        displayedOres =
            workingConfig
                .DetectOres
                .OrderBy(entry => entry.Key)
                .Take(20)
                .ToList();

        ComposeDialog();
    }

    private void ComposeDialog()
    {
        ElementBounds dialogBounds =
            ElementStdBounds
                .AutosizedMainDialog
                .WithAlignment(
                    EnumDialogArea.CenterMiddle);

        ElementBounds contentBounds =
            ElementBounds.Fixed(
                    0,
                    0,
                    560,
                    460)
                .WithFixedPadding(
                    GuiStyle.ElementToDialogPadding);

        ElementBounds descriptionBounds =
            ElementBounds.Fixed(
                20,
                65,
                500,
                40);

        ElementBounds closeButtonBounds =
            ElementBounds.Fixed(
                430,
                385,
                90,
                35);

        GuiComposer composer = capi.Gui
            .CreateCompo(
                "prospectorsinstinct-detection-settings",
                dialogBounds)
            .AddShadedDialogBG(
                contentBounds)
            .AddDialogTitleBar(
                "Detection Settings",
                OnCloseClicked)
            .AddStaticText(
                "Enabled Detectable Resources",
                CairoFont.WhiteSmallishText(),
                descriptionBounds);

        const double firstRowY = 120;
        const double rowSpacing = 48;

        for (int index = 0;
             index < displayedOres.Count;
             index++)
        {
            string oreName =
                displayedOres[index].Key;

            string switchKey =
                $"oreSwitch{index}";

            double rowY =
                firstRowY +
                index * rowSpacing;

            ElementBounds labelBounds =
                ElementBounds.Fixed(
                    20,
                    rowY + 5,
                    420,
                    30);

            ElementBounds switchBounds =
                ElementBounds.Fixed(
                    470,
                    rowY,
                    40,
                    30);

            composer
                .AddStaticText(
                    oreName,
                    CairoFont.WhiteSmallishText(),
                    labelBounds)
                .AddSwitch(
                    enabled =>
                        OnOreChanged(
                            oreName,
                            enabled),
                    switchBounds,
                    switchKey);
        }

        SingleComposer = composer
            .AddSmallButton(
                "Close",
                OnCloseButtonClicked,
                closeButtonBounds)
            .Compose();

        InitializeSwitches();
    }

    private void InitializeSwitches()
    {
        for (int index = 0;
             index < displayedOres.Count;
             index++)
        {
            string oreName =
                displayedOres[index].Key;

            string switchKey =
                $"oreSwitch{index}";

            SingleComposer
                .GetSwitch(switchKey)
                .On =
                    workingConfig
                        .DetectOres[oreName];
        }
    }

    private void OnOreChanged(
        string oreName,
        bool enabled)
    {
        workingConfig.DetectOres[oreName] =
            enabled;

        capi.Logger.Notification(
            "[Prospector's Instinct] {0}: {1}",
            oreName,
            enabled
        );
    }

    private void OnCloseClicked()
    {
        TryClose();
    }

    private bool OnCloseButtonClicked()
    {
        TryClose();
        return true;
    }
}