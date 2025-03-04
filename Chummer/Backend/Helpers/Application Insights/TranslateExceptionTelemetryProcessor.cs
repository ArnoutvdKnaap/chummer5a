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
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;

namespace Chummer
{
    public class TranslateExceptionTelemetryProcessor : ITelemetryProcessor
    {
        private ITelemetryProcessor Next { get; }

        // You can pass values from .config
        public string MyParamFromConfigFile { get; set; }

        [CLSCompliant(false)]

        // Link processors to each other in a chain.
        public TranslateExceptionTelemetryProcessor(ITelemetryProcessor next)
        {
            Next = next;
        }

        [CLSCompliant(false)]
        public void Process(ITelemetry item)
        {
            // To filter out an item, just return
            //if (!OKtoSend(item)) { return; }
            // Modify the item if required
            ModifyItem(item);

            Next.Process(item);
        }

        // Example: replace with your own criteria.
        private bool OKtoSend(ITelemetry item)
        {
            return !(item is ExceptionTelemetry);
        }

        // Example: replace with your own modifiers.
        private static void ModifyItem(ITelemetry item)
        {
            if (!(item is ExceptionTelemetry exceptionTelemetry))
                return;
            CultureInfo translateCultureInfo = GlobalSettings.InvariantCultureInfo;
            try
            {
                if (!exceptionTelemetry.Properties.ContainsKey("Translated"))
                    exceptionTelemetry.Properties.Add("Translated", TranslateExceptionMessage(exceptionTelemetry.Exception, translateCultureInfo));
            }
            catch (Exception ex)
            {
                if (!exceptionTelemetry.Properties.ContainsKey("Message"))
                    exceptionTelemetry.Properties.Add("Message", ex.Message);
                if (!exceptionTelemetry.Properties.ContainsKey("Translated"))
                    exceptionTelemetry.Properties.Add("Translated", ex.ToString());
            }
        }

        public static string TranslateExceptionMessage(Exception exception, CultureInfo targetCulture)
        {
            if (exception == null)
                return string.Empty;
            Assembly a = exception.GetType().Assembly;
            ResourceManager rm = new ResourceManager(a.GetName().Name, a);
            ResourceSet rsOriginal = rm.GetResourceSet(Thread.CurrentThread.CurrentUICulture, true, true);
            ResourceSet rsTranslated = rm.GetResourceSet(targetCulture, true, true);

            string result = exception.Message;

            foreach (DictionaryEntry item in rsOriginal)
            {
                if (!(item.Value is string message))
                    continue;

                string translated = rsTranslated.GetString(item.Key.ToString(), false);

                if (!message.Contains('{'))
                {
                    result = result.Replace(message, translated);
                }
                else if (!string.IsNullOrEmpty(translated))
                {
                    string pattern = Regex.Escape(message);
                    pattern = s_RgxFirstReplacePattern.Value.Replace(pattern, "(?<group$1>.*)");

                    Regex regex = new Regex(pattern);

                    string replacePattern = translated;
                    replacePattern = s_RgxSecondReplacePattern.Value.Replace(replacePattern, "${group$1}");
                    replacePattern = replacePattern.Replace("\\$", "$");

                    result = regex.Replace(result, replacePattern);
                }
            }

            return result;
        }

        private static readonly Lazy<Regex> s_RgxFirstReplacePattern = new Lazy<Regex>(() => new Regex(@"\\{([0-9]+)\}",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled));
        private static readonly Lazy<Regex> s_RgxSecondReplacePattern = new Lazy<Regex>(() => new Regex(@"{([0-9]+)}",
            RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled));
    }
}
