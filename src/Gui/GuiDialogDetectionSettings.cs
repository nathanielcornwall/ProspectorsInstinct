using System;
using System.Collections.Generic;
using System.Linq;
using ProspectorsInstinct.Config;
using Vintagestory.API.Client;
using ProspectorsInstinct.Metadata;

namespace ProspectorsInstinct.Gui;

public sealed class GuiDialogDetectionSettings : GuiDialog
{
    private const double VisibleListHeight = 260;
    private const double RowHeight = 42;

    private readonly ProspectorsInstinctConfig workingConfig;

    private readonly List<OreMetadata>
    displayedOres;

    private ElementBounds? scrollContainerBounds;

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
    OreMetadataProvider
        .GetAll()
        .Where(ore =>
            workingConfig
                .DetectOres
                .ContainsKey(ore.DisplayName))
        .OrderBy(ore => ore.Category)
        .ThenBy(ore => ore.DisplayName)
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
                    580,
                    460)
                .WithFixedPadding(
                    GuiStyle.ElementToDialogPadding);

        ElementBounds descriptionBounds =
            ElementBounds.Fixed(
                20,
                65,
                500,
                30);

        ElementBounds insetBounds =
            ElementBounds.Fixed(
                20,
                105,
                500,
                VisibleListHeight);

        ElementBounds clipBounds =
            insetBounds.ForkContainingChild(
                GuiStyle.HalfPadding,
                GuiStyle.HalfPadding,
                GuiStyle.HalfPadding,
                GuiStyle.HalfPadding);

        double totalListHeight =
            displayedOres.Count * RowHeight;

        scrollContainerBounds =
            ElementBounds.Fixed(
                0,
                0,
                450,
                totalListHeight)
            .WithParent(clipBounds);

        ElementBounds scrollbarBounds =
            ElementBounds.Fixed(
                525,
                105,
                20,
                VisibleListHeight);

        ElementBounds closeButtonBounds =
            ElementBounds.Fixed(
                460,
                385,
                90,
                35);

        SingleComposer = capi.Gui
            .CreateCompo(
                "prospectorsinstinct-detection-settings",
                dialogBounds)
            .AddShadedDialogBG(contentBounds)
            .AddDialogTitleBar(
                "Detection Settings",
                OnCloseClicked)
            .AddStaticText(
                "Enabled Detectable Resources",
                CairoFont.WhiteSmallishText(),
                descriptionBounds)
            .BeginChildElements()
                .AddInset(
                    insetBounds,
                    3)
                .BeginClip(
                    clipBounds)
                    .AddContainer(
                        scrollContainerBounds,
                        "scroll-content")
                .EndClip()
                .AddVerticalScrollbar(
                    OnScrollbarChanged,
                    scrollbarBounds,
                    "oreScrollbar")
                .AddSmallButton(
                    "Close",
                    OnCloseButtonClicked,
                    closeButtonBounds)
            .EndChildElements();

        PopulateScrollContainer();

        SingleComposer.Compose();

        SingleComposer
            .GetScrollbar("oreScrollbar")
            .SetHeights(
                (float)VisibleListHeight,
                (float)totalListHeight);
    }

    private void PopulateScrollContainer()
    {
        GuiElementContainer container =
            SingleComposer.GetContainer(
                "scroll-content");

        for (int index = 0;
             index < displayedOres.Count;
             index++)
        {
           string oreName =
    displayedOres[index].DisplayName;

            bool isEnabled =
                workingConfig.DetectOres[oreName];

            double rowY =
                index * RowHeight;

            ElementBounds labelBounds =
                ElementBounds.Fixed(
                        0,
                        rowY + 5,
                        370,
                        30)
                    .WithParent(
                        scrollContainerBounds);

            ElementBounds switchBounds =
                ElementBounds.Fixed(
                        390,
                        rowY,
                        40,
                        30)
                    .WithParent(
                        scrollContainerBounds);

            GuiElementStaticText label =
                new(
                    capi,
                    oreName,
                    EnumTextOrientation.Left,
                    labelBounds,
                    CairoFont.WhiteSmallishText());

            GuiElementSwitch oreSwitch =
                new(
                    capi,
                    enabled =>
                        OnOreChanged(
                            oreName,
                            enabled),
                    switchBounds);

            oreSwitch.On = isEnabled;

            container.Add(label);
            container.Add(oreSwitch);
        }
    }

    private void OnScrollbarChanged(
        float scrollPosition)
    {
        if (scrollContainerBounds == null)
        {
            return;
        }

        scrollContainerBounds.fixedY =
            -scrollPosition;

        scrollContainerBounds.CalcWorldBounds();

        capi.Logger.Debug(
            "[Prospector's Instinct] Scroll position: {0}",
            scrollPosition);
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
            enabled);
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