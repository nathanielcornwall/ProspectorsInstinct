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

    private const double CategoryHeaderHeight = 38;

    private readonly ProspectorsInstinctConfig workingConfig;

    private readonly List<OreMetadata>
    displayedOres;

    private ElementBounds? scrollContainerBounds;

    private readonly Dictionary<string, GuiElementSwitch>
    oreSwitches = new();

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

        int categoryCount =
    displayedOres
        .Select(ore => ore.Category)
        .Distinct()
        .Count();

double totalListHeight =
    displayedOres.Count * RowHeight
    + categoryCount * CategoryHeaderHeight;

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

                ElementBounds enableAllButtonBounds =
    ElementBounds.Fixed(
        20,
        385,
        110,
        35);

ElementBounds disableAllButtonBounds =
    ElementBounds.Fixed(
        140,
        385,
        110,
        35);

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
                "Detectable Resources",
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
    "Enable All",
    OnEnableAllClicked,
    enableAllButtonBounds)
.AddSmallButton(
    "Disable All",
    OnDisableAllClicked,
    disableAllButtonBounds)
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

    double currentY = 0;

    OreCategory? currentCategory = null;

    foreach (OreMetadata ore in displayedOres)
    {
        if (currentCategory != ore.Category)
        {
            currentCategory = ore.Category;

            ElementBounds headerBounds =
                ElementBounds.Fixed(
                        0,
                        currentY + 4,
                        430,
                        25)
                    .WithParent(
                        scrollContainerBounds);

            GuiElementStaticText header =
                new(
                    capi,
                    GetCategoryDisplayName(
                        ore.Category),
                    EnumTextOrientation.Left,
                    headerBounds,
                    CairoFont.WhiteSmallishText());

            container.Add(header);

            currentY += CategoryHeaderHeight;
        }

        string oreName =
            ore.DisplayName;

        bool isEnabled =
            workingConfig.DetectOres[oreName];

        ElementBounds labelBounds =
            ElementBounds.Fixed(
                    10,
                    currentY + 5,
                    360,
                    30)
                .WithParent(
                    scrollContainerBounds);

        ElementBounds switchBounds =
            ElementBounds.Fixed(
                    390,
                    currentY,
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

oreSwitches[oreName] = oreSwitch;

container.Add(label);
container.Add(oreSwitch);

        currentY += RowHeight;
    }
}

private static string GetCategoryDisplayName(
    OreCategory category)
{
    return category switch
    {
        OreCategory.Metal =>
            "Metals",

        OreCategory.PreciousMetal =>
            "Precious Metals",

        OreCategory.Industrial =>
            "Industrial Minerals",

        OreCategory.Chemical =>
            "Chemical Minerals",

        OreCategory.Fuel =>
            "Fuels",

        OreCategory.Gemstone =>
            "Gemstones",

        OreCategory.Misc =>
            "Other",

        _ =>
            category.ToString()
    };
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

private void SetAllOres(bool enabled)
{
    foreach (OreMetadata ore in displayedOres)
    {
        string oreName =
            ore.DisplayName;

        workingConfig.DetectOres[oreName] =
            enabled;

        if (oreSwitches.TryGetValue(
                oreName,
                out GuiElementSwitch? oreSwitch))
        {
            oreSwitch.On = enabled;
        }
    }

    capi.Logger.Notification(
        "[Prospector's Instinct] All detectable resources: {0}",
        enabled);
}

private bool OnEnableAllClicked()
{
    SetAllOres(true);
    return true;
}

private bool OnDisableAllClicked()
{
    SetAllOres(false);
    return true;
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