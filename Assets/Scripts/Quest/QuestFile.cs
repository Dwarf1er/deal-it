using System.Collections.Generic;
using UnityEngine;

public class QuestFile : MonoBehaviour {
    [SerializeField] private TextAsset file;
    private List<Quest> quests;
    private StringParser stringParser;

    private void ParseFile() {
        stringParser = new StringParser(file.text);
        
        quests = new List<Quest>();

        Quest quest = null;
        while(!stringParser.EndOfString()) {
            stringParser.ParseIndent();
            string type = stringParser.ParseLiteral();

            switch(type) {
                case "quest":
                    if(quest != null) {
                        quests.Add(quest);
                    }

                    quest = new Quest(stringParser.ParseColonString().Trim());

                    break;
                case "reward":
                    quest.reward = stringParser.ParseInt();
                    break;
                case "description":
                    quest.description = stringParser.ParseDelimitedString();
                    break;
                default:
                    quest.AddChild(ParseTask(type));
                    break;
            }

            stringParser.ParseLine();
        }
        quests.Add(quest);
    }

    public Quest[] GetQuests() {
        if(quests == null) {
            ParseFile();
        }

        return quests.ToArray();
    }

    private AbstractTask ParseTask(string type) {
        switch(type) {
            case "interact":
                return new InteractTask(stringParser.ParseDelimitedString());
            case "deal":
                return new DealTask(stringParser.ParseDelimitedString());
            default:
                throw new System.Exception("Unimplemented task " + type);
        }
    }
}