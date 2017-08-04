#region ================== Namespaces

using System;
using System.Collections.Generic;
using CodeImp.DoomBuilder.Map;
using System.Windows.Forms;

#endregion

namespace CodeImp.DoomBuilder.BuilderModes
{
	[FindReplace("Sector Tag", BrowseButton = false)]
	internal class FindSectorTagRoo : BaseFindSector
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

			// Interpret the number given
			int tag;

			//mxd. Check prefixes
			value = value.Trim().Replace(" ", "");

			if (int.TryParse(value, out tag))
			{
				// Where to search?
				ICollection<Sector> list = withinselection ? General.Map.Map.GetSelectedSectors(true) : General.Map.Map.Sectors;

				// Go for all sectors
				foreach (Sector s in list)
				{
					// Brightness matches?
					if (s.SectorTag == tag)
					{
						// Replace
						if (replace) s.SectorTag = replacetag;

						objs.Add(new FindReplaceObject(s, "Sector " + s.Index + (!replace ? " (tag " + s.SectorTag + ")" : "")));
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
