#region ================== Namespaces

using System;
using System.Collections.Generic;
using CodeImp.DoomBuilder.Map;
using System.Windows.Forms;

#endregion

namespace CodeImp.DoomBuilder.BuilderModes
{
	[FindReplace("Sidedef Tag", BrowseButton = false)]
	internal class FindSidedefTagRoo : BaseFindSidedef
	{
		#region ================== Properties
		#endregion

		#region ================== Methods

		// This is called to test if the item should be displayed
		public override bool DetermineVisiblity()
		{
			return General.Map.MERIDIAN;
		}

		// This is called to perform a search (and replace)
		// Returns a list of items to show in the results list
		// replacewith is null when not replacing
		public override FindReplaceObject[] Find(string value, bool withinselection, bool replace, string replacewith, bool keepselection)
		{
			List<FindReplaceObject> objs = new List<FindReplaceObject>();

			// Interpret the replacement
			int replacetag = -1;
			if (replace)
			{
				// If it cannot be interpreted, set replacewith to null (not replacing at all)
				if (!int.TryParse(replacewith, out replacetag)) replacewith = null;
				if (replacewith == null || replacetag < 0)
				{
					MessageBox.Show("Invalid replace value for this search type!", "Find and Replace", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return objs.ToArray();
				}
			}

			//mxd. Check prefixes
			value = value.Trim().Replace(" ", "");

            // Interpret the number given
            if (int.TryParse(value, out int tag))
			{
                // Where to search?
                ICollection<Sidedef> list = withinselection ? General.Map.Map.GetSidedefsFromSelectedLinedefs(true) : General.Map.Map.Sidedefs;

                // Go for all sectors
                foreach (Sidedef s in list)
				{
					// Tag matches?
					if (s.Tag == tag)
					{
                        // Replace
                        if (replace)
                            s.Tag = replacetag;

                        string side = s.IsFront ? "front" : "back";
                        objs.Add(new FindReplaceObject(s, "Sidedef " + s.Index + " (" + side + ")" + (!replace ? " (tag " + s.Tag + ")" : "")));
                    }
				}
			}

			//refresh map
			if (replace)
			{
				General.Map.Map.Update();
				General.Map.IsChanged = true;
			}

			return objs.ToArray();
		}

		#endregion

	}
}
