using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneFile : MonoBehaviour {
    [SerializeField] private TextAsset file;
    private StringParser stringParser;
    private CutsceneAbstract[] rootCutscenes;

    private void ParseFile() {
        stringParser = new StringParser(file.text);
        Stack<CutsceneAbstract> cutsceneStack = new Stack<CutsceneAbstract>();
        cutsceneStack.Push(new Cutscene("Base", false));
        while(!stringParser.EndOfString()) {
            int index = stringParser.ParseIndent();

            if(index > cutsceneStack.Count) {
                throw new Exception("Too many indents.");
            }

            string type = stringParser.ParseLiteral();
            CutsceneAbstract step = ParseType(type);

            while(cutsceneStack.Count > index + 1) {
                cutsceneStack.Pop();
            }

            cutsceneStack.Peek().AddChild(step);
            cutsceneStack.Push(step);
            
            stringParser.ParseLine();
        }

        while(cutsceneStack.Count > 1) {
            cutsceneStack.Pop();
        }

        this.rootCutscenes = cutsceneStack.Pop().GetChildren();
    }

    public CutsceneAbstract[] GetCutscenes() {
        if(rootCutscenes == null) {
            ParseFile();
        }
        return rootCutscenes;
    }

    private CutsceneAbstract ParseType(string type) {
        switch(type.ToLower()) {
            case "cutscene":
                return ParseCutscene();
            case "fade":
                return ParseFade();
            case "camera":
                return ParseCamera();
            case "look":
                return ParseLook();
            case "goto":
                return ParseGoto();
            case "wait":
                return ParseWait();
            case "play":
                return ParsePlay();
            case "destroy":
                return ParseDestroy();
            case "match":
                return ParseMatch();
            case "case":
                return ParseMatchCase();
            case "quest":
                return ParseQuest();
            case "dialogue":
                return ParseDialogue();
            case "fdestroy":
                return ParseFadeDestroy();
            case "patrol":
                return ParsePatrol();
            case "scene":
                return ParseScene();
            case "shop":
                return ParseShop();
            case "toggle":
                return ParseToggle();
            case "end":
                return ParseEnd();
            case "effect":
                return ParseEffect();
            case "sound":
                return ParseSound();
            default:
                throw new Exception("Unimplemented " + type);
        }
    }

    private Cutscene ParseCutscene() {
        String type = stringParser.ParseLiteral();
        bool loop = type.ToLower() == "loop";
        String name = stringParser.ParseColonString();
        return new Cutscene(name, loop);
    }

    private CutsceneFade ParseFade() {
        String type = stringParser.ParseLiteral();
        bool fout = type.ToLower() == "out";
        return new CutsceneFade(fout);
    }

    private CutsceneCameraFollow ParseCamera() {
        string[] targets = stringParser.ParseStringArray();
        return new CutsceneCameraFollow(targets);
    }

    private CutsceneLookAt ParseLook() {
        string[] actors = stringParser.ParseStringArray();
        string target = stringParser.ParseDelimitedString();
        return new CutsceneLookAt(actors, target);
    }

    private CutsceneGoto ParseGoto() {
        string[] actors = stringParser.ParseStringArray();
        string target = stringParser.ParseDelimitedString();
        Vector2 offset = Vector2.zero;
        if(!stringParser.EndOfLine()) {
            offset = stringParser.ParseVector2();
        }
        Vector2 direction = Vector2.zero;
        if(!stringParser.EndOfLine()) {
            direction = stringParser.ParseDirection();
        }
        return new CutsceneGoto(actors, target, offset, direction);
    }

    private CutsceneWait ParseWait() {
        float seconds = stringParser.ParseFloat();
        return new CutsceneWait(seconds);
    }

    private CutscenePlay ParsePlay() {
        return new CutscenePlay();
    }

    private CutsceneDestroy ParseDestroy() {
        string target = stringParser.ParseDelimitedString();
        return new CutsceneDestroy(target);
    }

    private CutsceneAbstract ParseMatch() {
        string matchType = stringParser.ParseLiteral();
        string target = stringParser.ParseColonString();

        switch(matchType.ToLower()) {
            case "interact":
                return new CutsceneIfInteract(target);
            case "quest":
                return new CutsceneIfQuest(target);
            case "deal":
                return new CutsceneIfDeal(target);
            default:
                throw new Exception("Not implemented match " + matchType);
        }
    }

    private Cutscene ParseMatchCase() {
        string value = stringParser.ParseColonString();
        return new Cutscene(value, false);
    }

    private CutsceneQuest ParseQuest() {
        string type = stringParser.ParseLiteral();
        bool start = type.ToLower() == "start";
        string name = stringParser.ParseDelimitedString();
        return new CutsceneQuest(name, start);
    }

    private CutsceneDialogue ParseDialogue() {
        string value = stringParser.ParseDelimitedString();
        if(stringParser.EndOfLine()) {
            return new CutsceneDialogue("", value);
        } else {
            string message = stringParser.ParseDelimitedString();
            return new CutsceneDialogue(value, message);
        }
    }

    private CutsceneFadeDestroy ParseFadeDestroy() {
        string target = stringParser.ParseDelimitedString();
        return new CutsceneFadeDestroy(target);
    }

    private CutscenePatrol ParsePatrol() {
        string[] actors = stringParser.ParseStringArray();
        return new CutscenePatrol(actors);
    }

    private CutsceneScene ParseScene() {
        string sceneName = stringParser.ParseDelimitedString();
        return new CutsceneScene(sceneName);
    }

    private CutsceneShop ParseShop() {
        return new CutsceneShop();
    }

    private CutsceneToggle ParseToggle() {
        string[] targets = stringParser.ParseStringArray();
        return new CutsceneToggle(targets);
    }

    private CutsceneEnd ParseEnd() {
        string title = stringParser.ParseDelimitedString();
        return new CutsceneEnd(title);
    }

    private CutsceneEffect ParseEffect() {
        return new CutsceneEffect(stringParser.ParseDelimitedString());
    }

    private CutsceneSound ParseSound() {
        return new CutsceneSound(stringParser.ParseDelimitedString());
    }
}
