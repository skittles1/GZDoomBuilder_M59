﻿using System.Collections.Generic;
using CodeImp.DoomBuilder.Map;
using CodeImp.DoomBuilder.VisualModes;
using CodeImp.DoomBuilder.GZBuilder.Geometry;
using CodeImp.DoomBuilder.Geometry;

namespace CodeImp.DoomBuilder.GZBuilder.Data 
{
	public static class LinksCollector 
	{
		private struct ThingsCheckResult 
		{
			public bool ProcessPathNodes;
			public bool ProcessInterpolationPoints;
			public bool ProcessThingsWithGoal;
			public bool ProcessCameras;
			public bool ProcessActorMovers;
			public bool ProcessPolyobjects;
		}
		
		public static List<List<Line3D>> GetThingLinks(ICollection<VisualThing> visualThings) 
		{
			List<Thing> things = new List<Thing>();
			foreach (VisualThing vt in visualThings) things.Add(vt.Thing);

			ThingsCheckResult result = CheckThings(things);
			if (result.ProcessPathNodes || result.ProcessInterpolationPoints || result.ProcessThingsWithGoal || result.ProcessCameras || result.ProcessPolyobjects)
				return GetThingLinks(result, true);
			return null;
		}

		public static List<List<Line3D>> GetThingLinks(ICollection<Thing> things) 
		{
			ThingsCheckResult result = CheckThings(things);
			if (result.ProcessPathNodes || result.ProcessInterpolationPoints || result.ProcessThingsWithGoal || result.ProcessCameras || result.ProcessPolyobjects)
				return GetThingLinks(result, false);
			return null;
		}

		private static ThingsCheckResult CheckThings(ICollection<Thing> things) 
		{
			ThingsCheckResult result = new ThingsCheckResult();

			foreach (Thing t in things) 
			{
				if(t.Type == 9024) //zdoom path node
					result.ProcessPathNodes = true;
				else if(t.Type == 9070) //zdoom camera interpolation point
					result.ProcessInterpolationPoints = true;
				else if(t.Type > 9299 && t.Type < 9304) //polyobjects
					result.ProcessPolyobjects = true;
				else if(t.Action == 229 && t.Args[1] != 0) //Thing_SetGoal
					result.ProcessThingsWithGoal = true;
				else if(t.Type == 9072 && (t.Args[0] != 0 || t.Args[1] != 0)) //camera with a path
					result.ProcessCameras = true;
				else if(t.Type == 9074 && (t.Args[0] != 0 || t.Args[1] != 0) && t.Args[3] != 0) //actor mover
					result.ProcessActorMovers = true;
			}

			if(result.ProcessActorMovers || result.ProcessCameras) result.ProcessInterpolationPoints = true;
			if(result.ProcessThingsWithGoal) result.ProcessPathNodes = true;
			return result;
		}

		private static List<List<Line3D>> GetThingLinks(ThingsCheckResult result, bool correctHeight) 
		{
			List<List<Line3D>> lines = new List<List<Line3D>> { new List<Line3D>(), new List<Line3D>() };
			Dictionary<int, List<Thing>> pathNodes = new Dictionary<int, List<Thing>>();
			Dictionary<int, List<Thing>> interpolationPoints = new Dictionary<int, List<Thing>>();
			List<Thing> thingsWithGoal = new List<Thing>();
			List<Thing> cameras = new List<Thing>();
			List<Thing> actorMovers = new List<Thing>();
			Dictionary<int, List<Thing>> actorMoverTargets = new Dictionary<int, List<Thing>>();
			Dictionary<int, List<Thing>> polyobjectAnchors = new Dictionary<int, List<Thing>>(); //angle, list of PolyobjectAnchor things (9300)
			Dictionary<int, List<Thing>> polyobjectStartSpots = new Dictionary<int, List<Thing>>(); //angle, list of PolyobjectStartSpot things (9301-9303)

			//collect relevant things
			foreach (Thing t in General.Map.Map.Things) 
			{
				if(result.ProcessPathNodes && t.Type == 9024) 
				{
					if(!pathNodes.ContainsKey(t.Tag))
						pathNodes[t.Tag] = new List<Thing>();
					pathNodes[t.Tag].Add(t);
				}

				if(result.ProcessInterpolationPoints && t.Type == 9070) 
				{
					if(!interpolationPoints.ContainsKey(t.Tag))
						interpolationPoints[t.Tag] = new List<Thing>();
					interpolationPoints[t.Tag].Add(t);
				}

				if (result.ProcessThingsWithGoal && t.Action == 229 && t.Args[1] != 0) 
					thingsWithGoal.Add(t);

				if (result.ProcessCameras && t.Type == 9072 && (t.Args[0] != 0 || t.Args[1] != 0)) 
					cameras.Add(t);

				if(result.ProcessActorMovers && t.Type == 9074 && (t.Args[0] != 0 || t.Args[1] != 0) && t.Args[3] != 0) 
					actorMovers.Add(t);

				if(result.ProcessPolyobjects)
				{
					if(t.Type == 9300)
					{
						if(!polyobjectAnchors.ContainsKey(t.AngleDoom))
							polyobjectAnchors[t.AngleDoom] = new List<Thing>();
						polyobjectAnchors[t.AngleDoom].Add(t);
					}
					else if(t.Type > 9300 && t.Type < 9304)
					{
						if(!polyobjectStartSpots.ContainsKey(t.AngleDoom))
							polyobjectStartSpots[t.AngleDoom] = new List<Thing>();
						polyobjectStartSpots[t.AngleDoom].Add(t);
					}
				}
			}

			if(actorMovers.Count > 0) 
			{
				List<int> targetedTags = new List<int>();
				foreach(Thing t in actorMovers) targetedTags.Add(t.Args[3]);
				foreach(Thing t in General.Map.Map.Things) 
				{
					if(targetedTags.Contains(t.Tag)) 
					{
						if(!actorMoverTargets.ContainsKey(t.Tag))
							actorMoverTargets[t.Tag] = new List<Thing>();
						actorMoverTargets[t.Tag].Add(t);
					}
				}
			}

			Vector3D start, end;

			//process path nodes
			if (result.ProcessPathNodes) 
			{
				foreach (KeyValuePair<int, List<Thing>> group in pathNodes) 
				{
					foreach(Thing t in group.Value) 
					{
						if(t.Args[0] == 0) continue; //no goal
						
						if(pathNodes.ContainsKey(t.Args[0])) 
						{
							start = t.Position;
							if(correctHeight) start.z += GetCorrectHeight(t);

							foreach(Thing tt in pathNodes[t.Args[0]]) 
							{
								end = tt.Position;
								if(correctHeight) end.z += GetCorrectHeight(tt);
								lines[0].Add(new Line3D(start, end));
							}
						}
					}
				}
			}

			//process things with Thing_SetGoal
			if (result.ProcessThingsWithGoal) 
			{
				foreach (Thing t in thingsWithGoal) 
				{
					if (pathNodes.ContainsKey(t.Args[1])) 
					{
						if (t.Args[0] == 0 || t.Args[0] == t.Tag) 
						{
							start = t.Position;
							if (correctHeight) start.z += GetCorrectHeight(t);

							foreach(Thing tt in pathNodes[t.Args[1]]) 
							{
								end = tt.Position;
								if(correctHeight) end.z += GetCorrectHeight(tt);

								lines[1].Add(new Line3D(start, end, General.Colors.Selection));
							}
						}
					}
				}
			}

			//process interpolation points
			if (result.ProcessInterpolationPoints) 
			{
				foreach (KeyValuePair<int, List<Thing>> group in interpolationPoints) 
				{
					foreach(Thing t in group.Value) 
					{
						int targetTag = t.Args[3] + (t.Args[4] << 8);
						if(targetTag == 0) continue; //no goal

						if(interpolationPoints.ContainsKey(targetTag)) 
						{
							start = t.Position;
							if(correctHeight) start.z += GetCorrectHeight(t);

							foreach(Thing tt in interpolationPoints[targetTag]) 
							{
								end = tt.Position;
								if(correctHeight) end.z += GetCorrectHeight(tt);
								lines[0].Add(new Line3D(start, end));
							}
						}
					}
				}
			}

			//process cameras
			if (result.ProcessCameras) 
			{
				foreach (Thing t in cameras) 
				{
					int targetTag = t.Args[0] + (t.Args[1] << 8);
					if (targetTag == 0) continue; //no goal

					if(interpolationPoints.ContainsKey(targetTag)) 
					{
						start = t.Position;
						if(correctHeight) start.z += GetCorrectHeight(t);

						foreach(Thing tt in interpolationPoints[targetTag]) 
						{
							end = tt.Position;
							if(correctHeight) end.z += GetCorrectHeight(tt);
							lines[1].Add(new Line3D(start, end, General.Colors.Selection));
						}
					}
				}
			}

			//process actor movers
			if(result.ProcessActorMovers) 
			{
				foreach(Thing t in actorMovers) 
				{
					int targetTag = t.Args[0] + (t.Args[1] << 8);
					if(targetTag == 0) continue; //no goal

					//add interpolation point target
					if(interpolationPoints.ContainsKey(targetTag)) 
					{
						start = t.Position;
						if(correctHeight) start.z += GetCorrectHeight(t);

						foreach(Thing tt in interpolationPoints[targetTag]) 
						{
							end = tt.Position;
							if(correctHeight) end.z += GetCorrectHeight(tt);
							lines[1].Add(new Line3D(start, end, General.Colors.Selection));
						}
					}

					//add thing-to-move target
					if(actorMoverTargets.ContainsKey(t.Args[3])) 
					{
						start = t.Position;
						if(correctHeight) start.z += GetCorrectHeight(t);

						foreach(Thing tt in actorMoverTargets[t.Args[3]]) 
						{
							end = tt.Position;
							if(correctHeight) end.z += GetCorrectHeight(tt);
							lines[1].Add(new Line3D(start, end, General.Colors.Selection));
						}
					}
				}
			}

			//process polyobjects
			if(result.ProcessPolyobjects)
			{
				foreach(KeyValuePair<int, List<Thing>> group in polyobjectAnchors)
				{
					if(!polyobjectStartSpots.ContainsKey(group.Key)) continue;
					foreach(Thing anchor in group.Value)
					{
						start = anchor.Position;
						if(correctHeight) start.z += GetCorrectHeight(anchor);

						foreach(Thing startspot in polyobjectStartSpots[group.Key]) 
						{
							end = startspot.Position;
							if(correctHeight) end.z += GetCorrectHeight(startspot);
							lines[1].Add(new Line3D(start, end, General.Colors.Selection));
						}
					}
				}
			}

			return lines;
		}

		private static float GetCorrectHeight(Thing thing) 
		{
			float height = thing.Height / 2f;
			if (thing.Sector != null) height += thing.Sector.FloorHeight;
			return height;
		}
	}
}
