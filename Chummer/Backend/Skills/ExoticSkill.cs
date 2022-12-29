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
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;

namespace Chummer.Backend.Skills
{
    public sealed class ExoticSkill : Skill
    {
        private string _strSpecific;

        public ExoticSkill(Character character, XmlNode node) : base(character, node)
        {
        }

        public void Load(XmlNode node)
        {
            node.TryGetStringFieldQuickly("specific", ref _strSpecific);
        }

        public static bool IsExoticSkillName(Character objCharacter, string strSkillName,
                                             CancellationToken token = default)
        {
            return IsExoticSkillNameTuple(objCharacter, strSkillName, token).Item1;
        }

        public static Tuple<bool, string> IsExoticSkillNameTuple(Character objCharacter, string strSkillName, CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(strSkillName))
                return new Tuple<bool, string>(false, string.Empty);
            XPathNodeIterator objXPathNameData = objCharacter.LoadDataXPath("skills.xml", token: token)
                                                             .SelectAndCacheExpression(
                                                                 "/chummer/skills/skill[exotic = 'True']/name");
            foreach (XPathNavigator objData in objXPathNameData)
            {
                token.ThrowIfCancellationRequested();
                if (strSkillName.StartsWith(objData.Value, StringComparison.OrdinalIgnoreCase))
                {
                    return new Tuple<bool, string>(true, objData.Value);
                }
            }
            return new Tuple<bool, string>(false, string.Empty);
        }

        public static async ValueTask<bool> IsExoticSkillNameAsync(Character objCharacter, string strSkillName,
                                                                   CancellationToken token = default)
        {
            return (await IsExoticSkillNameTupleAsync(objCharacter, strSkillName, token).ConfigureAwait(false)).Item1;
        }

        public static async ValueTask<Tuple<bool, string>> IsExoticSkillNameTupleAsync(Character objCharacter, string strSkillName,
                                                                   CancellationToken token = default)
        {
            token.ThrowIfCancellationRequested();
            if (string.IsNullOrEmpty(strSkillName))
                return new Tuple<bool, string>(false, string.Empty);
            XPathNodeIterator objXPathNameData
                = await (await objCharacter.LoadDataXPathAsync("skills.xml", token: token).ConfigureAwait(false))
                        .SelectAndCacheExpressionAsync("/chummer/skills/skill[exotic = 'True']/name", token)
                        .ConfigureAwait(false);
            foreach (XPathNavigator objData in objXPathNameData)
            {
                token.ThrowIfCancellationRequested();
                if (strSkillName.StartsWith(objData.Value, StringComparison.OrdinalIgnoreCase))
                    return new Tuple<bool, string>(true, objData.Value);
            }

            return new Tuple<bool, string>(false, string.Empty);
        }

        public override bool IsExoticSkill => true;

        public override bool AllowDelete
        {
            get
            {
                using (EnterReadLock.Enter(LockObject))
                    return !CharacterObject.Created && FreeBase + FreeKarma + RatingModifiers(Attribute) <= 0;
            }
        }

        public override bool BuyWithKarma
        {
            get => false;
            set
            {
                // Dummy
            }
        }

        public override int CurrentSpCost
        {
            get
            {
                using (EnterReadLock.Enter(LockObject))
                    return Math.Max(BasePoints, 0);
            }
        }

        /// <summary>
        /// How much karma this costs. Return value during career mode is undefined
        /// </summary>
        /// <returns></returns>
        public override int CurrentKarmaCost
        {
            get
            {
                using (EnterReadLock.Enter(LockObject))
                    return Math.Max(RangeCost(Base + FreeKarma, TotalBaseRating), 0);
            }
        }

        public override void WriteToDerived(XmlWriter objWriter)
        {
            objWriter.WriteElementString("specific", Specific);
        }

        public string Specific
        {
            get
            {
                using (EnterReadLock.Enter(LockObject))
                    return _strSpecific;
            }
            set
            {
                using (EnterReadLock.Enter(LockObject))
                {
                    // No need to write lock because interlocked guarantees safety
                    if (Interlocked.Exchange(ref _strSpecific, value) == value)
                        return;
                    OnPropertyChanged();
                }
            }
        }

        public string DisplaySpecific(string strLanguage)
        {
            using (EnterReadLock.Enter(LockObject))
            {
                return strLanguage.Equals(GlobalSettings.DefaultLanguage, StringComparison.OrdinalIgnoreCase)
                    ? Specific
                    : CharacterObject.TranslateExtra(Specific, strLanguage);
            }
        }

        public async ValueTask<string> DisplaySpecificAsync(string strLanguage, CancellationToken token = default)
        {
            using (await EnterReadLock.EnterAsync(LockObject, token).ConfigureAwait(false))
            {
                return strLanguage.Equals(GlobalSettings.DefaultLanguage, StringComparison.OrdinalIgnoreCase)
                    ? Specific
                    : await CharacterObject.TranslateExtraAsync(Specific, strLanguage, token: token)
                        .ConfigureAwait(false);
            }
        }

        public override string DisplaySpecialization(string strLanguage)
        {
            return DisplaySpecific(strLanguage);
        }

        public override ValueTask<string> DisplaySpecializationAsync(string strLanguage, CancellationToken token = default)
        {
            return DisplaySpecificAsync(strLanguage, token);
        }
    }
}
