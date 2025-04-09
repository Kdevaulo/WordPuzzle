using System;

using Kdevaulo.WordPuzzle.Core;

namespace Kdevaulo.WordPuzzle.Model
{
    public class VictoryModel : IVictoryModel
    {
        string[] IVictoryModel.GetVictoryText()
        {
            return Array.Empty<string>();
        }
    }
}