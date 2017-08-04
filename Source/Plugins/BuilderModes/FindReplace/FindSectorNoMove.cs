#region ================== Namespaces

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CodeImp.DoomBuilder.Config;
using CodeImp.DoomBuilder.Map;
using CodeImp.DoomBuilder.Windows;

#endregion

namespace CodeImp.DoomBuilder.BuilderModes
{
	[FindReplace("Sector No-move flag", BrowseButton = true)]
	internal class FindSectorNoMove : BaseFindSector
	{
		#region ================== Properties

		public override Image BrowseImage { get { return Properties.Resources.List; } }

		#endregion

		#region ================== Methods

		// This is called to test if the item should be displayed
		public override bool DetermineVisiblity()
		{
			return General.Map.MERIDIAN;
		}

		// This is called when the browse button is pressed
		public override string Browse(string initialvalue)
		{
			IDictionary<string, string> noMove;
			noMove = new Dictionary<string, string>(StringComparer.Ordinal)
			{
				{ "1", "No player/mob access (SF_NOMOVE)" }
			};
			return FlagsForm.ShowDialog(Form.ActiveForm, initialvalue, noMove);
		}

		// This is called to perform a search (and replace)
		// Returns a list of items to show in the results list
		// replacewith is null when not replacing
		public override FindReplaceObject[] Find(string value, bool withinselection, bool replace, string replacewith, bool keepselection)
		{
			List<FindReplaceObject> objs = new List<FindReplaceObject>();

			// Where to search?
			ICollection<Sector> list = withinselection ? General.Map.Map.GetSelectedSectors(true) : General.Map.Map.Sectors;

			bool noMoveFlag = !value.Contains("!1");
			bool replaceFlag = !replacewith.Contains("!1");

			// Go for all sectors
			foreach (Sector s in list)
			{
				// Flag matches?
				if (s.NoMove == noMoveFlag)
				{
					if (replace)
					{
						// Set new flag
						s.NoMove = replaceFlag;
					}

					// Add to list
					SectorEffectInfo info = General.Map.Config.GetSectorEffectInfo(s.Effect);
					if (!info.IsNull)
						objs.Add(new FindReplaceObject(s, "Sector " + s.Index + " (" + info.Title + ")"));
					else
						objs.Add(new FindReplaceObject(s, "Sector " + s.Index));
				}
			}

			return objs.ToArray();
		}

		#endregion

	}
}
