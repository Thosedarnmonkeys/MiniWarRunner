using HtmlAgilityPack;
using MiniWarRunner.Data.Armies;
using MiniWarRunner.Data.Armies.Warhammer40K;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniWarRunner.Data.ArmyParsers.BattleScribeHTML.GameSystemParsers
{
  public class Warhammer40KParser : SystemParser
  {
    private const string detachmentPath = "//li[@class='force']";

    public override Army Parse(string armyString)
    {
      var army = new Warhammer40KArmy();

      var doc = new HtmlDocument();
      doc.LoadHtml(armyString);

      var factions = new HashSet<string>();

      HtmlNodeCollection detachmentNodes = doc.DocumentNode.SelectNodes(detachmentPath);
      foreach (HtmlNode node in detachmentNodes)
      {
        var detachment = new Warhammer40kDetachment();
        string detachmentInfoString = node.SelectSingleNode("h2").InnerText;
        Tuple<string, string, int> detachmentInfo = ParseDetachmentInfo(detachmentInfoString);
        detachment.Name = detachmentInfo.Item1;
        detachment.CommandPoints = detachmentInfo.Item3;

        if (!factions.Contains(detachmentInfo.Item2))
          factions.Add(detachmentInfo.Item2);



      }

      string factionsString = string.Join(", ", factions);
      army.Faction = factionsString;
      army.CommandPoints = army.Detatchments.Sum(x => x.CommandPoints);

      return army;
    }

    private Tuple<string, string, int> ParseDetachmentInfo(string detachmentInfo)
    {
      
    }
  }
}
