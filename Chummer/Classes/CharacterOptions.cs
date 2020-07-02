/*  This file is part of Chummer5a.
 *
 *  Chummer5a is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  Chummer5a is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with Chummer5a.  If not, see <http://www.gnu.org/licenses/>.
 *
 *  You can obtain the full source code for Chummer5a at
 *  https://github.com/chummer5a/chummer5a
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using Chummer.Annotations;

// ReSharper disable StringLiteralTypo

namespace Chummer
{
    public class CharacterOptions : INotifyPropertyChanged
    {
        private Guid _guiSourceId = Guid.Empty;
        private string _strFileName = string.Empty;
        private string _strName = "Standard";
        private string _strImageFolder = string.Empty;

        // Settings.
        // ReSharper disable once InconsistentNaming
        private bool _blnAllow2ndMaxAttribute;
        private bool _blnAllowBiowareSuites;
        private bool _blnAllowCyberwareESSDiscounts;
        private bool _blnAllowEditPartOfBaseWeapon;
        private bool _blnAllowHigherStackedFoci;
        private bool _blnAllowInitiationInCreateMode;
        private bool _blnAllowObsolescentUpgrade;
        private bool _blnDontUseCyberlimbCalculation;
        private bool _blnAllowSkillRegrouping = true;
        private bool _blnAlternateMetatypeAttributeKarma;
        private bool _blnArmorDegradation;
        private bool _blnStrictSkillGroupsInCreateMode;
        private bool _blnAllowPointBuySpecializationsOnKarmaSkills;
        private bool _blnCalculateCommlinkResponse = true;
        private bool _blnCyberlegMovement;
        private bool _blnDontDoubleQualityPurchaseCost;
        private bool _blnDontDoubleQualityRefundCost;
        private bool _blnEnforceCapacity = true;
        private bool _blnESSLossReducesMaximumOnly;
        private bool _blnExceedNegativeQualities;
        private bool _blnExceedNegativeQualitiesLimit;
        private bool _blnExceedPositiveQualities;
        private bool _blnExceedPositiveQualitiesCostDoubled;
        private bool _blnExtendAnyDetectionSpell;
        private bool _blnDroneArmorMultiplierEnabled;
        private bool _blnFreeSpiritPowerPointsMAG;
        private bool _blnNoArmorEncumbrance;
        private bool _blnIgnoreArt;
        private bool _blnIgnoreComplexFormLimit;
        private bool _blnUnarmedImprovementsApplyToWeapons;
        private bool _blnLicenseRestrictedItems;
        private bool _blnMaximumArmorModifications;
        private bool _blnMetatypeCostsKarma = true;
        private bool _blnMoreLethalGameplay;
        private bool _blnMultiplyForbiddenCost;
        private bool _blnMultiplyRestrictedCost;
        private bool _blnNoSingleArmorEncumbrance;
        private bool _blnPrintExpenses;
        private bool _blnPrintFreeExpenses = true;
        private bool _blnPrintNotes;
        private bool _blnPrintSkillsWithZeroRating = true;
        private bool _blnRestrictRecoil = true;
        private bool _blnSpecialKarmaCostBasedOnShownValue;
        private bool _blnSpiritForceBasedOnTotalMAG;
        private bool _blnUnrestrictedNuyen;
        private bool _blnUseCalculatedPublicAwareness;
        private bool _blnUsePointsOnBrokenGroups;
        private string _strContactPointsExpression = "{CHAUnaug} * 3";
        private string _strKnowledgePointsExpression = "({INTUnaug} + {LOGUnaug}) * 2";
        private bool _blnDoNotRoundEssenceInternally;
        private bool _blnEnemyKarmaQualityLimit = true;
        private string _strEssenceFormat = "#,0.00";
        private int _intForbiddenCostMultiplier = 1;
        private int _intDroneArmorMultiplier = 2;
        private int _intLimbCount = 6;
        private int _intMetatypeCostMultiplier = 1;
        private decimal _decNuyenPerBP = 2000.0m;
        private int _intRestrictedCostMultiplier = 1;
        private bool _blnAutomaticBackstory = true;
        private bool _blnFreeMartialArtSpecialization;
        private bool _blnPrioritySpellsAsAdeptPowers;
        private bool _blnMysAdeptAllowPPCareer;
        private bool _blnMysAdeptSecondMAGAttribute;
        private bool _blnReverseAttributePriorityOrder;
        private string _strNuyenFormat = "#,0.##";
        private bool _blnCompensateSkillGroupKarmaDifference;
        private bool _increasedImprovedAbilityMultiplier;
        private bool _allowFreeGrids;
        private bool _blnAllowTechnomancerSchooling;
        private string _strBookXPath = string.Empty;
        private string _strExcludeLimbSlot = string.Empty;
        private int _intCyberlimbAttributeBonusCap = 4;
        private bool _blnUnclampAttributeMinimum;
        private bool _blnDroneMods;
        private bool _blnDroneModsMaximumPilot;

        // Karma variables.
        private int _intKarmaAttribute = 5;
        private int _intKarmaCarryover = 7;
        private int _intKarmaComplexFormOption = 2;
        private int _intKarmaComplexFormSkillsoft = 1;
        private int _intKarmaContact = 1;
        private int _intKarmaEnemy = 1;
        private int _intKarmaEnhancement = 2;
        private int _intKarmaImproveActiveSkill = 2;
        private int _intKarmaImproveComplexForm = 1;
        private int _intKarmaImproveKnowledgeSkill = 1;
        private int _intKarmaImproveSkillGroup = 5;
        private int _intKarmaInitiation = 3;
        private int _intKarmaInitiationFlat = 10;
        private int _intKarmaJoinGroup = 5;
        private int _intKarmaLeaveGroup = 1;
        private int _intKarmaManeuver = 5;
        private int _intKarmaMetamagic = 15;
        private int _intKarmaNewActiveSkill = 2;
        private int _intKarmaNewComplexForm = 4;
        private int _intKarmaNewKnowledgeSkill = 1;
        private int _intKarmaNewSkillGroup = 5;
        private int _intKarmaNuyenPer = 2000;
        private int _intKarmaQuality = 1;
        private int _intKarmaSpecialization = 7;
        private int _intKarmaKnoSpecialization = 7;
        private int _intKarmaSpell = 5;
        private int _intKarmaSpirit = 1;
        private int _intKarmaNewAIProgram = 5;
        private int _intKarmaNewAIAdvancedProgram = 8;
        private int _intKarmaMysticAdeptPowerPoint = 5;

        // Karma Foci variables.
        // Enchanting
        private int _intKarmaAlchemicalFocus = 3;
        private int _intKarmaDisenchantingFocus = 3;
        // Metamagic
        private int _intKarmaCenteringFocus = 3;
        private int _intKarmaFlexibleSignatureFocus = 3;
        private int _intKarmaMaskingFocus = 3;
        private int _intKarmaSpellShapingFocus = 3;
        // Power
        private int _intKarmaPowerFocus = 6;
        // Qi
        private int _intKarmaQiFocus = 2;
        // Spell
        private int _intKarmaCounterspellingFocus = 2;
        private int _intKarmaRitualSpellcastingFocus = 2;
        private int _intKarmaSpellcastingFocus = 2;
        private int _intKarmaSustainingFocus = 2;
        // Spirit
        private int _intKarmaBanishingFocus = 2;
        private int _intKarmaBindingFocus = 2;
        private int _intKarmaSummoningFocus = 2;
        // Weapon
        private int _intKarmaWeaponFocus = 3;

        // Default build settings.
        private string _strBuildMethod = "Priority";
        private int _intBuildPoints = 25;
        private int _intAvailability = 12;

        // Dictionary of names of custom data directories, with first value element being load order and second value element being whether or not it's enabled
        private readonly Dictionary<string, Tuple<int, bool>> _dicCustomDataDirectoryNames = new Dictionary<string, Tuple<int, bool>>();
        // Cached lists that should be updated every time _dicCustomDataDirectoryNames is updated
        private readonly List<CustomDataDirectoryInfo> _lstEnabledCustomDataDirectories = new List<CustomDataDirectoryInfo>();
        private readonly List<string> _lstEnabledCustomDataDirectoryPaths = new List<string>();

        // Sourcebook list.
        private readonly HashSet<string> _lstBooks = new HashSet<string>();

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string strPropertyName = null)
        {
            OnMultiplePropertyChanged(strPropertyName);
        }

        public void OnMultiplePropertyChanged(params string[] lstPropertyNames)
        {
            ICollection<string> lstNamesOfChangedProperties = null;
            foreach (string strPropertyName in lstPropertyNames)
            {
                if (lstNamesOfChangedProperties == null)
                    lstNamesOfChangedProperties = s_CharacterOptionsDependencyGraph.GetWithAllDependents(strPropertyName);
                else
                {
                    foreach (string strLoopChangedProperty in s_CharacterOptionsDependencyGraph.GetWithAllDependents(strPropertyName))
                        lstNamesOfChangedProperties.Add(strLoopChangedProperty);
                }
            }

            if ((lstNamesOfChangedProperties?.Count > 0) != true)
                return;

            if (lstNamesOfChangedProperties.Contains(nameof(NuyenDecimals)))
                _intCachedNuyenDecimals = -1;
            if (lstNamesOfChangedProperties.Contains(nameof(EssenceDecimals)))
                _intCachedEssenceDecimals = -1;
            foreach (string strPropertyToChange in lstNamesOfChangedProperties)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(strPropertyToChange));
            }
        }

        //A tree of dependencies. Once some of the properties are changed,
        //anything they depend on, also needs to raise OnChanged
        //This tree keeps track of dependencies
        private static readonly DependencyGraph<string> s_CharacterOptionsDependencyGraph =
            new DependencyGraph<string>(
                new DependencyGraphNode<string>(nameof(NuyenDecimals),
                    new DependencyGraphNode<string>(nameof(NuyenFormat))
                ),
                new DependencyGraphNode<string>(nameof(EssenceDecimals),
                    new DependencyGraphNode<string>(nameof(EssenceFormat))
                )
            );

        #region Initialization, Save, and Load Methods
        public CharacterOptions(CharacterOptions objOther = null)
        {
            if (objOther != null)
                CopyValues(objOther);
        }

        public void CopyValues(CharacterOptions objOther)
        {
            if (objOther == null)
                return;
            _guiSourceId = objOther._guiSourceId;
            _strFileName = objOther._strFileName;

            // Copy over via properties in order to trigger OnPropertyChanged as appropriate
            PropertyInfo[] aobjProperties = GetType().GetProperties();
            PropertyInfo[] aobjOtherProperties = objOther.GetType().GetProperties();
            foreach (PropertyInfo objProperty in GetType().GetProperties())
            {
                PropertyInfo objOtherProperty = aobjOtherProperties.FirstOrDefault(x => x.Name == objProperty.Name);
                if (objOtherProperty != null && objProperty.PropertyType.IsAssignableFrom(objOtherProperty.PropertyType))
                {
                    objProperty.SetValue(aobjProperties, objOtherProperty.GetValue(objOtherProperty));
                }
            }

            _dicCustomDataDirectoryNames.Clear();
            foreach (var kvpOther in objOther.CustomDataDirectoryNames)
            {
                _dicCustomDataDirectoryNames.Add(kvpOther.Key, new Tuple<int, bool>(kvpOther.Value.Item1, kvpOther.Value.Item2));
            }
            RecalculateEnabledCustomDataDirectories();

            _lstBooks.Clear();
            foreach (string strBook in objOther._lstBooks)
            {
                _lstBooks.Add(strBook);
            }
            RecalculateBookXPath();
        }

        /// <summary>
        /// Save the current settings to the settings file.
        /// </summary>
        public bool Save()
        {
            // Create the settings directory if it does not exist.
            string settingsDirectoryPath = Path.Combine(Utils.GetStartupPath, "settings");
            if (!Directory.Exists(settingsDirectoryPath) && !Utils.IsRunningInVisualStudio)
            {
                try
                {
                    Directory.CreateDirectory(settingsDirectoryPath);
                }
                catch (UnauthorizedAccessException)
                {
                    Program.MainForm.ShowMessageBox(LanguageManager.GetString("Message_Insufficient_Permissions_Warning"));
                    return false;
                }
            }
            string strFilePath = Path.Combine(Utils.GetStartupPath, "settings", _strFileName);
            using (FileStream objStream = new FileStream(strFilePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
            {
                using (XmlTextWriter objWriter = new XmlTextWriter(objStream, Encoding.UTF8)
                {
                    Formatting = Formatting.Indented,
                    Indentation = 1,
                    IndentChar = '\t'
                })
                {
                    objWriter.WriteStartDocument();

                    // <settings>
                    objWriter.WriteStartElement("settings");

                    // <id />
                    objWriter.WriteElementString("id", _guiSourceId.ToString("D", GlobalOptions.InvariantCultureInfo));
                    // <name />
                    objWriter.WriteElementString("name", _strName);
                    // <recentimagefolder />
                    if (!string.IsNullOrEmpty(_strImageFolder))
                    {
                        objWriter.WriteElementString("recentimagefolder", _strImageFolder);
                    }

                    // <licenserestricted />
                    objWriter.WriteElementString("licenserestricted", _blnLicenseRestrictedItems.ToString(GlobalOptions.InvariantCultureInfo));
                    // <printzeroratingskills />
                    objWriter.WriteElementString("printzeroratingskills", _blnPrintSkillsWithZeroRating.ToString(GlobalOptions.InvariantCultureInfo));
                    // <morelethalgameplay />
                    objWriter.WriteElementString("morelethalgameplay", _blnMoreLethalGameplay.ToString(GlobalOptions.InvariantCultureInfo));
                    // <spiritforcebasedontotalmag />
                    objWriter.WriteElementString("spiritforcebasedontotalmag", _blnSpiritForceBasedOnTotalMAG.ToString(GlobalOptions.InvariantCultureInfo));
                    // <printexpenses />
                    objWriter.WriteElementString("printexpenses", _blnPrintExpenses.ToString(GlobalOptions.InvariantCultureInfo));
                    // <printfreeexpenses />
                    objWriter.WriteElementString("printfreeexpenses", _blnPrintFreeExpenses.ToString(GlobalOptions.InvariantCultureInfo));
                    // <nuyenperbp />
                    objWriter.WriteElementString("nuyenperbp", _decNuyenPerBP.ToString(GlobalOptions.InvariantCultureInfo));
                    // <UnarmedImprovementsApplyToWeapons />
                    objWriter.WriteElementString("unarmedimprovementsapplytoweapons", _blnUnarmedImprovementsApplyToWeapons.ToString(GlobalOptions.InvariantCultureInfo));
                    // <allowinitiationincreatemode />
                    objWriter.WriteElementString("allowinitiationincreatemode", _blnAllowInitiationInCreateMode.ToString(GlobalOptions.InvariantCultureInfo));
                    // <usepointsonbrokengroups />
                    objWriter.WriteElementString("usepointsonbrokengroups", _blnUsePointsOnBrokenGroups.ToString(GlobalOptions.InvariantCultureInfo));
                    // <dontdoublequalities />
                    objWriter.WriteElementString("dontdoublequalities", _blnDontDoubleQualityPurchaseCost.ToString(GlobalOptions.InvariantCultureInfo));
                    // <dontdoublequalities />
                    objWriter.WriteElementString("dontdoublequalityrefunds", _blnDontDoubleQualityRefundCost.ToString(GlobalOptions.InvariantCultureInfo));
                    // <ignoreart />
                    objWriter.WriteElementString("ignoreart", _blnIgnoreArt.ToString(GlobalOptions.InvariantCultureInfo));
                    // <cyberlegmovement />
                    objWriter.WriteElementString("cyberlegmovement", _blnCyberlegMovement.ToString(GlobalOptions.InvariantCultureInfo));
                    // <allow2ndmaxattribute />
                    objWriter.WriteElementString("allow2ndmaxattribute", _blnAllow2ndMaxAttribute.ToString(GlobalOptions.InvariantCultureInfo));
                    // <contactpointsexpression />
                    objWriter.WriteElementString("contactpointsexpression", _strContactPointsExpression);
                    // <knowledgepointsexpression />
                    objWriter.WriteElementString("knowledgepointsexpression", _strKnowledgePointsExpression);
                    // <dronearmormultiplierenabled />
                    objWriter.WriteElementString("dronearmormultiplierenabled", _blnDroneArmorMultiplierEnabled.ToString(GlobalOptions.InvariantCultureInfo));
                    // <dronearmorflatnumber />
                    objWriter.WriteElementString("dronearmorflatnumber", _intDroneArmorMultiplier.ToString(GlobalOptions.InvariantCultureInfo));
                    // <nosinglearmorencumbrance />
                    objWriter.WriteElementString("nosinglearmorencumbrance", _blnNoSingleArmorEncumbrance.ToString(GlobalOptions.InvariantCultureInfo));
                    // <ignorecomplexformlimit />
                    objWriter.WriteElementString("ignorecomplexformlimit", _blnIgnoreComplexFormLimit.ToString(GlobalOptions.InvariantCultureInfo));
                    // <NoArmorEncumbrance />
                    objWriter.WriteElementString("noarmorencumbrance", _blnNoArmorEncumbrance.ToString(GlobalOptions.InvariantCultureInfo));
                    // <esslossreducesmaximumonly />
                    objWriter.WriteElementString("esslossreducesmaximumonly", _blnESSLossReducesMaximumOnly.ToString(GlobalOptions.InvariantCultureInfo));
                    // <allowskillregrouping />
                    objWriter.WriteElementString("allowskillregrouping", _blnAllowSkillRegrouping.ToString(GlobalOptions.InvariantCultureInfo));
                    // <metatypecostskarma />
                    objWriter.WriteElementString("metatypecostskarma", _blnMetatypeCostsKarma.ToString(GlobalOptions.InvariantCultureInfo));
                    // <metatypecostskarmamultiplier />
                    objWriter.WriteElementString("metatypecostskarmamultiplier", _intMetatypeCostMultiplier.ToString(GlobalOptions.InvariantCultureInfo));
                    // <limbcount />
                    objWriter.WriteElementString("limbcount", _intLimbCount.ToString(GlobalOptions.InvariantCultureInfo));
                    // <excludelimbslot />
                    objWriter.WriteElementString("excludelimbslot", _strExcludeLimbSlot);
                    // <allowcyberwareessdiscounts />
                    objWriter.WriteElementString("allowcyberwareessdiscounts", _blnAllowCyberwareESSDiscounts.ToString(GlobalOptions.InvariantCultureInfo));
                    // <maximumarmormodifications />
                    objWriter.WriteElementString("maximumarmormodifications", _blnMaximumArmorModifications.ToString(GlobalOptions.InvariantCultureInfo));
                    // <armordegredation />
                    objWriter.WriteElementString("armordegredation", _blnArmorDegradation.ToString(GlobalOptions.InvariantCultureInfo));
                    // <specialkarmacostbasedonshownvalue />
                    objWriter.WriteElementString("specialkarmacostbasedonshownvalue", _blnSpecialKarmaCostBasedOnShownValue.ToString(GlobalOptions.InvariantCultureInfo));
                    // <exceedpositivequalities />
                    objWriter.WriteElementString("exceedpositivequalities", _blnExceedPositiveQualities.ToString(GlobalOptions.InvariantCultureInfo));
                    // <exceedpositivequalitiescostdoubled />
                    objWriter.WriteElementString("exceedpositivequalitiescostdoubled", _blnExceedPositiveQualitiesCostDoubled.ToString(GlobalOptions.InvariantCultureInfo));

                    objWriter.WriteElementString("mysaddppcareer", MysAdeptAllowPPCareer.ToString(GlobalOptions.InvariantCultureInfo));

                    // <mysadeptsecondmagattribute />
                    objWriter.WriteElementString("mysadeptsecondmagattribute", MysAdeptSecondMAGAttribute.ToString(GlobalOptions.InvariantCultureInfo));

                    // <exceednegativequalities />
                    objWriter.WriteElementString("exceednegativequalities", _blnExceedNegativeQualities.ToString(GlobalOptions.InvariantCultureInfo));
                    // <exceednegativequalitieslimit />
                    objWriter.WriteElementString("exceednegativequalitieslimit", _blnExceedNegativeQualitiesLimit.ToString(GlobalOptions.InvariantCultureInfo));
                    // <multiplyrestrictedcost />
                    objWriter.WriteElementString("multiplyrestrictedcost", _blnMultiplyRestrictedCost.ToString(GlobalOptions.InvariantCultureInfo));
                    // <multiplyforbiddencost />
                    objWriter.WriteElementString("multiplyforbiddencost", _blnMultiplyForbiddenCost.ToString(GlobalOptions.InvariantCultureInfo));
                    // <restrictedcostmultiplier />
                    objWriter.WriteElementString("restrictedcostmultiplier", _intRestrictedCostMultiplier.ToString(GlobalOptions.InvariantCultureInfo));
                    // <forbiddencostmultiplier />
                    objWriter.WriteElementString("forbiddencostmultiplier", _intForbiddenCostMultiplier.ToString(GlobalOptions.InvariantCultureInfo));
                    // <donotroundessenceinternally />
                    objWriter.WriteElementString("donotroundessenceinternally", _blnDoNotRoundEssenceInternally.ToString(GlobalOptions.InvariantCultureInfo));
                    // <donotroundessenceinternally />
                    objWriter.WriteElementString("enemykarmaqualitylimit", _blnEnemyKarmaQualityLimit.ToString(GlobalOptions.InvariantCultureInfo));
                    // <nuyenformat />
                    objWriter.WriteElementString("nuyenformat", _strNuyenFormat);
                    // <essencedecimals />
                    objWriter.WriteElementString("essenceformat", _strEssenceFormat);
                    // <enforcecapacity />
                    objWriter.WriteElementString("enforcecapacity", _blnEnforceCapacity.ToString(GlobalOptions.InvariantCultureInfo));
                    // <restrictrecoil />
                    objWriter.WriteElementString("restrictrecoil", _blnRestrictRecoil.ToString(GlobalOptions.InvariantCultureInfo));
                    // <unrestrictednuyen />
                    objWriter.WriteElementString("unrestrictednuyen", _blnUnrestrictedNuyen.ToString(GlobalOptions.InvariantCultureInfo));
                    // <calculatecommlinkresponse />
                    objWriter.WriteElementString("calculatecommlinkresponse", _blnCalculateCommlinkResponse.ToString(GlobalOptions.InvariantCultureInfo));
                    // <allowhigherstackedfoci />
                    objWriter.WriteElementString("allowhigherstackedfoci", _blnAllowHigherStackedFoci.ToString(GlobalOptions.InvariantCultureInfo));
                    // <alloweditpartofbaseweapon />
                    objWriter.WriteElementString("alloweditpartofbaseweapon", _blnAllowEditPartOfBaseWeapon.ToString(GlobalOptions.InvariantCultureInfo));
                    // <breakskillgroupsincreatemode />
                    objWriter.WriteElementString("breakskillgroupsincreatemode", _blnStrictSkillGroupsInCreateMode.ToString(GlobalOptions.InvariantCultureInfo));
                    // <allowpointbuyspecializationsonkarmaskills />
                    objWriter.WriteElementString("allowpointbuyspecializationsonkarmaskills", _blnAllowPointBuySpecializationsOnKarmaSkills.ToString(GlobalOptions.InvariantCultureInfo));
                    // <extendanydetectionspell />
                    objWriter.WriteElementString("extendanydetectionspell", _blnExtendAnyDetectionSpell.ToString(GlobalOptions.InvariantCultureInfo));
                    //<dontusecyberlimbcalculation />
                    objWriter.WriteElementString("dontusecyberlimbcalculation", _blnDontUseCyberlimbCalculation.ToString(GlobalOptions.InvariantCultureInfo));
                    // <alternatemetatypeattributekarma />
                    objWriter.WriteElementString("alternatemetatypeattributekarma", _blnAlternateMetatypeAttributeKarma.ToString(GlobalOptions.InvariantCultureInfo));
                    // <reversekarmapriorityorder />
                    objWriter.WriteElementString("reverseattributepriorityorder", ReverseAttributePriorityOrder.ToString(GlobalOptions.InvariantCultureInfo));
                    // <printnotes />
                    objWriter.WriteElementString("printnotes", _blnPrintNotes.ToString(GlobalOptions.InvariantCultureInfo));
                    // <allowobsolescentupgrade />
                    objWriter.WriteElementString("allowobsolescentupgrade", _blnAllowObsolescentUpgrade.ToString(GlobalOptions.InvariantCultureInfo));
                    // <allowbiowaresuites />
                    objWriter.WriteElementString("allowbiowaresuites", _blnAllowBiowareSuites.ToString(GlobalOptions.InvariantCultureInfo));
                    // <freespiritpowerpointsmag />
                    objWriter.WriteElementString("freespiritpowerpointsmag", _blnFreeSpiritPowerPointsMAG.ToString(GlobalOptions.InvariantCultureInfo));
                    // <compensateskillgroupkarmadifference />
                    objWriter.WriteElementString("compensateskillgroupkarmadifference", _blnCompensateSkillGroupKarmaDifference.ToString(GlobalOptions.InvariantCultureInfo));
                    // <autobackstory />
                    objWriter.WriteElementString("autobackstory", _blnAutomaticBackstory.ToString(GlobalOptions.InvariantCultureInfo));
                    // <freemartialartspecialization />
                    objWriter.WriteElementString("freemartialartspecialization", _blnFreeMartialArtSpecialization.ToString(GlobalOptions.InvariantCultureInfo));
                    // <priorityspellsasadeptpowers />
                    objWriter.WriteElementString("priorityspellsasadeptpowers", _blnPrioritySpellsAsAdeptPowers.ToString(GlobalOptions.InvariantCultureInfo));
                    // <usecalculatedpublicawareness />
                    objWriter.WriteElementString("usecalculatedpublicawareness", _blnUseCalculatedPublicAwareness.ToString(GlobalOptions.InvariantCultureInfo));
                    // <increasedimprovedabilitymodifier />
                    objWriter.WriteElementString("increasedimprovedabilitymodifier", _increasedImprovedAbilityMultiplier.ToString(GlobalOptions.InvariantCultureInfo));
                    // <allowfreegrids />
                    objWriter.WriteElementString("allowfreegrids", _allowFreeGrids.ToString(GlobalOptions.InvariantCultureInfo));
                    // <allowtechnomancerschooling />
                    objWriter.WriteElementString("allowtechnomancerschooling", _blnAllowTechnomancerSchooling.ToString(GlobalOptions.InvariantCultureInfo));
                    // <cyberlimbattributebonuscap />
                    objWriter.WriteElementString("cyberlimbattributebonuscap", _intCyberlimbAttributeBonusCap.ToString(GlobalOptions.InvariantCultureInfo));
                    // <clampattributeminimum />
                    objWriter.WriteElementString("clampattributeminimum", _blnUnclampAttributeMinimum.ToString(GlobalOptions.InvariantCultureInfo));
                    // <dronemods />
                    objWriter.WriteElementString("dronemods", _blnDroneMods.ToString(GlobalOptions.InvariantCultureInfo));
                    // <dronemodsmaximumpilot />
                    objWriter.WriteElementString("dronemodsmaximumpilot", _blnDroneModsMaximumPilot.ToString(GlobalOptions.InvariantCultureInfo));

                    // <karmacost>
                    objWriter.WriteStartElement("karmacost");
                    // <karmaattribute />
                    objWriter.WriteElementString("karmaattribute", _intKarmaAttribute.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaquality />
                    objWriter.WriteElementString("karmaquality", _intKarmaQuality.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaspecialization />
                    objWriter.WriteElementString("karmaspecialization", _intKarmaSpecialization.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaknospecialization />
                    objWriter.WriteElementString("karmaknospecialization", _intKarmaKnoSpecialization.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmanewknowledgeskill />
                    objWriter.WriteElementString("karmanewknowledgeskill", _intKarmaNewKnowledgeSkill.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmanewactiveskill />
                    objWriter.WriteElementString("karmanewactiveskill", _intKarmaNewActiveSkill.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmanewskillgroup />
                    objWriter.WriteElementString("karmanewskillgroup", _intKarmaNewSkillGroup.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaimproveknowledgeskill />
                    objWriter.WriteElementString("karmaimproveknowledgeskill", _intKarmaImproveKnowledgeSkill.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaimproveactiveskill />
                    objWriter.WriteElementString("karmaimproveactiveskill", _intKarmaImproveActiveSkill.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaimproveskillgroup />
                    objWriter.WriteElementString("karmaimproveskillgroup", _intKarmaImproveSkillGroup.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaspell />
                    objWriter.WriteElementString("karmaspell", _intKarmaSpell.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaenhancement />
                    objWriter.WriteElementString("karmaenhancement", _intKarmaEnhancement.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmanewcomplexform />
                    objWriter.WriteElementString("karmanewcomplexform", _intKarmaNewComplexForm.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaimprovecomplexform />
                    objWriter.WriteElementString("karmaimprovecomplexform", _intKarmaImproveComplexForm.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmanewaiprogram />
                    objWriter.WriteElementString("karmanewaiprogram", _intKarmaNewAIProgram.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmanewaiadvancedprogram />
                    objWriter.WriteElementString("karmanewaiadvancedprogram", _intKarmaNewAIAdvancedProgram.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmanuyenper />
                    objWriter.WriteElementString("karmanuyenper", _intKarmaNuyenPer.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmacontact />
                    objWriter.WriteElementString("karmacontact", _intKarmaContact.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaenemy />
                    objWriter.WriteElementString("karmaenemy", _intKarmaEnemy.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmacarryover />
                    objWriter.WriteElementString("karmacarryover", _intKarmaCarryover.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaspirit />
                    objWriter.WriteElementString("karmaspirit", _intKarmaSpirit.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmamaneuver />
                    objWriter.WriteElementString("karmamaneuver", _intKarmaManeuver.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmainitiation />
                    objWriter.WriteElementString("karmainitiation", _intKarmaInitiation.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmainitiationflat />
                    objWriter.WriteElementString("karmainitiationflat", _intKarmaInitiationFlat.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmametamagic />
                    objWriter.WriteElementString("karmametamagic", _intKarmaMetamagic.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmacomplexformoption />
                    objWriter.WriteElementString("karmacomplexformoption", _intKarmaComplexFormOption.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmacomplexformskillsoft />
                    objWriter.WriteElementString("karmacomplexformskillsoft", _intKarmaComplexFormSkillsoft.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmajoingroup />
                    objWriter.WriteElementString("karmajoingroup", _intKarmaJoinGroup.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaleavegroup />
                    objWriter.WriteElementString("karmaleavegroup", _intKarmaLeaveGroup.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaalchemicalfocus />
                    objWriter.WriteElementString("karmaalchemicalfocus", _intKarmaAlchemicalFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmabanishingfocus />
                    objWriter.WriteElementString("karmabanishingfocus", _intKarmaBanishingFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmabindingfocus />
                    objWriter.WriteElementString("karmabindingfocus", _intKarmaBindingFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmacenteringfocus />
                    objWriter.WriteElementString("karmacenteringfocus", _intKarmaCenteringFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmacounterspellingfocus />
                    objWriter.WriteElementString("karmacounterspellingfocus", _intKarmaCounterspellingFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmadisenchantingfocus />
                    objWriter.WriteElementString("karmadisenchantingfocus", _intKarmaDisenchantingFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaflexiblesignaturefocus />
                    objWriter.WriteElementString("karmaflexiblesignaturefocus", _intKarmaFlexibleSignatureFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmamaskingfocus />
                    objWriter.WriteElementString("karmamaskingfocus", _intKarmaMaskingFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmapowerfocus />
                    objWriter.WriteElementString("karmapowerfocus", _intKarmaPowerFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaqifocus />
                    objWriter.WriteElementString("karmaqifocus", _intKarmaQiFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaritualspellcastingfocus />
                    objWriter.WriteElementString("karmaritualspellcastingfocus", _intKarmaRitualSpellcastingFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaspellcastingfocus />
                    objWriter.WriteElementString("karmaspellcastingfocus", _intKarmaSpellcastingFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaspellshapingfocus />
                    objWriter.WriteElementString("karmaspellshapingfocus", _intKarmaSpellShapingFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmasummoningfocus />
                    objWriter.WriteElementString("karmasummoningfocus", _intKarmaSummoningFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmasustainingfocus />
                    objWriter.WriteElementString("karmasustainingfocus", _intKarmaSustainingFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaweaponfocus />
                    objWriter.WriteElementString("karmaweaponfocus", _intKarmaWeaponFocus.ToString(GlobalOptions.InvariantCultureInfo));
                    // <karmaweaponfocus />
                    objWriter.WriteElementString("karmamysadpp", _intKarmaMysticAdeptPowerPoint.ToString(GlobalOptions.InvariantCultureInfo));
                    // </karmacost>
                    objWriter.WriteEndElement();

                    // <books>
                    objWriter.WriteStartElement("books");
                    foreach (string strBook in _lstBooks)
                        objWriter.WriteElementString("book", strBook);
                    // </books>
                    objWriter.WriteEndElement();

                    // <customdatadirectorynames>
	            	objWriter.WriteStartElement("customdatadirectorynames");
		            foreach (KeyValuePair<string, Tuple<int, bool>> dicDirectoryName in _dicCustomDataDirectoryNames)
		            {
		                objWriter.WriteStartElement("customdatadirectoryname");
		                objWriter.WriteElementString("directoryname", dicDirectoryName.Key);
                        objWriter.WriteElementString("order", dicDirectoryName.Value.Item1.ToString(GlobalOptions.InvariantCultureInfo));
                        objWriter.WriteElementString("enabled", dicDirectoryName.Value.Item2.ToString(GlobalOptions.InvariantCultureInfo));
		                objWriter.WriteEndElement();
		            }
		            // </customdatadirectorynames>
		            objWriter.WriteEndElement();
		            // <hascustomdirectories>

                    // <defaultbuild>
                    objWriter.WriteStartElement("defaultbuild");
                    // <buildmethod />
                    objWriter.WriteElementString("buildmethod", _strBuildMethod);
                    // <buildpoints />
                    objWriter.WriteElementString("buildpoints", _intBuildPoints.ToString(GlobalOptions.InvariantCultureInfo));
                    // <availability />
                    objWriter.WriteElementString("availability", _intAvailability.ToString(GlobalOptions.InvariantCultureInfo));
                    // </defaultbuild>
                    objWriter.WriteEndElement();

                    // </settings>
                    objWriter.WriteEndElement();

                    objWriter.WriteEndDocument();
                }
            }

            return true;
        }

        /// <summary>
        /// Load the settings from the settings file.
        /// </summary>
        /// <param name="strFileName">Settings file to load from.</param>
        /// <param name="blnShowDialogs">Whether or not to show message boxes on failures to load.</param>
        public bool Load(string strFileName, bool blnShowDialogs = true)
        {
            _strFileName = strFileName;
            string strFilePath = Path.Combine(Utils.GetStartupPath, "settings", _strFileName);
            XmlDocument objXmlDocument = new XmlDocument
            {
                XmlResolver = null
            };
            // Make sure the settings file exists. If not, ask the user if they would like to use the default settings file instead. A character cannot be loaded without a settings file.
            if (File.Exists(strFilePath))
            {
                try
                {
                    using (StreamReader objStreamReader = new StreamReader(strFilePath, Encoding.UTF8, true))
                    using (XmlReader objXmlReader = XmlReader.Create(objStreamReader, new XmlReaderSettings {XmlResolver = null}))
                        objXmlDocument.Load(objXmlReader);
                }
                catch (IOException)
                {
                    if (blnShowDialogs)
                        Program.MainForm.ShowMessageBox(LanguageManager.GetString("Message_CharacterOptions_CannotLoadCharacter"), LanguageManager.GetString("MessageText_CharacterOptions_CannotLoadCharacter"), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    return false;
                }
                catch (XmlException)
                {
                    if (blnShowDialogs)
                        Program.MainForm.ShowMessageBox(LanguageManager.GetString("Message_CharacterOptions_CannotLoadCharacter"), LanguageManager.GetString("MessageText_CharacterOptions_CannotLoadCharacter"), MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                if (blnShowDialogs)
                    Program.MainForm.ShowMessageBox(LanguageManager.GetString("Message_CharacterOptions_CannotLoadCharacter"), LanguageManager.GetString("MessageText_CharacterOptions_CannotLoadCharacter"), MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                return false;
            }

            return Load(objXmlDocument.GetFastNavigator().SelectSingleNode("//settings"));
        }

        /// <summary>
        /// Load the settings from a settings node.
        /// </summary>
        /// <param name="objXmlNode">Settings node to load from.</param>
        public bool Load(XPathNavigator objXmlNode)
        {
            if (objXmlNode == null)
                return false;
            // Setting id.
            string strId = string.Empty;
            if (objXmlNode.TryGetStringFieldQuickly("id", ref strId) && Guid.TryParse(strId, out Guid guidTemp))
                _guiSourceId = guidTemp;
            // Setting name.
            objXmlNode.TryGetStringFieldQuickly("name", ref _strName);
            // Most recent image folder location used.
            objXmlNode.TryGetStringFieldQuickly("recentimagefolder", ref _strImageFolder);
            // License Restricted items.
            objXmlNode.TryGetBoolFieldQuickly("licenserestricted", ref _blnLicenseRestrictedItems);
            // Print all Active Skills with a total value greater than 0 (as opposed to only printing those with a Rating higher than 0).
            objXmlNode.TryGetBoolFieldQuickly("printzeroratingskills", ref _blnPrintSkillsWithZeroRating);
            // More Lethal Gameplay.
            objXmlNode.TryGetBoolFieldQuickly("morelethalgameplay", ref _blnMoreLethalGameplay);
            // Spirit Force Based on Total MAG.
            objXmlNode.TryGetBoolFieldQuickly("spiritforcebasedontotalmag", ref _blnSpiritForceBasedOnTotalMAG);
            // Print Expenses.
            objXmlNode.TryGetBoolFieldQuickly("printexpenses", ref _blnPrintExpenses);
            // Print Free Expenses.
            objXmlNode.TryGetBoolFieldQuickly("printfreeexpenses", ref _blnPrintFreeExpenses);
            // Nuyen per Build Point
            objXmlNode.TryGetDecFieldQuickly("nuyenperbp", ref _decNuyenPerBP);
            // Knucks use Unarmed
            objXmlNode.TryGetBoolFieldQuickly("unarmedimprovementsapplytoweapons", ref _blnUnarmedImprovementsApplyToWeapons);
            // Allow Initiation in Create Mode
            objXmlNode.TryGetBoolFieldQuickly("allowinitiationincreatemode", ref _blnAllowInitiationInCreateMode);
            // Use Points on Broken Groups
            objXmlNode.TryGetBoolFieldQuickly("usepointsonbrokengroups", ref _blnUsePointsOnBrokenGroups);
            // Don't Double the Cost of purchasing Qualities in Career Mode
            objXmlNode.TryGetBoolFieldQuickly("dontdoublequalities", ref _blnDontDoubleQualityPurchaseCost);
            // Don't Double the Cost of removing Qualities in Career Mode
            objXmlNode.TryGetBoolFieldQuickly("dontdoublequalityrefunds", ref _blnDontDoubleQualityRefundCost);
            // Ignore Art Requirements from Street Grimoire
            objXmlNode.TryGetBoolFieldQuickly("ignoreart", ref _blnIgnoreArt);
            // Use Cyberleg Stats for Movement
            objXmlNode.TryGetBoolFieldQuickly("cyberlegmovement", ref _blnCyberlegMovement);
            // Allow a 2nd Max Attribute
            objXmlNode.TryGetBoolFieldQuickly("allow2ndmaxattribute", ref _blnAllow2ndMaxAttribute);
            // XPath expression for contact points
            if (!objXmlNode.TryGetStringFieldQuickly("contactpointsexpression", ref _strContactPointsExpression))
            {
                // Legacy shim
                int intTemp = 3;
                bool blnTemp = false;
                string strTemp = "{CHAUnaug}";
                if (objXmlNode.TryGetBoolFieldQuickly("usetotalvalueforcontacts", ref blnTemp) && blnTemp)
                    strTemp = "{CHA}";
                if (objXmlNode.TryGetBoolFieldQuickly("freecontactsmultiplierenabled", ref blnTemp) && blnTemp)
                    objXmlNode.TryGetInt32FieldQuickly("freekarmacontactsmultiplier", ref intTemp);
                _strContactPointsExpression = strTemp + " * " + intTemp.ToString(GlobalOptions.InvariantCultureInfo);
            }
            // XPath expression for knowledge points
            if (!objXmlNode.TryGetStringFieldQuickly("knowledgepointsexpression", ref _strKnowledgePointsExpression))
            {
                // Legacy shim
                int intTemp = 2;
                bool blnTemp = false;
                string strTemp = "({INTUnaug} + {LOGUnaug})";
                if (objXmlNode.TryGetBoolFieldQuickly("usetotalvalueforknowledge", ref blnTemp) && blnTemp)
                    strTemp = "({INT} + {LOG})";
                if (objXmlNode.TryGetBoolFieldQuickly("freekarmaknowledgemultiplierenabled", ref blnTemp) && blnTemp)
                    objXmlNode.TryGetInt32FieldQuickly("freekarmaknowledgemultiplier", ref intTemp);
                _strKnowledgePointsExpression = strTemp + " * " + intTemp.ToString(GlobalOptions.InvariantCultureInfo);
            }
            // Drone Armor Multiplier Enabled
            objXmlNode.TryGetBoolFieldQuickly("dronearmormultiplierenabled", ref _blnDroneArmorMultiplierEnabled);
            // Drone Armor Multiplier Value
            objXmlNode.TryGetInt32FieldQuickly("dronearmorflatnumber", ref _intDroneArmorMultiplier);
            // No Single Armor Encumbrance
            objXmlNode.TryGetBoolFieldQuickly("nosinglearmorencumbrance", ref _blnNoSingleArmorEncumbrance);
            // Ignore Armor Encumbrance
            objXmlNode.TryGetBoolFieldQuickly("noarmorencumbrance", ref _blnNoArmorEncumbrance);
            // Ignore Complex Form Limit
            objXmlNode.TryGetBoolFieldQuickly("ignorecomplexformlimit", ref _blnIgnoreComplexFormLimit);
            // Essence Loss Reduces Maximum Only.
            objXmlNode.TryGetBoolFieldQuickly("esslossreducesmaximumonly", ref _blnESSLossReducesMaximumOnly);
            // Allow Skill Regrouping.
            objXmlNode.TryGetBoolFieldQuickly("allowskillregrouping", ref _blnAllowSkillRegrouping);
            // Metatype Costs Karma.
            objXmlNode.TryGetBoolFieldQuickly("metatypecostskarma", ref _blnMetatypeCostsKarma);
            // Metatype Costs Karma.
            objXmlNode.TryGetBoolFieldQuickly("reverseattributepriorityorder", ref _blnReverseAttributePriorityOrder);
            // Metatype Costs Karma Multiplier.
            objXmlNode.TryGetInt32FieldQuickly("metatypecostskarmamultiplier", ref _intMetatypeCostMultiplier);
            // Limb Count.
            objXmlNode.TryGetInt32FieldQuickly("limbcount", ref _intLimbCount);
            // Exclude Limb Slot.
            objXmlNode.TryGetStringFieldQuickly("excludelimbslot", ref _strExcludeLimbSlot);
            // Allow Cyberware Essence Cost Discounts.
            objXmlNode.TryGetBoolFieldQuickly("allowcyberwareessdiscounts", ref _blnAllowCyberwareESSDiscounts);
            // Use Maximum Armor Modifications.
            objXmlNode.TryGetBoolFieldQuickly("maximumarmormodifications", ref _blnMaximumArmorModifications);
            // Allow Armor Degradation.
            objXmlNode.TryGetBoolFieldQuickly("armordegredation", ref _blnArmorDegradation);
            // Whether or not Karma costs for increasing Special Attributes is based on the shown value instead of actual value.
            objXmlNode.TryGetBoolFieldQuickly("specialkarmacostbasedonshownvalue", ref _blnSpecialKarmaCostBasedOnShownValue);
            // Allow more than 35 BP in Positive Qualities.
            objXmlNode.TryGetBoolFieldQuickly("exceedpositivequalities", ref _blnExceedPositiveQualities);
            // Double all positive qualities in excess of the limit
            objXmlNode.TryGetBoolFieldQuickly("exceedpositivequalitiescostdoubled", ref _blnExceedPositiveQualitiesCostDoubled);

            objXmlNode.TryGetBoolFieldQuickly("mysaddppcareer", ref _blnMysAdeptAllowPPCareer);

            // Split MAG for Mystic Adepts so that they have a separate MAG rating for Adept Powers instead of using the special PP rules for mystic adepts
            objXmlNode.TryGetBoolFieldQuickly("mysadeptsecondmagattribute", ref _blnMysAdeptSecondMAGAttribute);

            // Grant a free specialization when taking a martial art.
            objXmlNode.TryGetBoolFieldQuickly("freemartialartspecialization", ref _blnFreeMartialArtSpecialization);
            // Can spend spells from Magic priority as power points
            objXmlNode.TryGetBoolFieldQuickly("priorityspellsasadeptpowers", ref _blnPrioritySpellsAsAdeptPowers);
            // Allow more than 35 BP in Negative Qualities.
            objXmlNode.TryGetBoolFieldQuickly("exceednegativequalities", ref _blnExceedNegativeQualities);
            // Character can still only receive 35 BP from Negative Qualities (though they can still add as many as they'd like).
            objXmlNode.TryGetBoolFieldQuickly("exceednegativequalitieslimit", ref _blnExceedNegativeQualitiesLimit);
            // Whether or not Restricted items have their cost multiplied.
            objXmlNode.TryGetBoolFieldQuickly("multiplyrestrictedcost", ref _blnMultiplyRestrictedCost);
            // Whether or not Forbidden items have their cost multiplied.
            objXmlNode.TryGetBoolFieldQuickly("multiplyforbiddencost", ref _blnMultiplyForbiddenCost);
            // Restricted cost multiplier.
            objXmlNode.TryGetInt32FieldQuickly("restrictedcostmultiplier", ref _intRestrictedCostMultiplier);
            // Forbidden cost multiplier.
            objXmlNode.TryGetInt32FieldQuickly("forbiddencostmultiplier", ref _intForbiddenCostMultiplier);
            // Only round essence when its value is displayed
            objXmlNode.TryGetBoolFieldQuickly("donotroundessenceinternally", ref _blnDoNotRoundEssenceInternally);
            // Only round essence when its value is displayed
            objXmlNode.TryGetBoolFieldQuickly("enemykarmaqualitylimit", ref _blnEnemyKarmaQualityLimit);
            // Format in which nuyen values are displayed
            objXmlNode.TryGetStringFieldQuickly("nuyenformat", ref _strNuyenFormat);
            // Format in which essence values should be displayed (and to which they should be rounded)
            if (!objXmlNode.TryGetStringFieldQuickly("essenceformat", ref _strEssenceFormat))
            {
                int intTemp = 2;
                // Number of decimal places to round to when calculating Essence.
                objXmlNode.TryGetInt32FieldQuickly("essencedecimals", ref intTemp);
                EssenceDecimals = intTemp;
            }
            // Whether or not Capacity limits should be enforced.
            objXmlNode.TryGetBoolFieldQuickly("enforcecapacity", ref _blnEnforceCapacity);
            // Whether or not Recoil modifiers are restricted (AR 148).
            objXmlNode.TryGetBoolFieldQuickly("restrictrecoil", ref _blnRestrictRecoil);
            // Whether or not character are not restricted to the number of points they can invest in Nuyen.
            objXmlNode.TryGetBoolFieldQuickly("unrestrictednuyen", ref _blnUnrestrictedNuyen);
            // Whether or not a Commlink's Response should be calculated based on the number of programs it has running.
            objXmlNode.TryGetBoolFieldQuickly("calculatecommlinkresponse", ref _blnCalculateCommlinkResponse);
            // Whether or not Stacked Foci can go a combined Force higher than 6.
            objXmlNode.TryGetBoolFieldQuickly("allowhigherstackedfoci", ref _blnAllowHigherStackedFoci);
            // Whether or not the user can change the status of a Weapon Mod or Accessory being part of the base Weapon.
            objXmlNode.TryGetBoolFieldQuickly("alloweditpartofbaseweapon", ref _blnAllowEditPartOfBaseWeapon);
            // Whether or not the user can break Skill Groups while in Create Mode.
            objXmlNode.TryGetBoolFieldQuickly("breakskillgroupsincreatemode", ref _blnStrictSkillGroupsInCreateMode);
            // Whether or not the user is allowed to buy specializations with skill points for skills only bought with karma.
            objXmlNode.TryGetBoolFieldQuickly("allowpointbuyspecializationsonkarmaskills", ref _blnAllowPointBuySpecializationsOnKarmaSkills);
            // Whether or not any Detection Spell can be taken as Extended range version.
            objXmlNode.TryGetBoolFieldQuickly("extendanydetectionspell", ref _blnExtendAnyDetectionSpell);
            // Whether or not cyberlimbs are used for augmented attribute calculation.
            objXmlNode.TryGetBoolFieldQuickly("dontusecyberlimbcalculation", ref _blnDontUseCyberlimbCalculation);
            // House rule: Treat the Metatype Attribute Minimum as 1 for the purpose of calculating Karma costs.
            objXmlNode.TryGetBoolFieldQuickly("alternatemetatypeattributekarma", ref _blnAlternateMetatypeAttributeKarma);
            // Whether or not Notes should be printed.
            objXmlNode.TryGetBoolFieldQuickly("printnotes", ref _blnPrintNotes);
            // Whether or not Obsolescent can be removed/upgrade in the same manner as Obsolete.
            objXmlNode.TryGetBoolFieldQuickly("allowobsolescentupgrade", ref _blnAllowObsolescentUpgrade);
            // Whether or not Bioware Suites can be created and added.
            objXmlNode.TryGetBoolFieldQuickly("allowbiowaresuites", ref _blnAllowBiowareSuites);
            // House rule: Free Spirits calculate their Power Points based on their MAG instead of EDG.
            objXmlNode.TryGetBoolFieldQuickly("freespiritpowerpointsmag", ref _blnFreeSpiritPowerPointsMAG);
            // House rule: Whether to compensate for the karma cost difference between raising skill ratings and skill groups when increasing the rating of the last skill in the group
            objXmlNode.TryGetBoolFieldQuickly("compensateskillgroupkarmadifference", ref _blnCompensateSkillGroupKarmaDifference);
            // Optional Rule: Whether Life Modules should automatically create a character back story.
            objXmlNode.TryGetBoolFieldQuickly("autobackstory", ref _blnAutomaticBackstory);
            // House Rule: Whether Public Awareness should be a calculated attribute based on Street Cred and Notoriety.
            objXmlNode.TryGetBoolFieldQuickly("usecalculatedpublicawareness", ref _blnUseCalculatedPublicAwareness);
            // House Rule: Whether Improved Ability should be capped at 0.5 (false) or 1.5 (true) of the target skill's Learned Rating.
            objXmlNode.TryGetBoolFieldQuickly("increasedimprovedabilitymodifier", ref _increasedImprovedAbilityMultiplier);
            // House Rule: Whether lifestyles will give free grid subscriptions found in HT to players.
            objXmlNode.TryGetBoolFieldQuickly("allowfreegrids", ref _allowFreeGrids);
            // House Rule: Whether Technomancers should be allowed to receive Schooling discounts in the same manner as Awakened.
            objXmlNode.TryGetBoolFieldQuickly("allowtechnomancerschooling", ref _blnAllowTechnomancerSchooling);
            // House Rule: Maximum value that cyberlimbs can have as a bonus on top of their Customization.
            objXmlNode.TryGetInt32FieldQuickly("cyberlimbattributebonuscap", ref _intCyberlimbAttributeBonusCap);
            // House/Optional Rule: Attribute values are allowed to go below 0 due to Essence Loss.
            objXmlNode.TryGetBoolFieldQuickly("unclampattributeminimum", ref _blnUnclampAttributeMinimum);
            // Following two settings used to be stored in global options, so they are fetched from the registry if they are not present
            // Use Rigger 5.0 drone mods
            if (!objXmlNode.TryGetBoolFieldQuickly("dronemods", ref _blnDroneMods))
                GlobalOptions.LoadBoolFromRegistry(ref _blnDroneMods, "dronemods", string.Empty, true);
            // Apply maximum drone attribute improvement rule to Pilot, too
            if (!objXmlNode.TryGetBoolFieldQuickly("dronemodsmaximumpilot", ref _blnDroneModsMaximumPilot))
                GlobalOptions.LoadBoolFromRegistry(ref _blnDroneModsMaximumPilot, "dronemodsPilot", string.Empty, true);

            XPathNavigator xmlKarmaCostNode = objXmlNode.SelectSingleNode("karmacost");
            // Attempt to populate the Karma values.
            if (xmlKarmaCostNode != null)
            {
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaattribute", ref _intKarmaAttribute);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaquality", ref _intKarmaQuality);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaspecialization", ref _intKarmaSpecialization);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaknospecialization", ref _intKarmaKnoSpecialization);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmanewknowledgeskill", ref _intKarmaNewKnowledgeSkill);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmanewactiveskill", ref _intKarmaNewActiveSkill);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmanewskillgroup", ref _intKarmaNewSkillGroup);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaimproveknowledgeskill", ref _intKarmaImproveKnowledgeSkill);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaimproveactiveskill", ref _intKarmaImproveActiveSkill);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaimproveskillgroup", ref _intKarmaImproveSkillGroup);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaspell", ref _intKarmaSpell);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmanewcomplexform", ref _intKarmaNewComplexForm);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaimprovecomplexform", ref _intKarmaImproveComplexForm);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmanewaiprogram", ref _intKarmaNewAIProgram);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmanewaiadvancedprogram", ref _intKarmaNewAIAdvancedProgram);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmanuyenper", ref _intKarmaNuyenPer);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmacontact", ref _intKarmaContact);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaenemy", ref _intKarmaEnemy);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmacarryover", ref _intKarmaCarryover);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaspirit", ref _intKarmaSpirit);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmamaneuver", ref _intKarmaManeuver);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmainitiation", ref _intKarmaInitiation);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmainitiationflat", ref _intKarmaInitiationFlat);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmametamagic", ref _intKarmaMetamagic);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmacomplexformoption", ref _intKarmaComplexFormOption);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmajoingroup", ref _intKarmaJoinGroup);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaleavegroup", ref _intKarmaLeaveGroup);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmacomplexformskillsoft", ref _intKarmaComplexFormSkillsoft);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaenhancement", ref _intKarmaEnhancement);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmamysadpp", ref _intKarmaMysticAdeptPowerPoint);

                // Attempt to load the Karma costs for Foci.
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaalchemicalfocus", ref _intKarmaAlchemicalFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmabanishingfocus", ref _intKarmaBanishingFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmabindingfocus", ref _intKarmaBindingFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmacenteringfocus", ref _intKarmaCenteringFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmacounterspellingfocus", ref _intKarmaCounterspellingFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmadisenchantingfocus", ref _intKarmaDisenchantingFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaflexiblesignaturefocus", ref _intKarmaFlexibleSignatureFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmamaskingfocus", ref _intKarmaMaskingFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmapowerfocus", ref _intKarmaPowerFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaqifocus", ref _intKarmaQiFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaritualspellcastingfocus", ref _intKarmaRitualSpellcastingFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaspellcastingfocus", ref _intKarmaSpellcastingFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaspellshapingfocus", ref _intKarmaSpellShapingFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmasummoningfocus", ref _intKarmaSummoningFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmasustainingfocus", ref _intKarmaSustainingFocus);
                xmlKarmaCostNode.TryGetInt32FieldQuickly("karmaweaponfocus", ref _intKarmaWeaponFocus);
            }

            // Load Books.
            _lstBooks.Clear();
            foreach (XPathNavigator xmlBook in objXmlNode.Select("books/book"))
                _lstBooks.Add(xmlBook.Value);
            RecalculateBookXPath();

            // Load Custom Data Directory names.
            int intTopMostLoadOrder = 0;
            _dicCustomDataDirectoryNames.Clear();
            bool blnNeedToProcessInfosWithoutLoadOrder = false;
            foreach (XPathNavigator objXmlDirectoryName in objXmlNode.Select("customdatadirectorynames/customdatadirectoryname"))
            {
                string strDirectoryName = objXmlDirectoryName.SelectSingleNode("directoryname")?.Value;
                if (!string.IsNullOrEmpty(strDirectoryName))
                {
                    // Only load in directories that are either present in our GlobalOptions or are enabled
                    bool blnLoopEnabled = Convert.ToBoolean(objXmlDirectoryName.SelectSingleNode("enabled")?.Value);
                    if (blnLoopEnabled || GlobalOptions.CustomDataDirectoryInfos.Any(x => x.Name == strDirectoryName))
                    {
                        string strOrder = objXmlDirectoryName.SelectSingleNode("order")?.Value;
                        if (!string.IsNullOrEmpty(strOrder)
                            && int.TryParse(strOrder, NumberStyles.Integer, GlobalOptions.InvariantCultureInfo, out int intOrder))
                        {
                            intTopMostLoadOrder = Math.Max(intTopMostLoadOrder, intTopMostLoadOrder);
                            _dicCustomDataDirectoryNames.Add(strDirectoryName,
                                new Tuple<int, bool>(intOrder, blnLoopEnabled));
                        }
                        else
                            blnNeedToProcessInfosWithoutLoadOrder = true;
                    }
                }
            }
            // Add in the stragglers that didn't have any load order info
            if (blnNeedToProcessInfosWithoutLoadOrder)
            {
                foreach (XPathNavigator objXmlDirectoryName in objXmlNode.Select("customdatadirectorynames/customdatadirectoryname"))
                {
                    string strDirectoryName = objXmlDirectoryName.SelectSingleNode("directoryname")?.Value;
                    if (!string.IsNullOrEmpty(strDirectoryName))
                    {
                        string strOrder = objXmlDirectoryName.SelectSingleNode("order")?.Value;
                        if (string.IsNullOrEmpty(strOrder)
                            || !int.TryParse(strOrder, NumberStyles.Integer, GlobalOptions.InvariantCultureInfo, out int _))
                        {
                            // Only load in directories that are either present in our GlobalOptions or are enabled
                            bool blnLoopEnabled = Convert.ToBoolean(objXmlDirectoryName.SelectSingleNode("enabled")?.Value);
                            if (blnLoopEnabled || GlobalOptions.CustomDataDirectoryInfos.Any(x => x.Name == strDirectoryName))
                            {
                                _dicCustomDataDirectoryNames.Add(strDirectoryName,
                                    new Tuple<int, bool>(intTopMostLoadOrder, blnLoopEnabled));
                                ++intTopMostLoadOrder;
                            }
                        }
                    }
                }
            }

            if (_dicCustomDataDirectoryNames.Count == 0)
            {
                foreach (XPathNavigator objXmlDirectoryName in objXmlNode.Select("customdatadirectorynames/directoryname"))
                {
                    _dicCustomDataDirectoryNames.Add(objXmlDirectoryName.Value, new Tuple<int, bool>(intTopMostLoadOrder, true));
                    ++intTopMostLoadOrder;
                }
            }

            // Add in any directories that are in GlobalOptions but are not present in the settings so that we may enable them if we want to
            foreach (CustomDataDirectoryInfo objMissingDirectory in GlobalOptions.CustomDataDirectoryInfos.Where(x => !_dicCustomDataDirectoryNames.Keys.Contains(x.Name)))
            {
                _dicCustomDataDirectoryNames.Add(objMissingDirectory.Name, new Tuple<int, bool>(intTopMostLoadOrder, false));
                ++intTopMostLoadOrder;
            }

            // Load default build settings.
            XPathNavigator xmlDefaultBuildNode = objXmlNode.SelectSingleNode("defaultbuild");
            if (xmlDefaultBuildNode != null)
            {
                xmlDefaultBuildNode.TryGetStringFieldQuickly("buildmethod", ref _strBuildMethod);
                xmlDefaultBuildNode.TryGetInt32FieldQuickly("buildpoints", ref _intBuildPoints);
                xmlDefaultBuildNode.TryGetInt32FieldQuickly("availability", ref _intAvailability);
            }

            return true;
        }
        #endregion

        #region Properties and Methods
        /// <summary>
        /// Load the Options from the Registry (which will subsequently be converted to the XML Settings File format). Registry keys are deleted once they are read since they will no longer be used.
        /// </summary>
        public void LoadFromRegistry()
        {
            if (GlobalOptions.ChummerRegistryKey == null)
                return;
            // Print all Active Skills with a total value greater than 0 (as opposed to only printing those with a Rating higher than 0).
            GlobalOptions.LoadBoolFromRegistry(ref _blnPrintSkillsWithZeroRating, "printzeroratingskills", string.Empty, true);

            // More Lethal Gameplay.
            GlobalOptions.LoadBoolFromRegistry(ref _blnMoreLethalGameplay, "morelethalgameplay", string.Empty, true);

            // Spirit Force Based on Total MAG.
            GlobalOptions.LoadBoolFromRegistry(ref _blnSpiritForceBasedOnTotalMAG, "spiritforcebasedontotalmag", string.Empty, true);

            // Skill Defaulting Includes modifiers.
            bool blnTemp = false;
            GlobalOptions.LoadBoolFromRegistry(ref blnTemp, "skilldefaultingincludesmodifiers", string.Empty, true);

            // Print Expenses.
            GlobalOptions.LoadBoolFromRegistry(ref _blnPrintExpenses, "printexpenses", string.Empty, true);

            // Print Free Expenses.
            GlobalOptions.LoadBoolFromRegistry(ref _blnPrintFreeExpenses, "printfreeexpenses", string.Empty, true);

            // Nuyen per Build Point
            GlobalOptions.LoadDecFromRegistry(ref _decNuyenPerBP, "nuyenperbp", string.Empty, true);

            // No Single Armor Encumbrance
            GlobalOptions.LoadBoolFromRegistry(ref _blnNoSingleArmorEncumbrance, "nosinglearmorencumbrance", string.Empty, true);

            // Essence Loss Reduces Maximum Only.
            GlobalOptions.LoadBoolFromRegistry(ref _blnESSLossReducesMaximumOnly, "esslossreducesmaximumonly", string.Empty, true);

            // Allow Skill Regrouping.
            GlobalOptions.LoadBoolFromRegistry(ref _blnAllowSkillRegrouping, "allowskillregrouping", string.Empty, true);

            // Attempt to populate the Karma values.
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaAttribute, "karmaattribute", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaQuality, "karmaquality", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaSpecialization, "karmaspecialization", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaKnoSpecialization, "karmaknospecialization", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaNewKnowledgeSkill, "karmanewknowledgeskill", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaNewActiveSkill, "karmanewactiveskill", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaNewSkillGroup, "karmanewskillgroup", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaImproveKnowledgeSkill, "karmaimproveknowledgeskill", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaImproveActiveSkill, "karmaimproveactiveskill", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaImproveSkillGroup, "karmaimproveskillgroup", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaSpell, "karmaspell", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaEnhancement, "karmaenhancement", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaNewComplexForm, "karmanewcomplexform", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaImproveComplexForm, "karmaimprovecomplexform", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaNewAIProgram, "karmanewaiprogram", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaNewAIAdvancedProgram, "karmanewaiadvancedprogram", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaNuyenPer, "karmanuyenper", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaContact, "karmacontact", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaEnemy, "karmaenemy", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaCarryover, "karmacarryover", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaSpirit, "karmaspirit", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaManeuver, "karmamaneuver", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaInitiation, "karmainitiation", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaInitiationFlat, "karmainitiationflat", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaMetamagic, "karmametamagic", string.Empty, true);
            GlobalOptions.LoadInt32FromRegistry(ref _intKarmaComplexFormOption, "karmacomplexformoption", string.Empty, true);

            // Retrieve the sourcebooks that are in the Registry.
            string strBookList;
            object objBooksKeyValue = GlobalOptions.ChummerRegistryKey.GetValue("books");
            if (objBooksKeyValue != null)
            {
                strBookList = objBooksKeyValue.ToString();
            }
            else
            {
                // We were unable to get the Registry key which means the book options have not been saved yet, so create the default values.
                strBookList = "Shadowrun 5th Edition";
                GlobalOptions.ChummerRegistryKey.SetValue("books", strBookList);
            }
            string[] strBooks = strBookList.Split(',');

            XmlDocument objXmlDocument = XmlManager.Load("books.xml", EnabledCustomDataDirectoryPaths);

            foreach (string strBookName in strBooks)
            {
                string strCode = objXmlDocument.SelectSingleNode("/chummer/books/book[name = " + strBookName.CleanXPath() + " and not(hide)]/code")?.InnerText;
                if (!string.IsNullOrEmpty(strCode))
                {
                    _lstBooks.Add(strCode);
                }
            }
            RecalculateBookXPath();

            // Delete the Registry keys ones the values have been retrieve since they will no longer be used.
            GlobalOptions.ChummerRegistryKey.DeleteValue("books");
        }

        /// <summary>
        /// Determine whether or not a given book is in use.
        /// </summary>
        /// <param name="strCode">Book code to search for.</param>
        public bool BookEnabled(string strCode)
        {
            return _lstBooks.Contains(strCode);
        }

        /// <summary>
        /// XPath query used to filter items based on the user's selected source books and optional rules.
        /// </summary>
        public string BookXPath(bool excludeHidden = true)
        {
            string strPath = string.Empty;

            if (excludeHidden)
            {
                strPath = "not(hide)";
            }
            if (string.IsNullOrWhiteSpace(_strBookXPath) && _lstBooks.Count > 0)
            {
                RecalculateBookXPath();
            }
            if (string.IsNullOrWhiteSpace(strPath))
            {
                strPath = _strBookXPath;
            }
            else if (!string.IsNullOrEmpty(_strBookXPath))
            {
                strPath += " and " + _strBookXPath;
            }
            else
            {
                // Should not ever have a situation where BookXPath remains empty after recalculation, but it's here just in case
                Utils.BreakIfDebug();
            }
            if (!DroneMods)
            {
                if (string.IsNullOrEmpty(strPath))
                    strPath = "not(optionaldrone)";
                else
                    strPath += " and not(optionaldrone)";
            }
            return strPath;
        }

        /// <summary>
        /// XPath query used to filter items based on the user's selected source books.
        /// </summary>
        public void RecalculateBookXPath()
        {
            StringBuilder strBookXPath = new StringBuilder("(");
            _strBookXPath = string.Empty;

            foreach (string strBook in _lstBooks)
            {
                if (!string.IsNullOrWhiteSpace(strBook))
                {
                    strBookXPath.Append("source = \"");
                    strBookXPath.Append(strBook);
                    strBookXPath.Append("\" or ");
                }
            }
            if (strBookXPath.Length >= 4)
            {
                strBookXPath.Length -= 4;
                strBookXPath.Append(')');
                _strBookXPath = strBookXPath.ToString();
            }
            else
                _strBookXPath = string.Empty;
        }

        public IDictionary<string, Tuple<int, bool>> CustomDataDirectoryNames => _dicCustomDataDirectoryNames;

        public IReadOnlyList<string> EnabledCustomDataDirectoryPaths => _lstEnabledCustomDataDirectoryPaths;

        public IReadOnlyList<CustomDataDirectoryInfo> EnabledCustomDataDirectoryInfos => _lstEnabledCustomDataDirectories;

        public void RecalculateEnabledCustomDataDirectories()
        {
            _lstEnabledCustomDataDirectories.Clear();
            _lstEnabledCustomDataDirectoryPaths.Clear();
            foreach (string strEnabledCustomDataDirectoryName in _dicCustomDataDirectoryNames.Where(x => x.Value.Item2).OrderBy(x => x.Value.Item1).Select(x => x.Key))
            {
                CustomDataDirectoryInfo objInfoToAdd = GlobalOptions.CustomDataDirectoryInfos.FirstOrDefault(x => x.Name == strEnabledCustomDataDirectoryName);
                if (objInfoToAdd != null)
                {
                    _lstEnabledCustomDataDirectories.Add(objInfoToAdd);
                    _lstEnabledCustomDataDirectoryPaths.Add(objInfoToAdd.Path);
                }
                else
                    Utils.BreakIfDebug();
            }
        }

        public string SourceId => _guiSourceId.ToString("D", GlobalOptions.InvariantCultureInfo);

        /// <summary>
        /// Whether or not all Active Skills with a total score higher than 0 should be printed.
        /// </summary>
        public bool PrintSkillsWithZeroRating
        {
            get => _blnPrintSkillsWithZeroRating;
            set
            {
                if (_blnPrintSkillsWithZeroRating != value)
                {
                    _blnPrintSkillsWithZeroRating = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not the More Lethal Gameplay optional rule is enabled.
        /// </summary>
        public bool MoreLethalGameplay
        {
            get => _blnMoreLethalGameplay;
            set
            {
                if (_blnMoreLethalGameplay != value)
                {
                    _blnMoreLethalGameplay = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not to require licensing restricted items.
        /// </summary>
        public bool LicenseRestricted
        {
            get => _blnLicenseRestrictedItems;
            set
            {
                if (_blnLicenseRestrictedItems != value)
                {
                    _blnLicenseRestrictedItems = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not a Spirit's Maximum Force is based on the character's total MAG.
        /// </summary>
        public bool SpiritForceBasedOnTotalMAG
        {
            get => _blnSpiritForceBasedOnTotalMAG;
            set
            {
                if (_blnSpiritForceBasedOnTotalMAG != value)
                {
                    _blnSpiritForceBasedOnTotalMAG = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not the Karma and Nuyen Expenses should be printed on the character sheet.
        /// </summary>
        public bool PrintExpenses
        {
            get => _blnPrintExpenses;
            set
            {
                if (_blnPrintExpenses != value)
                {
                    _blnPrintExpenses = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not the Karma and Nuyen Expenses that have a cost of 0 should be printed on the character sheet.
        /// </summary>
        public bool PrintFreeExpenses
        {
            get => _blnPrintFreeExpenses;
            set
            {
                if (_blnPrintFreeExpenses != value)
                {
                    _blnPrintFreeExpenses = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Amount of Nuyen gained per BP spent.
        /// </summary>
        public decimal NuyenPerBP
        {
            get => _decNuyenPerBP;
            set
            {
                if (_decNuyenPerBP != value)
                {
                    _decNuyenPerBP = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not UnarmedAP, UnarmedReach and UnarmedDV Improvements apply to weapons that use the Unarmed Combat skill.
        /// </summary>
        public bool UnarmedImprovementsApplyToWeapons
        {
            get => _blnUnarmedImprovementsApplyToWeapons;
            set
            {
                if (_blnUnarmedImprovementsApplyToWeapons != value)
                {
                    _blnUnarmedImprovementsApplyToWeapons = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not characters may use Initiation/Submersion in Create mode.
        /// </summary>
        public bool AllowInitiationInCreateMode
        {
            get => _blnAllowInitiationInCreateMode;
            set
            {
                if (_blnAllowInitiationInCreateMode != value)
                {
                    _blnAllowInitiationInCreateMode = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not characters can spend skill points on broken groups.
        /// </summary>
        public bool UsePointsOnBrokenGroups
        {
            get => _blnUsePointsOnBrokenGroups;
            set
            {
                if (_blnUsePointsOnBrokenGroups != value)
                {
                    _blnUsePointsOnBrokenGroups = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not characters in Career Mode should pay double for qualities.
        /// </summary>
        public bool DontDoubleQualityPurchases
        {
            get => _blnDontDoubleQualityPurchaseCost;
            set
            {
                if (_blnDontDoubleQualityPurchaseCost != value)
                {
                    _blnDontDoubleQualityPurchaseCost = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not characters in Career Mode should pay double for removing Negative Qualities.
        /// </summary>
        public bool DontDoubleQualityRefunds
        {
            get => _blnDontDoubleQualityRefundCost;
            set
            {
                if (_blnDontDoubleQualityRefundCost != value)
                {
                    _blnDontDoubleQualityRefundCost = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not to ignore the art requirements from street grimoire.
        /// </summary>
        public bool IgnoreArt
        {
            get => _blnIgnoreArt;
            set
            {
                if (_blnIgnoreArt != value)
                {
                    _blnIgnoreArt = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not to ignore the limit on Complex Forms in Career mode.
        /// </summary>
        public bool IgnoreComplexFormLimit
        {
            get => _blnIgnoreComplexFormLimit;
            set
            {
                if (_blnIgnoreComplexFormLimit != value)
                {
                    _blnIgnoreComplexFormLimit = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not to use stats from Cyberlegs when calculating movement rates
        /// </summary>
        public bool CyberlegMovement
        {
            get => _blnCyberlegMovement;
            set
            {
                if (_blnCyberlegMovement != value)
                {
                    _blnCyberlegMovement = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Allow Mystic Adepts to increase their power points during career mode
        /// </summary>
        public bool MysAdeptAllowPPCareer
        {
            get => _blnMysAdeptAllowPPCareer;
            set
            {
                if (_blnMysAdeptAllowPPCareer != value)
                {
                    _blnMysAdeptAllowPPCareer = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Split MAG for Mystic Adepts so that they have a separate MAG rating for Adept Powers instead of using the special PP rules for mystic adepts
        /// </summary>
        public bool MysAdeptSecondMAGAttribute
        {
            get => _blnMysAdeptSecondMAGAttribute;
            set
            {
                if (_blnMysAdeptSecondMAGAttribute != value)
                {
                    _blnMysAdeptSecondMAGAttribute = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not to allow a 2nd max attribute with Exceptional Attribute
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public bool Allow2ndMaxAttribute
        {
            get => _blnAllow2ndMaxAttribute;
            set
            {
                if (_blnAllow2ndMaxAttribute != value)
                {
                    _blnAllow2ndMaxAttribute = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// The XPath expression to use to determine how many contact points the character has
        /// </summary>
        public string ContactPointsExpression
        {
            get => _strContactPointsExpression;
            set
            {
                string strNewValue = value.CleanXPath();
                if (_strContactPointsExpression != strNewValue)
                {
                    _strContactPointsExpression = strNewValue;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// The XPath expression to use to determine how many knowledge points the character has
        /// </summary>
        public string KnowledgePointsExpression
        {
            get => _strKnowledgePointsExpression;
            set
            {
                string strNewValue = value.CleanXPath();
                if (_strKnowledgePointsExpression != strNewValue)
                {
                    _strKnowledgePointsExpression = strNewValue;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// The Drone Body multiplier for maximal Armor
        /// </summary>
        public int DroneArmorMultiplier
        {
            get => _intDroneArmorMultiplier;
            set
            {
                if (_intDroneArmorMultiplier != value)
                {
                    _intDroneArmorMultiplier = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not Armor
        /// </summary>
        public bool DroneArmorMultiplierEnabled
        {
            get => _blnDroneArmorMultiplierEnabled;
            set
            {
                if (_blnDroneArmorMultiplierEnabled != value)
                {
                    _blnDroneArmorMultiplierEnabled = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// House Rule: Ignore Armor Encumbrance entirely.
        /// </summary>
        public bool NoArmorEncumbrance
        {
            get => _blnNoArmorEncumbrance;
            set
            {
                if (_blnNoArmorEncumbrance != value)
                {
                    _blnNoArmorEncumbrance = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not Essence loss only reduces MAG/RES maximum value, not the current value.
        /// </summary>
        public bool ESSLossReducesMaximumOnly
        {
            get => _blnESSLossReducesMaximumOnly;
            set
            {
                if (_blnESSLossReducesMaximumOnly != value)
                {
                    _blnESSLossReducesMaximumOnly = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not characters are allowed to put points into a Skill Group again once it is broken and all Ratings are the same.
        /// </summary>
        public bool AllowSkillRegrouping
        {
            get => _blnAllowSkillRegrouping;
            set
            {
                if (_blnAllowSkillRegrouping != value)
                {
                    _blnAllowSkillRegrouping = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Sourcebooks.
        /// </summary>
        public ICollection<string> Books => _lstBooks;

        /// <summary>
        /// Setting name.
        /// </summary>
        public string Name
        {
            get => _strName;
            set
            {
                if (_strName != value)
                {
                    _strName = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not Metatypes cost Karma equal to their BP when creating a character with Karma.
        /// </summary>
        public bool MetatypeCostsKarma
        {
            get => _blnMetatypeCostsKarma;
            set
            {
                if (_blnMetatypeCostsKarma != value)
                {
                    _blnMetatypeCostsKarma = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Multiplier for Metatype Karma Costs when converting from BP to Karma.
        /// </summary>
        public int MetatypeCostsKarmaMultiplier
        {
            get => _intMetatypeCostMultiplier;
            set
            {
                if (_intMetatypeCostMultiplier != value)
                {
                    _intMetatypeCostMultiplier = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Number of Limbs a standard character has.
        /// </summary>
        public int LimbCount
        {
            get => _intLimbCount;
            set
            {
                if (_intLimbCount != value)
                {
                    _intLimbCount = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Exclude a particular Limb Slot from count towards the Limb Count.
        /// </summary>
        public string ExcludeLimbSlot
        {
            get => _strExcludeLimbSlot;
            set
            {
                if (_strExcludeLimbSlot != value)
                {
                    _strExcludeLimbSlot = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Allow Cyberware Essence cost discounts.
        /// </summary>
        public bool AllowCyberwareESSDiscounts
        {
            get => _blnAllowCyberwareESSDiscounts;
            set
            {
                if (_blnAllowCyberwareESSDiscounts != value)
                {
                    _blnAllowCyberwareESSDiscounts = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not Armor Degradation is allowed.
        /// </summary>
        public bool ArmorDegradation
        {
            get => _blnArmorDegradation;
            set
            {
                if (_blnArmorDegradation != value)
                {
                    _blnArmorDegradation = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// If true, karma costs will not decrease from reductions due to essence loss. Effectively, essence loss becomes an augmented modifier, not one that alters minima and maxima.
        /// </summary>
        public bool SpecialKarmaCostBasedOnShownValue
        {
            get => _blnSpecialKarmaCostBasedOnShownValue;
            set
            {
                if (_blnSpecialKarmaCostBasedOnShownValue != value)
                {
                    _blnSpecialKarmaCostBasedOnShownValue = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not characters can have more than 25 BP in Positive Qualities.
        /// </summary>
        public bool ExceedPositiveQualities
        {
            get => _blnExceedPositiveQualities;
            set
            {
                if (_blnExceedPositiveQualities != value)
                {
                    _blnExceedPositiveQualities = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// If true, the karma cost of qualities is doubled after the initial 25.
        /// </summary>
        public bool ExceedPositiveQualitiesCostDoubled
        {
            get => _blnExceedPositiveQualitiesCostDoubled;
            set
            {
                if (_blnExceedPositiveQualitiesCostDoubled != value)
                {
                    _blnExceedPositiveQualitiesCostDoubled = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not characters can have more than 25 BP in Negative Qualities.
        /// </summary>
        public bool ExceedNegativeQualities
        {
            get => _blnExceedNegativeQualities;
            set
            {
                if (_blnExceedNegativeQualities != value)
                {
                    _blnExceedNegativeQualities = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// If true, the character will not receive additional BP from Negative Qualities past the initial 25
        /// </summary>
        public bool ExceedNegativeQualitiesLimit
        {
            get => _blnExceedNegativeQualitiesLimit;
            set
            {
                if (_blnExceedNegativeQualitiesLimit != value)
                {
                    _blnExceedNegativeQualitiesLimit = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not Restricted items have their cost multiplied.
        /// </summary>
        public bool MultiplyRestrictedCost
        {
            get => _blnMultiplyRestrictedCost;
            set
            {
                if (_blnMultiplyRestrictedCost != value)
                {
                    _blnMultiplyRestrictedCost = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not Forbidden items have their cost multiplied.
        /// </summary>
        public bool MultiplyForbiddenCost
        {
            get => _blnMultiplyForbiddenCost;
            set
            {
                if (_blnMultiplyForbiddenCost != value)
                {
                    _blnMultiplyForbiddenCost = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Cost multiplier for Restricted items.
        /// </summary>
        public int RestrictedCostMultiplier
        {
            get => _intRestrictedCostMultiplier;
            set
            {
                if (_intRestrictedCostMultiplier != value)
                {
                    _intRestrictedCostMultiplier = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Cost multiplier for Forbidden items.
        /// </summary>
        public int ForbiddenCostMultiplier
        {
            get => _intForbiddenCostMultiplier;
            set
            {
                if (_intForbiddenCostMultiplier != value)
                {
                    _intForbiddenCostMultiplier = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _intCachedNuyenDecimals = -1;
        /// <summary>
        /// Number of decimal places to round to when displaying nuyen values.
        /// </summary>
        public int NuyenDecimals
        {
            get
            {
                if (_intCachedNuyenDecimals >= 0)
                    return _intCachedNuyenDecimals;
                string strNuyenFormat = NuyenFormat;
                int intDecimalPlaces = strNuyenFormat.IndexOf('.');
                if (intDecimalPlaces == -1)
                    intDecimalPlaces = 0;
                else
                    intDecimalPlaces = strNuyenFormat.Length - intDecimalPlaces - 1;

                return _intCachedNuyenDecimals = intDecimalPlaces;
            }
            set
            {
                int intCurrentNuyenDecimals = NuyenDecimals;
                int intNewNuyenDecimals = Math.Max(value, 0);
                if (intNewNuyenDecimals < intCurrentNuyenDecimals)
                {
                    if (intNewNuyenDecimals > 0)
                        NuyenFormat = NuyenFormat.Substring(0, NuyenFormat.Length - (intNewNuyenDecimals - intCurrentNuyenDecimals));
                    else
                    {
                        int intDecimalPlaces = NuyenFormat.IndexOf('.');
                        if (intDecimalPlaces != -1)
                            NuyenFormat = NuyenFormat.Substring(0, intDecimalPlaces);
                    }
                }
                else if (intNewNuyenDecimals > intCurrentNuyenDecimals)
                {
                    StringBuilder objNuyenFormat = string.IsNullOrEmpty(NuyenFormat) ? new StringBuilder("#,0") : new StringBuilder(NuyenFormat);
                    if (intCurrentNuyenDecimals == 0)
                    {
                        objNuyenFormat.Append(".");
                        for (int i = 0; i < intNewNuyenDecimals; ++i)
                        {
                            objNuyenFormat.Append("0");
                        }
                    }
                    else
                    {
                        string strDecimalTypeToAdd = string.IsNullOrEmpty(NuyenFormat) ? "0" : NuyenFormat[NuyenFormat.Length - 1].ToString(GlobalOptions.InvariantCultureInfo);
                        intNewNuyenDecimals -= intCurrentNuyenDecimals;
                        for (int i = 0; i < intNewNuyenDecimals; ++i)
                        {
                            objNuyenFormat.Append(strDecimalTypeToAdd);
                        }
                    }
                    NuyenFormat = objNuyenFormat.ToString();
                }
            }
        }

        /// <summary>
        /// Format in which nuyen values should be displayed (does not include nuyen symbol).
        /// </summary>
        public string NuyenFormat
        {
            get => _strNuyenFormat;
            set
            {
                if (_strNuyenFormat != value)
                {
                    _strNuyenFormat = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _intCachedEssenceDecimals = -1;

        /// <summary>
        /// Number of decimal places to round to when calculating Essence.
        /// </summary>
        public int EssenceDecimals
        {
            get
            {
                if (_intCachedEssenceDecimals >= 0)
                    return _intCachedEssenceDecimals;
                string strEssenceFormat = EssenceFormat;
                int intDecimalPlaces = strEssenceFormat.IndexOf('.');
                if (intDecimalPlaces == -1)
                    intDecimalPlaces = 0;
                else
                    intDecimalPlaces = strEssenceFormat.Length - intDecimalPlaces - 1;

                return _intCachedEssenceDecimals = intDecimalPlaces;
            }
            set
            {
                int intCurrentEssenceDecimals = EssenceDecimals;
                int intNewEssenceDecimals = Math.Max(value, 0);
                if (intNewEssenceDecimals < intCurrentEssenceDecimals)
                {
                    if (intNewEssenceDecimals > 0)
                    {
                        int length = EssenceFormat.Length - (intCurrentEssenceDecimals - intNewEssenceDecimals);
                        if (length < 3) length = 3;
                        EssenceFormat = EssenceFormat.Substring(0, length);
                    }
                    else
                    {
                        int intDecimalPlaces = EssenceFormat.IndexOf('.');
                        if (intDecimalPlaces != -1)
                            EssenceFormat = EssenceFormat.Substring(0, intDecimalPlaces);
                    }
                }
                else if (intNewEssenceDecimals > intCurrentEssenceDecimals)
                {
                    StringBuilder objEssenceFormat = string.IsNullOrEmpty(EssenceFormat) ? new StringBuilder("#,0") : new StringBuilder(EssenceFormat);
                    if (intCurrentEssenceDecimals == 0)
                    {
                        objEssenceFormat.Append(".");
                    }
                    intNewEssenceDecimals -= intCurrentEssenceDecimals;
                    for (int i = 0; i < intNewEssenceDecimals; ++i)
                    {
                        objEssenceFormat.Append("0");
                    }
                    EssenceFormat = objEssenceFormat.ToString();
                }
            }
        }

        /// <summary>
        /// Display format for Essence.
        /// </summary>
        public string EssenceFormat
        {
            get => _strEssenceFormat;
            set
            {
                if (_strEssenceFormat != value)
                {
                    _strEssenceFormat = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Only round essence when its value is displayed
        /// </summary>
        public bool DontRoundEssenceInternally
        {
            get => _blnDoNotRoundEssenceInternally;
            set
            {
                if (_blnDoNotRoundEssenceInternally != value)
                {
                    _blnDoNotRoundEssenceInternally = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Do Enemies count towards Negative Quality Karma limit in create mode?
        /// </summary>
        public bool EnemyKarmaQualityLimit
        {
            get => _blnEnemyKarmaQualityLimit;
            set
            {
                if (_blnEnemyKarmaQualityLimit != value)
                {
                    _blnEnemyKarmaQualityLimit = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not Capacity limits should be enforced.
        /// </summary>
        public bool EnforceCapacity
        {
            get => _blnEnforceCapacity;
            set
            {
                if (_blnEnforceCapacity != value)
                {
                    _blnEnforceCapacity = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not Recoil modifiers are restricted (AR 148).
        /// </summary>
        public bool RestrictRecoil
        {
            get => _blnRestrictRecoil;
            set
            {
                if (_blnRestrictRecoil != value)
                {
                    _blnRestrictRecoil = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not characters are unrestricted in the number of points they can invest in Nuyen.
        /// </summary>
        public bool UnrestrictedNuyen
        {
            get => _blnUnrestrictedNuyen;
            set
            {
                if (_blnUnrestrictedNuyen != value)
                {
                    _blnUnrestrictedNuyen = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not Stacked Foci can have a combined Force higher than 6.
        /// </summary>
        public bool AllowHigherStackedFoci
        {
            get => _blnAllowHigherStackedFoci;
            set
            {
                if (_blnAllowHigherStackedFoci != value)
                {
                    _blnAllowHigherStackedFoci = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not the user can change the Part of Base Weapon flag for a Weapon Accessory or Mod.
        /// </summary>
        public bool AllowEditPartOfBaseWeapon
        {
            get => _blnAllowEditPartOfBaseWeapon;
            set
            {
                if (_blnAllowEditPartOfBaseWeapon != value)
                {
                    _blnAllowEditPartOfBaseWeapon = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not the user is allowed to break Skill Groups while in Create Mode.
        /// </summary>
        public bool StrictSkillGroupsInCreateMode
        {
            get => _blnStrictSkillGroupsInCreateMode;
            set
            {
                if (_blnStrictSkillGroupsInCreateMode != value)
                {
                    _blnStrictSkillGroupsInCreateMode = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not the user is allowed to buy specializations with skill points for skills only bought with karma.
        /// </summary>
        public bool AllowPointBuySpecializationsOnKarmaSkills
        {
            get => _blnAllowPointBuySpecializationsOnKarmaSkills;
            set
            {
                if (_blnAllowPointBuySpecializationsOnKarmaSkills != value)
                {
                    _blnAllowPointBuySpecializationsOnKarmaSkills = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not any Detection Spell can be taken as Extended range version.
        /// </summary>
        public bool ExtendAnyDetectionSpell
        {
            get => _blnExtendAnyDetectionSpell;
            set
            {
                if (_blnExtendAnyDetectionSpell != value)
                {
                    _blnExtendAnyDetectionSpell = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not cyberlimbs stats are used in attribute calculation
        /// </summary>
        public bool DontUseCyberlimbCalculation
        {
            get => _blnDontUseCyberlimbCalculation;
            set
            {
                if (_blnDontUseCyberlimbCalculation != value)
                {
                    _blnDontUseCyberlimbCalculation = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// House rule: Treat the Metatype Attribute Minimum as 1 for the purpose of calculating Karma costs.
        /// </summary>
        public bool AlternateMetatypeAttributeKarma
        {
            get => _blnAlternateMetatypeAttributeKarma;
            set
            {
                if (_blnAlternateMetatypeAttributeKarma != value)
                {
                    _blnAlternateMetatypeAttributeKarma = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// House rule: Whether to compensate for the karma cost difference between raising skill ratings and skill groups when increasing the rating of the last skill in the group
        /// </summary>
        public bool CompensateSkillGroupKarmaDifference
        {
            get => _blnCompensateSkillGroupKarmaDifference;
            set
            {
                if (_blnCompensateSkillGroupKarmaDifference != value)
                {
                    _blnCompensateSkillGroupKarmaDifference = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not Notes should be printed.
        /// </summary>
        public bool PrintNotes
        {
            get => _blnPrintNotes;
            set
            {
                if (_blnPrintNotes != value)
                {
                    _blnPrintNotes = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not Obsolescent can be removed/upgraded in the same way as Obsolete.
        /// </summary>
        public bool AllowObsolescentUpgrade
        {
            get => _blnAllowObsolescentUpgrade;
            set
            {
                if (_blnAllowObsolescentUpgrade != value)
                {
                    _blnAllowObsolescentUpgrade = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether or not Bioware Suites can be added and created.
        /// </summary>
        public bool AllowBiowareSuites
        {
            get => _blnAllowBiowareSuites;
            set
            {
                if (_blnAllowBiowareSuites != value)
                {
                    _blnAllowBiowareSuites = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// House rule: Free Spirits calculate their Power Points based on their MAG instead of EDG.
        /// </summary>
        public bool FreeSpiritPowerPointsMAG
        {
            get => _blnFreeSpiritPowerPointsMAG;
            set
            {
                if (_blnFreeSpiritPowerPointsMAG != value)
                {
                    _blnFreeSpiritPowerPointsMAG = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// House rule: Attribute values are clamped to 0 or are allowed to go below 0 due to Essence Loss.
        /// </summary>
        public bool UnclampAttributeMinimum
        {
            get => _blnUnclampAttributeMinimum;
            set
            {
                if (_blnUnclampAttributeMinimum != value)
                {
                    _blnUnclampAttributeMinimum = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Use Rigger 5.0 drone modding rules
        /// </summary>
        public bool DroneMods
        {
            get => _blnDroneMods;
            set
            {
                if (_blnDroneMods != value)
                {
                    _blnDroneMods = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Apply drone mod attribute maximum rule to Pilot, too
        /// </summary>
        public bool DroneModsMaximumPilot
        {
            get => _blnDroneModsMaximumPilot;
            set
            {
                if (_blnDroneModsMaximumPilot != value)
                {
                    _blnDroneModsMaximumPilot = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion

        #region Karma
        /// <summary>
        /// Karma cost to improve an Attribute = New Rating X this value.
        /// </summary>
        public int KarmaAttribute
        {
            get => _intKarmaAttribute;
            set
            {
                if (_intKarmaAttribute != value)
                {
                    _intKarmaAttribute = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost to purchase a Quality = BP Cost x this value.
        /// </summary>
        public int KarmaQuality
        {
            get => _intKarmaQuality;
            set
            {
                if (_intKarmaQuality != value)
                {
                    _intKarmaQuality = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost to purchase a Specialization for an active skill = this value.
        /// </summary>
        public int KarmaSpecialization
        {
            get => _intKarmaSpecialization;
            set
            {
                if (_intKarmaSpecialization != value)
                {
                    _intKarmaSpecialization = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost to purchase a Specialization for a knowledge skill = this value.
        /// </summary>
        public int KarmaKnowledgeSpecialization
        {
            get => _intKarmaKnoSpecialization;
            set
            {
                if (_intKarmaKnoSpecialization != value)
                {
                    _intKarmaKnoSpecialization = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost to purchase a new Knowledge Skill = this value.
        /// </summary>
        public int KarmaNewKnowledgeSkill
        {
            get => _intKarmaNewKnowledgeSkill;
            set
            {
                if (_intKarmaNewKnowledgeSkill != value)
                {
                    _intKarmaNewKnowledgeSkill = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost to purchase a new Active Skill = this value.
        /// </summary>
        public int KarmaNewActiveSkill
        {
            get => _intKarmaNewActiveSkill;
            set
            {
                if (_intKarmaNewActiveSkill != value)
                {
                    _intKarmaNewActiveSkill = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost to purchase a new Skill Group = this value.
        /// </summary>
        public int KarmaNewSkillGroup
        {
            get => _intKarmaNewSkillGroup;
            set
            {
                if (_intKarmaNewSkillGroup != value)
                {
                    _intKarmaNewSkillGroup = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost to improve a Knowledge Skill = New Rating x this value.
        /// </summary>
        public int KarmaImproveKnowledgeSkill
        {
            get => _intKarmaImproveKnowledgeSkill;
            set
            {
                if (_intKarmaImproveKnowledgeSkill != value)
                {
                    _intKarmaImproveKnowledgeSkill = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost to improve an Active Skill = New Rating x this value.
        /// </summary>
        public int KarmaImproveActiveSkill
        {
            get => _intKarmaImproveActiveSkill;
            set
            {
                if (_intKarmaImproveActiveSkill != value)
                {
                    _intKarmaImproveActiveSkill = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost to improve a Skill Group = New Rating x this value.
        /// </summary>
        public int KarmaImproveSkillGroup
        {
            get => _intKarmaImproveSkillGroup;
            set
            {
                if (_intKarmaImproveSkillGroup != value)
                {
                    _intKarmaImproveSkillGroup = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for each Spell = this value.
        /// </summary>
        public int KarmaSpell
        {
            get => _intKarmaSpell;
            set
            {
                if (_intKarmaSpell != value)
                {
                    _intKarmaSpell = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for each Enhancement = this value.
        /// </summary>
        public int KarmaEnhancement
        {
            get => _intKarmaEnhancement;
            set
            {
                if (_intKarmaEnhancement != value)
                {
                    _intKarmaEnhancement = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for a new Complex Form = this value.
        /// </summary>
        public int KarmaNewComplexForm
        {
            get => _intKarmaNewComplexForm;
            set
            {
                if (_intKarmaNewComplexForm != value)
                {
                    _intKarmaNewComplexForm = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost to improve a Complex Form = New Rating x this value.
        /// </summary>
        public int KarmaImproveComplexForm
        {
            get => _intKarmaImproveComplexForm;
            set
            {
                if (_intKarmaImproveComplexForm != value)
                {
                    _intKarmaImproveComplexForm = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Complex Form Options = Rating x this value.
        /// </summary>
        public int KarmaComplexFormOption
        {
            get => _intKarmaComplexFormOption;
            set
            {
                if (_intKarmaComplexFormOption != value)
                {
                    _intKarmaComplexFormOption = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Complex Form Skillsofts = Rating x this value.
        /// </summary>
        public int KarmaComplexFormSkillsoft
        {
            get => _intKarmaComplexFormSkillsoft;
            set
            {
                if (_intKarmaComplexFormSkillsoft != value)
                {
                    _intKarmaComplexFormSkillsoft = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for a new AI Program
        /// </summary>
        public int KarmaNewAIProgram
        {
            get => _intKarmaNewAIProgram;
            set
            {
                if (_intKarmaNewAIProgram != value)
                {
                    _intKarmaNewAIProgram = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for a new AI Advanced Program
        /// </summary>
        public int KarmaNewAIAdvancedProgram
        {
            get => _intKarmaNewAIAdvancedProgram;
            set
            {
                if (_intKarmaNewAIAdvancedProgram != value)
                {
                    _intKarmaNewAIAdvancedProgram = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Amount of Nuyen obtained per Karma point.
        /// </summary>
        public int KarmaNuyenPer
        {
            get => _intKarmaNuyenPer;
            set
            {
                if (_intKarmaNuyenPer != value)
                {
                    _intKarmaNuyenPer = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for a Contact = (Connection + Loyalty) x this value.
        /// </summary>
        public int KarmaContact
        {
            get => _intKarmaContact;
            set
            {
                if (_intKarmaContact != value)
                {
                    _intKarmaContact = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for an Enemy = (Connection + Loyalty) x this value.
        /// </summary>
        public int KarmaEnemy
        {
            get => _intKarmaEnemy;
            set
            {
                if (_intKarmaEnemy != value)
                {
                    _intKarmaEnemy = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Maximum amount of remaining Karma that is carried over to the character once they are created.
        /// </summary>
        public int KarmaCarryover
        {
            get => _intKarmaCarryover;
            set
            {
                if (_intKarmaCarryover != value)
                {
                    _intKarmaCarryover = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for a Spirit = this value.
        /// </summary>
        public int KarmaSpirit
        {
            get => _intKarmaSpirit;
            set
            {
                if (_intKarmaSpirit != value)
                {
                    _intKarmaSpirit = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for a Combat Maneuver = this value.
        /// </summary>
        public int KarmaManeuver
        {
            get => _intKarmaManeuver;
            set
            {
                if (_intKarmaManeuver != value)
                {
                    _intKarmaManeuver = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for an Initiation = KarmaInitiationFlat + (New Rating x this value).
        /// </summary>
        public int KarmaInitiation
        {
            get => _intKarmaInitiation;
            set
            {
                if (_intKarmaInitiation != value)
                {
                    _intKarmaInitiation = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for an Initiation = this value + (New Rating x KarmaInitiation).
        /// </summary>
        public int KarmaInitiationFlat
        {
            get => _intKarmaInitiationFlat;
            set
            {
                if (_intKarmaInitiationFlat != value)
                {
                    _intKarmaInitiationFlat = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for a Metamagic = this value.
        /// </summary>
        public int KarmaMetamagic
        {
            get => _intKarmaMetamagic;
            set
            {
                if (_intKarmaMetamagic != value)
                {
                    _intKarmaMetamagic = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost to join a Group = this value.
        /// </summary>
        public int KarmaJoinGroup
        {
            get => _intKarmaJoinGroup;
            set
            {
                if (_intKarmaJoinGroup != value)
                {
                    _intKarmaJoinGroup = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost to leave a Group = this value.
        /// </summary>
        public int KarmaLeaveGroup
        {
            get => _intKarmaLeaveGroup;
            set
            {
                if (_intKarmaLeaveGroup != value)
                {
                    _intKarmaLeaveGroup = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Alchemical Foci.
        /// </summary>
        public int KarmaAlchemicalFocus
        {
            get => _intKarmaAlchemicalFocus;
            set
            {
                if (_intKarmaAlchemicalFocus != value)
                {
                    _intKarmaAlchemicalFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Banishing Foci.
        /// </summary>
        public int KarmaBanishingFocus
        {
            get => _intKarmaBanishingFocus;
            set
            {
                if (_intKarmaBanishingFocus != value)
                {
                    _intKarmaBanishingFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Binding Foci.
        /// </summary>
        public int KarmaBindingFocus
        {
            get => _intKarmaBindingFocus;
            set
            {
                if (_intKarmaBindingFocus != value)
                {
                    _intKarmaBindingFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Centering Foci.
        /// </summary>
        public int KarmaCenteringFocus
        {
            get => _intKarmaCenteringFocus;
            set
            {
                if (_intKarmaCenteringFocus != value)
                {
                    _intKarmaCenteringFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Counterspelling Foci.
        /// </summary>
        public int KarmaCounterspellingFocus
        {
            get => _intKarmaCounterspellingFocus;
            set
            {
                if (_intKarmaCounterspellingFocus != value)
                {
                    _intKarmaCounterspellingFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Disenchanting Foci.
        /// </summary>
        public int KarmaDisenchantingFocus
        {
            get => _intKarmaDisenchantingFocus;
            set
            {
                if (_intKarmaDisenchantingFocus != value)
                {
                    _intKarmaDisenchantingFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Flexible Signature Foci.
        /// </summary>
        public int KarmaFlexibleSignatureFocus
        {
            get => _intKarmaFlexibleSignatureFocus;
            set
            {
                if (_intKarmaFlexibleSignatureFocus != value)
                {
                    _intKarmaFlexibleSignatureFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Masking Foci.
        /// </summary>
        public int KarmaMaskingFocus
        {
            get => _intKarmaMaskingFocus;
            set
            {
                if (_intKarmaMaskingFocus != value)
                {
                    _intKarmaMaskingFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Power Foci.
        /// </summary>
        public int KarmaPowerFocus
        {
            get => _intKarmaPowerFocus;
            set
            {
                if (_intKarmaPowerFocus != value)
                {
                    _intKarmaPowerFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Qi Foci.
        /// </summary>
        public int KarmaQiFocus
        {
            get => _intKarmaQiFocus;
            set
            {
                if (_intKarmaQiFocus != value)
                {
                    _intKarmaQiFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Ritual Spellcasting Foci.
        /// </summary>
        public int KarmaRitualSpellcastingFocus
        {
            get => _intKarmaRitualSpellcastingFocus;
            set
            {
                if (_intKarmaRitualSpellcastingFocus != value)
                {
                    _intKarmaRitualSpellcastingFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Spellcasting Foci.
        /// </summary>
        public int KarmaSpellcastingFocus
        {
            get => _intKarmaSpellcastingFocus;
            set
            {
                if (_intKarmaSpellcastingFocus != value)
                {
                    _intKarmaSpellcastingFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Spell Shaping Foci.
        /// </summary>
        public int KarmaSpellShapingFocus
        {
            get => _intKarmaSpellShapingFocus;
            set
            {
                if (_intKarmaSpellShapingFocus != value)
                {
                    _intKarmaSpellShapingFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Summoning Foci.
        /// </summary>
        public int KarmaSummoningFocus
        {
            get => _intKarmaSummoningFocus;
            set
            {
                if (_intKarmaSummoningFocus != value)
                {
                    _intKarmaSummoningFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Sustaining Foci.
        /// </summary>
        public int KarmaSustainingFocus
        {
            get => _intKarmaSustainingFocus;
            set
            {
                if (_intKarmaSustainingFocus != value)
                {
                    _intKarmaSustainingFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Karma cost for Weapon Foci.
        /// </summary>
        public int KarmaWeaponFocus
        {
            get => _intKarmaWeaponFocus;
            set
            {
                if (_intKarmaWeaponFocus != value)
                {
                    _intKarmaWeaponFocus = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// How much Karma a single Power Point costs for a Mystic Adept.
        /// </summary>
        public int KarmaMysticAdeptPowerPoint
        {
            get => _intKarmaMysticAdeptPowerPoint;
            set
            {
                if (_intKarmaMysticAdeptPowerPoint != value)
                {
                    _intKarmaMysticAdeptPowerPoint = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region Default Build
        /// <summary>
        /// Whether Life Modules should automatically generate a character background.
        /// </summary>
        public bool AutomaticBackstory
        {
            get => _blnAutomaticBackstory;
            set
            {
                if (_blnAutomaticBackstory != value)
                {
                    _blnAutomaticBackstory = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether to use the rules from SR4 to calculate Public Awareness.
        /// </summary>
        public bool UseCalculatedPublicAwareness
        {
            get => _blnUseCalculatedPublicAwareness;
            set
            {
                if (_blnUseCalculatedPublicAwareness != value)
                {
                    _blnUseCalculatedPublicAwareness = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether Martial Arts grant a free specialization in a skill.
        /// </summary>
        public bool FreeMartialArtSpecialization
        {
            get => _blnFreeMartialArtSpecialization;
            set
            {
                if (_blnFreeMartialArtSpecialization != value)
                {
                    _blnFreeMartialArtSpecialization = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether Spells from Magic Priority can also be spent on power points.
        /// </summary>
        public bool PrioritySpellsAsAdeptPowers
        {
            get => _blnPrioritySpellsAsAdeptPowers;
            set
            {
                if (_blnPrioritySpellsAsAdeptPowers != value)
                {
                    _blnPrioritySpellsAsAdeptPowers = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Last folder from which a mugshot was added
        /// </summary>
        public string RecentImageFolder
        {
            get => _strImageFolder;
            set
            {
                if (_strImageFolder != value)
                {
                    _strImageFolder = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Allows characters to spend their Karma before Priority Points.
        /// </summary>
        public bool ReverseAttributePriorityOrder
        {
            get => _blnReverseAttributePriorityOrder;
            set
            {
                if (_blnReverseAttributePriorityOrder != value)
                {
                    _blnReverseAttributePriorityOrder = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether the Improved Ability power (SR5 309) should be capped at 0.5 of current Rating or 1.5 of current Rating.
        /// </summary>
        public bool IncreasedImprovedAbilityMultiplier
        {
            get => _increasedImprovedAbilityMultiplier;
            set
            {
                if (_increasedImprovedAbilityMultiplier != value)
                {
                    _increasedImprovedAbilityMultiplier = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Whether lifestyles will automatically give free grid subscriptions found in (HT)
        /// </summary>
        public bool AllowFreeGrids
        {
            get => _allowFreeGrids;
            set
            {
                if (_allowFreeGrids != value)
                {
                    _allowFreeGrids = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Whether Technomancers are allowed to use the Schooling discount on their initiations in the same manner as awakened.
        /// </summary>
        public bool AllowTechnomancerSchooling
        {
            get => _blnAllowTechnomancerSchooling;
            set
            {
                if (_blnAllowTechnomancerSchooling != value)
                {
                    _blnAllowTechnomancerSchooling = value;
                    OnPropertyChanged();
                }
            }
        }
        /// <summary>
        /// Maximum value of bonuses that can affect cyberlimbs.
        /// </summary>
        public int CyberlimbAttributeBonusCap
        {
            get => _intCyberlimbAttributeBonusCap;
            set
            {
                if (_intCyberlimbAttributeBonusCap != value)
                {
                    _intCyberlimbAttributeBonusCap = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Percentage by which adding an Initiate Grade to an Awakened is discounted if a member of a Group.
        /// </summary>
        public decimal KarmaMAGInitiationGroupPercent { get; set; } = 0.1m;

        /// <summary>
        /// Percentage by which adding a Submersion Grade to a Technomancer is discounted if a member of a Group.
        /// </summary>
        public decimal KarmaRESInitiationGroupPercent { get; set; } = 0.1m;

        /// <summary>
        /// Percentage by which adding an Initiate Grade to an Awakened is discounted if performing an Ordeal.
        /// </summary>
        public decimal KarmaMAGInitiationOrdealPercent { get; set; } = 0.1m;

        /// <summary>
        /// Percentage by which adding a Submersion Grade to a Technomancer is discounted if performing an Ordeal.
        /// </summary>
        public decimal KarmaRESInitiationOrdealPercent { get; set; } = 0.2m;

        /// <summary>
        /// Percentage by which adding an Initiate Grade to an Awakened is discounted if performing an Ordeal.
        /// </summary>
        public decimal KarmaMAGInitiationSchoolingPercent { get; set; } = 0.1m;

        /// <summary>
        /// Percentage by which adding a Submersion Grade to a Technomancer is discounted if performing an Ordeal.
        /// </summary>
        public decimal KarmaRESInitiationSchoolingPercent { get; set; } = 0.1m;

        #endregion

        #region Constant Values
        /// <summary>
        /// The value by which Specializations add to dicepool.
        /// </summary>
        public int SpecializationBonus { get; } = 2;

        #endregion
    }
}
