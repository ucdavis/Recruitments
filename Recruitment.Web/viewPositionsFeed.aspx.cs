using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CAESDO.Recruitment.BLL;
using System.ServiceModel.Syndication;
using System.Xml;

public partial class viewPositionsFeed : System.Web.UI.Page
{
    private const int MaxItemsInFeed = 25;

    protected void Page_Load(object sender, EventArgs e)
    {
        var positiondFeed = CreateFeed();

        OutputFeed(positiondFeed);
    }

    private SyndicationFeed CreateFeed()
    {
        var positionsFeed = new SyndicationFeed("UC Davis Recruitments Open Positions", "This feed displays the top 25 positions that are open for applications", Request.Url)
                                            {
                                                Copyright = TextSyndicationContent.CreatePlaintextContent(
                                                    "Developed By The College Of Agricultural And Environmental Science Dean's Office")
                                            };

        List<SyndicationItem> feedItems = CreateFeedItems();

        positionsFeed.Items = feedItems;

        return positionsFeed;
    }

    private List<SyndicationItem> CreateFeedItems()
    {
        string departmentFis = Request.QueryString["DepartmentFIS"];
        string schoolCode = Request.QueryString["SchoolCode"];

        var positions = PositionBLL.GetByStatusAndDepartment(false, true, true, departmentFis, schoolCode).Take(MaxItemsInFeed);

        List<SyndicationItem> feedItems = new List<SyndicationItem>();
        foreach (var position in positions)
        {
            var positionUrl =
                new Uri(GetFullyQualifiedUrl("/PositionDetails.aspx") + "?PositionID=" + position.ID.ToString());


            SyndicationItem item = new SyndicationItem();

            item.Title = TextSyndicationContent.CreatePlaintextContent(position.PositionTitle);
            item.Links.Add(SyndicationLink.CreateAlternateLink(positionUrl));
            item.Summary = TextSyndicationContent.CreatePlaintextContent(position.ShortDescription);  
            item.PublishDate = position.DatePosted;

            feedItems.Add(item);
        }

        return feedItems;
    }

    private void OutputFeed(SyndicationFeed feed)
    {
        XmlWriter feedWriter = XmlWriter.Create(Response.OutputStream);

        var rssFeedFormatter = new Rss20FeedFormatter(feed);
        rssFeedFormatter.WriteTo(feedWriter);

        feedWriter.Close();
    }

    private static string GetFullyQualifiedUrl(string relativeUrl)
    {
        return HttpContext.Current.Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.SafeUnescaped) +
               HttpContext.Current.Request.ApplicationPath + relativeUrl;
    }
}
