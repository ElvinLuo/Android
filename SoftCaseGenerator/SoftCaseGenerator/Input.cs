// -----------------------------------------------------------------------
// <copyright file="Input.cs" company="Expedia">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace SoftCaseGenerator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Input
    {
        /// <summary>
        /// Used to save all valid available config items, values and node
        /// </summary>
        private List<string[]> validConfigItems;

        /// <summary>
        /// Used to save all invalid available config items, values and node 
        /// </summary>
        private List<string[]> invalidConfigItems;

        /// <summary>
        /// Initializes a new instance of the Input class.
        /// </summary>
        /// <param name="validConfigItems">Pass in valid</param>
        /// <param name="invalidConfigItems">Pass in invalid</param>
        public Input(List<string[]> validConfigItems, List<string[]> invalidConfigItems)
        {
            this.validConfigItems = validConfigItems;
            this.invalidConfigItems = invalidConfigItems;
        }

        /// <summary>
        /// Begin to generate
        /// </summary>
        public void GenerateSoftCase()
        {
            foreach (string[] item in validConfigItems)
            {
            }
        }

    }
}
