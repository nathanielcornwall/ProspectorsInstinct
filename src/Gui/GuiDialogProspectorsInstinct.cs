using System;
using ProspectorsInstinct.Config;
using Vintagestory.API.Client;

namespace ProspectorsInstinct.Gui;

public sealed class GuiDialogProspectorsInstinct : GuiDialog
{
    private ProspectorsInstinctConfig workingConfig;

    private GuiDialogDetectionSettings? detectionDialog;

    public override string ToggleKeyCombinationCode =>
        "prospectorsinstinct-config";

    public GuiDialogProspectorsInstinct(
        ICoreClientAPI capi)
        : base(capi)
    {
        workingConfig =
            ProspectorsInstinctModSystem
                .Config
                .Clone();

        ComposeDialog();
    }

    public override bool TryOpen()
    {
        workingConfig =
            ProspectorsInstinctModSystem
                .Config
                .Clone();

                detectionDialog?.Dispose();
    detectionDialog = null;

        SingleComposer
            .GetSwitch("enableModSwitch")
            .On = workingConfig.Enabled;

        SingleComposer
            .GetSwitch("requirePickSwitch")
            .On = workingConfig.RequireProspectingPick;

            SingleComposer
    .GetSwitch("debugModeSwitch")
    .On = workingConfig.DebugMode;

        SingleComposer
            .GetSlider("scanRadiusSlider")
            .SetValues(
                workingConfig.ScanRadius,
                1,
                12,
                1,
                " blocks");

        SingleComposer
            .GetDynamicText("scanRadiusLabel")
            .SetNewText(
                $"Scan Radius: {workingConfig.ScanRadius} blocks",
                forceRedraw: true);

        int densityStep =
            (int)Math.Round(
                workingConfig.ParticleDensity * 10);

        SingleComposer
            .GetSlider("particleDensitySlider")
            .SetValues(
                densityStep,
                1,
                20,
                1,
                string.Empty);

        SingleComposer
            .GetDynamicText("particleDensityLabel")
            .SetNewText(
                $"Particle Density: {workingConfig.ParticleDensity:0.0}x",
                forceRedraw: true);

        return base.TryOpen();
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
            600)
                .WithFixedPadding(
                    GuiStyle.ElementToDialogPadding);

        ElementBounds descriptionBounds =
    ElementBounds.Fixed(
        20,
        55,
        500,
        30);

ElementBounds enableLabelBounds =
    ElementBounds.Fixed(
        20,
        105,
        420,
        30);

ElementBounds enableSwitchBounds =
    ElementBounds.Fixed(
        470,
        100,
        40,
        30);

// ---------- Scan Radius ----------

ElementBounds radiusLabelBounds =
    ElementBounds.Fixed(
        20,
        150,
        500,
        25);

ElementBounds radiusSliderBounds =
    ElementBounds.Fixed(
        20,
        180,
        500,
        35);

// ---------- Particle Density ----------

ElementBounds densityLabelBounds =
    ElementBounds.Fixed(
        20,
        235,
        500,
        25);

ElementBounds densitySliderBounds =
    ElementBounds.Fixed(
        20,
        265,
        500,
        35);

// ---------- Require Pick ----------

ElementBounds pickLabelBounds =
    ElementBounds.Fixed(
        20,
        325,
        420,
        30);

ElementBounds pickSwitchBounds =
    ElementBounds.Fixed(
        470,
        320,
        40,
        30);

        ElementBounds debugLabelBounds =
    ElementBounds.Fixed(
        20,
        380,
        420,
        30);

ElementBounds debugSwitchBounds =
    ElementBounds.Fixed(
        470,
        375,
        40,
        30);

        ElementBounds detectionButtonBounds =
    ElementBounds.Fixed(
        20,
        445,
        500,
        40);

// ---------- Buttons ----------

ElementBounds saveButtonBounds =
    ElementBounds.Fixed(
        330,
        545,
        90,
        35);

ElementBounds cancelButtonBounds =
    ElementBounds.Fixed(
        430,
        545,
        90,
        35);

        SingleComposer = capi.Gui
            .CreateCompo(
                "prospectorsinstinct-config",
                dialogBounds)
            .AddShadedDialogBG(
                contentBounds)
            .AddDialogTitleBar(
                "Prospector's Instinct",
                OnCloseClicked)
            .AddStaticText(
                "Configure how Prospector's Instinct behaves.",
                CairoFont.WhiteSmallishText(),
                descriptionBounds)
            .AddStaticText(
                "Enable Prospector's Instinct",
                CairoFont.WhiteSmallishText(),
                enableLabelBounds)
            .AddSwitch(
                OnEnabledChanged,
                enableSwitchBounds,
                "enableModSwitch")
            .AddDynamicText(
                $"Scan Radius: {workingConfig.ScanRadius} blocks",
                CairoFont.WhiteSmallishText(),
                radiusLabelBounds,
                "scanRadiusLabel")
            .AddSlider(
                OnScanRadiusChanged,
                radiusSliderBounds,
                "scanRadiusSlider")
            .AddDynamicText(
                $"Particle Density: {workingConfig.ParticleDensity:0.0}x",
                CairoFont.WhiteSmallishText(),
                densityLabelBounds,
                "particleDensityLabel")
            .AddSlider(
                OnParticleDensityChanged,
                densitySliderBounds,
                "particleDensitySlider")
            .AddStaticText(
                "Require Prospecting Pick",
                CairoFont.WhiteSmallishText(),
                pickLabelBounds)
            .AddSwitch(
                OnRequirePickChanged,
                pickSwitchBounds,
                "requirePickSwitch")
            .AddStaticText(
                "Debug Mode",
                CairoFont.WhiteSmallishText(),
                debugLabelBounds)
            .AddSwitch(
                OnDebugModeChanged,
                debugSwitchBounds,
                "debugModeSwitch")    
                .AddSmallButton(
    "Detection Settings...",
    OnDetectionSettingsClicked,
    detectionButtonBounds)     
            .AddSmallButton(
                "Save",
                OnSaveClicked,
                saveButtonBounds)
            .AddSmallButton(
                "Cancel",
                OnCancelClicked,
                cancelButtonBounds)
            .Compose();

        SingleComposer
            .GetSwitch("enableModSwitch")
            .On = workingConfig.Enabled;

        SingleComposer
            .GetSwitch("requirePickSwitch")
            .On = workingConfig.RequireProspectingPick;

            SingleComposer
    .GetSwitch("debugModeSwitch")
    .On = workingConfig.DebugMode;

        SingleComposer
            .GetSlider("scanRadiusSlider")
            .SetValues(
                workingConfig.ScanRadius,
                1,
                12,
                1,
                " blocks");

        int densityStep =
            (int)Math.Round(
                workingConfig.ParticleDensity * 10);

        SingleComposer
            .GetSlider("particleDensitySlider")
            .SetValues(
                densityStep,
                1,
                20,
                1,
                string.Empty);
    }

    private void OnEnabledChanged(bool enabled)
    {
        workingConfig.Enabled = enabled;

        capi.Logger.Notification(
            "[Prospector's Instinct] Working config Enabled changed to {0}.",
            enabled
        );
    }

    private bool OnScanRadiusChanged(int radius)
    {
        workingConfig.ScanRadius = radius;

        SingleComposer
            .GetDynamicText("scanRadiusLabel")
            .SetNewText(
                $"Scan Radius: {radius} blocks",
                forceRedraw: true);

        capi.Logger.Notification(
            "[Prospector's Instinct] Working scan radius changed to {0}.",
            radius
        );

        return true;
    }

    private bool OnParticleDensityChanged(int densityStep)
    {
        workingConfig.ParticleDensity =
            densityStep / 10f;

        SingleComposer
            .GetDynamicText("particleDensityLabel")
            .SetNewText(
                $"Particle Density: {workingConfig.ParticleDensity:0.0}x",
                forceRedraw: true);

        capi.Logger.Notification(
            "[Prospector's Instinct] Working particle density changed to {0:0.0}x.",
            workingConfig.ParticleDensity
        );

        return true;
    }

    private void OnRequirePickChanged(bool enabled)
    {
        workingConfig.RequireProspectingPick = enabled;

        capi.Logger.Notification(
            "[Prospector's Instinct] Require Prospecting Pick changed to {0}.",
            enabled
        );
    }

    private void OnDebugModeChanged(bool enabled)
{
    workingConfig.DebugMode = enabled;

    capi.Logger.Notification(
        "[Prospector's Instinct] Debug Mode changed to {0}.",
        enabled
    );
}

private bool OnDetectionSettingsClicked()
{
    detectionDialog ??=
    new GuiDialogDetectionSettings(
        capi,
        workingConfig);

    if (detectionDialog.IsOpened())
    {
        detectionDialog.TryClose();
    }
    else
    {
        detectionDialog.TryOpen();
    }

    return true;
}

    private bool OnSaveClicked()
    {
        ProspectorsInstinctModSystem
            .Config
            .CopyFrom(workingConfig);

        ConfigManager.Save(
            capi,
            ProspectorsInstinctModSystem.Config);

        capi.Logger.Notification(
            "[Prospector's Instinct] UI settings applied."
        );

        TryClose();
        return true;
    }

    private void OnCloseClicked()
    {
        OnCancelClicked();
    }

    private bool OnCancelClicked()
    {
        workingConfig =
            ProspectorsInstinctModSystem
                .Config
                .Clone();

        TryClose();
        return true;
    }
}