using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

public class StringParser {
    private char[] source;
    private int offset;
    private char indentCharacter;
    private int indentCount;

    public StringParser(string source) {
        this.source = source.ToCharArray();
        this.offset = 0;
        InitIndent();
    }

    private void InitIndent() {
        int i = 0;
        while(i < source.Length) {
            while(source[i++] != '\n') {}
            if(source[i] == ' ' || source[i] == '\t') {
                indentCharacter = source[i];
                indentCount = 0;
                while(source[i++] == indentCharacter) {
                    indentCount++;    
                }
                return;
            }
        }
    }

    public int ParseIndent() {
        int count = 0;
        while(source[offset] == indentCharacter) {
            count++;
            offset++;
        }

        return count / indentCount;
    }

    private bool IsWhitespace(char c) {
        switch(c) {
            case '\t':
            case ' ':
            case '\n':
            case '\r':
                return true;
            default:
                return false;
        }
    }

    private bool IsNumerical(char c) {
        return 48 <= c && c <= 57;
    }

    private void SkipWhitespace() {
        while(IsWhitespace(source[offset])) {
            offset++;
        }
    }

    public bool EndOfLine() {
        int i = offset;
        while(i < source.Length && source[i] != '\n') {
            if(!IsWhitespace(source[i])) return false;
            i++;
        }
        return true;
    }

    public bool EndOfString() {
        return offset >= source.Length;
    }

    public string ParseLine() {
        StringBuilder stringBuilder = new StringBuilder();
        while(offset < source.Length && source[offset] != '\n') {
            stringBuilder.Append(source[offset++]);
        }
        offset++;

        return stringBuilder.ToString();
    }

    public string ParseLiteral() {
        SkipWhitespace();
        StringBuilder stringBuilder = new StringBuilder();

        while(!IsWhitespace(source[offset])) {
            stringBuilder.Append(source[offset++]);
        }

        return stringBuilder.ToString();
    }

    public Vector2 ParseDirection() {
        string type = ParseLiteral().ToLower();
        switch(type) {
            case "up":
                return Vector2.up;
            case "down":
                return Vector2.down;
            case "left":
                return Vector2.left;
            case "right":
                return Vector2.right;
            case "zero":
                return Vector2.zero;
            default:
                throw new System.Exception("Unknown direction " + type);
        }
    }

    public string ParseDelimitedString() {
        SkipWhitespace();
        char delimiter = source[offset++];
        StringBuilder stringBuilder = new StringBuilder();

        while(source[offset] != delimiter) {
            stringBuilder.Append(source[offset++]);
        }
        offset++;

        return stringBuilder.ToString();
    }

    public string ParseColonString() {
        SkipWhitespace();
        StringBuilder stringBuilder = new StringBuilder();

        while(source[offset] != ':') {
            stringBuilder.Append(source[offset++]);
        }
        offset++;

        return stringBuilder.ToString();
    }

    public int ParseInt() {
        SkipWhitespace();
        StringBuilder stringBuilder = new StringBuilder();

        if(source[offset] == '-') {
            stringBuilder.Append(source[offset++]);
        }

        while(IsNumerical(source[offset])) {
            stringBuilder.Append(source[offset++]);
        }

        return int.Parse(stringBuilder.ToString());
    }

    public float ParseFloat() {
        SkipWhitespace();
        StringBuilder stringBuilder = new StringBuilder();

        if(source[offset] == '-') {
            stringBuilder.Append(source[offset++]);
        }

        while(IsNumerical(source[offset]) || source[offset] == '.') {
            stringBuilder.Append(source[offset++]);
        }

        return float.Parse(stringBuilder.ToString());
    }

    private T[] ParseCollection<T>(char startDelimiter, char endDelimiter, Func<T> parser) {
        SkipWhitespace();
        if(source[offset] != startDelimiter) {
            throw new Exception("Start delimiter does not match. Expected " + startDelimiter + " but got " + source[offset]);
        }
        offset++;
        SkipWhitespace();

        List<T> items = new List<T>();

        while(source[offset] != endDelimiter) {
            items.Add(parser());
            SkipWhitespace();
            if(source[offset] == ',') {
                offset++;
                SkipWhitespace();
            }
        }
        offset++;

        return items.ToArray();
    }

    public Vector2 ParseVector2() {
        float[] collection = ParseCollection('(', ')', ParseFloat);

        if(collection.Length != 2) {
            throw new Exception("Collection is not size 2.");
        }

        return new Vector2(collection[0], collection[1]);
    }

    public int[] ParseIntArray() {
        return ParseCollection('[', ']', ParseInt);
    }

    public float[] ParseFloatArray() {
        return ParseCollection('[', ']', ParseFloat);
    }

    public string[] ParseStringArray() {
        return ParseCollection('[', ']', ParseDelimitedString);
    }
}
