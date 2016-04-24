using System;
using System.Text.RegularExpressions;

namespace MeliMelo.Mangas.Utils
{
    /// <summary>
    /// Defines a chapter parser
    /// </summary>
    public class MangaParser
    {
        /// <summary>
        /// Creates a new ChapterParser
        /// </summary>
        public MangaParser()
        {
            description_regex_ = new Regex(kDescriptionPattern, RegexOptions.Compiled);
            title_regex_ = new Regex(kTitlePattern, RegexOptions.Compiled);
        }

        /// <summary>
        /// Parses a chapter number from a feed item (Experimental)
        /// </summary>
        /// <param name="item">Feed item</param>
        /// <returns>Chapter number (Or null)</returns>
        public float? ParseChapterNumber(string item)
        {
            var match = title_regex_.Match(item);

            if (match.Success)
            {
                float value = 0;

                if (float.TryParse(match.Groups[1].Value.Replace(".", ","), out value))
                {
                    return value;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Parses the description item of a feed and retrieves the translation team
        /// </summary>
        /// <param name="item">Item to parse</param>
        /// <returns>The translation team (Or null)</returns>
        public Tuple<string, string> ParseDescription(string item)
        {
            var match = description_regex_.Match(item);

            if (match.Success)
            {
                return new Tuple<string, string>(match.Groups[2].Value, match.Groups[3].Value);
            }
            else
            {
                return new Tuple<string, string>(item, "");
            }
        }

        /// <summary>
        /// Pattern used to match description feed item
        /// </summary>
        protected const string kDescriptionPattern = @"^((.+) )?by (.+)$";

        /// <summary>
        /// Pattern used to match title feed item
        /// </summary>
        protected const string kTitlePattern = @"^.+ (.+)$";

        /// <summary>
        /// Description regex
        /// </summary>
        protected Regex description_regex_;

        /// <summary>
        /// Title regex
        /// </summary>
        protected Regex title_regex_;
    }
}
