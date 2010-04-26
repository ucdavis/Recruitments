using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public class GenericComparer<T> : IComparer<T>
{
    private SortDirection sortDirection;  

    public SortDirection SortDirection
    {
        get { return this.sortDirection; }
        set { this.sortDirection = value; } 
    }

    private string sortExpression; 

	public GenericComparer(string sortExpression, SortDirection sortDirection)
	{
        this.sortExpression = sortExpression;
        this.sortDirection = sortDirection; 
	} 

    public int Compare(T x, T y)
    {
        PropertyInfo propertyInfo = typeof(T).GetProperty(sortExpression);
        IComparable obj1 = (IComparable)propertyInfo.GetValue(x, null);
        IComparable obj2 = (IComparable)propertyInfo.GetValue(y, null);


        if (SortDirection == SortDirection.Ascending)
        {
            return obj1.CompareTo(obj2);
        }
        else return obj2.CompareTo(obj1); 

     }   
}
