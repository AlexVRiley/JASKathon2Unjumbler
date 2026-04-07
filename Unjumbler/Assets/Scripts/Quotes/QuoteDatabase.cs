using UnityEngine;

[CreateAssetMenu(fileName = "NewQuote", menuName = "Scripts/Quotes/QuoteDatabase")]
public class QuoteDatabase : ScriptableObject
{
    public string quoteText;
    public string quoteAuthor;
}
