using HtmlAgilityPack;
using MiniWarRunner.Data.Armies;
using MiniWarRunner.Data.Armies.Warhammer40K;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MiniWarRunner.Data.ArmyParsers.BattleScribeHTML.GameSystemParsers
{
  public class Warhammer40KParser : SystemParser
  {
    private const string detachmentPath = @"//li[@class='force']";
    private const string categoryPath = @"//li[@class='category']";
    private const string unitPath = @"//li[@class='rootselection']";


    private const string cpRegex = @" \+(\d+)CP ";
    private const string pointsRegex = @" (\d+)pts";

    public override Army Parse(string armyString)
    {
      var army = new Warhammer40KArmy();

      var doc = new HtmlDocument();
      doc.LoadHtml(armyString);

      var factions = new HashSet<string>();

      HtmlNodeCollection detachmentNodes = doc.DocumentNode.SelectNodes(detachmentPath);
      foreach (HtmlNode detachmentNode in detachmentNodes)
      {
        var detachment = new Warhammer40kDetachment();
        string detachmentInfoString = detachmentNode.SelectSingleNode("h2").InnerText;
        (string detachmentName, string faction, int commandPoints) = ParseDetachmentInfo(detachmentInfoString);
        detachment.Name = detachmentName;
        detachment.CommandPoints = commandPoints;

        if (!factions.Contains(faction))
          factions.Add(faction);

        HtmlNodeCollection categoryNodes = detachmentNode.SelectNodes(categoryPath);
        foreach(HtmlNode categoryNode in categoryNodes)
        {
          string categoryInfoString = categoryNode.SelectSingleNode("h3").InnerText;
          if (categoryInfoString == "Configuration")
            continue;

          int plStartIndex = categoryInfoString.IndexOf(" [");
          string unitTypeString = categoryInfoString.Substring(0, plStartIndex);

          Warhammer40KUnitSlot slot = ConvertCategoryToUnitSlot(unitTypeString);

          HtmlNodeCollection unitNodes = categoryNode.SelectNodes(unitPath);
          foreach (HtmlNode unitNode in unitNodes)
          {
            string unitInfoString = unitNode.SelectSingleNode("h4").InnerText;
            (string unitName, int points) = ParseUnitInfo(unitInfoString);

            var unit = new Warhammer40KUnit();
            unit.Name = unitName;
            unit.Points = points;
            unit.Slot = slot;
            detachment.Units.Add(unit);
          }
        }
      }

      string factionsString = string.Join(", ", factions);
      army.Faction = factionsString;
      army.CommandPoints = army.Detatchments.Sum(x => x.CommandPoints);

      return army;
    }

    private (string name, string faction, int cp) ParseDetachmentInfo(string detachmentInfo)
    {
      int cp = 0;
      var regex = new Regex(cpRegex);
      Match match = regex.Match(detachmentInfo);
      int.TryParse(match.Value, out cp);

      string cpString = $" +{cp}CP";
      int cpStartIndex = detachmentInfo.IndexOf(cpString);
      string name = detachmentInfo.Substring(0, cpStartIndex + 1);

      int factionStartIndex = name.Length + cpString.Length + 2;
      string faction = detachmentInfo.Substring(factionStartIndex);
      int closeBracketIndex = faction.IndexOf(")");
      faction = faction.Substring(0, closeBracketIndex);

      return (name, faction, cp);
    }

    private Warhammer40KUnitSlot ConvertCategoryToUnitSlot(string category)
    {
      return category switch
      {
        "HQ" => Warhammer40KUnitSlot.HQ,
        "Troops" => Warhammer40KUnitSlot.Troop,
        "Elites" => Warhammer40KUnitSlot.Elite,
        "Fast Attack" => Warhammer40KUnitSlot.FastAttack,
        "Heavy Support" => Warhammer40KUnitSlot.HeavySupport,
        "Flyer" => Warhammer40KUnitSlot.Flyer,
        "Dedicated Transport" => Warhammer40KUnitSlot.DedicatedTransport,
        "Lord of War" => Warhammer40KUnitSlot.LordOfWar,
        _ => throw new ArgumentException(nameof(category)),
      };
    }

    private (string name, int points) ParseUnitInfo(string unitInfo)
    {
      int nameEndIndex = unitInfo.IndexOf(" [");
      string name = unitInfo.Substring(0, nameEndIndex);

      int points;
      var regex = new Regex(pointsRegex);
      Match match = regex.Match(unitInfo);
      int.TryParse(match.Value, out points);

      return (name, points);
    }
  }
}
