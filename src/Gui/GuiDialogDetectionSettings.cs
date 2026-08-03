using Vintagestory.API.Client;

namespace ProspectorsInstinct.Gui;

public sealed class GuiDialogDetectionSettings : GuiDialog
{
    public override string ToggleKeyCombinationCode =>
        "prospectorsinstinct-detection-settings";

    public GuiDialogDetectionSettings(
        ICoreClientAPI capi)
        : base(capi)
    {
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
                "Detection settings are under development.",
                CairoFont.WhiteSmallishText(),
                descriptionBounds)
            .AddSmallButton(
                "Close",
                OnCloseButtonClicked,
                closeButtonBounds)
            .Compose();
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