using System;
using System.Linq;
using ProspectorsInstinct.Config;
using Vintagestory.API.Client;

namespace ProspectorsInstinct.Gui;

public sealed class GuiDialogDetectionSettings : GuiDialog
{
    private readonly ProspectorsInstinctConfig workingConfig;

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
                    420)
                .WithFixedPadding(
                    GuiStyle.ElementToDialogPadding);

        ElementBounds descriptionBounds =
            ElementBounds.Fixed(
                20,
                65,
                500,
                40);

        ElementBounds firstOreLabelBounds =
    ElementBounds.Fixed(
        20,
        150,
        420,
        30);

ElementBounds firstOreSwitchBounds =
    ElementBounds.Fixed(
        470,
        145,
        40,
        30);

        ElementBounds closeButtonBounds =
            ElementBounds.Fixed(
                430,
                345,
                90,
                35);


        SingleComposer = capi.Gui
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
                descriptionBounds)
            .AddStaticText(
    "Native Copper",
    CairoFont.WhiteSmallishText(),
    firstOreLabelBounds)
.AddSwitch(
    OnNativeCopperChanged,
    firstOreSwitchBounds,
    "nativeCopperSwitch")
    .AddStaticText(
    "More resources coming in v0.9.1",
    CairoFont.WhiteDetailText(),
    ElementBounds.Fixed(
        20,
        190,
        400,
        25))
            .AddSmallButton(
                "Close",
                OnCloseButtonClicked,
                closeButtonBounds)
            .Compose();
            SingleComposer
    .GetSwitch("nativeCopperSwitch")
    .On =
        workingConfig
            .DetectOres["Native Copper"];
    }
private void OnNativeCopperChanged(bool enabled)
{
    workingConfig.DetectOres["Native Copper"] =
        enabled;

    capi.Logger.Notification(
        $"Native Copper: {enabled}");
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