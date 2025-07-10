using HtmlAgilityPack;
using WCOkobold;


const string websiteUrl = "https://www.wcostream.tv";
const string websiteEpisodeListUrl = $"{websiteUrl}/anime/";


Console.WriteLine("WCO-Kobold starting...");


// * Read user submitted URL via console.
Console.WriteLine("Please enter the URL of the anime/cartoon list you wish to download.");
var userUrlInput = Console.ReadLine() ?? string.Empty;
try
{
    if (userUrlInput == string.Empty)
    {
        throw new InvalidUrlException();
    }
}
catch (NullReferenceException)
{
    Console.WriteLine("Please enter a valid URL.");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}


// * Navigate bot to episode list and builds dictionary of all available episodes.
var htmlDoc = new HtmlDocument();
Dictionary<string, string> episodeDictionary = new Dictionary<string, string>();

try
{
    // ie https://www.wcostream.tv/anime/b-the-beginning-succession
    // Get the HTML of the episode list.
    htmlDoc.Load(userUrlInput);

    // Gets an array of html list elements relating to the episodes available.
    const string wcoEpisodeListHtmLid = "catlist-listview";
    var htmlBody = htmlDoc.GetElementbyId(wcoEpisodeListHtmLid);
    var htmlEpisodeList = htmlBody.FirstChild;
    var htmlEpisodeListElements = htmlEpisodeList.ChildNodes;

    // Builds a dictionary of URL-Title pairs, to be iterated over when downloading episodes.
    foreach (var htmlEpisodeElement in htmlEpisodeListElements)
    {
        var htmlEpisodeElementWrapper = htmlEpisodeElement.FirstChild;
        
        var episodeTitle = htmlEpisodeElementWrapper.GetAttributeValue("title", string.Empty);
        var episodeUrl = htmlEpisodeElementWrapper.GetAttributeValue("href", string.Empty);
        
        if (episodeUrl == string.Empty || episodeTitle == string.Empty) 
        {
            throw new InvalidUrlException();
        }
        
        episodeDictionary.Add(episodeUrl, episodeTitle);
    }
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

// TODO clean up the download filenames before/after download - that 

// TODO iterate over array. Async/Await all downloads.

// * Close program
Environment.Exit(0);